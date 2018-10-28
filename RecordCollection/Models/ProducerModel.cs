using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecordCollection.Models
{
    public class ProducerModel
    {
        public string Name { get; set; }
        public List<Cd> Albums { get; }

        [DisplayFormat(DataFormatString = "{0:0.###}")]
        public decimal AverageScore { get; set; }
        public int Count { get; set; }

        public ProducerModel(string name)
        {
            Name = name;
        }

        public ProducerModel()
        {
        }

        public List<Cd> GetAlbumsForProducer(List<Cd> albums)
        {
            List<Cd> albumsList = new List<Cd>();
            foreach (var album in albums)
            {
                if (album.Producer == Name)
                    albumsList.Add(album);
            }

            return albumsList;
        }

        public void GetAlbumCountForProducer(List<Cd> albums)
        {
            Count = albums.Where(a => a.Producer == Name).Count();
        }
        public static IEnumerable<SelectListItem> GetCount()
        {

            var selectList = new List<SelectListItem>();
            int counter = 1;
            while(counter < 6)
            {
                var sli = new SelectListItem { Value = counter.ToString(), Text = counter.ToString() };
                selectList.Add(sli);
                counter++;
            }
            return selectList;
        }
    }
}