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
            int totalPatient = Convert.ToInt32(result["Total_Patient"]);

            var studentPercent = students / totalPatient * 100; //convert to percent
            var staffPercent = staffs / totalPatient * 100; //convert to percent

            // Pass each patient detail to a seperate Dictionary or make 'em independent objects
            Dictionary<string, object> Students = new Dictionary<string, object>();
            Students.Add("student_patients", students);
            Students.Add("student_percentage", studentPercent);

            Dictionary<string, object> Staffs = new Dictionary<string, object>();
            Staffs.Add("staff_patients", staffs);
            Staffs.Add("staff_percentage", staffPercent);

            // Pass the seperate dictionaries of student and staff patients into a list of dictionaries/array of objects
            List<Dictionary<string, object>> Patients = new List<Dictionary<string, object>>();
            Patients.Add(Students);
            Patients.Add(Staffs);


            // result.Add("student_percent", studentPercent);
            // result.Add("staff_percent", staffPercent);

            return Ok(Patients);
        }


        
    }
}