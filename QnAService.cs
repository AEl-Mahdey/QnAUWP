using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace QnA
{
    public class QnAService
    {
        string host = "https://YOUR-RESOURCE-NAME.azurewebsites.net/qnamaker";

        // Authorization endpoint key
        // From Publish Page
        string endpoint_key = "YOUR-ENDPOINT-KEY";

        // Management APIs postpend the version to the route
        // From Publish Page, value after POST
        // Example: /knowledgebases/ZZZ15f8c-d01b-4698-a2de-85b0dbf3358c/generateAnswer
        string route = "/knowledgebases/YOUR-KNOWLEDGE-BASE-ID/generateAnswer";

        // JSON format for passing question to service
        public string AswerQuestion(string question)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // POST method
                request.Method = HttpMethod.Post;

                // Add host + service to get full URI
                request.RequestUri = new Uri(host + route);

                // set question
                request.Content = new StringContent(question, Encoding.UTF8, "application/json");

                // set authorization
                request.Headers.Add("Authorization", "EndpointKey " + endpoint_key);

                // Send request to Azure service, get response
                var response = client.SendAsync(request).Result;
                return response.Content.ReadAsStringAsync().Result;
            }

        }

    }
}
