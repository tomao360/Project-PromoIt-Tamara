using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoItProject.Model
{
    public class Tweet: BaseModel
    {
        public int TwitterID { get; set; }
        public string TwitterUserName { get; set; }
        public string Hashtag { get; set; }
        public string TweetID { get; set; }
        public DateTime TweetDate { get; set; }
    }
}
