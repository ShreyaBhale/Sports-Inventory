//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SportsInventoryMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class procurementOrderItem
    {
        public int id { get; set; }
        public Nullable<System.DateTime> dateOforder { get; set; }
        public Nullable<int> productId { get; set; }
        public Nullable<int> supplierId { get; set; }
        public Nullable<int> NoOfProducts { get; set; }
        public Nullable<double> TotalPrice { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}