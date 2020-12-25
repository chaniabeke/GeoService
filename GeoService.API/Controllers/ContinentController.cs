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
    [Route("api/[controller]")]
    public class ContinentController : ControllerBase
    {
        private readonly ContinentManager manager;
        private readonly ILogger logger;
        private string hostUrl;

        public ContinentController(IConfiguration iconfiguration, ContinentManager manager, ILoggerFactory loggerFactory)
        {
            hostUrl = iconfiguration.GetValue<string>("profiles:GeoService.Api:applicationUrl");
            this.manager = manager;
            logger = loggerFactory.AddFile("ControllerLogs.txt").CreateLogger("Continent");
        }

        [HttpPost]
        public ActionResult<ContinentInApi> PostContinent([FromBody] ContinentInApi continentAPI)
        {
            logger.LogInformation($"Post api/continent/ called");
            try
            {
                Domain.Models.Continent continent = ContinentMapper.ContinentInMapper(continentAPI);
                Domain.Models.Continent continentCreated = manager.AddContinent(continent);
                if (continentCreated != null)
                {
                    //TODO RETURN WITH CORRECT CONTINENT ID
                    ContinentOutApi continentOut = ContinentMapper.ContinentOutMapper(hostUrl, continentCreated);
                    return CreatedAtAction(nameof(GetContinent), new { id = continentOut.Id }, continentOut);
                }
                else
                {
                    logger.LogError("error");
                    return NotFound("not found message");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [HttpHead("{id}")]
        public ActionResult<ContinentOutApi> GetContinent(int id)
        {
            logger.LogInformation(id, $"Get api/continent/{id} called");
            try
            {
                Domain.Models.Continent continent = manager.Find(id);
                if (continent != null)
                {
                    ContinentOutApi continentOut = ContinentMapper.ContinentOutMapper(hostUrl, continent);
                    return Ok(continentOut);
                }
                else
                {
                    logger.LogError($"Continent with {id} is not found");
                    return NotFound($"Continent with {id} is not found");
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