using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace RecordCollection
{
    public class CdXml2
    {
        public static List<Models.Cd> GetCds()
        {
            var cd = new Models.Cd();
            var cds = new List<Models.Cd>();
            XmlDataDocument xmldoc = new XmlDataDocument();
            XmlNodeList parentNode;
            int i = 0;
            string str = null;
            FileStream fs = new FileStream(@"C:\Users\Daniel\Documents\Visual Studio 2015\Projects\RecordCollection\RecordCollection\cds.xml", FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            parentNode = xmldoc.GetElementsByTagName("CD");
            for (i = 0; i <= parentNode.Count - 1; i++)
            {
                var albumTitle = parentNode[i].ChildNodes.Item(0).InnerText;
                var artistName = parentNode[i].ChildNodes.Item(1).FirstChild.InnerText;
                var artistType = parentNode[i].ChildNodes.Item(1).LastChild.InnerText;
                var producer = parentNode[i].ChildNodes.Item(2).InnerText;
                var gender = parentNode[i].ChildNodes.Item(3).InnerText;
                var genre = parentNode[i].ChildNodes.Item(4).InnerText;
                var country = parentNode[i].ChildNodes.Item(5).InnerText;
                var labelName = parentNode[i].ChildNodes.Item(6).FirstChild.InnerText;
                var labelType = parentNode[i].ChildNodes.Item(6).LastChild.InnerText;
                var year = parentNode[i].ChildNodes.Item(7).InnerText;
                var rating = parentNode[i].ChildNodes.Item(8).InnerText;
                cd = new Models.Cd(albumTitle, artistName, artistType, producer, gender,genre, country, labelName, labelType, year, rating);
                cds.Add(cd);
            }

            return cds;
        }

        public static List<Models.Chart> GetCds(string chart)
        {
            var cd = new Models.Chart();
            var cds = new List<Models.Chart>();
            XmlDataDocument xmldoc = new XmlDataDocument();
            XmlNodeList parentNode;
            int i = 0;
            string str = null;
            FileStream fs = new FileStream(@"C:\Users\Daniel\Documents\Visual Studio 2015\Projects\RecordCollection\RecordCollection\cds.xml", FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            parentNode = xmldoc.GetElementsByTagName("CD");
            for (i = 0; i <= parentNode.Count - 1; i++)
            {
                var albumTitle = parentNode[i].ChildNodes.Item(0).InnerText;
                var artistName = parentNode[i].ChildNodes.Item(1).FirstChild.InnerText;
                var artistType = parentNode[i].ChildNodes.Item(1).LastChild.InnerText;
                var producer = parentNode[i].ChildNodes.Item(2).InnerText;
                var gender = parentNode[i].ChildNodes.Item(3).InnerText;
                var genre = parentNode[i].ChildNodes.Item(4).InnerText;
                var country = parentNode[i].ChildNodes.Item(5).InnerText;
                var labelName = parentNode[i].ChildNodes.Item(6).FirstChild.InnerText;
                var labelType = parentNode[i].ChildNodes.Item(6).LastChild.InnerText;
                var year = parentNode[i].ChildNodes.Item(7).InnerText;
                var rating = parentNode[i].ChildNodes.Item(8).InnerText;
                cd = new Models.Chart(albumTitle, artistName, artistType, producer, gender, genre, country, labelName, labelType, year, rating);
                cds.Add(cd);
            }

            return cds;
        }
    }
}