using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace RecordCollection
{
    public class CdXml
    {
        public static string ArtistName;
        public static string ArtistType;
        public static string LabelName;
        public static string LabelType;
        public static string AlbumTitle;
        public static string Producer;
        public static string Gender;
        public static string Genre;
        public static string Country;
        public static string Year;
        public static string Rating;
        public static List<Models.Cd> GetFile()
        {
            string file = "C:\\Users\\Daniel\\Documents\\Visual Studio 2015\\Projects\\RecordCollection\\RecordCollection\\cds.xml";
            string line;
            int counter = 0;
            var cd = new Models.Cd();
            var cds = new List<Models.Cd>();
            using (StreamReader reader = new StreamReader(file))
            {
                string field = "";
                while ((line = reader.ReadLine()) != null)
                {
                    if (field.Contains("<ARTIST>") || field.Contains("<COMPANY>"))
                    {
                        if (!(line.Contains("ARTIST>") || line.Contains("COMPANY>") || line.Contains("CD>")))
                            GetMultipleValueField(field, line);
                        else
                            field = line;
                    }
                    else
                    {
                        GetLine(line);
                        field = line;
                    }


                    //else
                    //    field = line;

                    if (line.Contains("</CD>"))
                    {
                        cd = new Models.Cd(AlbumTitle, ArtistName, ArtistType, Producer, Gender, Genre, Country, LabelName, LabelType, Year, Rating);
                        cds.Add(cd);
                    }
                    counter++;
                }
                file = reader.ReadLine();
            }
            return cds;
        }
        public static List<Models.Chart> GetFile(string modelType)
        {
            string file = "C:\\Users\\Daniel\\Documents\\Visual Studio 2015\\Projects\\RecordCollection\\RecordCollection\\cds.xml";
            string line;
            int counter = 0;
            var chartItems = new List<Models.Chart>();
            var chartItem = new Models.Chart();
            using (StreamReader reader = new StreamReader(file))
            {
                string field = "";
                while ((line = reader.ReadLine()) != null)
                {
                    if (field.Contains("<ARTIST>") || field.Contains("<COMPANY>"))
                    {
                        if (!(line.Contains("ARTIST>") || line.Contains("COMPANY>") || line.Contains("CD>")))
                            GetMultipleValueField(field, line);
                        else
                            field = line;
                    }
                    else
                    {
                        GetLine(line);
                        field = line;
                    }


                    //else
                    //    field = line;

                    if (line.Contains("</CD>"))
                    {
                        chartItem = new Models.Chart(AlbumTitle, ArtistName, ArtistType, Producer, Gender, Genre, Country, LabelName, LabelType, Year, Rating);
                        chartItems.Add(chartItem);
                    }
                    counter++;
                }
                file = reader.ReadLine();
            }
            return chartItems;
        }

        public static void GetMultipleValueField(string field, string line)
        {
            string name = "";
            string type = "";
            int posA = line.IndexOf(">");
            int posB = line.LastIndexOf("<");
            int length = (posB) - (posA + 1);
            string t = line.Substring(posA + 1, length);
            if (line.Contains("<NAME>"))
                name = t;

            if (line.Contains("<TYPE>"))
                type = t;

            if (field.Contains("<ARTIST>"))
            {
                if (!string.IsNullOrEmpty(name))
                    ArtistName = name;

                if (!string.IsNullOrEmpty(type))
                    ArtistType = type;                                                           
            }

            if (field.Contains("<COMPANY>"))
            {
                if (!string.IsNullOrEmpty(name))
                    LabelName = name;

                if (!string.IsNullOrEmpty(type))
                    LabelType = type;
            }

        }

        public static void GetLine(string line)
        {
            int posA = line.IndexOf(">");
            int posB = line.LastIndexOf("<");
            int length = (posB) - (posA + 1);
            if (line.Contains("<TITLE>"))
            {
                AlbumTitle = line.Substring(posA + 1, length);
                
            }

            if (line.Contains("<PRODUCER>"))
            {
                Producer = line.Substring(posA + 1, length);
            }

            if (line.Contains("<GENDER>"))
            {
                Gender = line.Substring(posA + 1, length);
            }

            if (line.Contains("<GENRE>"))
            {
                Genre = line.Substring(posA + 1, length);
            }

            if (line.Contains("<COUNTRY>"))
            {
                Country = line.Substring(posA + 1, length);
            }
            if (line.Contains("<YEAR>"))
            {
                Year = line.Substring(posA + 1, length);
            }

            if (line.Contains("<RATING>"))
            {
                Rating = line.Substring(posA + 1, length);
            }
        }
    }
}