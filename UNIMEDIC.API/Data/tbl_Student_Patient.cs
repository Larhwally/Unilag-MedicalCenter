//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UNIMEDIC.API.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Student_Patient
    {
        public int ItbId { get; set; }
        public int taskid { get; set; }
        public string Matric_number { get; set; }
        public Nullable<System.DateTime> Year_of_admission { get; set; }
        public int Patient_id { get; set; }
        public string Status { get; set; }
        public string Createdby { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    
        public virtual tbl_Patient tbl_Patient { get; set; }
    }
}
