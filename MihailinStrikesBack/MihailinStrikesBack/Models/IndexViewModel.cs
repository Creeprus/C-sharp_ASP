using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MihailinStrikesBack.Models
{
    public class IndexViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Images>Image { get; set; }
        public IEnumerable<Post2Sprav> Post2Sprav { get; set; }
        public IEnumerable<UserMerge> Merge { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
        public SortViewImage SortViewImage { get; set; }
        public SortViewPosts SortViewPosts { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public FilterViewImage FilterViewImage { get; set; }
        public FilterViewPost FilterViewPost { get; set; }

    }
}
