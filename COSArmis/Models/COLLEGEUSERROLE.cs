//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudentSearch.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class COLLEGEUSERROLE
    {
        public int COLLEGEUSERID { get; set; }
        public int COLLEGEROLEID { get; set; }
        public int ID { get; set; }
    
        public virtual COLLEGEROLE COLLEGEROLE { get; set; }
        public virtual COLLEGEUSER COLLEGEUSER { get; set; }
    }
}
