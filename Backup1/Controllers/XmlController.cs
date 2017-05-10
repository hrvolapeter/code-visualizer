using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;

namespace code_visualizer
{
    public static class XmlController
    {
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

        public static int CountRows(string xmlPath)
        {
            XPathDocument doc = new XPathDocument(xmlPath);
            XPathNavigator nav = doc.CreateNavigator();
            return RecCountRows(nav);
        }

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

        public static int CountFuncs(string xmlPath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);
            var nodes = doc.GetElementsByTagName("function");
            return nodes.Count;
        }

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

        private static string GetImportString(XmlNode node)
        {
            string ret = "";
            for (int i = 1; i < node.ChildNodes.Count - 1; i++)
            {
                ret = ret + node.ChildNodes[i].InnerText;
            }
            return ret;
        }

    }
}
