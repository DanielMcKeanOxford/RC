using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace RecordCollection.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult Index()
        {
            var chartItems = CdXml2.GetCds("Chart");
            var genres = chartItems.Select(c => c.Genre).Distinct();
            Dictionary<string, int> genreDict = new Dictionary<string, int>();
            foreach (var genre in genres)
            {
                int count = GetCountForGenre(chartItems, genre);
                genreDict.Add(genre, count);
            }
            StringBuilder chartData = new StringBuilder();
            StringBuilder xAxisData = new StringBuilder();
            var last = genreDict.Last();
            foreach (var i in genreDict)
            {
                xAxisData.Append(i.Key);
                chartData.Append(i.Value);
                if (!i.Equals(last))
                {
                    xAxisData.Append(',');
                    chartData.Append(',');
                }
            }
            xAxisData = xAxisData.Replace("R'n'B", "RnB");
            var xString = xAxisData.ToString();
            ViewBag.XAxisData = xString;
            ViewBag.ChartData = chartData.ToString();
            return View(chartItems);
        }

        public int GetCountForGenre(List<Models.Chart> items, string genreName)
        {
            int count = items.Where(i => i.Genre == genreName).Count();
            return count;
        }

        public int GetCountForDecade(List<Models.Chart> items, string decadeString)
        {
            Models.Chart.Decade decade = (Models.Chart.Decade)Enum.Parse(typeof(Models.Chart.Decade), decadeString);
            int count = items.Where(i => i.ReleaseDecade == decade).Count();
            return count;
        }

        public static int GetCountForDecade(List<Models.Chart> items, Models.Chart.Decade decade)
        {
            int count = items.Where(i => i.ReleaseDecade == decade).Count();
            return count;
        }

        public void GetDecade(List<Models.Chart> items)
        {
            foreach (var item in items)
            {
                item.GetReleaseDecade();
            }
        }

        public ActionResult Decade()
        {
            var chartItems = CdXml2.GetCds("Chart");
            GetDecade(chartItems);
            //var decades = chartItems.Select(c => c.ReleaseDecade).Distinct();
            var decadeStrings = new List<string>() { "Fifties", "Sixties", "Seventies", "Eighties", "Nineties", "Naughties", "TwentyTens" };
            Dictionary<string, int> decadeDict = new Dictionary<string, int>();
            foreach (var decade in decadeStrings)
            {
                int count = GetCountForDecade(chartItems, decade);
                decadeDict.Add(decade, count);
            }
            StringBuilder chartData = new StringBuilder();
            StringBuilder xAxisData = new StringBuilder();
            var last = decadeDict.Last();
            foreach (var i in decadeDict)
            {
                xAxisData.Append(i.Key);
                chartData.Append("{name:\"" + i.Key + "\", " + "y:" + i.Value + "}");

                if (!i.Equals(last))
                {
                    xAxisData.Append(',');
                    chartData.Append(',');
                }
            }
            var xString = xAxisData.ToString();
            ViewBag.XAxisData = xString;
            ViewBag.ChartData = chartData.ToString();
            return View(chartItems);
        }
        private int GetCountForRating(List<Models.Chart> items, int number)
        {
            var count = items.Count(i => i.RatingNumber == number);
            return count;
        }

        public ActionResult Rating()
        {
            var chartItems = CdXml2.GetCds("Chart");
            Dictionary<int, int> ratingDict = new Dictionary<int, int>();
            StringBuilder chartData = new StringBuilder();
            StringBuilder xAxisData = new StringBuilder();
            int counter = 5;

            while(counter > -1)
            {
                var count = GetCountForRating(chartItems, counter);
                ratingDict.Add(counter, count);
                counter -= 1;
            }

            var last = ratingDict.Last();
            foreach (var i in ratingDict)
            {
                xAxisData.Append(i.Key);
                chartData.Append(i.Value);
                if (!i.Equals(last))
                {
                    xAxisData.Append(',');
                    chartData.Append(',');
                }
            }
            var xString = xAxisData.ToString();
            ViewBag.XAxisData = xString;
            ViewBag.ChartData = chartData.ToString();
            return View(chartItems);
        }

        public ActionResult RatingAverageByDecade()
        {
            var chartItems = CdXml2.GetCds("Chart");
            var decadeQueryString = Request.QueryString["Decade"];
            Models.Chart.Decade decade = (Models.Chart.Decade)Enum.Parse(typeof(Models.Chart.Decade), decadeQueryString);

            var totalRatingSum = 0;
            foreach (var chartItem in chartItems)
            {
                chartItem.GetReleaseDecade();
                chartItem.GetRatingNumberFromStars();
            }

            chartItems = chartItems.Where(c => c.ReleaseDecade == decade).ToList();
            foreach(var chartItem in chartItems)
            {
                totalRatingSum += chartItem.RatingNumber;
            }

            int count = GetCountForDecade(chartItems, decade);
            ViewData["Total"] = count;
            var percentage = Math.Round(((decimal)totalRatingSum / (count * 5)) * 100, 1);
            ViewData["Decade"] = decadeQueryString;
            ViewData["DecadeAverage"] = percentage;
            return View(chartItems);
        }
    }
}