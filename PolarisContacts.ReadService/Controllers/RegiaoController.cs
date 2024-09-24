using Microsoft.AspNetCore.Mvc;
using PolarisContacts.ReadService.Application.Interfaces.Services;
using PolarisContacts.ReadService.Application.Services;
using PolarisContacts.Domain;

namespace PolarisContacts.ReadService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegiaoController(ILogger<RegiaoController> logger, IRegiaoService regiaoService) : ControllerBase
    {
        private readonly ILogger<RegiaoController> _logger = logger;
        private readonly IRegiaoService _regiaoService = regiaoService;

        [HttpGet("GetAll")]
        public async Task<IEnumerable<Regiao>> GetAll()
        {
            try
            {
                return await _regiaoService.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<Regiao> GetById(int id)
        {
            try
            {
                return await _regiaoService.GetById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
