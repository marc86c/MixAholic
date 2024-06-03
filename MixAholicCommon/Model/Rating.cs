using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixAholicCommon.Model
{
    public class Rating
    {
        public int RatingID { get; set; }
        public int MixID { get; set; }
        public decimal Stars { get; set; }
        public int UserID { get; set; }
        public string CommentTitle { get; set; }
        public string CommentText { get; set; }
    }
}
