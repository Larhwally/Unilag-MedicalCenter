using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Unilag_Medic.Data;
using Unilag_Medic.ViewModel;

namespace Unilag_Medic.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        public object obj = new object();
        public object objs = new object();

        public PatientController(IWebHostEnvironment env)
        {
            _environment = env;
        }
        // GET: api/Patient
        [HttpGet]
        public IActionResult GetPatient()
        {
            EntityConnection con = new EntityConnection("tbl_patient");
            List<Dictionary<string, object>> result = con.SelectAll();

            return Ok(result);
        }

        // GET: api/Patient/5
        [HttpGet("{id}")]
        public IActionResult GetPatientbyId(int id)
        {
            EntityConnection con = new EntityConnection("tbl_patient");
            Dictionary<string, string> pairs = new Dictionary<string, string>
            {
                { "itbId", id + "" }
            };
            //string record = EntityConnection.ToJson(con.SelectByColumn(pairs));
            if (con.SelectByColumn(pairs).Count > 0)
            {
                return Ok(con.SelectByColumn(pairs));
            }
            else
            {
                return NotFound();
            }

        }


        // POST: api/Patient
        [HttpPost]
        public IActionResult Post([FromBody] Dictionary<string, object> param)
        {
            // Uploading image
            // string fName = file.FileName;
            // string uniqueName = Guid.NewGuid() + "" + "_" + fName;
            // string photoUrl = "http://localhost:5000/api/Uploads/" + uniqueName;

            // if (!file.ContentType.StartsWith("image/"))
            // {
            //     objs = new { message = "not an image file" };
            //     return BadRequest(objs);
            // }
           
            // if (!fName.EndsWith("jpg") & !file.FileName.EndsWith("jpeg") & !file.FileName.EndsWith("png"))
            // {
            //     objs = new { message = "image is not in correct format" };
            //     return BadRequest(objs);
            // }
            // if (file.Length < 1024 * 1024 * 5)
            // {
            //     string path = Path.Combine(_environment.ContentRootPath, "upload/" + uniqueName);

            //     using (var stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            //     {
            //         await file.CopyToAsync(stream);
            //     }

            //     //save image details to the databse
            //     // EntityConnection con = new EntityConnection("tbl_upload");
            //     // Dictionary<string, object> param = new Dictionary<string, object>();
            //     // //param.Add("patientId", patientId);
            //     // param.Add("photoUrl", photoUrl);
            //     // param.Add("fullPath", path);
            //     // param.Add("uniquePath", uniqueName);
            //     // param.Add("createBy", "admin");
            //     // param.Add("createDate", DateTime.Now.ToShortDateString());
            //     // con.Insert(param);
            //     // objs = new { data = photoUrl };
            //     // return Ok(objs);
            // }
            // else
            // {
            //     objs = new { message = "File too large" };
            //     return BadRequest(objs);
            // }


            EntityConnection con = new EntityConnection("tbl_patient");
            if (param != null)
            {
                // Auto generate hospital number
                param.Remove("hospitalNumber");

                EntityConnection forHospNum = new EntityConnection("tbl_patient");
                var hospitalNumber = forHospNum.GenerateUniqueHospitalNumber();


                Dictionary<string, object> genericPatient = new Dictionary<string, object>();

                //genericPatient.Add("photoUrl", photoUrl);

                string[] patientRecord = { "surname", "otherNames", "phoneNumber", "altPhoneNum", "email", "ethnicGroup", "gender", "nhisNumber", "hmoId", "dateOfBirth", "maritalStatus", "address", "stateId", "nationalityId", "patientType", "nokName", "nokAddress", "nokPhoneNum", "nokRelationship", "faculty", "department", "photoUrl", "status", "recordStaffId" };

                genericPatient = Utility.Pick(param, patientRecord);

                genericPatient.Add("hospitalNumber", hospitalNumber);

                genericPatient.Add("createDate", DateTime.Now.ToString());

                long patientId = con.InsertScalar(genericPatient);

                genericPatient.Add("itbId", patientId);

                int patientTypeId = Convert.ToInt32(genericPatient["patientType"]);

                // check if patient type is a staff
                if (patientTypeId == 1)
                {
                    Dictionary<string, object> staff = new Dictionary<string, object>();

                    string[] staffPatientRecord = { "staffCode", "dateOfEmployment", "designation", "partnerName", "partnerHospNum", "partnerIsStaff", "status", "createdBy" };

                    staff = Utility.Pick(param, staffPatientRecord);

                    staff.Add("patientId", patientId);
                    staff.Add("createDate", DateTime.Now.ToString());

                    EntityConnection connection = new EntityConnection("tbl_staff_patient");

                    long staffId = connection.InsertScalar(staff);

                    param.Add("staffId", staffId);

                    //connection.InsertStaffPatient(staff);
                }
                else if (patientTypeId == 2) //if patient type is a student
                {
                    Dictionary<string, object> student = new Dictionary<string, object>();

                    string[] studentPatientRecord = { "matricNumber", "yearOfAdmission", "parentName", "parentAddress1", "parentAddress2", "parentPhone1", "parentPhone2", "localGuardian", "localGuardAddress1", "localGuardAddress2", "localGuardPhone1", "localGuardPhone2", "localGuardianRelationship", "status", "recordStaffId" };

                    student = Utility.Pick(param, studentPatientRecord);

                    student.Add("patientId", patientId);
                    student.Add("createDate", DateTime.Now.ToString());

                    EntityConnection connect = new EntityConnection("tbl_student_patient");

                    connect.InsertStudentPatient(student);
                }
                // check if patient is a non staff
                else if (patientTypeId == 4)
                {
                    Dictionary<string, object> nonStaff = new Dictionary<string, object>();

                    string[] nonStaffRecord = { "companyName", "dateOfAppointment", "companyAddress", "designation", "recordStaffId" };

                    nonStaff = Utility.Pick(param, nonStaffRecord); // pick the data specified in the array out of the param dictionary containing full patient data and save it in a dictionary

                    nonStaff.Add("patientId", patientId);

                    EntityConnection cons = new EntityConnection("tbl_nonstaff");

                    param.Remove("faculty");


                    cons.InsertNonStaff(nonStaff);
                }

                param.Add("hospitalNumber", hospitalNumber);
                obj = new { data = param };
                return Created("", obj);

            }
            else
            {
                return BadRequest();
            }


        }

        // PUT: api/Patient/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Dictionary<string, string> content)
        {
            EntityConnection con = new EntityConnection("tbl_patient");
            if (id != 0)
            {
                con.Update(id, content);
                return Ok(content);
                //Response.WriteAsync("Record updated successfully!");
            }
            else
            {
                return BadRequest();
            }

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            EntityConnection con = new EntityConnection("tbl_patient");
            if (id != 0)
            {
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("itbId", id + "");
                con.Delete(param).ToString();

            }
            else
            {
                return NotFound();
            }
            obj = new { message = "Record deleted successfully!" };
            return Ok(obj);
        }


    }
}
