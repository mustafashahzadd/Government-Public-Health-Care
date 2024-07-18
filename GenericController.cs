using AutoMapper;
using Azure;
using Azure.Core;
using Governement_Public_Health_Care.DB_Context;
using Governement_Public_Health_Care.DTO;
using Governement_Public_Health_Care.LogErrors;
using Governement_Public_Health_Care.Models;
using Governement_Public_Health_Care.NewFolder;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using RestSharp.Authenticators;
using System.Security.Cryptography.Xml;
using System.Transactions;

namespace Governement_Public_Health_Care
{

    [Route("api/[controller]")]
    [ApiController]
    public class DisceaseRegistryController : Controller
    {
        private readonly IGenericInterface<DiseaseRegistry, int> DiseaseRegistryInterface;
        private readonly ErrorsFile errorsFile;
        private readonly IMapper mapper;
        private readonly HttpClient httpClient;
        private readonly HealthCareContext healthCareContext;
        private readonly SharedDataService sharedDataService;


        public DisceaseRegistryController(ErrorsFile errorsFile, IMapper mapper,
       IGenericInterface<DiseaseRegistry, int> DiseaseRegistryInterface,
       HttpClient httpClient, HealthCareContext healthCareContext,
       SharedDataService sharedDataService)
        {
            this.errorsFile = errorsFile;
            this.mapper = mapper;
            this.DiseaseRegistryInterface = DiseaseRegistryInterface;
            this.httpClient = httpClient;
            this.healthCareContext = healthCareContext;
            this.sharedDataService = sharedDataService;
        }


        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]

        public async Task<IActionResult> CreateDiseaseRegistry([FromBody] DiseaseRegistryDTO diseaseRegistryDTO)
        {
            try
            {
                sharedDataService.ApiCallReceived = true;
                if (diseaseRegistryDTO == null)
                {
                    return BadRequest("Nothing Found!");
                }
                if (!ModelState.IsValid)
                {

                    return BadRequest("Model State is not valid");
                }
                var Entity = mapper.Map<DiseaseRegistry>(diseaseRegistryDTO);
                bool result = await DiseaseRegistryInterface.Create(Entity);
                if (!result)
                {
                    return BadRequest("UnSuccessful in creating!");
                }
                return StatusCode(200, "Successfully Created");
            }
            catch (Exception ex)
            {
                errorsFile.ErrorsDetail(ex.Message + " - " + ex.InnerException?.Message);
                return BadRequest(ex.Message);
            }



        }


        [HttpGet("GetDoctors")]

        //[HttpGet("{DiseaseRegisteryID}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]

        public async Task<IActionResult> GetDoctors()
        {
            try
            {
                sharedDataService.ApiCallReceived = true;

                HttpResponseMessage GetDetail = await httpClient.GetAsync("https://localhost:7004/api/Doctor/");
                GetDetail.EnsureSuccessStatusCode();
                string responseBody = await GetDetail.Content.ReadAsStringAsync();
                return Ok(responseBody);
            }
            catch (Exception ex)
            {
                errorsFile.ErrorsDetail(ex.Message + " - " + ex.InnerException?.Message);
                return BadRequest(ex.Message);
            }



        }


        [HttpGet("{DoctorID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDoctorID(int DoctorID)
        {
            try
            {
                sharedDataService.ApiCallReceived = true;

                if (DoctorID < 0)
                {
                    return BadRequest("Invalid Entry!");
                }
                // Request jo Doctor ke api url ko link karde with host
                var client = new RestClient("https://localhost:7004/api/Doctor/");
                var request = new RestRequest("{id}", Method.Get);
                request.AddUrlSegment("id", DoctorID);

                // Now execute the request asynchronously and await the response
                var response = await client.ExecuteAsync(request);

                // Check success of response
                if (response.IsSuccessful)
                {
                    // Return the content of the response
                    return Ok(response.Content);
                }
                else
                {
                    // Handle error, return an appropriate error response
                    return StatusCode((int)response.StatusCode, response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                errorsFile.ErrorsDetail(ex.Message + " - " + ex.InnerException?.Message);
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{UtilityID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDoctor(int UtilityID)
        {
            if (UtilityID <= 0)
            {
                return BadRequest("Invalid Entry");
            }

            try
            {
                var client = new RestClient("https://localhost:7004");
                var request = new RestRequest($"api/PatientUtilityBill/{UtilityID}", Method.Delete); // Correct method and route
                request.AddHeader("Content-Type", "application/json");

                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    return Ok($"{UtilityID} is successfully deleted!");
                }
                else
                {
                    errorsFile.ErrorsDetail($"Failed to delete doctor with ID: {UtilityID} - {response.ErrorMessage}");
                    return StatusCode((int)response.StatusCode, response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                errorsFile.ErrorsDetail($"Exception when attempting to delete doctor with ID: {UtilityID} - {ex.Message}");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }





        //[HttpPost("TransferDiseaseRegistry")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> TransferDiseaseRegistry([FromBody] int diseaseRegistryID)
        //{
        //    try
        //    {


        //        var registry = await DiseaseRegistryInterface.GetByID(diseaseRegistryID);
        //        if (registry == null)
        //        {
        //            errorsFile.ErrorsDetail($"No data found for ID: {diseaseRegistryID}.");
        //            return BadRequest("No data found for this ID");
        //        }


        //        //Instantiate the transformation service
        //  //k      var TransformationService = new PatientTransformation(errorsFile, healthCareContext);
        //        var registryDto = mapper.Map<DiseaseRegistryDTO>(registry);
        //        //var transformedDTO = await TransformationService.TransformingData(registryDto);

        //        if (transformedDTO == null)
        //        {
        //            errorsFile.ErrorsDetail($"Transformation failed for ID: {diseaseRegistryID}.");
        //            return BadRequest("Transformation failed.");
        //        }


        //        var client = new RestClient("https://localhost:7004");
        //        var request = new RestRequest("api/Doctor", Method.Post);
        //        request.AddHeader("Content-Type", "application/json");

        //        request.AddJsonBody(transformedDTO);
        //        //If you continue to experience issues, consider the

        //        var response = await client.ExecuteAsync(request);

        //        if (!response.IsSuccessful)
        //        {
        //            // Log detailed error
        //            string errorDetails = $"External API call failed for ID: {diseaseRegistryID}. " +
        //                                  $"Status Code: {response.StatusCode}, " +
        //                                  $"Status Description: '{response.StatusDescription}', " +
        //                                  $"Response Content: '{response.Content}', " +
        //                                  $"Error Message: '{response.ErrorMessage}', " +
        //                                  $"Exception: {response.ErrorException}";
        //            errorsFile.ErrorsDetail(errorDetails);

        //            // Create a detailed error object
        //            var detailedError = new
        //            {
        //                message = "There was an error processing your request.",
        //                detailedMessage = $"The external service failed to process the record for ID: {diseaseRegistryID}.",
        //                externalApiResponse = response.Content,
        //                exceptionDetails = response.ErrorException?.ToString()
        //            };

        //            // Return detailed error information in the response body
        //            return StatusCode((int)response.StatusCode, detailedError);
        //        }



        //        errorsFile.ErrorsDetail($"External API call successful for ID: {diseaseRegistryID}.");
        //        return Ok(response.Content);
        //    }
        //    catch (Exception ex)
        //    {
        //        errorsFile.ErrorsDetail("An error occurred during the TransferDiseaseRegistry process.", ex);
        //        return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
        //    }
        //}

        [HttpPatch]
        [Route("UpdateSpecific")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSpecific([FromBody] DiseaseRegistryDTO diseaseRegistryDTO)
        {
            if (diseaseRegistryDTO == null)
            {
                return BadRequest("The body is empty!");
            }

            try
            {
                var entity = mapper.Map<DiseaseRegistry>(diseaseRegistryDTO);
                var result = await DiseaseRegistryInterface.Update(entity); // Renamed to 'Update'

                if (!result)
                {
                    return BadRequest("Unsuccessful in updating!");
                }

                return Ok($"{diseaseRegistryDTO.Id} successfully updated");
            }
            catch (Exception ex)
            {
                errorsFile.ErrorsDetail(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

    }
}






















