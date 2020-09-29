using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Unilag_Medic.Data;
using Unilag_Medic.Models;

namespace Unilag_Medic.Controllers
{
    [Authorize]
    //[Produces("application/json")]
    public class GeneralController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public GeneralController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public List<NationalityModel> countries { get; set; }
        public object obj = new object();
        public object obj1 = new object();
        public object obj2 = new object();
        public object notes = new object();

        [Route("MedicalUnits")]
        [HttpPost]
        public IActionResult PostMedUnit([FromBody] Dictionary<string, object> details)
        {
            EntityConnection con = new EntityConnection("tbl_medunits");
            if (details != null)
            {
                details.Add("createDate", DateTime.Now.ToString());
                con.Insert(details);
                return Ok(details);
            }
            else
            {
                obj = new { message = "error in creating record" };
                return BadRequest(obj);
            }
        }

        [Route("MedicalUnits")]
        [HttpGet]
        public IActionResult GetMedUnit()
        {
            EntityConnection con = new EntityConnection("tbl_medunits");
            List<Dictionary<string, object>> rec = con.SelectAll();
            return Ok(rec);
        }


        [Route("VitalUnits")]
        [HttpPost]
        public IActionResult PostUnit([FromBody] Dictionary<string, object> details)
        {
            EntityConnection con = new EntityConnection("tbl_vitalunits");
            if (details != null)
            {
                con.Insert(details);
                return Ok(details);
            }
            else
            {
                obj = new { message = "error in creating record" };
                return BadRequest(obj);
            }
        }

        [Route("VitalUnits")]
        [HttpGet]
        public IActionResult GetUnit()
        {
            EntityConnection con = new EntityConnection("tbl_vitalunits");
            List<Dictionary<string, object>> rec = con.SelectAll();
            return Ok(rec);
        }
        //Begin  GET requests



        //POST and GET method for Visit

        [Route("Clinics")]
        [HttpPost]
        public IActionResult PostClinic([FromBody] Dictionary<string, object> param)
        {
            EntityConnection con = new EntityConnection("tbl_clinic");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                return Created("New record added successfully!", param);
                //Response.WriteAsync("Record saved successfully!");
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record, check details and try again!");
                obj = new { message = "Error in creating record, check details and try again!" };
                return BadRequest(obj);
            }

            //return Ok(param);
        }


        [Route("Clinics")]
        [HttpGet]
        public IActionResult GetClinic()
        {
            EntityConnection con = new EntityConnection("tbl_clinic");
            //string rec = EntityConnection.ToJson(con.Select());
            List<Dictionary<string, object>> rec = con.SelectAll();
            return Ok(rec);
        }

        //End of clinic POST and GET

        //POST and GET clinic schedule

        [Route("ClinicSchedules")]
        [HttpPost]
        public IActionResult PostClinicSchedule([FromBody] Dictionary<string, object> param)
        {
            EntityConnection con = new EntityConnection("tbl_clinicopenschedule");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                return Created("Record created successful", param);
                //Response.WriteAsync("Record saved successfully!");
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record, check details and try again!");
                obj = new { message = "Error in creating record, check details and try again!" };
                return BadRequest(obj);
            }

        }


        [Route("ClinicSchedules")]
        [HttpGet]
        public IActionResult GetClinicSchedule()
        {
            EntityConnection con = new EntityConnection("tbl_clinicopenschedule");
            //string rec = "{'Status': true, 'Data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.SelectAll();
            return Ok(rec);
        }

        //End of clinic schedule

        //state GET and POST method

        [Route("States")]
        [HttpGet]
        public IActionResult GetState()
        {
            EntityConnection con = new EntityConnection("tbl_state");
            //string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.SelectAll();
            return Ok(rec);
        }

        [Route("States")]
        [HttpPost]
        public IActionResult PostState([FromBody] Dictionary<string, object> param)
        {
            EntityConnection con = new EntityConnection("tbl_state");

            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToShortDateString());
                con.Insert(param);
                return Created("Record added successful", param);
            }
            else
            {
                obj = new { message = "Error in creating record, check details and try again!" };
                return BadRequest(obj);
            }
        }

        //End of State POST and GET



        //Nationality GET and POST method

        [Route("Nationalities")]
        [HttpGet]
        public IActionResult GetNational()
        {
            EntityConnection con = new EntityConnection("tbl_nationality");
            //string result = "{'status': true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.SelectAll();
            return Ok(rec);
        }

        [Route("Nationalities")]
        [HttpPost]
        public IActionResult PostNationality([FromBody] Dictionary<string, object> param)
        {
            EntityConnection con = new EntityConnection("tbl_nationality");

            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToShortDateString());
                con.Insert(param);
                return Created("Record added successful", param);
            }
            else
            {
                obj = new { message = "Error in creating record, check details and try again!" };
                return BadRequest(obj);
            }
        }

        //End of Nationality


        //Begin Patient type GET and POST

        [Route("PatientTypes")]
        [HttpGet]
        public IActionResult GetPatienttype()
        {
            EntityConnection con = new EntityConnection("tbl_patienttype");
            //string result = "{'status': true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.SelectAll();
            return Ok(rec);
        }

        [Route("PatientTypes")]
        [HttpPost]
        public IActionResult PostPatienttype([FromBody] Dictionary<string, object> param)
        {
            EntityConnection con = new EntityConnection("tbl_patienttype");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                return Created("Record created successful", param);
                //Response.WriteAsync("Record saves successfully!");
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record");
                obj = new { message = "Error in creating record, check details and try again!" };
                return BadRequest(obj);
            }
        }

        //End POST and GET method


        //Begin POST and GET Visittype

        [Route("VisitTypes")]
        [HttpGet]
        public IActionResult GetVisittype()
        {
            EntityConnection con = new EntityConnection("tbl_visittype");
            //string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.SelectAll();
            return Ok(rec);
        }

        [Route("VisitTypes")]
        [HttpPost]
        public IActionResult PostVisittype([FromBody] Dictionary<string, object> param)
        {
            EntityConnection con = new EntityConnection("tbl_visittype");

            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                long id = con.InsertScalar(param);
                param.Add("itbIb", id);
                return Created("New record added successfully!", param);
                //Response.WriteAsync("Record saved successfully!");
            }
            else
            {
                obj = new { message = "Error in creating record, check details and try again!" };
                return BadRequest(obj);
            }
            //return Ok(param);
        }

        //End Visittype GET and POST


        //Begin HMO Post and GET method

        [Route("Hmos")]
        [HttpGet]
        public IActionResult GetHmo()
        {
            EntityConnection con = new EntityConnection("tbl_hmo");
            //string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.SelectAll();
            return Ok(rec);
        }

        [Route("Hmos")]
        [HttpPost]
        public IActionResult PostHMO([FromBody] Dictionary<string, object> param)
        {
            EntityConnection con = new EntityConnection("tbl_hmo");

            if (param != null)
            {
                //param.Add("createBy", model.email);
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                return Created("Record created successful", param);
                //Response.WriteAsync("Record saves successfully!");
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record");
                obj = new { message = "Error in creating record, check details and try again!" };
                return BadRequest(obj);
            }
        }

        //END of HMO GET and POst method


        //Begin Department POST and GET method

        [Route("Departments")]
        [HttpGet]
        public IActionResult GetDepartment()
        {
            EntityConnection con = new EntityConnection("tbl_department");
            //string result = "{'status':true, 'data':}" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.SelectAll();
            return Ok(rec);
        }

        [Route("Departments")]
        [HttpPost]
        public IActionResult PostDepartment([FromBody] Dictionary<string, object> param)
        {
            EntityConnection con = new EntityConnection("tbl_department");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                return Created("Record created successful", param);
                //Response.WriteAsync("Record saves successfully!");
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record");
                obj = new { message = "Error in creating record, check details and try again!" };
                return BadRequest(obj);
            }
        }

        //End of Department POST and GET method 


        //Begin faculty POST and GET method
        [Route("Faculty")]
        [HttpGet]
        public IActionResult GetFaculty()
        {
            EntityConnection con = new EntityConnection("tbl_faculty");
            //string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.SelectAll();
            return Ok(rec);
        }

        [Route("Faculty")]
        [HttpPost]
        public IActionResult PostFaculty([FromBody] Dictionary<string, object> param)
        {
            EntityConnection con = new EntityConnection("tbl_faculty");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                return Created("Record created successful", param);
                //Response.WriteAsync("Record saves successfully!");
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record");
                obj = new { message = "Error in creating record, check details and try again!" };
                return BadRequest(obj);
            }
            //return Ok(param);
        }
        //End of faculty POST and GET method


        //Begin Doctor POST and GET method

        [Route("Doctors")]
        [HttpGet]
        public IActionResult GetDoctor()
        {
            EntityConnection con = new EntityConnection("tbl_doctor");
            //string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.GetDoctors();
            return Ok(rec);
        }

        [Route("Doctors")]
        [HttpPost]
        public IActionResult PostDoctor([FromBody] Dictionary<string, object> param)
        {
            EntityConnection con = new EntityConnection("tbl_doctor");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                //Response.WriteAsync("Record saves successfully!");
                return Created("Record created successful", param);
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record");
                obj = new { message = "Error in creating record, check details and try again!" };
                return BadRequest(obj);
            }

        }
        //END of Doctor POST and GET method


        //Begin specialization POST and GET method
        [Route("Specializations")]
        [HttpGet]
        public IActionResult GetDocSpec()
        {
            EntityConnection con = new EntityConnection("tbl_specialization");
            //string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.SelectAll();
            return Ok(rec);
        }

        [Route("Specializations")]
        [HttpPost]
        public IActionResult PostSpecialization([FromBody] Dictionary<string, object> param)
        {
            EntityConnection con = new EntityConnection("tbl_specialization");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                return Created("Record created successful", param);
                //Response.WriteAsync("Record saved successfully!");
            }
            else
            {
                //var resp = Response.WriteAsync("Error in creating record, check details and try again!");
                obj = new { message = "Error in creating record, check details and try again!" };
                return BadRequest(obj);
            }

            //return Ok(param);
        }

        //End of specialization GET and POST method





        [Route("Stafftypes")]
        [HttpGet]
        public IActionResult GetStaffType()
        {
            EntityConnection con = new EntityConnection("tbl_stafftype");
            List<Dictionary<string, object>> rec = con.SelectAll();
            if (rec.Count > 0)
            {
                obj = new { rec };
                return Ok(obj);
            }
            else
            {
                obj = new { message = "No record of staff types" };
                return BadRequest(obj);
            }
        }



        [Route("Stafftypes")]
        [HttpPost]
        public IActionResult Poststafftype([FromBody] Dictionary<string, object> param)
        {
            EntityConnection con = new EntityConnection("tbl_stafftype");

            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                con.Insert(param);
                return Created("", param);
                //Response.WriteAsync("Record saved successfully!");
            }
            else
            {
                return BadRequest("Error in creating record");
            }
            //return Ok(param);
        }
        //End of POST requests

        [Route("LastVisits")]
        [HttpGet("{id:int}")]
        public IActionResult GetLastVisit(int patientId)
        {
            EntityConnection con = new EntityConnection("tbl_visit");
            Dictionary<string, string> result = new Dictionary<string, string>()
                {
                  {"patientId", patientId + ""}
               };
            if (con.LastVisit(patientId).Count > 0)
            {
                return Ok(con.LastVisit(patientId));
            }
            else
            {
                obj = new { message = patientId + " does not have a previous appointment record yet" };
                return NotFound(obj);
            }

        }

        //Get daily appointments by using date(yyyy-mm-dd) as parameter
        [Route("Appointments")]
        [HttpGet("{visitDate}")]
        public IActionResult GetDailyVisit(string visitDate)
        {
            EntityConnection con = new EntityConnection("tbl_visit");
            Dictionary<string, string> result = new Dictionary<string, string>()
            {
                {"visitDateTime", visitDate }
            };

            if (con.DailyVisit(visitDate).Count > 0)
            {
                return Ok(con.DailyVisit(visitDate));
            }
            else
            {
                string[] arr = new string[0];
                return Ok(arr);
            }
        }

        //Get Doctor's appointment list by assignedTo
        [Route("DoctorsAppointment")]
        [HttpGet("{assignedTo}")]
        public IActionResult GetDoctorAppointment(int assignedTo)
        {
            EntityConnection con = new EntityConnection("tbl_visit");
            Dictionary<string, string> result = new Dictionary<string, string>()
            {
                {"assignedTo", assignedTo.ToString() }
            };

            if (con.DoctorsAppoinmentList(assignedTo).Count > 0)
            {
                return Ok(con.DoctorsAppoinmentList(assignedTo));
            }
            else
            {
                obj = new { message = "No patients assigned yet" };
                return NotFound(obj);
            }

        }


        // Drug Type Post and Get endpoints
        [Route("DrugCategories")]
        [HttpPost]
        public IActionResult PostDrugType([FromBody] Dictionary<string, object> param)
        {
            EntityConnection connection = new EntityConnection("tbl_drugtype");
            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                connection.Insert(param);
                return Created("", param);
                //Response.WriteAsync("Record saved successfully!");
            }
            else
            {
                obj = new { message = "Error in creating record" };
                return BadRequest(obj);
            }
        }


        [Route("DrugCategories")]
        [HttpGet]
        public IActionResult GetDrugType()
        {
            EntityConnection con = new EntityConnection("tbl_drugtype");
            //string result = "{'status':true, 'data':" + EntityConnection.ToJson(con.Select()) + "}";
            List<Dictionary<string, object>> rec = con.Select();

            if (rec != null)
            {
                return Ok(rec);
            }
            else
            {
                return Ok(new string[0]);
            }

        }


        // Drug search
        [Route("SearchDrug")]
        [HttpGet("drugName")]
        public IActionResult SearchDrugs(string drugName)
        {
            EntityConnection connection = new EntityConnection("tbl_druginventory");
            Dictionary<string, object> result = new Dictionary<string, object>()
            {
                {"drugName", drugName}
            };
            if (connection.DrugSearch(drugName).Count > 0)
            {
                obj = connection.DrugSearch(drugName);
                return Ok(obj);
            }
            else
            {
                return Ok(new string[0]);
            }
        }

        // Begin lab request 
        [Route("LabRequests")]
        [HttpGet("{id}")]
        public IActionResult GetLabRequests(int id)
        {
            EntityConnection connection = new EntityConnection("tbl_labtest_request");

            Dictionary<string, string> rec = new Dictionary<string, string>();
            rec.Add("itbId", id + "");

            Dictionary<string, object> result = connection.SelectLabRequest(id);

            if (result.Count > 0)
            {
                obj = result["labTestId"];
                notes = result["testNote"];

                string labTestIds = obj.ToString();
                string labNotes = notes.ToString();

                result.Remove("labTestId");
                result.Remove("testNote");

                // Check if labtestId has values and not ab anempty array
                if (labTestIds.Trim() != "")
                {
                    string[] testIds = labTestIds.Split(',');
                    string[] orQueries = testIds.Select(labtestid => "itbId =" + labtestid).ToArray();

                    if (orQueries != null)
                    {
                        var labTestNames = connection.LabTestNames(orQueries);
                        result.Add("labTestId", labTestNames);
                    }
                }
                else
                {
                    result.Add("labTestId", new string[0]);
                }

                // Check if lab notes is empty
                if (labNotes.Trim() != "")
                {
                    string[] labRequestNotes = labNotes.Split('|');
                    result.Add("testNote", labRequestNotes);
                }
                else
                {
                    result.Add("testNote", new string[0]);
                }

                obj = new { data = result };
                return Ok(obj);
            }
            else
            {
                return Ok(new string[0]);
            }

        }

        [Route("LabRequests")]
        [HttpPost]
        public IActionResult PostLabRequest([FromBody] Dictionary<string, object> param)
        {
            EntityConnection connection = new EntityConnection("tbl_labtest_request");

            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                JArray labTestId = (JArray)param["labTestId"];
                JArray labTestNote = (JArray)param["testNote"];

                param.Remove("labTestId");
                param.Remove("testNote");

                param.Add("labTestId", string.Join(",", labTestId));
                param.Add("testNote", string.Join("|", labTestNote));

                connection.Insert(param);
                return Created("", param);
            }
            else
            {
                obj = new { message = "Error in creating record" };
                return BadRequest(obj);
            }


        }




        //Begin other prescription
        [Route("Xray")]
        [HttpGet]
        public IActionResult GetXray()
        {
            EntityConnection connection = new EntityConnection("tbl_xray_request");
            List<Dictionary<string, object>> result = connection.SelectAll();

            if (result.Count > 0)
            {
                obj = new { data = result };
                return Ok(obj);
            }
            else
            {
                return Ok(new string[0]);
            }

        }

        [Route("Xray")]
        [HttpPost]
        public IActionResult PostXray([FromBody] Dictionary<string, object> param)
        {
            EntityConnection connection = new EntityConnection("tbl_xray_request");

            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                connection.Insert(param);
                return Created("", param);
            }
            else
            {
                obj = new { message = "Error in creating record" };
                return BadRequest(obj);
            }


        }


        //Begin Post and get for referrral made after diagnosis     
        [Route("Referrals")]
        [HttpGet]
        public IActionResult GetReferrals()
        {
            EntityConnection connection = new EntityConnection("tbl_referral");
            List<Dictionary<string, object>> result = connection.SelectAll();

            if (result.Count > 0)
            {
                obj = new { data = result };
                return Ok(obj);
            }
            else
            {
                return Ok(new string[0]);
            }

        }

        [Route("Referrals")]
        [HttpPost]
        public IActionResult PostReferrals([FromBody] Dictionary<string, object> param)
        {
            EntityConnection connection = new EntityConnection("tbl_referral");

            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                connection.Insert(param);
                return Created("", param);
            }
            else
            {
                obj = new { message = "Error in creating record" };
                return BadRequest(obj);
            }

        }


        // Begin Get Lab test request, x-ray request and referrals  by visitId
        [Route("DoctorsNotes")]
        [HttpGet("visitId")]
        public IActionResult GetDoctorsNotes(int visitId)
        {
            // Begin Lab test GET
            EntityConnection connection = new EntityConnection("tbl_labtest_request");
            Dictionary<string, string> rec = new Dictionary<string, string>();
            rec.Add("visitId", visitId + "");

            List labTests = new List<Dictionary<string, object>>();
            Dictionary xray = new Dictionary<string, object>();
            Dictionary referrals = new Dictionary<string, object>();

            Dictionary<string, object> result = connection.GetLabRequestByVisit(visitId);

            if (result.Count > 0)
            {
                obj = result["labTestId"];
                notes = result["testNote"];

                string labTestIds = obj.ToString();
                string labNotes = notes.ToString();

                result.Remove("labTestId");
                result.Remove("testNote");

                string[] testIds;
                string[] testNames;
                string[] testNotes;

                // Check if labtestId has values and not ab anempty array
                if (labTestIds.Trim() != "")
                {
                    testIds = labTestIds.Split(',');
                    string[] orQueries = testIds.Select(labtestid => "itbId =" + labtestid).ToArray();

                    if (orQueries != null)
                    {
                        testNames = connection.LabTestNames(orQueries);
                    }
                }

                // Check if lab notes is empty
                if (labNotes.Trim() != "")
                {
                    testNotes = labNotes.Split('|');
                    result.Add("testNote", labRequestNotes);
                }

                for (int i = 0; i < testIds.Length; i++)
                {
                    var labTest = new Dictionary<string, object>();
                    labTest.Append("id", testIds[i]);
                    labTest.Append("testName", testNames[i]);
                    labTest.Append("testNote", testNotes[i]);
                }
            }
            // End Lab test GET

            // Begin xray GET
            EntityConnection konnect = new EntityConnection("tbl_xray_request");

            Dictionary<string, string> record = new Dictionary<string, string>();
            record.Add("visitId", visitId + "");

            Dictionary<string, object> res = konnect.GetXrayRequestByVisit(visitId);

            if (res.Count > 0)
            {
                xray = res;
            }
            else
            {
                xray = null;
            }
            // End X-ray request GET

            // Begin referrals GET
            EntityConnection conns = new EntityConnection("tbl_referral");

            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("visitId", visitId + "");

            Dictionary<string, object> tempres = conns.GetReferralByVisit(visitId);

            if (tempres.Count > 0)
            {
                referrals = tempres;
            }
            else
            {
                referrals = null;
            }

            // End Referrals GET

            // Insert all result of the above operations independently into seperate dictionaries
            // Dictionary<string, object> Dic1 = new Dictionary<string, object>();
            // Dic1.

            // if (true)
            // {

            // }

            notes = new { data = labTests, xray, referrals };

            return Ok(notes);



        }






        //Begin POST and GET of lab tests 
        // Endpoint for adding and getting all lab test types in the database
        [Route("LabTests")]
        [HttpGet]
        public IActionResult GetLabTest()
        {
            EntityConnection connection = new EntityConnection("tbl_lab_test");
            List<Dictionary<string, object>> result = connection.SelectAll();

            if (result.Count > 0)
            {
                obj = new { data = result };
                return Ok(obj);
            }
            else
            {
                return Ok(new string[0]);
            }

        }

        [Route("LabTests")]
        [HttpPost]
        public IActionResult PostLabTest([FromBody] Dictionary<string, object> param)
        {
            EntityConnection connection = new EntityConnection("tbl_lab_test");

            if (param != null)
            {
                param.Add("createDate", DateTime.Now.ToString());
                connection.Insert(param);
                return Created("", param);
            }
            else
            {
                obj = new { message = "Error in creating record" };
                return BadRequest(obj);
            }


        }


        [Route("Countries")]
        [HttpGet]
        public async Task<IActionResult> GetCountriesAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://restcountries.eu/rest/v2/all");

            var client = _clientFactory.CreateClient();


            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                var resp = await JsonSerializer.DeserializeAsync<List<Dictionary<string, object>>>(responseStream);

                countries = new List<NationalityModel>();

                foreach (var item in resp)
                {
                    var country = new NationalityModel
                    {
                        Name = item["name"].ToString(),
                        Capital = item["capital"].ToString(),
                        Region = item["region"].ToString(),
                        Subregion = item["subregion"].ToString()
                    };
                    countries.Add(country);
                }

                // Insert result inside Nation table
                EntityConnection connection = new EntityConnection("tbl_nationality");
                connection.InsertResult(countries);

                return Ok(countries);

            }
            else
            {
                NotFound();
                //nationalityModel = new List<NationalityModel>();
            }

            return null;


        }




        //End of POST requests







    }
}
