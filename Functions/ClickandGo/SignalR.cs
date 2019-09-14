using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Azure.Documents.Client;
using ClickandGo.Repository;
using ClickandGo.Models;
using Newtonsoft.Json;

namespace ClickandGo
{
    public class SignalR
    {

        [FunctionName("negotiateDriver")]
        public static SignalRConnectionInfo GetSignalRInfoDriver(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
            [SignalRConnectionInfo(HubName = "driver")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo; 
        }

        [FunctionName("negotiateRider")]
        public static SignalRConnectionInfo GetSignalRInfoRider(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
            [SignalRConnectionInfo(HubName = "rider")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }

        [FunctionName("messageDriver")]
        public static async Task<IActionResult> SendMessageRider(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] string message,
            [SignalR(HubName = "driver")] IAsyncCollector<SignalRMessage> signalRMessages)
        {


            try
            {
                Carrier carrier = new Carrier();
                JsonConvert.DeserializeObject(message);
                carrier = JsonConvert.DeserializeObject<Carrier>(message);            


                DocumentDBRepository<Carrier> b = new DocumentDBRepository<Carrier>("clickandgo", "Carrier");

                return new OkObjectResult(carrier);

                await b.CreateItemAsync(carrier);

                await signalRMessages.AddAsync(
                    new SignalRMessage
                    {
                        Target = "trip",
                        Arguments = new[] { message }

                    });                
            }
            catch (Exception e)
            {
                return new OkObjectResult(e); 
               
            }

            return new OkObjectResult("success");
        }



    }
}
