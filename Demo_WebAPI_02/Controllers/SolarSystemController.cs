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
    }
}
