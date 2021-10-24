using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Services.WeatherAPI
{
    public class APICaller
    {
        public static async Task<APIResponse> Get(string url, string authId = null)
        {
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrWhiteSpace(authId))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", authId);

                var request = await client.GetAsync(url);
                if (request.IsSuccessStatusCode)
                {
                    return new APIResponse { Response = await request.Content.ReadAsStringAsync() };
                }
                else
                    return new APIResponse { ErrorMessage = request.ReasonPhrase };
            }
        }
    }
}
