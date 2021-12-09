using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MihailinStrikesBack.Models
{
    public class Post2Sprav
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("PostID")]
        public Post Post_ID { get; set; }
        [ForeignKey("ImageID")]
        public Images Image_ID { get; set; }
    }
    public enum SortStatePost
    {
        PostIDAsc,
        PostIDDesc,
        PostTitleAsc,
        PostTitleDesc,
        PostDateAsc,
        PostDateDesc,

    }
}
