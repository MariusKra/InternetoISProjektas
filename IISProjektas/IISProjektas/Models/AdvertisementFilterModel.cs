using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IISProjektas.Models
{
    public class AdvertisementFilterModel
    {
        //public IEnumerable<IISProjektas.Models.AdvertisementModel> adsList { get; set; }
        
        [Display(Name = "AdCategory", ResourceType = typeof(IISProjektas.Resources))]
        public int categoryFilter { get; set; }

        [Display(Name = "AdAuthor", ResourceType = typeof(IISProjektas.Resources))]       
        public string usernameFilter { get; set; }

        [Display(Name = "AdDesc", ResourceType = typeof(IISProjektas.Resources))]
        public string descriptionFilter { get; set; }        

        public string dateCreated { get; set; }

        public IEnumerable<SelectListItem> categoryDropDownList { get; set; }

        public PagedList.IPagedList<IISProjektas.Models.AdvertisementModel> adsList { get; set; }

       
         
    }
}