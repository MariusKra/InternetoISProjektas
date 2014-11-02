using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IISProjektas.Models
{
    public class MessageListModel
    {
        public int Id { get; set; }
        public string receiver { get; set; }
        public IEnumerable<MessageModel> messages { get;set;}
        
    }
}