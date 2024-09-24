using Microsoft.AspNetCore.Mvc;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Application.Services;
using PolarisContacts.Domain;

namespace PolarisContacts.ReadService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TelefoneController(ILogger<TelefoneController> logger, ITelefoneService telefoneService) : ControllerBase
    {
        private readonly ILogger<TelefoneController> _logger = logger;
        private readonly ITelefoneService _telefoneService = telefoneService;

        [HttpGet]
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

        [HttpGet]
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
