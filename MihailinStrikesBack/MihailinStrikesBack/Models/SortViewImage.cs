using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MihailinStrikesBack.Models
{
    public class SortViewImage
    {
        public SortStateImage ImageName { get; private set; }
        public SortStateImage ImageId { get; private set; }
        public SortStateImage Current { get; private set; }
        public SortViewImage(SortStateImage sortOrder)
        {
            ImageId = sortOrder == SortStateImage.ImageIdAsc ?
            SortStateImage.ImageIdDesc : SortStateImage.ImageIdAsc;
            ImageName = sortOrder == SortStateImage.ImageNameAsc ?
            SortStateImage.ImageNameDesc : SortStateImage.ImageNameAsc;
            Current = sortOrder;
        }

    }
}
