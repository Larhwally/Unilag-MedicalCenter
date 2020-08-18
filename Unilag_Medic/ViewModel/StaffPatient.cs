using System;

namespace Unilag_Medic.ViewModel
{
    public class StaffPatient
    {
        public string staffCode { get; set; }
        public DateTime dateOfEmployment { get; set; }
        public int patientId { get; set; }
        public string partnerName { get; set; }
        public string partnerHospNum { get; set; }
        public string partnerStatus { get; set; }
        public string status { get; set; }
        public string createdBy { get; set; }
        public DateTime createDate { get; set; }
    }

}
