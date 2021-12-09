using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MihailinStrikesBack.Models
{
    public class SortViewPosts
    {
        public SortStatePost Title { get; private set; }
        public SortStatePost PostID { get; private set; }
       
        public SortStatePost Date { get; private set; }
        public SortStatePost Current { get; private set; }
        public SortViewPosts(SortStatePost sortOrder)
        {
            PostID = sortOrder == SortStatePost.PostIDAsc ?
            SortStatePost.PostIDDesc : SortStatePost.PostIDAsc;
            Title = sortOrder == SortStatePost.PostTitleAsc ?
            SortStatePost.PostTitleDesc : SortStatePost.PostTitleAsc;
            Date = sortOrder == SortStatePost.PostDateAsc ?
         SortStatePost.PostDateDesc : SortStatePost.PostDateAsc;
            Current = sortOrder;
        }

    }
}
