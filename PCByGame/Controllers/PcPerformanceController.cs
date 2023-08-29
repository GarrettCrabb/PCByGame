using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCByGame.Repositories;
using PCByGame.Models;
using Microsoft.AspNetCore.Authorization;

namespace PCByGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PcPerformanceController : ControllerBase
    {
        private readonly IPcPerformanceRepository _pcPerformanceRepository;

        public PcPerformanceController(IPcPerformanceRepository pcPerformanceRepository)
        {
            _pcPerformanceRepository = pcPerformanceRepository;
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetAllByPcId(int id)
        {
            return Ok(_pcPerformanceRepository.GetAllByPcId(id));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(PcPerformance pcPerformance)
        {
            _pcPerformanceRepository.AddPcPerformance(pcPerformance);
            return NoContent();
        }
        /*should i change this to GetById instead of being get*/
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(PcPerformance pcPerformance)
        {
            _pcPerformanceRepository.UpdatePcPerformance(pcPerformance);
            return CreatedAtAction("GetAllByPcId", new { id = pcPerformance.Id }, pcPerformance);
        }
    }
}
