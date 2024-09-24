using Microsoft.AspNetCore.Mvc;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Application.Services;
using PolarisContacts.Domain;

namespace PolarisContacts.ReadService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatoController(ILogger<ContatoController> logger, IContatoService contatoService) : ControllerBase
    {
        private readonly ILogger<ContatoController> _logger = logger;
        private readonly IContatoService _contatoService = contatoService;

        [HttpGet]
        public async Task<IEnumerable<Contato>> GetAllContatosByIdUsuario(int idUsuario)
        {
            try
            {
                return await _contatoService.GetAllContatosByIdUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<Contato> GetContatoByIdAsync(int idContato)
        {
            try
            {
                return await _contatoService.GetContatoByIdAsync(idContato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IEnumerable<Contato>> SearchContatosByIdUsuario(int idUsuario, string searchTerm)
        {
            try
            {
                return await _contatoService.SearchContatosByIdUsuario(idUsuario, searchTerm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
    }
}
