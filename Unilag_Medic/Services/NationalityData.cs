using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Unilag_Medic.Models;

namespace Unilag_Medic.Services
{
    public class NationalityData
    {
        private IHttpClientFactory _clientFactory;

        public NationalityModel nationalityModel;

        public string errorString;

        protected async Task OnInitializedAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://restcountries.eu/rest/v2/all");

            var client = _clientFactory.CreateClient();

            HttpResponseMessage responseMessage = await client.SendAsync(request);

            if (responseMessage.IsSuccessStatusCode)
            {
                nationalityModel = await responseMessage.Content.ReadFromJsonAsync<NationalityModel>();
                errorString = null;
            }
            else
            {
                errorString = $"There was an error loading nationalities: {responseMessage.ReasonPhrase}";
            }
        }


    }
}