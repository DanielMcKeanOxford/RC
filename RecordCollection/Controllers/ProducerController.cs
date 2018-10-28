using RecordCollection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecordCollection.Controllers
{
    public class ProducerController : Controller
    {
        // GET: Producer
        public ActionResult Index()
        {
            var albumCountQueryString = Request.QueryString["selectedGenre"];
            int albumCountQueryStringInt = 0;
            if (albumCountQueryString != null)
                albumCountQueryStringInt = int.Parse(albumCountQueryString);

            var chartItems = CdXml2.GetCds();


            var selectList = new List<SelectListItem>();
            int counter = 1;
            while (counter < 6)
            {
                var sli = new SelectListItem { Value = counter.ToString(), Text = counter.ToString() };
                selectList.Add(sli);
                if (counter == 1)
                    sli.Selected = true;
                counter++;
            }
            ViewBag.NumberOfAlbums = selectList;

            var chartItems2 = chartItems.GroupBy(c => c.Producer).Where(c => c.Count() > 1);

            List<ProducerModel> producers = new List<ProducerModel>();
            ProducerModel producer = new ProducerModel();
            foreach(var chartItem in chartItems2)
            {
                producer = new ProducerModel(chartItem.Key);
                producer.AverageScore = Generic.GetAveragePercentageScoreForItems(chartItems.Where(c => c.Producer == producer.Name).ToList());
                producers.Add(producer);
                producer.GetAlbumsForProducer(chartItems);
                producer.GetAlbumCountForProducer(chartItems);
            }
            return View(producers);
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            var chartItems = CdXml2.GetCds();
            var selectedGenre = form["NumberOfAlbums"];
            var chartItems2 = chartItems.GroupBy(c => c.Producer).Where(c => c.Count() > int.Parse(selectedGenre));
            List<ProducerModel> producers = new List<ProducerModel>();
            ProducerModel producer = new ProducerModel();
            foreach (var chartItem in chartItems2)
            {
                producer = new ProducerModel(chartItem.Key);
                producer.AverageScore = Generic.GetAveragePercentageScoreForItems(chartItems.Where(c => c.Producer == producer.Name).ToList());
                producers.Add(producer);
                producer.GetAlbumsForProducer(chartItems);
                producer.GetAlbumCountForProducer(chartItems);
            }


            var selectList = new List<SelectListItem>();
            int counter = 1;
            while (counter < 6)
            {
                var sli = new SelectListItem { Value = counter.ToString(), Text = counter.ToString() };
                selectList.Add(sli);
                counter++;

                if (sli.Value == selectedGenre)
                    sli.Selected = true;
            }
            ViewBag.NumberOfAlbums = selectList;
            return View(producers);
        }
    }
}