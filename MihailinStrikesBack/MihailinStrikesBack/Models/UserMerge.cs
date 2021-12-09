using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MihailinStrikesBack.Models
{
    public class UserMerge
    {
    
        public User Users { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
       public Post Posts { get; set; }
        public Images Image { get; set; }
        public Post2Sprav Post2 { get; set; }
    }
}
