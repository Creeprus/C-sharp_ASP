using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MihailinStrikesBack.Models
{
    public class FilterViewModel
    {
        public int? SelectId { get; private set; }
        public string SelectLogin { get; private set; }
        public string SelectEmail { get; private set; }
        public int SelectRole { get; private set; }
        public FilterViewModel(int? id,string email,string login,int role)
        {
            SelectId = id;
            SelectLogin = login;
            SelectEmail = email;
            SelectRole = role;
        }
    }
    public class FilterViewImage
    {
        public int? SelectId { get; private set; }
        public string SelectName { get; private set; }
        public string SelectPath { get; private set; }


        public FilterViewImage(int? id, string name, string path)
        {
            SelectId = id;
            SelectName = name;
            SelectPath = path;
           
        }
    }
    public class FilterViewPost
    {
        public int? SelectId { get; private set; }
        public string SelectTitle { get; private set; }
       
        public FilterViewPost(int? id, string title)
        {
            SelectId = id;
           
            SelectTitle = title;
        }
    }
}
