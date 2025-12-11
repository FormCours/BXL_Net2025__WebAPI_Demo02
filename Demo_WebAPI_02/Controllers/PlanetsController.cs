using Demo_WebAPI_02.DTO;
using Demo_WebAPI_02.Models;
using Demo_WebAPI_02.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Demo_WebAPI_02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanetsController : ControllerBase
    {
        private readonly PlanetRepository _planetRepository;

        public PlanetsController(PlanetRepository planetRepository)
        {
            _planetRepository = planetRepository;
        }

        [HttpGet]
        public IActionResult GetAllPlanets()
        {
            IEnumerable<Planet> planets = _planetRepository.GetAll();

            return Ok(planets.Select(p =>  new PlanetsResponseAllDto()
            {
                Id = p.Id,
                Name = p.Name
            }));
        }
    }
}
