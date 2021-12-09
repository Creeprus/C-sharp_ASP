using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MihailinStrikesBack.Models
{
    public class Images
    {
        [Key]
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string PathImage { get; set; }
    }
    public enum SortStateImage
    {
        ImageIdAsc,
        ImageIdDesc,
        ImageNameAsc,
       ImageNameDesc,
       ImagePathAsc,
       ImagePathDesc,
    }
}
