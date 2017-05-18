using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.IO;
using System.Net.Http;
using System.Collections.Generic;
using System.Xml.Linq;

namespace code_visualizer
{
	public class XmlFormatter : BufferedMediaTypeFormatter
	{
		public XmlFormatter ()
		{
			SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/xml"));
		}

		public override bool CanWriteType(Type type)
		{
			return true;
		}

		public override bool CanReadType(Type type)
		{ 
			return false;
		}

		public override void WriteToStream(Type type, object value, Stream stream, HttpContent contentHeaders)
		{
			using (var writer = new StreamWriter(stream)) {
				dynamic output = value;
				if (output != null) {
					if (output.Type == JobType.Loops || output.Type == JobType.Imports || output.Type == JobType.ReturnTypesCount || output.Type == JobType.ParamTypesCount) {
						WriteItemDict((XmlOutput<List<KeyValuePair<string, int>>>)output, writer);
					} else {
						WriteItemInt((XmlOutput<int>)output, writer);
					}	
				} else {
					throw new InvalidOperationException ("Cannot serialize type");
				}
			}
			stream.Close();
		}


		private void WriteItemDict(XmlOutput<List<KeyValuePair<string, int>>> output, StreamWriter writer)
		{
			string elementName = "";
			string attributeName = "type";
			switch (output.Type) 
			{
				case JobType.Loops:
					elementName = "loop";
					break;
				case JobType.Imports: 
					elementName = "import";
					attributeName = "name";
					break;
				case JobType.ReturnTypesCount:
					elementName = "return";
					break;
				case JobType.ParamTypesCount:
					elementName = "arguments";
                    break;
			}
			XElement root = new XElement("versions");
			foreach (var list in output.Data) 
			{
				XElement version = new XElement("version");
				foreach (var item in list) 
				{
					XElement elem = new XElement(elementName);
					elem.SetAttributeValue(attributeName, item.Key);
					elem.SetValue(item.Value);
					version.Add(elem);
				}
				root.Add(version);
			}
			writer.WriteLine(root.ToString());
		}


		private void WriteItemInt(XmlOutput<int> output, StreamWriter writer)
		{
            string elementName = "";
			switch (output.Type) 
			{
				case JobType.ToDo:
					elementName = "todo";
					break;
				case JobType.AvgRowsPerFunction:
					elementName = "rowsperfuction";
					break;
				case JobType.FuncCount:
					elementName = "function";
					break;
				case JobType.Ifs:
					elementName = "if";
					break;
				case JobType.RowCount:
					elementName = "rows";
					break;
			}
			XElement root = new XElement("versions");
			foreach (var item in output.Data) 
			{
				XElement version = new XElement("version");
				XElement elem = new XElement(elementName, item);
				version.Add(elem);
				root.Add(version);
			}
			writer.WriteLine(root.ToString());
		}
	}
}

