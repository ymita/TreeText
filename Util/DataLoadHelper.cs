using System.Collections.ObjectModel;
using System.Xml;
using TreeText.Model;

namespace TreeText.Util
{
    public static class DataLoadHelper
    {
        public static ObservableCollection<Node> getNodes(string rootPath)
        {
            ObservableCollection<Node> Level1Nodes = new ObservableCollection<Node>();
            XmlDocument doc = new XmlDocument();
            doc.Load(rootPath);
            XmlNodeList templateNodes = doc.SelectNodes("Categories/Category");
            Level1Nodes = retreiveNodes(templateNodes);
            return Level1Nodes;
        }
        private static ObservableCollection<Node> retreiveNodes(XmlNodeList nodeList)
        {
            ObservableCollection<Node> nodes = new ObservableCollection<Node>();
            foreach (XmlNode xmlNode in nodeList)
            {
                Node node = new Node();
                node.ID = int.Parse(xmlNode.SelectNodes("ID").Item(0).InnerText);
                node.Title = xmlNode.SelectNodes("Title").Item(0).InnerText;
                if(xmlNode.SelectNodes("Content").Item(0) != null)
                {
                    node.Content = xmlNode.SelectNodes("Content").Item(0).InnerText;
                }
                if(xmlNode.SelectNodes("Templates").Count > 0)
                {
                    node.Children = retreiveNodes(xmlNode.SelectNodes("Templates/Template"));
                }
                nodes.Add(node);
            }
            return nodes;
        }
    }
}
