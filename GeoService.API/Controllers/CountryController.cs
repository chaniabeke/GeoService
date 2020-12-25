using GeoService.API.Mappers;
using GeoService.API.Models;
using GeoService.Domain.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace GeoService.API.Controllers
{
    [ApiController]
    [Route("api/continent/")]
    public class CountryController : ControllerBase
    {
        private readonly ContinentManager continentManager;
        private readonly CountryManager countryManager;
        private readonly ILogger logger;
        private string hostUrl;

        public CountryController(IConfiguration iconfiguration, ContinentManager continentManager,
            CountryManager countryManager, ILoggerFactory loggerFactory)
        {
            hostUrl = iconfiguration.GetValue<string>("profiles:GeoService.Api:applicationUrl");
            this.continentManager = continentManager;
            this.countryManager = countryManager;
            logger = loggerFactory.AddFile("ControllerLogs.txt").CreateLogger("Country");
        }


        [HttpPost("{continentId}/country")]
        public ActionResult<CountryInApi> PostCountry([FromBody] CountryInApi countryInApi)
        {
            logger.LogInformation($"Post api/continent/ called");
            try
            {
                Domain.Models.Country country = CountryMapper.CountryInMapper(continentManager, countryInApi);
                Domain.Models.Country countryCreated = countryManager.AddCountry(country);
                if (countryCreated != null)
                {
                    CountryOutApi countryOut = CountryMapper.CountryOutMapper(hostUrl, countryCreated);
                    return CreatedAtAction(nameof(GetCountry), new { continentId = countryCreated.Continent.Id, 
                        countryId = countryCreated.Id }, countryOut);
                }
                else
                {
                    logger.LogError("Country can not be null.");
                    return NotFound("Country can not be null.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{continentId}/country/{countryId}")]
        [HttpHead("{continentId}/country/{countryId}")]
        public ActionResult<CountryOutApi>GetCountry(int continentId, int countryId)
        {
            logger.LogInformation(countryId, $"Get api/continent/{continentId}/country/{countryId} called");
            try
            {
                //TODO MANAGER - check if country is from continent
                Domain.Models.Country country = countryManager.Find(continentId, countryId);
                if (country != null)
                {
                    CountryOutApi countryOut = CountryMapper.CountryOutMapper(hostUrl, country);
                    return Ok(countryOut);
                }
                else
                {
                    logger.LogError($"Country with {countryId} is not found");
                    return NotFound($"Country with {countryId} is not found");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.InnerException.Message);
                return NotFound(ex.Message);
            }
        }
    }
}