using System.Xml;

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
                if (node.InnerText.ToLower().Contains("todo"))
                {
                    count++;
                }
            }
            return count;
        }

        public static void AnalyzeXml(string xmlPath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);
            //TODO
        }
    }
}
