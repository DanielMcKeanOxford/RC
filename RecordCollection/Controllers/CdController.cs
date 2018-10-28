using RecordCollection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecordCollection.Controllers
{
    public class CdController : Controller
    {
        // GET: Cd
        //public ActionResult Index()
        //{
        //    var cds = CdXml.GetFile();
        //    return View(cds);
        //}

        public ActionResult Index()
        {
            var cds = CdXml2.GetCds();
            var genreTypeQueryString = Request.QueryString["GenreType"];
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "All", Value = "All"});
            var genres = cds.Select(c => c.Genre).Distinct();
            foreach (var genre in genres)
            {
                var sl = new SelectListItem { Value = genre, Text = genre };
                if (genre == genreTypeQueryString)
                    sl.Selected = true;
                items.Add(sl);
            }
            ViewBag.GenreType = items;

            if(Request.QueryString.Keys.Count == 1)
                cds = GetCdsFromQueryString(cds, Request.QueryString.Keys[0].ToString());

            return View(cds);
        }

        public List<Cd> GetCdsFromQueryString(List<Cd> cds, string queryStringType )
        {
            switch(queryStringType) {
                case "GenreType":
                    var genreTypeQueryString = Request.QueryString["GenreType"];
                    cds = cds.Where(c => c.Genre == genreTypeQueryString).ToList();
                    break;
                case "RatingNumber":
                    var ratingNumberQueryString = Request.QueryString["RatingNumber"];
                    cds = cds.Where(c => c.RatingNumber == int.Parse(ratingNumberQueryString)).ToList();
                    break;
                case "Decade":
                    foreach (var cd in cds)
                    {
                        cd.GetReleaseDecade();
                    }
                    cds = cds.Where(c => c.ReleaseDecade == (Models.Chart.Decade)Enum.Parse(typeof(Models.Chart.Decade), (Request.QueryString["Decade"]))).ToList();
                    ViewBag.TableTitle = string.Format("{0}", Request.QueryString["Decade"].ToString());
                    break;
            }

            return cds;
        }

        public ActionResult SelectCategory()
        {

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Rock", Value = "Rock", Selected = true });
            items.Add(new SelectListItem { Text = "Blues", Value = "Blues" });
            items.Add(new SelectListItem { Text = "Jazz", Value = "Jazz" });
            items.Add(new SelectListItem { Text = "Folk", Value = "Folk" });
            ViewBag.GenreType = items;
            return View();

        }

        public ActionResult Cds()
        {
            var cds = CdXml2.GetCds();
            return View(cds);
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            string selectedGenre = form["GenreType"];
            var cds = CdXml2.GetCds();
            List<SelectListItem> itemsList = new List<SelectListItem>();
            var genreTypeQueryString = Request.QueryString["GenreType"];
            itemsList.Add(new SelectListItem { Text = "All", Value = "All" });
            var genres = cds.Select(c => c.Genre).Distinct();
            foreach (var genre in genres)
            {
                var sl = new SelectListItem { Value = genre, Text = genre };
                if (genre == selectedGenre)
                    sl.Selected = true;
                itemsList.Add(sl);
            }
            ViewData["GenreType"] = itemsList;
            if (selectedGenre == "All" || selectedGenre == null)
            {
                return View(cds);
            }
            cds = cds.Where(c => c.Genre == selectedGenre).ToList();
            return View(cds);
        }
    }
}