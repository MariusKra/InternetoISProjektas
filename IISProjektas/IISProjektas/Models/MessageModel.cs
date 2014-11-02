using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IISProjektas.Models
{
    public class MessageModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Žinutės tekstas yra būtinas")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Skelbimo aprašymas turi būti nuo 1 iki 1000 simbolių ilgio.")]
        public string text { get; set; }
        public int state { get; set; }
        public int sender_id { get; set; }
        public int receiver_id { get; set; }
        public string sent_date { get; set; }
        public string senderName { get; set; }
    }
}