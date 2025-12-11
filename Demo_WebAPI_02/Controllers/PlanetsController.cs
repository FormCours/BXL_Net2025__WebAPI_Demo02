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
        [HttpPost]
        public IActionResult AddPlanet(PlanetRequestDto planetRequestDto)
        {
            Planet planetToAdd = new Planet()
            {
                Name = planetRequestDto.Name,
                DiscoveryDate = planetRequestDto.DiscoveryDate,
                Desc = planetRequestDto.Description,
                NbMoon = planetRequestDto.NbMoon,
                SolarSystemId = planetRequestDto.StelarSystemId,
                Id = 0
            };

            Planet result =_planetRepository.Insert(planetToAdd);

            return CreatedAtAction(nameof(GetAllPlanets), new PlanetResponseDto()
            {
              Id=result.Id,
              Name=result.Name,
              DiscoveryDate=result.DiscoveryDate,
              Description=result.Desc,
              NbMoon=result.NbMoon,
              StelarSystemId=result.SolarSystemId,
            });
        }
    }
}
