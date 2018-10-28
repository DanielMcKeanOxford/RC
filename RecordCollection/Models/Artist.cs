using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecordCollection.Models
{
    public class Artist
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public List<Cd> Albums { get; }

        [DisplayFormat(DataFormatString = "{0:0.###}")]
        public decimal AverageScore { get; set; }

        public int Count { get; set; }

        public Artist(string name)
        {
            Name = name;
        }
        public Artist(int artistId, string name)
        {
            ArtistId = artistId;
            Name = name;
        }

        public Artist()
        {
        }
        public List<Cd> GetAlbumsForArtist()
        {
            var chartItems = CdXml2.GetCds();
            List<Cd> albumsList = new List<Cd>();
            foreach (var album in chartItems)
            {
                if (album.ArtistName == Name)
                    albumsList.Add(album);
            }

            return albumsList;
        }

        public void GetAlbumCountForArtist(List<Cd> albums)
        {
            Count = albums.Where(a => a.ArtistName == Name).Count();
        }

        public void GetAverageScoreFromAlbums(List<Cd> albums)
        {
            int totalRatingSum = 0;
            foreach (var item in albums)
            {
                totalRatingSum += item.RatingNumber;
            }

            AverageScore = Math.Round(((decimal)totalRatingSum / (Count * 5)) * 100, 0);
        }
    }
}