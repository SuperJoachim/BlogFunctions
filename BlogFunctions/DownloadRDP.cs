using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace BlogFunctions
{
    public static class DownloadRDP
    {
        [FunctionName("DownloadRDP")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            IDictionary<string, string> queryParams = req.GetQueryNameValuePairs()
            .ToDictionary(x => x.Key, x => x.Value);

            string content = "full address:s:" + queryParams["server"];
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(content);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-rdp");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = queryParams["server"] + ".rdp"
            };

            return response;
        }
    }
}
