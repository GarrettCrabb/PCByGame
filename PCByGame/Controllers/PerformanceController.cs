using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCByGame.Models;
using PCByGame.Repositories;

namespace PCByGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformanceController : ControllerBase
    {
        private readonly IPerformanceRepository _performanceRepository;

        public PerformanceController(IPerformanceRepository performanceRepository)
        {
            _performanceRepository = performanceRepository;
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(Performance performance)
        {
            int performanceId = _performanceRepository.AddPerformance(performance);
            return Ok(performanceId);
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(Performance performance)
        {
            _performanceRepository.UpdatePerformance(performance);
            return NoContent();
        }
    }
}
