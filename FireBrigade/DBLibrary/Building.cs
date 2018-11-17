//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Building
    {
        public int BuildingID { get; set; }
        public string BuildingNo { get; set; }
        public string BuildingName { get; set; }
        public string BuildingTown { get; set; }
        public string BuildingCity { get; set; }
        public string BuildingPostCode { get; set; }
        public int TypeID { get; set; }
        public int AreaID { get; set; }
        public int ContactID { get; set; }
        public int DocumentID { get; set; }
    
        public virtual Area Area { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual Document Document { get; set; }
        public virtual Type Type { get; set; }
    }
}