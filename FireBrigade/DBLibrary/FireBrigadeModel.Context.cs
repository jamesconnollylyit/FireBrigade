﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBLibrary
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FireDBEntities : DbContext
    {
        public FireDBEntities(string connectionString)
            : base(connectionString)
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AccessLevel> AccessLevels { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
