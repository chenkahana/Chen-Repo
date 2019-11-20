using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace JFTES_Configurator
{
    public static class XmlEditor
    {
        public static XmlDocument doc = new XmlDocument();
        public static void UiConfigChange(List <Station> stationToUpdate)
        {
            XmlNode node;

            /**
             *Get XML from IP of Station 
             * Change the values according to other values in the table (has sme/ is pilot)
             * Save XML
             **/
            string file = @"\\192.168.0.120\c\jftes\jftapp\config\uiconfig.xml";
            string test = @"C:\Users\user\Desktop\uiConfig.xml";
            List<string> ports = new List<string>();
            doc.Load(test);
            node = doc.SelectSingleNode("UIConfig");
            node = node.SelectSingleNode("AvailableNodes");
            XmlNodeList childNodes = node.ChildNodes;
            foreach (XmlNode childNode in childNodes)
            {
                ports.Add(((XmlElement)childNode).GetAttribute("Port"));
            }
            
            node.RemoveAll();

            for (int i = 0; i < stationToUpdate.Count; i++)
            {
                int newPort;
                XmlNode newNode = doc.CreateElement("Node");
                ((XmlElement)newNode).SetAttribute("IP", stationToUpdate[i].getIP());
                ((XmlElement)newNode).SetAttribute("Name", stationToUpdate[i].getName());
                try
                {
                    newPort = Int32.Parse(ports[i]);
                }catch(ArgumentOutOfRangeException)
                {
                    newPort = Int32.Parse(ports[i - 1]);
                    newPort++;
                    ports.Add(newPort + "");
                }

                ((XmlElement)newNode).SetAttribute("Port", newPort+"");
                string stationType = stationToUpdate[i].getName().Equals("CGFXBE") || stationToUpdate[i].getName().Equals("SIMSERVER") || stationToUpdate[i].getName().Equals("SimHost") ? "Backend" : "Forntend";
                ((XmlElement)newNode).SetAttribute("Type", stationType);

                node.AppendChild(newNode);
            }
            doc.Save(test);

        }
    }
}
