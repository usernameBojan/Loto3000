using Loto3000.Application.Dto.Draw;
using Loto3000.Application.Dto.Tickets;
using Loto3000.Application.Services;
using Loto3000.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loto3000.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DrawController : ControllerBase
    {
        private readonly IDrawService service;
        public DrawController(IDrawService service)
        {
            this.service = service;
        }

        [HttpGet("{id:int}")]
        public ActionResult<DrawDto> GetDraw(int id)
        {
            try
            {
                return Ok(service.GetDraw(id));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("draws")]
        public ActionResult <IEnumerable<DrawDto>> GetAllDraws()
        {
            try
            {
                return Ok(service.GetDraws());
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost("activate-first-session")]
        public ActionResult<DrawDto> ActivateDrawSession()
        {
            if(service.GetActiveDraw() != null)
            {
                return Unauthorized("This action serves for activating the first session only, every next session is activated automatically.");
            }

            try
            {
                service.ActivateDrawSession();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex}");
            }
        }

        [HttpPost("initiate-draw")]
        public ActionResult<DrawDto> InitiateDraw()
        {
            try
            {
                service.InitiateDraw();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex}");
            }
        }
    }
}