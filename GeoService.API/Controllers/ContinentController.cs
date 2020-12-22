using GeoService.API.Mappers;
using GeoService.API.Models;
using GeoService.Domain.Managers;
using Microsoft.AspNetCore.Mvc;
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

        public ContinentController(ContinentManager manager, ILoggerFactory loggerFactory)
        {
            this.manager = manager;
            this.logger = loggerFactory.AddFile("ControllerLogs.txt").CreateLogger("Continent");
        }

        [HttpPost]
        public ActionResult<ContinentApi> Post([FromBody] ContinentApi continentAPI)
        {
            Domain.Models.Continent continent = ContinentMapper.ContinentInMapper(continentAPI);
            Domain.Models.Continent continentCreated = manager.AddContinent(continent);
            if (continentCreated != null)
            {
                //TODO CustomerId als link
                return CreatedAtAction(nameof(Get), new { id = continentCreated.Id }, continentCreated);
            }
            else
            {
                //TODO return exception
                return NotFound("not found message");
            }
        }

        [HttpGet("{id}")]
        [HttpHead("{id}")]
        public ActionResult<ContinentApi> Get(int id)
        {
            try
            {
                //logger.LogInformation(1001,"Get called");
                //logger.LogCritical("Critical log");
                //logger.LogDebug("Debug log");
                //logger.LogError("error log");
                //logger.LogTrace("trace log");
                return Ok(manager.Find(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}