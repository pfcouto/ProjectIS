using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace AdministratorConsole
{
    class XmlHandler
    {
        public static void OutputTXml(string filename, List<Transaction> transactions)
        {
            XmlDocument doc = new XmlDocument();
            // Create the XML Declaration, and append it to XML document
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);

            XmlElement root = doc.CreateElement("transactions");
            doc.AppendChild(root);


            foreach (var transaction in transactions)
            {
                root.AppendChild(createTransaction(doc, transaction.Id.ToString(), transaction.Type.ToString(), "origin", "destiny", transaction.Date.ToString("dd-MM-yyyy HH:mm:ss"), transaction.Value.ToString() + "€"));
            }

            doc.Save(@filename);
        }

        public static XmlElement createTransaction(XmlDocument doc, string t_id, string t_type, string t_origin, string t_destiny, string t_date,string t_value)
        {
            XmlElement transaction = doc.CreateElement("transaction");
            transaction.SetAttribute("type", t_type);
            XmlElement id = doc.CreateElement("id");
            id.InnerText = t_id;
            XmlElement origin = doc.CreateElement("origin");
            origin.InnerText = t_origin;
            XmlElement destiny = doc.CreateElement("destiny");
            destiny.InnerText = t_destiny;
            XmlElement date = doc.CreateElement("date");
            date.InnerText = t_date;
            XmlElement value = doc.CreateElement("value");
            value.InnerText = t_value;

            transaction.AppendChild(id);
            transaction.AppendChild(origin);
            transaction.AppendChild(destiny);
            transaction.AppendChild(date);
            transaction.AppendChild(value);
            return transaction;
        }
    }
}
