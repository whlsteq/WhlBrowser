﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace WhlBrowser
{
    public class DataTransfer
    {
        //Global variable (string) to the settings file.
        string settingsFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+ @"\WhlBrowser\settings.xml";
        
        /// <summary>
        /// Create the settings file.
        /// </summary>
        
        public void CreateXmlFile()
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\WhlBrowser"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\WhlBrowser");
            }
            // Check to see if the file already exists.
            if (!File.Exists(settingsFilePath))
            {
                // Create settings file if it doesn't exists.
                File.Create(settingsFilePath).Close();

                XmlWriterSettings wSet= new XmlWriterSettings();

                wSet.Indent = true;
                XmlWriter xmlWriter = XmlWriter.Create(settingsFilePath, wSet);

                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("settings");
                xmlWriter.WriteStartElement("searchengines");
                xmlWriter.WriteEndElement();    //end searchengine
                xmlWriter.WriteEndElement();    //end settings
                xmlWriter.WriteEndDocument();   //end document

                xmlWriter.Flush();
                xmlWriter.Close();


                Process.Start(settingsFilePath);
            }
        }

        public bool AddSearchEngine(string name, string prefix)
        {
            bool result=false;
            var doc = Doc();

            var searchengines = doc.GetElementsByTagName("searchengines");
            var el = searchengines[0].AppendChild(doc.CreateElement("engine"));
            var at1 = el.Attributes.Append(doc.CreateAttribute("name"));
            var at2 = el.Attributes.Append(doc.CreateAttribute("prefix"));

            at1.InnerText= name;
            at2.InnerText = prefix;

            //Check if it was saved

            var children=searchengines[0].ChildNodes;
            for (int i = 0; i < children.Count; i++)
            {
                    if (children[i].Attributes.GetNamedItem("name").InnerText == name && children[i].Attributes.GetNamedItem("prefix").InnerText == prefix)
                    {
                        result = true;
                    }
                
            }
            SaveXmlDocument(doc);
            return result;

            
        }
        private XmlDocument Doc()
        {
            XmlDocument doc= new XmlDocument();
            doc.Load(settingsFilePath);
            return doc;
        }

        private void SaveXmlDocument(XmlDocument Doc)
        {
            Doc.Save(settingsFilePath);
        }
        public List<string> GetSearchEngines()
        {
            List<string> list = new List<string>();
            var doc = Doc();
            var searchEngines = doc.GetElementsByTagName("engine");
            for (int i = 0; i < searchEngines.Count; i++)
            {
                list.Add(searchEngines[i].Attributes.GetNamedItem("name").InnerText);
            }
            return list;
        }
    }
}
