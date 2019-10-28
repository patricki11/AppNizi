﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AppNiZiAPI.Variables;
using AppNiZiAPI.Models.Repositories;
using AppNiZiAPI.Models;
using Microsoft.Extensions.DependencyInjection;
using AppNiZiAPI.Infrastructure;

namespace AppNiZiAPI
{
    public static class GetConsumptionById
    {
        [FunctionName("GetConsumptionById")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = (Routes.APIVersion + Routes.Consumption))] HttpRequest req,
            ILogger log, string consumptionId)
        {
            log.LogDebug($"Triggered '" + typeof(GetConsumptionById).Name + "' with parameter: '" + consumptionId + "'");

            if (!int.TryParse(consumptionId, out int id)) return new BadRequestObjectResult(Messages.ErrorIncorrectId);

            IConsumptionRepository consumptionRepository = DIContainer.Instance.GetService<IConsumptionRepository>();
            Consumption consumption = consumptionRepository.GetConsumptionByConsumptionId(id);
            
            var consumptionJson = JsonConvert.SerializeObject(consumption);
            return consumptionJson != null && consumption.Id != 0
                ? (ActionResult)new OkObjectResult(consumptionJson)
                : new BadRequestObjectResult(Messages.ErrorIncorrectId);
        }
    }
}
