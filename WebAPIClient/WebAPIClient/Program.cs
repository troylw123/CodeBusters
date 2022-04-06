using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebAPIClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private const string codeBustersUrl = "https://localhost:5001/api/User";
        static async Task Main(string[] args)
        {
            // var content = await client.GetStringAsync(codeBustersUrl);
           
            // Console.WriteLine(content);
            User user = await client.GetFromJsonAsync<User>($"{codeBustersUrl}/2");
            Console.WriteLine($" \nHere is the user info you requested: \n \n" +
            $"Id: {user.Id} \n" +
            $"Name: {user.FullName} \n" +
            $"Email: {user.Email} \n" +
            $"Is Admin?: {user.isAdmin} \n");
        }

        public class User
        {
            [JsonPropertyName("id")]
            public int Id {get; set;}
            [JsonPropertyName("fullName")]
            public string FullName {get; set;}
            [JsonPropertyName("email")]
            public string Email {get; set;}
            [JsonPropertyName("isAdmin")]
            public bool isAdmin {get; set;}
        }
    }
}
