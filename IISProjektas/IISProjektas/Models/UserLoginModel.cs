using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IISProjektas.Models
{
    public class UserLoginModel
    {
        [Required]
        [Display(Name = "username", ResourceType = typeof(IISProjektas.Resources))]
        [StringLength(100, ErrorMessage = "{0} turi būti bent {2} simbolių ilgio.", MinimumLength = 6)]
        public string username { get; set; }
               
        [Required]
        [Display(Name = "password", ResourceType = typeof(IISProjektas.Resources))]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0} turi būti bent {2} simbolių ilgio.", MinimumLength = 6)]
        public string password { get; set; }

        [Display(Name = "Remember")]
        public bool RememberMe { get; set; }

    }
}