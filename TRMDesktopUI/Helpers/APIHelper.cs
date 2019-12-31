using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TRMDesktopUI.Models;

// 11 Create Class; handles all api call interactions, the views will call this calls for services needed 
namespace TRMDesktopUI.Helpers
{
    // 11 Extract interface to be able to use it in the dependency injection system
    public class APIHelper : IAPIHelper
    {
        private HttpClient apiClient;

        // 11 As soon as the APIHelper is called it'll initialize and perpare the
        // HttpClient property for usage with this constructor; one HttpClient for the duration of application
        public APIHelper()
        {
            InitializeClient();
        }
        private void InitializeClient()
        {
            // 11 Add reference to System.Configuration to be able to retrieve value from App Settings
            string api = ConfigurationManager.AppSettings["api"];
            apiClient = new HttpClient();
            // 11 Set the base address from the App Settings to be able use a relative path in the Authenitcate call
            apiClient.BaseAddress = new Uri(api);
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // 11 Calls out to API and returns token information
        public async Task<AuthenticatedUser> Authenticate(string username, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            });
            // 11 Use relative path to aquire token
            using (HttpResponseMessage response = await apiClient.PostAsync("/Token", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    // 11 Add nuget package aspnet.webapi.client to be able to use the ReadAsAsync method
                    var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
