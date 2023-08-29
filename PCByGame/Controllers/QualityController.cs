using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCByGame.Repositories;

namespace PCByGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QualityController : ControllerBase
    {
        private readonly IQualityRepository _qualityRepository;

        public QualityController(IQualityRepository qualityRepository)
        {
            _qualityRepository = qualityRepository;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_qualityRepository.GetAll());
        }
    }
}
