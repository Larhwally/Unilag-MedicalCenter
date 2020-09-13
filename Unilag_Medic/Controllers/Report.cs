using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Unilag_Medic.Data;

namespace Unilag_Medic.Controllers
{
    [Authorize]
    public class Report : Controller
    {

        [Route("PatientReport")]
        [HttpGet]
        public IActionResult GetPatientReport()
        {
            EntityConnection connection = new EntityConnection("tbl_patient");
            Dictionary<string, object> result = connection.PatientReport();

            // Get the values of total patients, student patients and staff patients
            double students = Convert.ToDouble(result["student_patient"]);
            double staffs = Convert.ToDouble(result["staff_patient"]);
            double nonStaffs = Convert.ToDouble(result["non_staff_patient"]);
            int totalPatient = Convert.ToInt32(result["Total_Patient"]);


            var studentPercent = students / totalPatient * 100; //convert to percent
            var staffPercent = staffs / totalPatient * 100; //convert to percent
            var nonStaffPercent = nonStaffs / totalPatient * 100; //convert to percent

            // Pass each patient detail to a seperate Dictionary or make 'em independent objects
            Dictionary<string, object> Students = new Dictionary<string, object>();
            Students.Add("student_patients", students);
            Students.Add("student_percentage", studentPercent);

            Dictionary<string, object> Staffs = new Dictionary<string, object>();
            Staffs.Add("staff_patients", staffs);
            Staffs.Add("staff_percentage", staffPercent);

            Dictionary<string, object> NonStaffs = new Dictionary<string, object>();
            NonStaffs.Add("non_staffs", nonStaffs);
            NonStaffs.Add("non_staff_peercentage", nonStaffPercent);

            // Pass the seperate dictionaries of student and staff patients into a list of dictionaries/array of objects
            List<Dictionary<string, object>> Patients = new List<Dictionary<string, object>>();
            Patients.Add(Students);
            Patients.Add(Staffs);
            Patients.Add(NonStaffs);


            // result.Add("student_percent", studentPercent);
            // result.Add("staff_percent", staffPercent);

            return Ok(Patients);
        }


         // Get visit record report
        [Route("AppointmentReport")]
        [HttpGet]
        public IActionResult GetAppointmentReport()
        {
            EntityConnection connection = new EntityConnection("tbl_visit");
            Dictionary<string, object> result = connection.AppointmentReport();

            // Get the total appointments, awaiting vitals, attended to appointments
            int totalAppointment = Convert.ToInt32(result["Total_Appointment"]);
            double awaitingVitals = Convert.ToDouble(result["Awaiting_vitals"]);
            double attendedTo = Convert.ToDouble(result["Attended"]);

            // Get the percentage of awaiting and attended
            var awaitingPercent = awaitingVitals / totalAppointment * 100;
            var attendendPercent = attendedTo / totalAppointment * 100;

            // Pass each result into a seperate dictionary to make them independent objects/dictionaries
            Dictionary<string, object> awaitingRecord = new Dictionary<string, object>();
            awaitingRecord.Add("label", "Awaiting");
            awaitingRecord.Add("count", awaitingVitals);
            awaitingRecord.Add("percentage", awaitingPercent);

            Dictionary<string, object> attendedRecorded = new Dictionary<string, object>();
            attendedRecorded.Add("label", "Attended");
            attendedRecorded.Add("count", attendedTo);
            attendedRecorded.Add("percentage", attendendPercent);


            // Pipe the dictionaries into a list of dictionaries/array of objects
            List<Dictionary<string, object>> Appointments = new List<Dictionary<string, object>>();
            Appointments.Add(awaitingRecord);
            Appointments.Add(attendedRecorded);

            return Ok(Appointments);


        }


        
    }
}