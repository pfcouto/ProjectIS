using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace VCardsMiddleware
{
    public class XmlHelper
    {
        readonly static string FILEPATH = AppDomain.CurrentDomain.BaseDirectory + @"App_Data\logs.xml";

        public static void WriteLog(string type, string text)
        {
            XmlDocument doc = new XmlDocument();
            
            doc.Load(FILEPATH);

            XmlNode root = doc.SelectSingleNode("/logs");

            XmlElement newLog = doc.CreateElement("log");

            newLog.SetAttribute("type", type);

            newLog.InnerText = $"({DateTime.Now}) {text}";

            root.AppendChild(newLog);

            doc.Save(FILEPATH);
        }

        public static string ReadLogs()
        {
            XmlDocument doc = new XmlDocument();
            
            doc.Load(FILEPATH);

            return doc.OuterXml;
        }
    }
}