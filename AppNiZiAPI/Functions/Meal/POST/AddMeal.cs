using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AppNiZiAPI.Models.Repositories;
using AppNiZiAPI.Variables;
using AppNiZiAPI.Security;
using AppNiZiAPI.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using AppNiZiAPI.Models;
using Aliencube.AzureFunctions.Extensions.OpenApi.Attributes;
using System.Net;
using Microsoft.OpenApi.Models;
using Aliencube.AzureFunctions.Extensions.OpenApi.Enums;

namespace AppNiZiAPI.Functions.Meal.POST
{
    public static class AddMeal
    {
        [FunctionName("AddMeal")]
        [OpenApiOperation("AddMeal", "Meal", Summary = "Adds a meal", Description = "Adds a meal to the specified patient", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiResponseBody(HttpStatusCode.OK, "application/json", typeof(string), Summary = Messages.OKUpdate)]
        [OpenApiResponseBody(HttpStatusCode.Unauthorized, "application/json", typeof(string), Summary = Messages.AuthNoAcces)]
        [OpenApiResponseBody(HttpStatusCode.BadRequest, "application/json", typeof(string), Summary = Messages.ErrorMissingValues)]
        [OpenApiRequestBody("application/json", typeof(Models.Meal), Description = "The meal object to be added")]
        [OpenApiParameter("patientId", Description = "The patient who is adding the meal", In = ParameterLocation.Path, Required = true, Type = typeof(int))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function,  "post", Route = (Routes.APIVersion+Routes.AddMeal))] HttpRequest req,
            ILogger log,int patientId)
        {
            #region AuthCheck
            AuthResultModel authResult = await DIContainer.Instance.GetService<IAuthorization>().CheckAuthorization(req, patientId);
            if (!authResult.Result)
                return new StatusCodeResult(authResult.StatusCode);
            #endregion


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            Models.Meal meal = new Models.Meal();
            JsonConvert.PopulateObject(requestBody, meal);
            meal.PatientId = patientId;
            IMealRepository mealRepository = DIContainer.Instance.GetService<IMealRepository>();
            bool succes = mealRepository.AddMeal(meal);

            return succes != false
                ? (ActionResult)new OkObjectResult(succes)
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
