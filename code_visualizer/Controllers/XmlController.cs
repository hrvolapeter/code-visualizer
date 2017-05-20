using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;
using System.Linq;

namespace code_visualizer
{
	public static class XmlController
	{
		/// <summary>
		/// Counts all comments that contain TODO or FIXME
		/// </summary>
		/// <param name="xmlPath">string that contains path of the xml file</param>
		/// <returns>number of comments</returns>
		public static int CountTodo(string xmlPath)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(xmlPath);
			var count = 0;
			var nodes = doc.GetElementsByTagName("comment");
			foreach (XmlNode node in nodes)
			{
				if (node.InnerText.ToLower().Contains("todo") || node.InnerText.ToLower().Contains("fixme"))
				{
					count++;
				}
			}
			return count;
		}


		/// <summary>
		/// Counts total number of rows in a file
		/// </summary>
		/// <param name="xmlPath">string that contains path of the xml file</param>
		/// <returns>number of rows</returns>
		public static int CountRows(string xmlPath)
		{
			XPathDocument doc = new XPathDocument(xmlPath);
			XPathNavigator nav = doc.CreateNavigator();
			return RecCountRows(nav);
		}


		/// <summary>
		/// Counts total number of rows in a file recursively
		/// </summary>
		/// <param name="nav">XPathNavigator of a file</param>
		/// <returns>number of rows</returns>
		private static int RecCountRows(XPathNavigator nav)
		{
			int count = 0;
			if (nav.HasChildren)
			{
				nav.MoveToFirstChild();
				count += RecCountRows(nav);
				while (nav.MoveToNext())
				{
					count += RecCountRows(nav);
				}
				nav.MoveToParent();
			}
			if(nav.Value.Contains(";"))
			{
				count++;
			} else if (nav.Value.Contains("{") && nav.Value.Contains("}")) {
				count += 2;
			}
			return count;
		}


		/// <summary>
		/// Counts total number of functions
		/// </summary>
		/// <param name="xmlPath">string that contains path of the xml file</param>
		/// <returns>number of functions</returns>
		public static int CountFuncs(string xmlPath)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(xmlPath);
			var nodes = doc.GetElementsByTagName("function");
			return nodes.Count;
		}


		/// <summary>
		/// Counts number of occurances of parameter types
		/// </summary>
		/// <param name="xmlPath">string that contains path of the xml file</param>
		/// <returns>dictionary that contains type of a parameter and number of its occurances</returns>
		public static Dictionary<string, int> CountParamTypes(string xmlPath)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(xmlPath);
			var nsMgr = new XmlNamespaceManager(doc.NameTable);
			nsMgr.AddNamespace("ns", "http://www.srcML.org/srcML/src");
			var nodes = doc.SelectNodes("//ns:function//ns:parameter//ns:type/ns:name", nsMgr);
			var ret = new Dictionary<string, int>();
			foreach (XmlNode node in nodes)
			{
				string type = node.InnerText;
				try
				{
					ret.Add(type, 1);
				}
				catch (ArgumentException)
				{
					ret[type]++;
				}
			}
			return ret;
		}


		/// <summary>
		/// Gets all import names and the number of imports
		/// </summary>
		/// <param name="xmlPath">string that contains path of the xml file</param>
		/// <returns>dictionary that contains names and number of imports</returns>
		public static Dictionary<string, int> CountImports(string xmlPath)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(xmlPath);
			var nodes = doc.GetElementsByTagName("using");
			var ret = new Dictionary<string, int>();
			foreach (XmlNode node in nodes)
			{
				string importName = GetImportString(node);
				try
				{
					ret.Add(importName, 1);
				}
				catch (ArgumentException)
				{
					ret[importName]++;
				}
			}
			nodes = doc.GetElementsByTagName("import");
			foreach (XmlNode node in nodes)
			{
				string importName = GetImportString(node);
				try
				{
					ret.Add(importName, 1);
				}
				catch (ArgumentException)
				{
					ret[importName]++;
				}
			}
			return ret;
		}


		/// <summary>
		/// Gets name of the import from the node
		/// </summary>
		/// <param name="node">XmlNode of an import</param>
		/// <returns>name of the import</returns>
		private static string GetImportString(XmlNode node)
		{
			string ret = "";
			for (int i = 1; i < node.ChildNodes.Count - 1; i++)
			{
				ret = ret + node.ChildNodes[i].InnerText;
			}
			return ret;
		}

		/// <summary>
		/// Counts average number of rows per function (excluding comments and empty lines)
		/// </summary>
		/// <param name="xmlPath">string that contains path of the xml file</param>
		/// <returns>average number of rows per function</returns>
		public static double GetAverageRowsPerFunction(string xmlPath)
		{
			XElement root = XElement.Load(xmlPath);
			var count = root.Elements("funtion")
				.Where(x => !x.Name.ToString().ToLower().Equals("comment"))
				.Count();
			return ((double)count) / CountFuncs(xmlPath);
		}


		/// <summary>
		/// Counts all occurances of loops (for, foreach, while, do)
		/// </summary>
		/// <param name="xmlPath">string that contains path of the xml file</param>
		/// <returns>dictionary that contains type of loop and number of occurances</returns>
		public static Dictionary<string, int> CountLoops(string xmlPath)
		{
			Dictionary<string, int> loops = new Dictionary<string, int>();
			string[] loopTypes = new string[] { "for", "foreach", "while", "do" };
			XmlDocument doc = new XmlDocument();
			doc.Load(xmlPath);
			foreach (var type in loopTypes)
			{
				loops.Add(type, doc.GetElementsByTagName(type).Count);         
			}
			return loops;
		}


		/// <summary>
		/// Counts number of all occurances of if statement
		/// </summary>
		/// <param name="xmlPath">string that contains path of the xml file</param>
		/// <returns>occurances of if statement</returns>
		public static int CountIfs(string xmlPath)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(xmlPath);
			return doc.GetElementsByTagName("if").Count + doc.GetElementsByTagName("elseif").Count;
		}


		/// <summary>
		/// Counts number of all occurances of else statement
		/// </summary>
		/// <param name="xmlPath">string that contains path of the xml file</param>
		/// <returns>occurances of else statement</returns>
		public static int CountElses(string xmlPath)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(xmlPath);
			return doc.GetElementsByTagName("else").Count + doc.GetElementsByTagName("elseif").Count;
		}


		/// <summary>
		/// Specifies types of return parametres and number of their occurances
		/// </summary>
		/// <param name="xmlPath">string that contains path of the xml file</param>
		/// <returns>dictionary that contains type of parameter and number of occurances</returns>
		public static Dictionary<string, int> CountReturnTypes(string xmlPath)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(xmlPath);
			var dict = new Dictionary<string, int>();
			var funcs = doc.GetElementsByTagName("function");
			foreach (XmlNode func in funcs)
			{
				var ret = func.FirstChild.FirstChild.Value;
				if (dict.ContainsKey(ret))
				{
					dict.Add(ret, 1);
				} else
				{
					dict[ret]++;
				}
			}
			return dict;
		}
	}
}
