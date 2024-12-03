using Microsoft.AspNetCore.Mvc;
using PolarisContacts.ReadService.Application.Interfaces.Services;
using PolarisContacts.ReadService.Application.Services;
using PolarisContacts.ReadService.Domain;

namespace PolarisContacts.ReadService.Controllers
{
    [ApiController]
    [Route("Read/[controller]")]
    public class TelefoneController(ILogger<TelefoneController> logger, ITelefoneService telefoneService) : ControllerBase
    {
        private readonly ILogger<TelefoneController> _logger = logger;
        private readonly ITelefoneService _telefoneService = telefoneService;

        [HttpGet("GetTelefonesByIdContato/{idContato}")]
        public async Task<IEnumerable<Telefone>> GetTelefonesByIdContato(int idContato)
        {
            try
            {
                return await _telefoneService.GetTelefonesByIdContato(idContato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetTelefoneById/{id}")]
        public async Task<Telefone> GetTelefoneById(int id)
        {
            try
            {
                return await _telefoneService.GetTelefoneById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
