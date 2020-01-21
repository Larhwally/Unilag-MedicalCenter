using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using UNIMEDIC.API.Data;

namespace UNIMEDIC.API.Services
{
    public class UniMedClass : UniMedInterface
    {
        UNILAG_MCEntities db = new UNILAG_MCEntities();
        public List<GET_tbl_Admin_Result> Gettbl_AdminByID(int id, int taskid)
        {
            return db.GET_tbl_Admin(id, taskid).ToList();
        }

        public List<GET_tbl_Clinic_Result> Gettbl_ClinicByID(int id, int taskid)
        {
            return db.GET_tbl_Clinic(id, taskid).ToList();
        }

        public List<GET_tbl_ClinicOpenSchedule_Result> Gettbl_ClinicOpenScheduleByID(int id, int taskid)
        {
            return db.GET_tbl_ClinicOpenSchedule(id, taskid).ToList();
        }

        public List<GET_tbl_Department_Result> Gettbl_DepartmentByID(int id, int taskid)
        {
            return db.GET_tbl_Department(id, taskid).ToList();
        }

        public List<GET_tbl_Dependent_Result> Gettbl_DependentByID(int id, int taskid)
        {
            return db.GET_tbl_Dependent(id, taskid).ToList();
        }

        public List<GET_tbl_Diagnosis_Result> Gettbl_DiagnosisByID(int id, int taskid)
        {
            return db.GET_tbl_Diagnosis(id, taskid).ToList();
        }

        public List<GET_tbl_Doctor_Result> Gettbl_DoctorByID(int id, int taskid)
        {
            return db.GET_tbl_Doctor(id, taskid).ToList();
        }

        public List<GET_tbl_Emergency_Result> Gettbl_EmergencyByID(int id, int taskid)
        {
            return db.GET_tbl_Emergency(id, taskid).ToList();
        }

        public List<GET_tbl_Faculty_Result> Gettbl_FacultyByID(int id, int taskid)
        {
            return db.GET_tbl_Faculty(id, taskid).ToList();
        }

        public List<GET_tbl_Login_Result> Gettbl_LoginByID(int id, int taskid)
        {
            return db.GET_tbl_Login(id, taskid).ToList();
        }

        public List<GET_tbl_MedicalStaff_Result> Gettbl_MedicalStaffByID(int id, int taskid)
        {
            return db.GET_tbl_MedicalStaff(id, taskid).ToList();
        }

        public List<GET_tbl_NextOfKin_Result> Gettbl_NextOfKinByID(int id, int taskid)
        {
            return db.GET_tbl_NextOfKin(id, taskid).ToList();
        }

        public List<GET_tbl_Patient_Result> Gettbl_PatientByID(int id, int taskid)
        {
            return db.GET_tbl_Patient(id, taskid).ToList();
        }

        public List<GET_tbl_Staff_Patient_Result> Gettbl_Staff_PatientByID(int id, int taskid)
        {
            return db.GET_tbl_Staff_Patient(id, taskid).ToList();
        }

        public List<GET_tbl_Student_Patient_Result> Gettbl_Student_PatientByID(int id, int taskid)
        {
            return db.GET_tbl_Student_Patient(id, taskid).ToList();
        }

        public List<GET_tbl_Visit_Result> Gettbl_VisitByID(int id, int taskid)
        {
            return db.GET_tbl_Visit(id, taskid).ToList();
        }

        public List<GET_tbl_VitalSigns_Result> Gettbl_VitalSignsByID(int id, int taskid)
        {
            return db.GET_tbl_VitalSigns(id, taskid).ToList();
        }


        public PostingResponse Posttbl_Admin(tbl_Admin tbl_admin)
        {
            var reVal = new ObjectParameter("retVal", 0);
            var retMsg = new ObjectParameter("retMsg", 0);
            try
            {
                db.POST_tbl_Admin(tbl_admin.ItbId
                , tbl_admin.Medstaff_id
                , tbl_admin.Status
                , tbl_admin.Createdby
                , tbl_admin.CreateDate
                , tbl_admin.taskid, reVal, retMsg);
            }
            catch (Exception ex)
            {
                return new PostingResponse
                {
                    respcode = -1,
                    responseMsg = (string)ex.Message
                };
            }
            return new PostingResponse
            {
                respcode = (Int32)reVal.Value,
                responseMsg = (string)retMsg.Value
            };
        }
        public PostingResponse Posttbl_Clinic(tbl_Clinic tbl_clinic)
        {
            var reVal = new ObjectParameter("retVal", 0);
            var retMsg = new ObjectParameter("retMsg", 0);
            try
            {
                db.POST_tbl_Clinic(tbl_clinic.ItbId
                , tbl_clinic.Clinic_name
                , tbl_clinic.Clinic_type
                , tbl_clinic.Clinic_description
                , tbl_clinic.clinic_open_schedule
                , tbl_clinic.Status
                , tbl_clinic.CreatedBy
                , tbl_clinic.CreateDate
                , tbl_clinic.taskid, reVal, retMsg);
            }
            catch (Exception ex)
            {
                return new PostingResponse
                {
                    respcode = -1,
                    responseMsg = (string)ex.Message
                };
            }
            return new PostingResponse
            {
                respcode = (Int32)reVal.Value,
                responseMsg = (string)retMsg.Value
            };
        }
        public PostingResponse Posttbl_ClinicOpenSchedule(tbl_ClinicOpenSchedule tbl_clinicopenschedule)
        {
            var reVal = new ObjectParameter("retVal", 0);
            var retMsg = new ObjectParameter("retMsg", 0);
            try
            {
                db.POST_tbl_ClinicOpenSchedule(tbl_clinicopenschedule.ItbId
                , tbl_clinicopenschedule.Clinic_day
                , tbl_clinicopenschedule.Clinic_time
                , tbl_clinicopenschedule.Status
                , tbl_clinicopenschedule.Createdby
                , tbl_clinicopenschedule.CreateDate
                , tbl_clinicopenschedule.taskid, reVal, retMsg);
            }
            catch (Exception ex)
            {
                return new PostingResponse
                {
                    respcode = -1,
                    responseMsg = (string)ex.Message
                };
            }
            return new PostingResponse
            {
                respcode = (Int32)reVal.Value,
                responseMsg = (string)retMsg.Value
            };
        }
        public PostingResponse Posttbl_Department(tbl_Department tbl_department)
        {
            var reVal = new ObjectParameter("retVal", 0);
            var retMsg = new ObjectParameter("retMsg", 0);
            try
            {
                db.POST_tbl_Department(tbl_department.ItbId
                , tbl_department.Department_title
                , tbl_department.Status
                , tbl_department.Createdby
                , tbl_department.CreateDate
                , tbl_department.Faculty_id
                , tbl_department.taskid, reVal, retMsg);
            }
            catch (Exception ex)
            {
                return new PostingResponse
                {
                    respcode = -1,
                    responseMsg = (string)ex.Message
                };
            }
            return new PostingResponse
            {
                respcode = (Int32)reVal.Value,
                responseMsg = (string)retMsg.Value
            };
        }
        public PostingResponse Posttbl_Dependent(tbl_Dependent tbl_dependent)
        {
            var reVal = new ObjectParameter("retVal", 0);
            var retMsg = new ObjectParameter("retMsg", 0);
            try
            {
                db.POST_tbl_Dependent(tbl_dependent.ItbId
                , tbl_dependent.Relationship
                , tbl_dependent.Guardian_id
                , tbl_dependent.Status
                , tbl_dependent.Createdby
                , tbl_dependent.CreateDate
                , tbl_dependent.taskid, reVal, retMsg);
            }
            catch (Exception ex)
            {
                return new PostingResponse
                {
                    respcode = -1,
                    responseMsg = (string)ex.Message
                };
            }
            return new PostingResponse
            {
                respcode = (Int32)reVal.Value,
                responseMsg = (string)retMsg.Value
            };
        }
        public PostingResponse Posttbl_Diagnosis(tbl_Diagnosis tbl_diagnosis)
        {
            var reVal = new ObjectParameter("retVal", 0);
            var retMsg = new ObjectParameter("retMsg", 0);
            try
            {
                db.POST_tbl_Diagnosis(tbl_diagnosis.ItbId
                , tbl_diagnosis.Diagnosis
                , tbl_diagnosis.Examination
                , tbl_diagnosis.Prescription
                , tbl_diagnosis.Visit_id
                , tbl_diagnosis.Vital_id
                , tbl_diagnosis.Doctor_id
                , tbl_diagnosis.Status
                , tbl_diagnosis.CreatedBy
                , tbl_diagnosis.CreateDate
                , tbl_diagnosis.taskid, reVal, retMsg);
            }
            catch (Exception ex)
            {
                return new PostingResponse
                {
                    respcode = -1,
                    responseMsg = (string)ex.Message
                };
            }
            return new PostingResponse
            {
                respcode = (Int32)reVal.Value,
                responseMsg = (string)retMsg.Value
            };
        }
        public PostingResponse Posttbl_Doctor(tbl_Doctor tbl_doctor)
        {
            var reVal = new ObjectParameter("retVal", 0);
            var retMsg = new ObjectParameter("retMsg", 0);
            try
            {
                db.POST_tbl_Doctor(tbl_doctor.ItbId
                , tbl_doctor.Specialization
                , tbl_doctor.Staff_id
                , tbl_doctor.Status
                , tbl_doctor.Createdby
                , tbl_doctor.CreateDate
                , tbl_doctor.taskid, reVal, retMsg);
            }
            catch (Exception ex)
            {
                return new PostingResponse
                {
                    respcode = -1,
                    responseMsg = (string)ex.Message
                };
            }
            return new PostingResponse
            {
                respcode = (Int32)reVal.Value,
                responseMsg = (string)retMsg.Value
            };
        }
        public PostingResponse Posttbl_Emergency(tbl_Emergency tbl_emergency)
        {
            var reVal = new ObjectParameter("retVal", 0);
            var retMsg = new ObjectParameter("retMsg", 0);
            try
            {
                db.POST_tbl_Emergency(tbl_emergency.ItbId
                , tbl_emergency.Emergency_case_number
                , tbl_emergency.Patient_id
                , tbl_emergency.Status
                , tbl_emergency.Createdby
                , tbl_emergency.CreateDate
                , tbl_emergency.taskid, reVal, retMsg);
            }
            catch (Exception ex)
            {
                return new PostingResponse
                {
                    respcode = -1,
                    responseMsg = (string)ex.Message
                };
            }
            return new PostingResponse
            {
                respcode = (Int32)reVal.Value,
                responseMsg = (string)retMsg.Value
            };
        }
        public PostingResponse Posttbl_Faculty(tbl_Faculty tbl_faculty)
        {
            var reVal = new ObjectParameter("retVal", 0);
            var retMsg = new ObjectParameter("retMsg", 0);
            try
            {
                db.POST_tbl_Faculty(tbl_faculty.ItbId
                , tbl_faculty.Faculty_title
                , tbl_faculty.Status
                , tbl_faculty.Createdby
                , tbl_faculty.CreateDate
                , tbl_faculty.taskid, reVal, retMsg);
            }
            catch (Exception ex)
            {
                return new PostingResponse
                {
                    respcode = -1,
                    responseMsg = (string)ex.Message
                };
            }
            return new PostingResponse
            {
                respcode = (Int32)reVal.Value,
                responseMsg = (string)retMsg.Value
            };
        }
        public PostingResponse Posttbl_Login(tbl_Login tbl_login)
        {
            var reVal = new ObjectParameter("retVal", 0);
            var retMsg = new ObjectParameter("retMsg", 0);
            try
            {
                db.POST_tbl_Login(tbl_login.ItbId
                , tbl_login.Username
                , tbl_login.Password
                , tbl_login.User_type
                , tbl_login.Medical_staff
                , tbl_login.UserID
                , tbl_login.status
                , tbl_login.Createdby
                , tbl_login.CreateDate
                , tbl_login.taskid, reVal, retMsg);
            }
            catch (Exception ex)
            {
                return new PostingResponse
                {
                    respcode = -1,
                    responseMsg = (string)ex.Message
                };
            }
            return new PostingResponse
            {
                respcode = (Int32)reVal.Value,
                responseMsg = (string)retMsg.Value
            };
        }
        public PostingResponse Posttbl_MedicalStaff(tbl_MedicalStaff tbl_medicalstaff)
        {
            var reVal = new ObjectParameter("retVal", 0);
            var retMsg = new ObjectParameter("retMsg", 0);
            try
            {
                db.POST_tbl_MedicalStaff(tbl_medicalstaff.ItbId
                , tbl_medicalstaff.Staff_code
                , tbl_medicalstaff.First_name
                , tbl_medicalstaff.Last_name
                , tbl_medicalstaff.Other_name
                , tbl_medicalstaff.Gender
                , tbl_medicalstaff.Position
                , tbl_medicalstaff.Phone_num
                , tbl_medicalstaff.Address
                , tbl_medicalstaff.Nationality
                , tbl_medicalstaff.Staff_type
                , tbl_medicalstaff.Role
                , tbl_medicalstaff.Status
                , tbl_medicalstaff.Createdby
                , tbl_medicalstaff.CreateDate
                , tbl_medicalstaff.taskid, reVal, retMsg);
            }
            catch (Exception ex)
            {
                return new PostingResponse
                {
                    respcode = -1,
                    responseMsg = (string)ex.Message
                };
            }
            return new PostingResponse
            {
                respcode = (Int32)reVal.Value,
                responseMsg = (string)retMsg.Value
            };
        }
        public PostingResponse Posttbl_NextOfKin(tbl_NextOfKin tbl_nextofkin)
        {
            var reVal = new ObjectParameter("retVal", 0);
            var retMsg = new ObjectParameter("retMsg", 0);
            try
            {
                db.POST_tbl_NextOfKin(tbl_nextofkin.ItbId
                , tbl_nextofkin.Last_name
                , tbl_nextofkin.First_name
                , tbl_nextofkin.Other_names
                , tbl_nextofkin.Phone_number
                , tbl_nextofkin.Gender
                , tbl_nextofkin.Address
                , tbl_nextofkin.Relationship
                , tbl_nextofkin.Status
                , tbl_nextofkin.Createdby
                , tbl_nextofkin.CreateDate
                , tbl_nextofkin.taskid, reVal, retMsg);
            }
            catch (Exception ex)
            {
                return new PostingResponse
                {
                    respcode = -1,
                    responseMsg = (string)ex.Message
                };
            }
            return new PostingResponse
            {
                respcode = (Int32)reVal.Value,
                responseMsg = (string)retMsg.Value
            };
        }
        public PostingResponse Posttbl_Patient(tbl_Patient tbl_patient)
        {
            var reVal = new ObjectParameter("retVal", 0);
            var retMsg = new ObjectParameter("retMsg", 0);
            try
            {
                db.POST_tbl_Patient(tbl_patient.ItbId
                , tbl_patient.Last_name
                , tbl_patient.First_name
                , tbl_patient.Other_names
                , tbl_patient.Phone_number
                , tbl_patient.Gender
                , tbl_patient.Hospital_number
                , tbl_patient.Date_of_birth
                , tbl_patient.Address
                , tbl_patient.Marital_status
                , tbl_patient.Nationality
                , tbl_patient.Patient_type
                , tbl_patient.Next_of_kin
                , tbl_patient.Department
                , tbl_patient.Dependent_id
                , tbl_patient.Status
                , tbl_patient.CreatedBy
                , tbl_patient.CreateDate
                , tbl_patient.taskid, reVal, retMsg);
            }
            catch (Exception ex)
            {
                return new PostingResponse
                {
                    respcode = -1,
                    responseMsg = (string)ex.Message
                };
            }
            return new PostingResponse
            {
                respcode = (Int32)reVal.Value,
                responseMsg = (string)retMsg.Value
            };
        }
        public PostingResponse Posttbl_Staff_Patient(tbl_Staff_Patient tbl_staff_patient)
        {
            var reVal = new ObjectParameter("retVal", 0);
            var retMsg = new ObjectParameter("retMsg", 0);
            try
            {
                db.POST_tbl_Staff_Patient(tbl_staff_patient.ItbId
                , tbl_staff_patient.Staff_code
                , tbl_staff_patient.No_of_dependent
                , tbl_staff_patient.Date_of_employment
                , tbl_staff_patient.Patient_id
                , tbl_staff_patient.Status
                , tbl_staff_patient.Createdby
                , tbl_staff_patient.CreateDate
                , tbl_staff_patient.taskid, reVal, retMsg);
            }
            catch (Exception ex)
            {
                return new PostingResponse
                {
                    respcode = -1,
                    responseMsg = (string)ex.Message
                };
            }
            return new PostingResponse
            {
                respcode = (Int32)reVal.Value,
                responseMsg = (string)retMsg.Value
            };
        }
        public PostingResponse Posttbl_Student_Patient(tbl_Student_Patient tbl_student_patient)
        {
            var reVal = new ObjectParameter("retVal", 0);
            var retMsg = new ObjectParameter("retMsg", 0);
            try
            {
                db.POST_tbl_Student_Patient(tbl_student_patient.ItbId
                , tbl_student_patient.Matric_number
                , tbl_student_patient.Year_of_admission
                , tbl_student_patient.Patient_id
                , tbl_student_patient.Status
                , tbl_student_patient.Createdby
                , tbl_student_patient.CreateDate
                , tbl_student_patient.taskid, reVal, retMsg);
            }
            catch (Exception ex)
            {
                return new PostingResponse
                {
                    respcode = -1,
                    responseMsg = (string)ex.Message
                };
            }
            return new PostingResponse
            {
                respcode = (Int32)reVal.Value,
                responseMsg = (string)retMsg.Value
            };
        }
        public PostingResponse Posttbl_Visit(tbl_Visit tbl_visit)
        {
            var reVal = new ObjectParameter("retVal", 0);
            var retMsg = new ObjectParameter("retMsg", 0);
            try
            {
                db.POST_tbl_Visit(tbl_visit.ItbId
                , tbl_visit.Visit_datetime
                , tbl_visit.Patient_id
                , tbl_visit.Recordstaff_id
                , tbl_visit.Clinic_id
                , tbl_visit.Status
                , tbl_visit.CreatedBy
                , tbl_visit.CreateDate
                , tbl_visit.taskid, reVal, retMsg);
            }
            catch (Exception ex)
            {
                return new PostingResponse
                {
                    respcode = -1,
                    responseMsg = (string)ex.Message
                };
            }
            return new PostingResponse
            {
                respcode = (Int32)reVal.Value,
                responseMsg = (string)retMsg.Value
            };
        }
        public PostingResponse Posttbl_VitalSigns(tbl_VitalSigns tbl_vitalsigns)
        {
            var reVal = new ObjectParameter("retVal", 0);
            var retMsg = new ObjectParameter("retMsg", 0);
            try
            {
                db.POST_tbl_VitalSigns(tbl_vitalsigns.ItbId
                , tbl_vitalsigns.Blood_pressure
                , tbl_vitalsigns.Temperature
                , tbl_vitalsigns.Pulse
                , tbl_vitalsigns.Respiratory
                , tbl_vitalsigns.Visit_id
                , tbl_vitalsigns.Nurse_id
                , tbl_vitalsigns.Status
                , tbl_vitalsigns.CreatedBy
                , tbl_vitalsigns.CreateDate
                , tbl_vitalsigns.taskid, reVal, retMsg);
            }
            catch (Exception ex)
            {
                return new PostingResponse
                {
                    respcode = -1,
                    responseMsg = (string)ex.Message
                };
            }
            return new PostingResponse
            {
                respcode = (Int32)reVal.Value,
                responseMsg = (string)retMsg.Value
            };
        }

    }
}