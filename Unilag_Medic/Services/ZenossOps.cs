using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization.Json;
using Unilag_Medic.Data;
using Unilag_Medic.Models;


namespace Unilag_Medic.Services
{

    public class ZenossOps : IZenossOps
    {
        private string baseURL = "https://zenoss.unilag.edu.ng/api/v2";

        public string BaseURL { get => baseURL; set => baseURL = value; }

        // Generate token by making a call to Zenoss API
        [System.Obsolete]
        public async Task<ZenosLoginModel> GetSessionTokenAsync()
        {
            RestClient client = new RestClient();
            client = new RestClient(BaseURL + "/user/session");

            //  get RestSharp to ignore errors in SSL certificates
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            JObject jObjectBody = new JObject();
            jObjectBody.Add("email", "lms@unilag.edu.ng");
            jObjectBody.Add("password", "Unilag.1960");
            jObjectBody.Add("api_key", "12d06d0e08eef80daeb2f7b3f53f9e2563b437d48fbd4b3db653ea240c35d674");

            RestRequest restRequest = new RestRequest(Method.POST);

            client.ClearHandlers(); //clear handlers to change response format away from xml

            client.AddHandler("application/json", new JsonDeserializer()); //Make response into a json format
            restRequest.AddHeader("X-Requested-With", "XMLHttpRequest");
            // restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddParameter("application/json", jObjectBody, ParameterType.RequestBody);

            IRestResponse restResponse = await client.ExecuteAsync(restRequest);
            if (restResponse.IsSuccessful)
            {

                var content = JsonConvert.DeserializeObject<Dictionary<string, object>>(restResponse.Content);

                var sessionToken = content["session_token"];

                var tempSession = new ZenosLoginModel
                {
                    session_token = sessionToken.ToString()
                };

                return tempSession;

            }
            else
            {
                string errorMessage = restResponse.ErrorMessage;
            }

            return null;
        }


        // Fetch student Details using MatricNo as parameter by making an API call to the Zenoss API
        [System.Obsolete]
        public async Task<StudentModel> GetStudentDetailAsync(string matricNum, string sessionToken)
        {
            RestClient client = new RestClient();
            client = new RestClient(baseURL + "/melon");

            //  get RestSharp to ignore errors in SSL certificates
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            //JObject reqBody = new JObject("GetStudentProgramDetails", new JObject("MatricNo", matricNum));

            JObject jStudent = new JObject();
            jStudent.Add("MatricNo", matricNum);

            JObject jObjectBody = new JObject();
            jObjectBody.Add("GetStudentProgramDetails", jStudent);

            RestRequest restRequest = new RestRequest(Method.POST);

            client.ClearHandlers();
            client.AddHandler("application/json", new JsonDeserializer());
            restRequest.AddHeader("X-DreamFactory-API-Key", "12d06d0e08eef80daeb2f7b3f53f9e2563b437d48fbd4b3db653ea240c35d674");
            restRequest.AddHeader("X-DreamFactory-Session-Token", sessionToken);
            restRequest.AddParameter("application/json", jObjectBody, ParameterType.RequestBody);


            IRestResponse restResponse = await client.ExecuteAsync(restRequest);
            if (restResponse.IsSuccessful)
            {

                var _content = JsonConvert.DeserializeObject<Dictionary<string, object>>(restResponse.Content);
                var resp = (JObject)_content["StudentProgramResponse"];
                var content = (JObject)resp["student"];

                Dictionary<string, object> studentDictionary = new Dictionary<string, object>();

                studentDictionary.Add("MatricNo", content["MatricNo"]);
                studentDictionary.Add("ProgrammeID", resp["ProgrammeID"]);
                studentDictionary.Add("NameTitle", content["NameTitle"]);
                studentDictionary.Add("FirstName", content["FirstName"]);
                studentDictionary.Add("LastName", content["LastName"]);
                studentDictionary.Add("MiddleName", content["MiddleName"]);
                studentDictionary.Add("Gender", content["Gender"]);
                studentDictionary.Add("Religion", content["Religion"]);
                studentDictionary.Add("DateOfBirth", content["DateOfBirth"]);
                studentDictionary.Add("Nationality", content["Nationality"]);
                studentDictionary.Add("StateOfOrigin", content["StateOfOrigin"]);
                studentDictionary.Add("MaritalStatus", content["MaritalStatus"]);
                studentDictionary.Add("NextOfKinName", content["NextOfKinName"]);
                studentDictionary.Add("AddressOfNextOfKin", content["AddressOfNextOfKin"]);
                studentDictionary.Add("NextOfKinTelephoneNumber", content["NextOfKinTelephoneNumber"]);
                studentDictionary.Add("SponsorName", content["SponsorName"]);
                studentDictionary.Add("SponsorContactAddress", content["SponsorContactAddress"]);
                studentDictionary.Add("SponsorTelephoneNumber", content["SponsorTelephoneNumber"]);
                studentDictionary.Add("Address", content["Address"]);
                studentDictionary.Add("PostalAddress", content["PostalAddress"]);
                studentDictionary.Add("Email", content["Email"]);
                studentDictionary.Add("Telephone", content["Telephone"]);
                studentDictionary.Add("FathersPhone", content["FathersPhone"]);
                studentDictionary.Add("FathersEmail", content["FathersEmail"]);
                studentDictionary.Add("MothersPhone", content["MothersPhone"]);
                studentDictionary.Add("MothersEmail", content["MothersEmail"]);


                var studentResult = new StudentModel
                {
                    MatricNo = studentDictionary["MatricNo"].ToString(),
                    ProgrammeId = studentDictionary["ProgrammeID"].ToString(),
                    NameTitle = studentDictionary["NameTitle"].ToString(),
                    FirstName = studentDictionary["FirstName"].ToString(),
                    LastName = studentDictionary["LastName"].ToString(),
                    MiddleName = studentDictionary["MiddleName"].ToString(),
                    Gender = studentDictionary["Gender"].ToString(),
                    Religion = studentDictionary["Religion"].ToString(),
                    DateOfBirth = studentDictionary["DateOfBirth"].ToString(),
                    Nationality = studentDictionary["Nationality"].ToString(),
                    StateOfOrigin = studentDictionary["StateOfOrigin"].ToString(),
                    MaritalStatus = studentDictionary["MaritalStatus"].ToString(),
                    NextOfKinName = studentDictionary["NextOfKinName"].ToString(),
                    AddressOfNextOfKin = studentDictionary["AddressOfNextOfKin"].ToString(),
                    NextOfKinTelephoneNumber = studentDictionary["NextOfKinTelephoneNumber"].ToString(),
                    SponsorName = studentDictionary["SponsorName"].ToString(),
                    SponsorContactAddress = studentDictionary["SponsorContactAddress"].ToString(),
                    SponsorTelephoneNumber = studentDictionary["SponsorTelephoneNumber"].ToString(),
                    Address = studentDictionary["Address"].ToString(),
                    PostalAddress = studentDictionary["PostalAddress"].ToString(),
                    Email = studentDictionary["Email"].ToString(),
                    Telephone = studentDictionary["Telephone"].ToString(),
                    FathersPhone = studentDictionary["FathersPhone"].ToString(),
                    FathersEmail = studentDictionary["FathersEmail"].ToString(),
                    MothersPhone = studentDictionary["MothersPhone"].ToString(),
                    MothersEmail = studentDictionary["MothersEmail"].ToString()

                };

                return studentResult;

            }
            else
            {
                string errorMessage = restResponse.ErrorMessage;
            }

            return null;

        }


        // Fetch staff Details using StaffID as parameter by making an API call to the Zenoss API
        [System.Obsolete]
        public async Task<StaffModel> GetStaffDetailAsync(string staffId, string sessionToken)
        {
            RestClient client = new RestClient();
            client = new RestClient(baseURL + "/melon");

            //  get RestSharp to ignore errors in SSL certificates
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            //JObject reqBody = new JObject("GetStudentProgramDetails", new JObject("MatricNo", matricNum));

            JObject jStaffId = new JObject();
            jStaffId.Add("StaffID", staffId);

            JObject jObjectBody = new JObject();
            jObjectBody.Add("StaffProfileRequest", jStaffId);

            RestRequest restRequest = new RestRequest(Method.POST);

            client.ClearHandlers();
            client.AddHandler("application/json", new JsonDeserializer());
            restRequest.AddHeader("X-DreamFactory-API-Key", "12d06d0e08eef80daeb2f7b3f53f9e2563b437d48fbd4b3db653ea240c35d674");
            restRequest.AddHeader("X-DreamFactory-Session-Token", sessionToken);
            restRequest.AddParameter("application/json", jObjectBody, ParameterType.RequestBody);



            IRestResponse restResponse = await client.ExecuteAsync(restRequest);
            if (restResponse.IsSuccessful)
            {
                var _content = JsonConvert.DeserializeObject<Dictionary<string, object>>(restResponse.Content);
                var resp = (JArray)_content["StaffProfileResponse"];
                //var content = resp;

                Dictionary<string, object> staffDictionary = new Dictionary<string, object>();

                foreach (var item in resp)
                {
                    staffDictionary.Add("StaffID", item["StaffID"]);
                    staffDictionary.Add("NameTitle", item["NameTitle"]);
                    staffDictionary.Add("FirstName", item["FirstName"]);
                    staffDictionary.Add("LastName", item["LastName"]);
                    staffDictionary.Add("OtherName", item["OtherName"]);
                    staffDictionary.Add("DateOfBirth", item["DateOfBirth"]);
                    staffDictionary.Add("Nationality", item["Nationality"]);
                    staffDictionary.Add("StateOfOrigin", item["StateOfOrigin"]);
                    staffDictionary.Add("MaritalStatus", item["MaritalStatus"]);
                    staffDictionary.Add("Address", item["Address"]);
                    staffDictionary.Add("DateOfFirstAppointment", item["DateOfFirstAppointment"]);
                    staffDictionary.Add("Telephone", item["Telephone"]);
                    staffDictionary.Add("Mobile", item["Mobile"]);
                    staffDictionary.Add("InstitutionEmail", item["InstitutionEmail"]);
                }

                var staffModel = new StaffModel
                {
                    NameTitle = staffDictionary["NameTitle"].ToString(),
                    StaffID = staffDictionary["StaffID"].ToString(),
                    FirstName = staffDictionary["FirstName"].ToString(),
                    LastName = staffDictionary["LastName"].ToString(),
                    OtherName = staffDictionary["OtherName"].ToString(),
                    DateOfBirth = staffDictionary["DateOfBirth"].ToString(),
                    Nationality = staffDictionary["Nationality"].ToString(),
                    StateOfOrigin = staffDictionary["StateOfOrigin"].ToString(),
                    MaritalStatus = staffDictionary["MaritalStatus"].ToString(),
                    Address = staffDictionary["Address"].ToString(),
                    InstitutionEmail = staffDictionary["InstitutionEmail"].ToString(),
                    Telephone = staffDictionary["Telephone"].ToString(),
                    Mobile = staffDictionary["Mobile"].ToString(),
                    DateOfFirstAppointment = staffDictionary["DateOfFirstAppointment"].ToString(),

                };

                // ZenossConnection connection = new ZenossConnection("tbl_staffs");
                // connection.InsertZenossStaff(staffModel);

                return staffModel;

            }
            else
            {
                string errorMessage = restResponse.ErrorMessage;
            }

            return null;

        }


        // Fetch all staff record from Zenoss API and save it on medical center database
        [System.Obsolete]
        public async Task<List<StaffModel>> GetAllStaffAsync(string session_token)
        {
            RestClient client = new RestClient();
            client = new RestClient(baseURL + "/melon");

            //  get RestSharp to ignore errors in SSL certificates
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            int Limit = 100;
            int OffSet = 0;

            JObject param = new JObject();
            param.Add("Limit", Limit);
            param.Add("Offset", OffSet);

            JObject jObjectBody = new JObject();
            jObjectBody.Add("AllStaffProfileRequest", param);

            RestRequest restRequest = new RestRequest(Method.POST);

            client.ClearHandlers();
            client.AddHandler("application/json", new JsonDeserializer());
            restRequest.AddHeader("X-DreamFactory-API-Key", "12d06d0e08eef80daeb2f7b3f53f9e2563b437d48fbd4b3db653ea240c35d674");
            restRequest.AddHeader("X-DreamFactory-Session-Token", session_token);
            restRequest.AddParameter("application/json", jObjectBody, ParameterType.RequestBody);



            IRestResponse restResponse = await client.ExecuteAsync(restRequest);
            if (restResponse.IsSuccessful)
            {
                var _content = JsonConvert.DeserializeObject<Dictionary<string, object>>(restResponse.Content);
                var resp = (JArray)_content["StaffProfileResponse"];
                //var content = resp;

                List<StaffModel> allStaffs = new List<StaffModel>();

                foreach (var item in resp)
                {
                    var staffModel = new StaffModel
                    {
                        NameTitle = item["NameTitle"].ToString(),
                        StaffID = item["StaffID"].ToString(),
                        FirstName = item["FirstName"].ToString(),
                        LastName = item["LastName"].ToString(),
                        OtherName = item["OtherName"].ToString(),
                        DateOfBirth = item["DateOfBirth"].ToString(),
                        Nationality = item["Nationality"].ToString(),
                        StateOfOrigin = item["StateOfOrigin"].ToString(),
                        MaritalStatus = item["MaritalStatus"].ToString(),
                        Address = item["Address"].ToString(),
                        InstitutionEmail = item["InstitutionEmail"].ToString(),
                        Telephone = item["Telephone"].ToString(),
                        Mobile = item["Mobile"].ToString(),
                        DateOfFirstAppointment = item["DateOfFirstAppointment"].ToString(),
                    };

                    allStaffs.Add(staffModel);
                }
                // to check and remove duplicate record from the data
                var uniqueStaff = allStaffs.GroupBy(x => x.StaffID).Select(y => y.First());
                ZenossConnection connection = new ZenossConnection("tbl_zenoss_staffs");
                connection.InsertZenossStaff(uniqueStaff);

                return allStaffs;

            }
            else
            {
                string errorMessage = restResponse.ErrorMessage;
            }

            return null;

        }


        [System.Obsolete]
        // Get a list of all states by country from making an API call to universal-tutorial API
        public async Task<List<string>> GetStateByCountry(string countryName, string token, int nationalityId)
        {
            RestClient client = new RestClient();
            client = new RestClient("https://www.universal-tutorial.com/api/states/" + countryName);

            RestRequest restRequest = new RestRequest(Method.GET);

            client.ClearHandlers();
            client.AddHandler("application/json", new JsonDeserializer());

            restRequest.AddHeader("Authorization", "Bearer " + token);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("api-token", "yA2wHVvLxmnmcJNkF9fhoTC5oTHLRubmATgzYA9szrbLzYwCS8FTirH-7WzAmxGfjXc");

            IRestResponse restResponse = await client.ExecuteAsync(restRequest);
            
            
            List<string> allStates = new List<string>();
            if (restResponse.IsSuccessful)
            {
                var _content = JsonConvert.DeserializeObject<JArray>(restResponse.Content);

                
                foreach (var state in _content)
                {
                    //Dictionary<string, object> stateDic = new Dictionary<string, object>();
                    //stateDic.Add("State", state["state_name"]);
                    var t = state["state_name"].ToString();
                    allStates.Add(t);
                }
                
            }
            else
            {
                string errorMessage = restResponse.ErrorMessage;
            }
            
            EntityConnection connection = new EntityConnection("tbl_state");
            connection.InsertStates(allStates, nationalityId);
            Console.Write("record inserted successful!");
            return allStates;

        }




    }
}