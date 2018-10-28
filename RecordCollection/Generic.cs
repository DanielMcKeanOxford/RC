using RecordCollection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecordCollection
{
    public class Generic
    {
        public static decimal GetAveragePercentageScoreForItems(List<Cd> items)
        {
            int totalRatingSum = 0;
            int count = items.Count();
            foreach (var item in items)
            {
                totalRatingSum += item.RatingNumber;
            }

            var percentage = Math.Round(((decimal)totalRatingSum / (count * 5)) * 100, 1);
            return percentage;
        }

        private static int GetCountForRating(List<Models.Chart> items, int number)
        {
            var count = items.Count(i => i.RatingNumber == number);
            return count;
        }

        public static int GetCountForDecade(List<Models.Chart> items, Models.Chart.Decade decade)
        {
            int count = items.Where(i => i.ReleaseDecade == decade).Count();
            return count;
        }

        public static int GetCountForDecade(List<Models.Chart> items, string decadeString)
        {
            Models.Chart.Decade decade = (Models.Chart.Decade)Enum.Parse(typeof(Models.Chart.Decade), decadeString);
            int count = items.Where(i => i.ReleaseDecade == decade).Count();
            return count;
        }

        public static void GetDecade(List<Models.Chart> items)
        {
            foreach (var item in items)
            {
                item.GetReleaseDecade();
            }
        }
    }
}