using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNIMEDIC.API.Data;

namespace UNIMEDIC.API.Services
{
    public interface UniMedInterface
    {
        List<GET_tbl_Admin_Result> Gettbl_AdminByID(int id, int taskid);
        PostingResponse Posttbl_Admin(tbl_Admin tbl_admin);

        List<GET_tbl_Clinic_Result> Gettbl_ClinicByID(int id, int taskid);
        PostingResponse Posttbl_Clinic(tbl_Clinic tbl_clinic);

        List<GET_tbl_ClinicOpenSchedule_Result> Gettbl_ClinicOpenScheduleByID(int id, int taskid);
        PostingResponse Posttbl_ClinicOpenSchedule(tbl_ClinicOpenSchedule tbl_clinicopenschedule);

        List<GET_tbl_Department_Result> Gettbl_DepartmentByID(int id, int taskid);
        PostingResponse Posttbl_Department(tbl_Department tbl_department);

        List<GET_tbl_Dependent_Result> Gettbl_DependentByID(int id, int taskid);
        PostingResponse Posttbl_Dependent(tbl_Dependent tbl_dependent);

        List<GET_tbl_Diagnosis_Result> Gettbl_DiagnosisByID(int id, int taskid);
        PostingResponse Posttbl_Diagnosis(tbl_Diagnosis tbl_diagnosis);

        List<GET_tbl_Doctor_Result> Gettbl_DoctorByID(int id, int taskid);
        PostingResponse Posttbl_Doctor(tbl_Doctor tbl_doctor);

        List<GET_tbl_Emergency_Result> Gettbl_EmergencyByID(int id, int taskid);
        PostingResponse Posttbl_Emergency(tbl_Emergency tbl_emergency);

        List<GET_tbl_Faculty_Result> Gettbl_FacultyByID(int id, int taskid);
        PostingResponse Posttbl_Faculty(tbl_Faculty tbl_faculty);

        List<GET_tbl_Login_Result> Gettbl_LoginByID(int id, int taskid);
        PostingResponse Posttbl_Login(tbl_Login tbl_login);

        List<GET_tbl_MedicalStaff_Result> Gettbl_MedicalStaffByID(int id, int taskid);
        PostingResponse Posttbl_MedicalStaff(tbl_MedicalStaff tbl_medicalstaff);

        List<GET_tbl_NextOfKin_Result> Gettbl_NextOfKinByID(int id, int taskid);
        PostingResponse Posttbl_NextOfKin(tbl_NextOfKin tbl_nextofkin);

        List<GET_tbl_Patient_Result> Gettbl_PatientByID(int id, int taskid);
        PostingResponse Posttbl_Patient(tbl_Patient tbl_patient);

        List<GET_tbl_Staff_Patient_Result> Gettbl_Staff_PatientByID(int id, int taskid);
        PostingResponse Posttbl_Staff_Patient(tbl_Staff_Patient tbl_staff_patient);

        List<GET_tbl_Student_Patient_Result> Gettbl_Student_PatientByID(int id, int taskid);
        PostingResponse Posttbl_Student_Patient(tbl_Student_Patient tbl_student_patient);

        List<GET_tbl_Visit_Result> Gettbl_VisitByID(int id, int taskid);
        PostingResponse Posttbl_Visit(tbl_Visit tbl_visit);

        List<GET_tbl_VitalSigns_Result> Gettbl_VitalSignsByID(int id, int taskid);
        PostingResponse Posttbl_VitalSigns(tbl_VitalSigns tbl_vitalsigns);


    }
}
