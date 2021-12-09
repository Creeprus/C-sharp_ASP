using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MihailinStrikesBack.Models
{
    public class SortViewModel
    {
        public SortState IdSort { get; private set; }
        public SortState LoginSort { get; private set; }
        public SortState EmailSort { get; private set; }
        public SortState RoleSort { get; private set; }

        public SortState Current { get; private set; }

        public SortViewModel(SortState sortOrder)
        {
           IdSort = sortOrder == SortState.IdAsc ?
            SortState.IdDesc : SortState.IdAsc;
            EmailSort = sortOrder == SortState.EmailAsc ?
            SortState.EmailDesc : SortState.EmailAsc;
            LoginSort = sortOrder == SortState.LoginAsc ?
             SortState.LoginDesc : SortState.LoginAsc;
            RoleSort = sortOrder == SortState.RoleAsc ?
                SortState.RoleDesc : SortState.RoleAsc;
            Current = sortOrder;
          
        }
       
    }
   
    public class SortViewPost
    {
        public SortStatePost PostId { get; private set; }
        public SortStatePost PostTitle { get; private set; }
        public SortStatePost PostDate { get; private set; }
        public SortViewPost(SortStatePost sortOrder)
        {
           PostId = sortOrder == SortStatePost.PostIDAsc ?
            SortStatePost.PostIDDesc : SortStatePost.PostIDAsc;
            PostTitle = sortOrder == SortStatePost.PostTitleAsc?
         SortStatePost.PostTitleDesc : SortStatePost.PostTitleAsc;
            PostDate = sortOrder == SortStatePost.PostTitleAsc ?
      SortStatePost.PostTitleDesc : SortStatePost.PostTitleAsc;

        }
    }
}
