using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace azurefunctions
{
    public static class exchange_rate_calc
    {
        [FunctionName("exchange_rate_calc")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string pesos = req.Query["pesos"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            pesos = pesos ?? data?.pesos;

            
            int numero = Int32.Parse(pesos);
            
            var dollars = numero / 44.95 ;

            return pesos != null

            ? (ActionResult)new OkObjectResult($"Hello, {pesos} pesos(ARG) equals {dollars} dollars(USD) ")
            : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
