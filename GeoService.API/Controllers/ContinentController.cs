using GeoService.Domain.Managers;
using GeoService.EF.DataAccess;
using Microsoft.AspNetCore.Mvc;
using GeoService.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoService.API.Mappers;
using Microsoft.Extensions.Logging;

namespace GeoService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContinentController : ControllerBase
    {
        private readonly ContinentManager manager;
        private readonly ILogger logger;

        public ContinentController(ContinentManager manager, ILogger<ContinentController> logger)
        {
            this.manager = manager;
            this.logger = logger;
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
                logger.LogInformation("Get called");
                return Ok(manager.Find(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}
