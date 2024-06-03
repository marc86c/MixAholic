using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixAholicCommon.Model
{
    public class Mix
    {
        public int MixID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatorUserID { get; set; }
        public DateTime PublishDate { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public List<Rating> Ratings { get; set; } = new List<Rating>();

        public decimal RatingStars => Ratings.Select(x => x.Stars).Sum();
        public int RatingCount => Ratings.Count();
    }
}
