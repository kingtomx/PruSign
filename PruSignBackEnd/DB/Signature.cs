//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PruSignBackEnd.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Signature
    {
        public string image { get; set; }
        public Nullable<System.DateTime> datetime { get; set; }
        public string customername { get; set; }
        public string customerid { get; set; }
        public string documentid { get; set; }
        public string applicationid { get; set; }
        public string hash { get; set; }
        public long id { get; set; }
    }
}