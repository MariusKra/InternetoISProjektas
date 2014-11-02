using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IISProjektas.Models
{
    public class CommentModel
    {
        public int CommentId { get; set; }

        public string text { get; set; }

        public string date_created { get; set; }

        public string username { get; set; }
    }
}