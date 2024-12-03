using Microsoft.AspNetCore.Mvc;
using PolarisContacts.ReadService.Application.Interfaces.Services;
using PolarisContacts.ReadService.Domain;

namespace PolarisContacts.ReadService.Controllers
{
    [ApiController]
    [Route("Read/[controller]")]
    public class CelularController(ILogger<CelularController> logger, ICelularService celularService) : ControllerBase
    {
        private readonly ILogger<CelularController> _logger = logger;
        private readonly ICelularService _celularService = celularService;

        [HttpGet("GetCelularesByIdContato/{idContato}")]
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

        [HttpGet("GetCelularById/{id}")]
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
