using IISProjektas.Views.Shared.EditorTemplates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IISProjektas.Models
{
    public class AdvertisementModel
    {

        
        [HiddenInput]
        public int Id { get; set; }

        public string author { get; set; }

        [HiddenInput]
        public int author_id { get; set; }

        public string name { get; set; }

        public string description { get; set; }
        
        public string category { get; set; }

        public string dateCreated { get; set; }

       // public string image { get; set; }
        public byte[] image { get; set; }

        public double commentRating { get; set; }

        public int commentDropDown { get; set; }

        public bool isRated { get; set; }

        public IEnumerable<CommentModel> comments { get; set; }

    }
}