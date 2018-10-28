using MvcSiteMapProvider;
using RecordCollection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecordCollection.Controllers
{
    public class ArtistController : Controller
    {
        // GET: Artist
        public ActionResult Index()
        {
            var t = SiteMaps.Current.CurrentNode;
            var chartItems = CdXml2.GetCds();

            var chartItems2 = chartItems.GroupBy(c => c.ArtistName);

            List<Artist> artists = new List<Artist>();
            Artist artist = new Artist();
            var artistId = 1;
            foreach (var chartItem in chartItems2)
            {
                artist = new Artist(artistId, chartItem.Key);
                artist.AverageScore = Generic.GetAveragePercentageScoreForItems(chartItems.Where(c => c.ArtistName == artist.Name).ToList());
                artists.Add(artist);
                artist.GetAlbumsForArtist();
                artist.GetAlbumCountForArtist(chartItems);
                artistId++;
            }
            return View(artists);
        }

        public ActionResult ArtistOverview()
        {
            var artistName = Request.FilePath;
            artistName = artistName.Substring(artistName.LastIndexOf('/') + 1);

            Artist artist = new Artist(artistName);
            var chartItems = CdXml2.GetCds();
            var artistAlbums = artist.GetAlbumsForArtist();
            artist.GetAlbumCountForArtist(artistAlbums);
            artist.GetAverageScoreFromAlbums(artistAlbums);
            return View(artist);
        }

    }
}