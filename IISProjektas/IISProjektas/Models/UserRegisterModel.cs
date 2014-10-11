using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IISProjektas.Models
{
    
    public class UserRegisterModel 
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        [Display(Name = "username", ResourceType = typeof(IISProjektas.Resources))]
        [StringLength(100, ErrorMessage = "{0} turi būti bent {2} simbolių ilgio.", MinimumLength = 6)]
        public string username { get; set; }

        [Required]
        [Display(Name = "email", ResourceType = typeof(IISProjektas.Resources))]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$",
        ErrorMessage = "Įrašykite tinkamą elektroninį paštą")]
        public string email { get; set; }

        [Required]
        [Display(Name = "password", ResourceType = typeof(IISProjektas.Resources))]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0} turi būti bent {2} simbolių ilgio.", MinimumLength = 6)]
        public string password { get; set; }

        [Display(Name = "confirmPassword", ResourceType = typeof(IISProjektas.Resources))]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "Slaptažodžiai nesutampa")]        
        public string confirmPassword { get; set; }

    }
}