using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ServiceBus.Messaging;

namespace MyFirstFunction.QueueWriter
{
    public static class QueueWriter
    {
        [FunctionName("QueueWriter")]
        [return: ServiceBus("goroundandround", AccessRights.Send, Connection = "MyConnectionKey")]
        public static async Task<string> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            return await req.Content.ReadAsStringAsync();
        }
    }
}
