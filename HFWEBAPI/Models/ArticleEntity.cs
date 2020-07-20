using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HFWEBAPI.Models
{
    public class ArticleEntity
    {
        public string id { get; set; }
        public int userId { get; set; }
        public string name { get; set; }
        public string shortIntro { get; set; }
        public string content { get; set; }
        public string publishDate { get; set; }
        public string publishBy { get; set; }
        public string createdBy { get; set; }
        public string createdDate { get; set; }
        public string modifiedBy { get; set; }
        public string modifiedDate { get; set; }
        public bool isActive { get; set; }
        public bool isService { get; set; }

    }
}
