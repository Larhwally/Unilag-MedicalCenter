using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UNIMEDIC.API.Data;
using UNIMEDIC.API.Services;

namespace UNIMEDIC.API.Controllers
{
    public class UniMedController : ApiController
    {
        private UniMedInterface spr = new UniMedClass();

        [HttpGet]
        [Route("Gettbl_Admin")]
        public List<GET_tbl_Admin_Result> Gettbl_Admin(int id, int taskid)
        {
            try
            {
                return spr.Gettbl_AdminByID(id, taskid);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpPost]
        [Route("POSTtbl_Admin")]
        public PostingResponse Posttbl_Admin(tbl_Admin request)
        {
            try
            {
                if (request == null)
                {
                    return null;
                }
                return spr.Posttbl_Admin(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpGet]
        [Route("Gettbl_Clinic")]
        public List<GET_tbl_Clinic_Result> Gettbl_Clinic(int id, int taskid)
        {
            try
            {
                return spr.Gettbl_ClinicByID(id, taskid);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpPost]
        [Route("POSTtbl_Clinic")]
        public PostingResponse Posttbl_Clinic(tbl_Clinic request)
        {
            try
            {
                if (request == null)
                {
                    return null;
                }
                return spr.Posttbl_Clinic(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpGet]
        [Route("Gettbl_ClinicOpenSchedule")]
        public List<GET_tbl_ClinicOpenSchedule_Result> Gettbl_ClinicOpenSchedule(int id, int taskid)
        {
            try
            {
                return spr.Gettbl_ClinicOpenScheduleByID(id, taskid);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpPost]
        [Route("POSTtbl_ClinicOpenSchedule")]
        public PostingResponse Posttbl_ClinicOpenSchedule(tbl_ClinicOpenSchedule request)
        {
            try
            {
                if (request == null)
                {
                    return null;
                }
                return spr.Posttbl_ClinicOpenSchedule(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpGet]
        [Route("Gettbl_Department")]
        public List<GET_tbl_Department_Result> Gettbl_Department(int id, int taskid)
        {
            try
            {
                return spr.Gettbl_DepartmentByID(id, taskid);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpPost]
        [Route("POSTtbl_Department")]
        public PostingResponse Posttbl_Department(tbl_Department request)
        {
            try
            {
                if (request == null)
                {
                    return null;
                }
                return spr.Posttbl_Department(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpGet]
        [Route("Gettbl_Dependent")]
        public List<GET_tbl_Dependent_Result> Gettbl_Dependent(int id, int taskid)
        {
            try
            {
                return spr.Gettbl_DependentByID(id, taskid);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpPost]
        [Route("POSTtbl_Dependent")]
        public PostingResponse Posttbl_Dependent(tbl_Dependent request)
        {
            try
            {
                if (request == null)
                {
                    return null;
                }
                return spr.Posttbl_Dependent(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpGet]
        [Route("Gettbl_Diagnosis")]
        public List<GET_tbl_Diagnosis_Result> Gettbl_Diagnosis(int id, int taskid)
        {
            try
            {
                return spr.Gettbl_DiagnosisByID(id, taskid);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpPost]
        [Route("POSTtbl_Diagnosis")]
        public PostingResponse Posttbl_Diagnosis(tbl_Diagnosis request)
        {
            try
            {
                if (request == null)
                {
                    return null;
                }
                return spr.Posttbl_Diagnosis(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpGet]
        [Route("Gettbl_Doctor")]
        public List<GET_tbl_Doctor_Result> Gettbl_Doctor(int id, int taskid)
        {
            try
            {
                return spr.Gettbl_DoctorByID(id, taskid);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpPost]
        [Route("POSTtbl_Doctor")]
        public PostingResponse Posttbl_Doctor(tbl_Doctor request)
        {
            try
            {
                if (request == null)
                {
                    return null;
                }
                return spr.Posttbl_Doctor(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpGet]
        [Route("Gettbl_Emergency")]
        public List<GET_tbl_Emergency_Result> Gettbl_Emergency(int id, int taskid)
        {
            try
            {
                return spr.Gettbl_EmergencyByID(id, taskid);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpPost]
        [Route("POSTtbl_Emergency")]
        public PostingResponse Posttbl_Emergency(tbl_Emergency request)
        {
            try
            {
                if (request == null)
                {
                    return null;
                }
                return spr.Posttbl_Emergency(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpGet]
        [Route("Gettbl_Faculty")]
        public List<GET_tbl_Faculty_Result> Gettbl_Faculty(int id, int taskid)
        {
            try
            {
                return spr.Gettbl_FacultyByID(id, taskid);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpPost]
        [Route("POSTtbl_Faculty")]
        public PostingResponse Posttbl_Faculty(tbl_Faculty request)
        {
            try
            {
                if (request == null)
                {
                    return null;
                }
                return spr.Posttbl_Faculty(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpGet]
        [Route("Gettbl_Login")]
        public List<GET_tbl_Login_Result> Gettbl_Login(int id, int taskid)
        {
            try
            {
                return spr.Gettbl_LoginByID(id, taskid);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpPost]
        [Route("POSTtbl_Login")]
        public PostingResponse Posttbl_Login(tbl_Login request)
        {
            try
            {
                if (request == null)
                {
                    return null;
                }
                return spr.Posttbl_Login(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpGet]
        [Route("Gettbl_MedicalStaff")]
        public List<GET_tbl_MedicalStaff_Result> Gettbl_MedicalStaff(int id, int taskid)
        {
            try
            {
                return spr.Gettbl_MedicalStaffByID(id, taskid);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpPost]
        [Route("POSTtbl_MedicalStaff")]
        public PostingResponse Posttbl_MedicalStaff(tbl_MedicalStaff request)
        {
            try
            {
                if (request == null)
                {
                    return null;
                }
                return spr.Posttbl_MedicalStaff(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpGet]
        [Route("Gettbl_NextOfKin")]
        public List<GET_tbl_NextOfKin_Result> Gettbl_NextOfKin(int id, int taskid)
        {
            try
            {
                return spr.Gettbl_NextOfKinByID(id, taskid);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpPost]
        [Route("POSTtbl_NextOfKin")]
        public PostingResponse Posttbl_NextOfKin(tbl_NextOfKin request)
        {
            try
            {
                if (request == null)
                {
                    return null;
                }
                return spr.Posttbl_NextOfKin(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpGet]
        [Route("Gettbl_Patient")]
        public List<GET_tbl_Patient_Result> Gettbl_Patient(int id, int taskid)
        {
            try
            {
                return spr.Gettbl_PatientByID(id, taskid);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpPost]
        [Route("POSTtbl_Patient")]
        public PostingResponse Posttbl_Patient(tbl_Patient request)
        {
            try
            {
                if (request == null)
                {
                    return null;
                }
                return spr.Posttbl_Patient(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpGet]
        [Route("Gettbl_Staff_Patient")]
        public List<GET_tbl_Staff_Patient_Result> Gettbl_Staff_Patient(int id, int taskid)
        {
            try
            {
                return spr.Gettbl_Staff_PatientByID(id, taskid);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpPost]
        [Route("POSTtbl_Staff_Patient")]
        public PostingResponse Posttbl_Staff_Patient(tbl_Staff_Patient request)
        {
            try
            {
                if (request == null)
                {
                    return null;
                }
                return spr.Posttbl_Staff_Patient(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpGet]
        [Route("Gettbl_Student_Patient")]
        public List<GET_tbl_Student_Patient_Result> Gettbl_Student_Patient(int id, int taskid)
        {
            try
            {
                return spr.Gettbl_Student_PatientByID(id, taskid);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpPost]
        [Route("POSTtbl_Student_Patient")]
        public PostingResponse Posttbl_Student_Patient(tbl_Student_Patient request)
        {
            try
            {
                if (request == null)
                {
                    return null;
                }
                return spr.Posttbl_Student_Patient(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpGet]
        [Route("Gettbl_Visit")]
        public List<GET_tbl_Visit_Result> Gettbl_Visit(int id, int taskid)
        {
            try
            {
                return spr.Gettbl_VisitByID(id, taskid);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpPost]
        [Route("POSTtbl_Visit")]
        public PostingResponse Posttbl_Visit(tbl_Visit request)
        {
            try
            {
                if (request == null)
                {
                    return null;
                }
                return spr.Posttbl_Visit(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpGet]
        [Route("Gettbl_VitalSigns")]
        public List<GET_tbl_VitalSigns_Result> Gettbl_VitalSigns(int id, int taskid)
        {
            try
            {
                return spr.Gettbl_VitalSignsByID(id, taskid);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [HttpPost]
        [Route("POSTtbl_VitalSigns")]
        public PostingResponse Posttbl_VitalSigns(tbl_VitalSigns request)
        {
            try
            {
                if (request == null)
                {
                    return null;
                }
                return spr.Posttbl_VitalSigns(request);
            }
            catch (Exception ex)
            {
                return null;
            }
        }



    }
}
