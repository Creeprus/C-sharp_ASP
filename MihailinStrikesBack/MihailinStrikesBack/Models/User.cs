
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MihailinStrikesBack.Models
{
    public class User
    {
        [Key ]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public int Role_Id { get; set; }
       
        [ForeignKey("ImageID")]
        public Images ID_Image { get; set; }

        public DateTime BirthDate { get; set; }
       // public int Post_ID { get; set; }
    }
    public class LogUser
    {
        public string Login { get; set; }
        public string Password { get; set; }
    
    }
    public enum SortState
    {
        IdAsc,
        IdDesc,
        EmailAsc,
        EmailDesc,
        LoginAsc,
        LoginDesc,
        RoleAsc,
        RoleDesc,

    }
}
