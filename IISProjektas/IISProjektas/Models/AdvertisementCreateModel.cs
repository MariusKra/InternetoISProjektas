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
        [StringLength(20, ErrorMessage = "{0} turi būti bent {2} simbolių ilgio.", MinimumLength = 6)]
        public string name { get; set; }

        [Required(ErrorMessage = "Skelbimo aprašymas yra būtinas")]
        [Display(Name = "AdDesc", ResourceType = typeof(IISProjektas.Resources))]
        [StringLength(100, ErrorMessage = "{0} turi būti bent {2} simbolių ilgio.", MinimumLength = 10)]
        public string description { get; set; }


        [Display(Name = "AdCategory", ResourceType = typeof(IISProjektas.Resources))]
        public int category_id { get; set; }
               
        //[FileSize(10240)]
        [FileTypes("jpg,jpeg,png")]
        [Display(Name = "AdImage", ResourceType = typeof(IISProjektas.Resources))]
        public HttpPostedFileBase Image { get; set; }
        //[DataType(DataType.ImageUrl)]
        //public string Image { get; set; }

        
        public IEnumerable<SelectListItem> categoryDropDown { get; set; }
        
    }
}