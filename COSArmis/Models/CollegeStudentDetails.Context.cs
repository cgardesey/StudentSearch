﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class StudEntities : DbContext
    {
        public StudEntities()
            : base("name=StudEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<VIEWALLCADYEARS> VIEWALLCADYEARS { get; set; }
    
        public virtual ObjectResult<procStudentInfoDetails_Result> procStudentInfoDetails(Nullable<int> cOLLEGEID, Nullable<int> aCADYEAR, Nullable<int> sEM)
        {
            var cOLLEGEIDParameter = cOLLEGEID.HasValue ?
                new ObjectParameter("COLLEGEID", cOLLEGEID) :
                new ObjectParameter("COLLEGEID", typeof(int));
    
            var aCADYEARParameter = aCADYEAR.HasValue ?
                new ObjectParameter("ACADYEAR", aCADYEAR) :
                new ObjectParameter("ACADYEAR", typeof(int));
    
            var sEMParameter = sEM.HasValue ?
                new ObjectParameter("SEM", sEM) :
                new ObjectParameter("SEM", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<procStudentInfoDetails_Result>("procStudentInfoDetails", cOLLEGEIDParameter, aCADYEARParameter, sEMParameter);
        }
    }
}
