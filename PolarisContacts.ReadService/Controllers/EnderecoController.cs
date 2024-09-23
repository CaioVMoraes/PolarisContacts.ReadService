using Microsoft.AspNetCore.Mvc;
using PolarisContacts.Application.Interfaces.Services;
using PolarisContacts.Application.Services;

namespace PolarisContacts.ReadService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController(ILogger<EnderecoController> logger, IEnderecoService enderecoService) : ControllerBase
    {
        private readonly ILogger<EnderecoController> _logger = logger;
        private readonly IEnderecoService _enderecoService = enderecoService;

        [HttpPost]
        public bool Post()
        {
            try
            {
                _enderecoService.AddEndereco(new Domain.Endereco());
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return true;
        }
    }
}
