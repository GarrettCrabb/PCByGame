using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCByGame.Repositories;
using PCByGame.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;
/*Ask about conflict of routes with Id*/
namespace PCByGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PcController : ControllerBase
    {
        private readonly IPcRepository _pcRepository;

        public PcController(IPcRepository pcRepository)
        {
            _pcRepository = pcRepository;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_pcRepository.GetAll());
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetPcById(int id)
        {
            var pc = _pcRepository.GetPcById(id);
            if (pc == null)
            {
                return NotFound();
            }
            return Ok(pc);
        }

        [Authorize]
        [HttpGet("userpcs")]
        public IActionResult GetUserPcs()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Ok(_pcRepository.GetPcByUserId(firebaseUserId));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(Pc pc) 
        {
            _pcRepository.AddPc(pc);
            return CreatedAtAction("Get", new { id = pc.Id }, pc);
        }
        /*should i change the created at action to GetById instead of just being a get, change to getById*/
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, Pc pc) 
        {
            _pcRepository.UpdatePc(pc);
            return CreatedAtAction("GetUserPcs", new { id = pc.Id }, pc);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _pcRepository.DeletePc(id);
            return NoContent();
        }
    }
}
