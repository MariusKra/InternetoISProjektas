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
    
    public partial class Message
    {
        public int Id { get; set; }
        public string text { get; set; }
        public int state { get; set; }
        public int sender_id { get; set; }
        public int receiver_id { get; set; }
        public Nullable<System.DateTime> date_sent { get; set; }
    
        public virtual MessageState MessageState { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
