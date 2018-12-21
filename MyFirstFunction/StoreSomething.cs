using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace MyFirstFunction
{
    public static class StoreSomething
    {
        [FunctionName("StoreSomething")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log, [Blob("samples-workitems/test", FileAccess.Write)] out string dataToStore)
        {
            var name = Guid.NewGuid().ToString();
            var data = req.GetQueryNameValuePairs().FirstOrDefault(pair => pair.Key == "data").Value;

            log.Info($"storing {data}");
            dataToStore = data;
            


            return req.CreateResponse(HttpStatusCode.OK, data);
        }
    }
}
