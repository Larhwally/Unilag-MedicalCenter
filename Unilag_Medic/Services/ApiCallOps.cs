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

    public class ApiCallOps : IApiCallOps
    {
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