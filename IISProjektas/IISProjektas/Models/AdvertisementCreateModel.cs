using IISProjektas.Views.Shared.EditorTemplates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IISProjektas.Models
{
    public class AdvertisementCreateModel
    {


        [HiddenInput]
        public int Id { get; set; }

        [Required(ErrorMessage = "Skelbimo pavadinimas yra būtinas")]
        [Display(Name = "AdName", ResourceType = typeof(IISProjektas.Resources))]
        [StringLength(25, ErrorMessage = "Skelbimo pavadinimas turi būti nuo 6 iki 25 simbolių ilgio", MinimumLength = 6)]
        public string name { get; set; }

        [Required(ErrorMessage = "Skelbimo aprašymas yra būtinas")]
        [Display(Name = "AdDesc", ResourceType = typeof(IISProjektas.Resources))]
        //[StringLength(200, ErrorMessage = "{0} turi būti bent {2} simbolių ilgio.", MinimumLength = 10)]
        //[MyStringLengthAttribute(200, MinimumLength = 10, ErrorMessage = "Netinkama reikšmė")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Skelbimo aprašymas turi būti nuo 10 iki 1000 simbolių ilgio.")]
        public string description { get; set; }

        [Required]
        [Display(Name = "AdCategory", ResourceType = typeof(IISProjektas.Resources))]
        public int category_id { get; set; }
               
        //[FileSize(10240)]
        [Required(ErrorMessage = "Skelbimo nuotrauka yra būtina")]
        [FileTypes("jpg,jpeg,png")]
        [Display(Name = "AdImage", ResourceType = typeof(IISProjektas.Resources))]
        public HttpPostedFileBase Image { get; set; }
        //[DataType(DataType.ImageUrl)]
        //public string Image { get; set; }

        
        public IEnumerable<SelectListItem> categoryDropDown { get; set; }
                
        
    }
}