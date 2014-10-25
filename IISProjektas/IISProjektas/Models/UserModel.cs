using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IISProjektas.Models
{
    public class UserModel
    {
        
        public int Id { get; set; }

        [Display(Name = "username", ResourceType = typeof(IISProjektas.Resources))]
        [StringLength(100, ErrorMessage = "{0} turi būti bent {2} simbolių ilgio.", MinimumLength = 6)]
        public string username { get; set; }

        [Required]
        [Display(Name = "email", ResourceType = typeof(IISProjektas.Resources))]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$",
        ErrorMessage = "Įrašykite tinkamą elektroninį paštą")]
        public string email { get; set; }

        public string password { get; set; }


        public int state { get; set; }
        public int  permission { get; set; }

        public IEnumerable<SelectListItem> permisiondropdown { get; set; }
        public IEnumerable<SelectListItem> statedropdown { get; set; }

        public string permissionsText { get; set; }
        public string stateText { get; set; }


    }
}