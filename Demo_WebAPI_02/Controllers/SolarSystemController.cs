using Demo_WebAPI_02.DTO;
using Demo_WebAPI_02.Models;
using Demo_WebAPI_02.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo_WebAPI_02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolarSystemController : ControllerBase
    {
        private readonly SolarSystemRepository _solarSystemRepository;

        public SolarSystemController(SolarSystemRepository solarSystemRepository)
        {
            // Le constructeur aura le repository via "injection de dépendance"
            // -> Pour le moment : C'est magique. Bisous.
            _solarSystemRepository = solarSystemRepository;
        }

        [HttpGet("count")]
        public IActionResult ObtainsNumberOfSystemSolar()
        {
            // Utilisation des service/repository pour résoudre la demande
            int nbSystem = _solarSystemRepository.GetCount();

            // Envoi de la réponse adapté au client (Réponse simple - sans DTO)
            return Ok(new
            {
                VladiDit = $"Nombre de systeme trouvé : {nbSystem}"
            });
        }


        [HttpGet("{id}")]
        public IActionResult GetSolarSystemById(int id)
        {
            // Utilisation des service/repository pour résoudre la demande
            SolarSystem? solarSystem = _solarSystemRepository.GetById(id);

            if (solarSystem is null)
            {
                return NotFound($"Solar system with id {id} not found");
            }

            // Envoi de la réponse adapté au client
            return Ok(new SolarSystemResponseDto
            {
                Id = solarSystem.Id,
                Name = solarSystem.Name,
                NbPlanets = solarSystem.Planets.Count(),
                NbStars = solarSystem.Stars.Count(),
            });     
        }


        [HttpGet]
        public IActionResult GetAllSolarSystem()
        {
            // Utilisation des service/repository pour résoudre la demande
            IEnumerable<SolarSystem> systems = _solarSystemRepository.GetAll();

            // Envoi de la réponse adapté au client
            return Ok(systems.Select(s => new SolarSystemResponseAllDto()
                {
                 Id = s.Id, Name = s.Name
            }));
        }

        [HttpPost]
        public IActionResult InsertSolarSystem(SolarSystemRequestDto data)
        {
            // Element à ajouter (DTO -> Model)
            SolarSystem systemSolarToAdd = new SolarSystem()
            {
                Name = data.Name,
            };

            // Utilisation du service pour ajouter l'élément en DB
            SolarSystem result = _solarSystemRepository.Insert(systemSolarToAdd);

            // Objet a renvoyer au client (Model -> DTO)
            SolarSystemResponseDto dto = new SolarSystemResponseDto()
            {
                Id = result.Id,
                Name = result.Name,
            };

            // Envoi d'une réponse au client avec :
            //  - Un code de statut : 201 - CREATE
            //  - La localisation (Champs "location" dans le header de la réponse)
            //  - Les données (format DTO)
            return CreatedAtAction(nameof(GetSolarSystemById), new {id = result.Id}, dto);
        }


    }
}
