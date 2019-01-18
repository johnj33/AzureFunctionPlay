using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ServiceBus.Messaging;

namespace MyFirstFunction
{
    public static class ReadFromQueue
    {
        [FunctionName("ReadFromQueue")]
        public static void Run([ServiceBusTrigger("GoRoundAndRound", AccessRights.Manage, Connection = "MyConnectionKey")]string myQueueItem, TraceWriter log)
        {
            log.Info($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
