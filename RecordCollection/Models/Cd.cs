using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static RecordCollection.Models.Chart;

namespace RecordCollection.Models
{
    public class Cd
    {
        public string AlbumTitle { get; set; }
        public string ArtistName { get; set; }
        public string ArtistType { get; set; }
        public string Producer { get; set; }
        public string Gender { get; set; }
        public string Genre { get; set; }
        public string Country { get; set; }
        public string RecordCompanyName { get; set; }
        public string RecordCompanyType { get; set; }
        public string YearOfRelease { get; set; }
        public string Rating { get; set; }
        public IEnumerable<SelectListItem> Genres { get;  set;}
        public Decade ReleaseDecade { get; set; }
        public int RatingNumber
        {
            get
            {
                var rating = this.Rating.Split('*');
                var count = rating.Count() - 1;
                return count;
            }
        }

        public decimal RatingPercentage {
            get
            {
                return Math.Round(((decimal)RatingNumber / 5) * 100, 1);
            }
        }
        public Cd(string albumTitle, string artistName, string artistType, string producer, string gender, string genre, string country, string recordCompanyName, string recordCompanyType, string yearOfRelease,
            string rating)
        {
            AlbumTitle = albumTitle;
            ArtistName = artistName;
            ArtistType = artistType;
            Producer = producer;
            Gender = gender;
            Genre = genre;
            Country = country;
            RecordCompanyName = recordCompanyName;
            RecordCompanyType = recordCompanyType;
            YearOfRelease = yearOfRelease;
            Rating = rating;
        }

        public Cd()
        {

        }

        public enum GenreType
        {
            Rock,
            Jazz,
            Blues,
            Folk
        }

        public static List<Models.Cd> GetCds()
        {
            return CdXml2.GetCds();
        }

        public void GetReleaseDecade()
        {
            string yearOfRelease = this.YearOfRelease;
            DateTime yearOfReleaseDate = new DateTime(int.Parse(YearOfRelease), 1, 1);
            //DateTime dt = DateTime.ParseExact(s, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            DateTime twentiesBeginning = new DateTime(1920, 1, 1);
            DateTime twentiesEnd = new DateTime(1929, 12, 31);
            DateTime thirtiesEnd = new DateTime(1939, 12, 31);
            DateTime fortiesEnd = new DateTime(1949, 12, 31);
            DateTime fiftiesEnd = new DateTime(1959, 12, 31);
            DateTime sixtiesEnd = new DateTime(1969, 12, 31);
            DateTime seventiesEnd = new DateTime(1979, 12, 31);
            DateTime eightiesEnd = new DateTime(1989, 12, 31);
            DateTime ninetiesEnd = new DateTime(1999, 12, 31);
            DateTime naughtiesEnd = new DateTime(2009, 12, 31);
            DateTime twentyTensEnd = new DateTime(2019, 12, 31);

            if (yearOfReleaseDate >= twentiesBeginning && yearOfReleaseDate <= twentiesEnd)
                this.ReleaseDecade = Decade.Twenties;

            if (yearOfReleaseDate > twentiesEnd && yearOfReleaseDate <= thirtiesEnd)
                this.ReleaseDecade = Decade.Thirties;

            if (yearOfReleaseDate > thirtiesEnd && yearOfReleaseDate <= fortiesEnd)
                this.ReleaseDecade = Decade.Forties;

            if (yearOfReleaseDate > fortiesEnd && yearOfReleaseDate <= fiftiesEnd)
                this.ReleaseDecade = Decade.Fifties;

            if (yearOfReleaseDate > fiftiesEnd && yearOfReleaseDate <= sixtiesEnd)
                this.ReleaseDecade = Decade.Sixties;

            if (yearOfReleaseDate > sixtiesEnd && yearOfReleaseDate <= seventiesEnd)
                this.ReleaseDecade = Decade.Seventies;

            if (yearOfReleaseDate > seventiesEnd && yearOfReleaseDate <= eightiesEnd)
                this.ReleaseDecade = Decade.Eighties;

            if (yearOfReleaseDate > eightiesEnd && yearOfReleaseDate <= ninetiesEnd)
                this.ReleaseDecade = Decade.Nineties;

            if (yearOfReleaseDate > ninetiesEnd && yearOfReleaseDate <= naughtiesEnd)
                this.ReleaseDecade = Decade.Naughties;

            if (yearOfReleaseDate > naughtiesEnd && yearOfReleaseDate <= twentyTensEnd)
                this.ReleaseDecade = Decade.TwentyTens;

        }

        public static IEnumerable<SelectListItem> GetGenres()
        {
            var cds = GetCds();
            var genres = cds.Select(c => c.Genre).Distinct();
            var selectList = new List<SelectListItem>();

            foreach (var genre in genres)
            {
                var sl = new SelectListItem { Value = genre, Text = genre };
                selectList.Add(sl);
            }
            return selectList;
        }
    }
}