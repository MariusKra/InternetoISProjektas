using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IISProjektas.Models
{
    public class UserEditModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [ReadOnly(true)]
        [Display(Name = "username", ResourceType = typeof(IISProjektas.Resources))]
        [StringLength(100, ErrorMessage = "{0} turi būti bent {2} simbolių ilgio.", MinimumLength = 6)]
        public string username { get; set; }

        [Required]
        [Display(Name = "email", ResourceType = typeof(IISProjektas.Resources))]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$",
        ErrorMessage = "Įrašykite tinkamą elektroninį paštą")]
        public string email { get; set; }

        [HiddenInput]
        public string oldPassword { get; set; }

        [Required]
        [Compare("oldPassword", ErrorMessage = "Įvestas buvęs slaptažodis neteisingas")]
        [Display(Name = "password", ResourceType = typeof(IISProjektas.Resources))]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0} turi būti bent {2} simbolių ilgio.", MinimumLength = 6)]
        public string password { get; set; }
               
        

        [Display(Name = "newPassword", ResourceType = typeof(IISProjektas.Resources))]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0} turi būti bent {2} simbolių ilgio.", MinimumLength = 6)]
        public string newPassword { get; set; }


        [Display(Name = "confirmPassword", ResourceType = typeof(IISProjektas.Resources))]
        [DataType(DataType.Password)]
        [Compare("newPassword", ErrorMessage = "Slaptažodžiai nesutampa")]
        public string confirmNewPassword { get; set; }

    }
}