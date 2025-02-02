﻿using GeoService.API.Mappers;
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
        #region Fields

        private readonly ContinentManager continentManager;
        private readonly CountryManager countryManager;
        private readonly ILogger logger;
        private string hostUrl;

        #endregion Fields

        #region Constructor

        public ContinentController(IConfiguration iconfiguration, ContinentManager continentManager,
            CountryManager countryManager, ILoggerFactory loggerFactory)
        {
            hostUrl = iconfiguration.GetValue<string>("profiles:GeoService.Api:applicationUrl");
            this.continentManager = continentManager;
            this.countryManager = countryManager;
            logger = loggerFactory.AddFile("ControllerLogs.txt").CreateLogger("Continent");
        }

        #endregion Constructor

        #region Methods

        [HttpPost]
        public ActionResult<ContinentInApi> PostContinent([FromBody] ContinentInApi continentAPI)
        {
            logger.LogInformation($"Post api/continent/ called");
            try
            {
                if (continentAPI == null) return BadRequest();
                Domain.Models.Continent continent = ContinentMapper.ContinentInMapper(countryManager, continentAPI);
                Domain.Models.Continent continentCreated = continentManager.AddContinent(continent);
                if (continentCreated != null)
                {
                    ContinentOutApi continentOut = ContinentMapper.ContinentOutMapper(hostUrl, continentCreated);
                    return CreatedAtAction(nameof(GetContinent), new { id = continentOut.Id }, continentOut);
                }
                else
                {
                    logger.LogError("Continent can not be null.");
                    return NotFound("Continent can not be null.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutContinent(int id, [FromBody] ContinentInApi continentIn)
        {
            logger.LogInformation($"Put api/continent/ called");
            if (continentIn == null) return BadRequest();
            try
            {
                if (continentManager.Find(id) == null)
                {
                    Domain.Models.Continent continent = ContinentMapper.ContinentInMapper(countryManager, continentIn);
                    Domain.Models.Continent continentCreated = continentManager.AddContinent(continent);
                    ContinentOutApi continentOut = ContinentMapper.ContinentOutMapper(hostUrl, continentCreated);
                    return CreatedAtAction(nameof(GetContinent), new { id = continentOut.Id }, continentOut);
                }
                Domain.Models.Continent continentUpdate = ContinentMapper.ContinentInMapper(countryManager, continentIn);
                continentUpdate.Id = id;
                continentManager.UpdateContinent(id, continentUpdate);
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [HttpHead("{id}")]
        public ActionResult<ContinentOutApi> GetContinent(int id)
        {
            logger.LogInformation(id, $"Get api/continent/{id} called");
            try
            {
                Domain.Models.Continent continent = continentManager.Find(id);
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
                logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContinent(int id)
        {
            logger.LogInformation(id, $"Delete api/continent/{id} called");
            if (continentManager.Find(id) == null)
            {
                return NotFound();
            }
            try
            {
                continentManager.RemoveContinent(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        #endregion Methods
    }
}