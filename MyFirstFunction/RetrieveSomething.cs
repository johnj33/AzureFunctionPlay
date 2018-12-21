using System.IO;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace MyFirstFunction
{
    public static class RetrieveSomething
    {
        [FunctionName("RetrieveSomething")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log, [Blob("samples-workitems/test", FileAccess.Read)] string dataToStore)
        {
            return req.CreateResponse(HttpStatusCode.OK, dataToStore);
        }
    }
}
