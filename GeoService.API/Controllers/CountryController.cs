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
        public ActionResult<CountryInApi> PostCountry(int continentId, [FromBody] CountryInApi countryInApi)
        {
            logger.LogInformation($"Post api/continent/{continentId}/country/ called");
            try
            {
                Domain.Models.Country country = CountryMapper.CountryInMapper(continentManager, countryInApi);
                Domain.Models.Country countryCreated = countryManager.AddCountry(country);
                if (countryCreated != null)
                {
                    CountryOutApi countryOut = CountryMapper.CountryOutMapper(hostUrl, countryCreated);
                    return CreatedAtAction(nameof(GetCountry), new
                    {
                        continentId = countryCreated.Continent.Id,
                        countryId = countryCreated.Id
                    }, countryOut);
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
        public ActionResult<CountryOutApi> GetCountry(int continentId, int countryId)
        {
            logger.LogInformation(countryId, $"Get api/continent/{continentId}/country/{countryId} called");
            try
            {
                if (continentManager.Find(continentId) != null && countryManager.Find(countryId) != null
                    && countryManager.Find(countryId).Continent.Id == continentId)
                {
                    Domain.Models.Country country = countryManager.Find(continentId, countryId);
                    CountryOutApi countryOut = CountryMapper.CountryOutMapper(hostUrl, country);
                    return Ok(countryOut);
                }
                else
                {
                    logger.LogError($"Country with id{countryId} is not found");
                    return NotFound($"Country with id{countryId} is not found");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{continentId}/country/{countryId}")]
        public IActionResult PutCountry(int continentId, int countryId, [FromBody] CountryInApi countryInApi)
        {
            logger.LogInformation($"Post api/continent/{continentId}/country/{countryId} called");
            if (countryInApi == null) return BadRequest();
            try
            {
                if (countryManager.Find(countryId) == null)
                {
                    Domain.Models.Country country = CountryMapper.CountryInMapper(continentManager, countryInApi);
                    Domain.Models.Country countryCreated = countryManager.AddCountry(country);
                    CountryOutApi countryOut = CountryMapper.CountryOutMapper(hostUrl, countryCreated);
                    return CreatedAtAction(nameof(GetCountry), new { continentId = countryCreated.Continent.Id, countryId = countryCreated.Id }, countryOut);
                }
                Domain.Models.Country countryUpdate = CountryMapper.CountryInMapper(continentManager, countryInApi);
                countryManager.UpdateCountry(countryId, countryUpdate, countryUpdate.Continent.Id);
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{continentId}/country/{countryId}")]
        public IActionResult DeleteCountry(int continentId, int countryId)
        {
            logger.LogInformation($"delete api/continent/{continentId}/country/{countryId} called");
            try
            {
                if (continentManager.Find(continentId) != null && countryManager.Find(countryId) != null
                   && countryManager.Find(countryId).Continent.Id == continentId)
                {
                    countryManager.RemoveCountry(countryId);
                    return NoContent();
                }
                else
                {
                    logger.LogError($"Country with id{countryId} is not found");
                    return NotFound($"Country with id{countryId} is not found");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}