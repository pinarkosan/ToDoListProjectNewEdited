//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ToDoList.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ToDoListItems
    {
        public int ItemId { get; set; }
        public string ListItemName { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> Deadline { get; set; }
        public string Status { get; set; }
        public string IsActive { get; set; }
        public Nullable<int> ListId { get; set; }
    
        public virtual TodoLists TodoLists { get; set; }
    }
}
