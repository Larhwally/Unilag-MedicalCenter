using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization.Json;
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

            client.ClearHandlers();
            client.AddHandler("application/json", new JsonDeserializer());
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


        // Fetch student Details by making an API call to the Zenoss API
        [System.Obsolete]
        public async Task<StudentModel> GetStudentDetailAsync(string matricNum, string sessionToken)
        {
            RestClient client = new RestClient();
            client = new RestClient(baseURL + "/melon");

            //  get RestSharp to ignore errors in SSL certificates
            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            //JObject reqBody = new JObject("GetStudentProgramDetails", new JObject("MatricNo", matricNum));

            JObject jStaffId = new JObject();
            jStaffId.Add("MatricNo", matricNum);

            JObject jObjectBody = new JObject();
            jObjectBody.Add("GetStudentProgramDetails", jStaffId);

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




    }
}