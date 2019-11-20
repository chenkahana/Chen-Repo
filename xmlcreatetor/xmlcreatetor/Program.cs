using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace xmlcreatetor
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode node;
            string file = @"C:\Users\user\Desktop\uiConfig.xml";
            doc.Load(file);
            node = doc.SelectSingleNode("UIConfig");
            XmlNodeList nodeList = node.ChildNodes;
            node = node.SelectSingleNode("AvailableNodes");
            foreach (XmlNode childNode in node.ChildNodes)
            {
                Console.WriteLine(childNode.Attributes["Name"].Value);
            }
            Console.ReadLine();
            node.RemoveAll();

            for(int i = 0; i < 10; i++)
            {
                XmlNode newNode = doc.CreateElement("Name");
                ((XmlElement)newNode).SetAttribute("Maor", i+"");
                ((XmlElement)newNode).SetAttribute("Chen", i + "");
                ((XmlElement)newNode).SetAttribute("Dor", i + "");

                node.AppendChild(newNode);
                doc.Save(file);
            }
            foreach (XmlNode childNode in node.ChildNodes)
            {
                Console.WriteLine(childNode.Attributes["Dor"].Value);
            }
            Console.ReadLine();

            doc.Save(file);

            Console.ReadLine();
        }
    }
}
