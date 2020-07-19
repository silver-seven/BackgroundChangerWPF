using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

/* Name: XmlHandler 
 * Version: 00.00.01
 *
 */

namespace BackgroundChanger.Source
{
    class XMLHandler
    {
        private string fileName;
        private string rootNodeName;
        private IList<string> userDatas;
        private XmlDocument xml;

        public XMLHandler()
        {
            fileName = "user.xml";
            rootNodeName = "User";
            userDatas = new List<string>();
        }

        private void CreateBase()
        {
            xml = new XmlDocument();
            XmlNode rootNode = xml.CreateElement(rootNodeName);
            xml.AppendChild(rootNode);

            XmlNode rootDataNode;
            XmlNode dataNode = xml.CreateElement("Path");

            foreach (string userData in userDatas)
            {
                rootDataNode = xml.CreateElement(userData);
                rootNode.AppendChild(rootDataNode);

            }
        }

        /* 
         * Create XML File
         */
        public void Create()
        {
            CreateBase();
        }

        public void Create(string file)
        {
            fileName = file;
            CreateBase();
        }

        /* 
         * Load XML File
         */
        public void Load()
        {
            xml = new XmlDocument();
            xml.Load(fileName);
        }

        public void Load(string file)
        {
            xml = new XmlDocument();
            fileName = file;
            xml.Load(fileName);
        }

        /* 
         * Save XML File
         */
        public void Save()
        {
            xml.Save(fileName);
        }

        /* Node: root Node 
         * example: AddToRoot("Timer")
         */
        public void AddToRoot(string Node)
        {
            XmlNode rootDataNode = xml.DocumentElement;
            XmlNode dataNode = xml.CreateElement(Node);
            rootDataNode.AppendChild(dataNode);
        }

        /* Node: root Node 
         * data: innerText data
         * example: AddToRoot("Timer", "42")
         */
        public void AddToRoot(string Node, string data)
        {
            XmlNode rootDataNode = xml.DocumentElement;
            XmlNode dataNode = xml.CreateElement(Node);
            dataNode.InnerText = data;
            rootDataNode.AppendChild(dataNode);
        }

        /* rootPath: root Node 
         * Node: node name
         * data: innerText data
         * example: Add("//Nodes", "Node", "text")
         */
        public void Add(string rootPath, string Node, string data)
        {
            XmlNode dataNode = xml.CreateElement(Node);
            dataNode.InnerText = data;
            xml.DocumentElement.SelectSingleNode(rootPath).AppendChild(dataNode);
        }

        /* rootPath: root Node 
         * strOut: text
         * example: GetXMLNodes("//Nodes")
         */
        public string GetXMLNode(string rootPath)
        {
            if (File.Exists(fileName))
            {
                XmlNode rootNode = xml.DocumentElement.SelectSingleNode(rootPath);
                return rootNode.InnerText;
            }
            else
            {
                return "-1";
            }
        }

        /* rootPath: root Node 
         * listOut: string list
         * example: GetXMLNodes("//Nodes", ref list)
         */
        public void GetXMLNodes(string rootPath, ref IList<string> listOut)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(fileName);
            listOut.Clear();
            XmlNode rootNode = xml.DocumentElement.SelectSingleNode(rootPath);
            foreach (XmlNode childNode in rootNode)
            {
                listOut.Add(childNode.InnerXml);
            }
        }

        /* rootPath: root Node 
         * text: string list
         * example: RemoveNodeByInnerText("//Nodes", "data1")
         */
        public void RemoveNodeByInnerText(string rootPath, string text)
        {
            XmlNode rootNode = xml.DocumentElement.SelectSingleNode(rootPath);
            foreach (XmlNode childNode in rootNode)
            {
                if (childNode.InnerText.Equals(text))
                {
                    childNode.ParentNode.RemoveChild(childNode);
                    break;
                }
            }
        }

        /* rootPath: root Node 
          * text: string list
          * example: ChangeNodeInnerText("//Timer", "50")
          */
        public void ChangeNodeInnerText(string rootPath, string text)
        {
            XmlNode rootNode = xml.DocumentElement.SelectSingleNode(rootPath);
            rootNode.InnerText = text;
        }
    }
}
