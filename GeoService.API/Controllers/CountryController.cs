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
        private readonly CountryManager manager;
        private readonly ILogger logger;
        private string hostUrl;

        public CountryController(IConfiguration iconfiguration, CountryManager manager, ILoggerFactory loggerFactory)
        {
            hostUrl = iconfiguration.GetValue<string>("profiles:GeoService.Api:applicationUrl");
            this.manager = manager;
            logger = loggerFactory.AddFile("ControllerLogs.txt").CreateLogger("Country");
        }

        //[HttpPost]
        //public ActionResult<ContinentInApi> Post([FromBody] ContinentInApi continentAPI)
        //{
        //    logger.LogInformation($"Post api/continent/ called");
        //    try
        //    {
        //        Domain.Models.Continent continent = ContinentMapper.ContinentInMapper(continentAPI);
        //        Domain.Models.Continent continentCreated = manager.AddContinent(continent);
        //        if (continentCreated != null)
        //        {
        //            ContinentOutApi continentOut = ContinentMapper.ContinentOutMapper(hostUrl, continentCreated);
        //            return CreatedAtAction(nameof(Get), new { id = continentOut.Id }, continentOut);
        //        }
        //        else
        //        {
        //            logger.LogError("error");
        //            return NotFound("not found message");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //        return NotFound(ex.Message);
        //    }
        //}

        [HttpGet("{continentId}/country/{countryId}")]
        [HttpHead("{continentId}/country/{countryId}")]
        public ActionResult<CountryOutApi> GetCountry(int continentId, int countryId)
        {
            logger.LogInformation(countryId, $"Get api/continent/{continentId}/country/{countryId} called");
            try
            {
                //TODO check if country is from continent
                Domain.Models.Country country = manager.Find(continentId, countryId);
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