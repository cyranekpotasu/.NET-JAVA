using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lab03
{
    public static class PersonFetcher
    {
        private static readonly string randomPersonUrl = "https://randomuser.me/api/?format=json";

        public static async Task<JToken> FetchPerson(HttpClient client = null)
        {
            client = client ?? new HttpClient();
            var responseJson = await client.GetStringAsync(randomPersonUrl);
            client.Dispose();
            return JObject.Parse(responseJson)["results"][0];
        }
    }
}
