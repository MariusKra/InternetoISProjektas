//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IISProjektas
{
    using System;
    using System.Collections.Generic;
    
    public partial class MessageState
    {
        public MessageState()
        {
            this.Messages = new HashSet<Message>();
        }
    
        public int Id { get; set; }
        public string name { get; set; }
    
        public virtual ICollection<Message> Messages { get; set; }
    }
}