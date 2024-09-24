using Microsoft.AspNetCore.Mvc;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Application.Services;
using PolarisContacts.Domain;

namespace PolarisContacts.ReadService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController(ILogger<EnderecoController> logger, IEnderecoService enderecoService) : ControllerBase
    {
        private readonly ILogger<EnderecoController> _logger = logger;
        private readonly IEnderecoService _enderecoService = enderecoService;

        [HttpGet]
        public async Task<IEnumerable<Endereco>> GetEnderecosByIdContato(int idContato)
        {
            try
            {
                return await _enderecoService.GetEnderecosByIdContato(idContato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<Endereco> GetEnderecoById(int id)
        {
            try
            {
                return await _enderecoService.GetEnderecoById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
