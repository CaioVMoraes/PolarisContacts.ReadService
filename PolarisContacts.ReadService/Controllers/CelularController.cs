using Microsoft.AspNetCore.Mvc;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Domain;

namespace PolarisContacts.ReadService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CelularController(ILogger<CelularController> logger, ICelularService celularService) : ControllerBase
    {
        private readonly ILogger<CelularController> _logger = logger;
        private readonly ICelularService _celularService = celularService;

        [HttpGet]
        public async Task<IEnumerable<Celular>> GetCelularesByIdContato(int idContato)
        {
            try
            {
                return await _celularService.GetCelularesByIdContato(idContato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<Celular> GetCelularById(int id)
        {
            try
            {
                return await _celularService.GetCelularById(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
