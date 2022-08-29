using Loto3000.Application.Dto.Draw;
using Loto3000.Application.Services;
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

        [HttpPost("activate")]
        public ActionResult<DrawDto> ActivateDrawSession()
        {
            try
            {
                service.ActivateDrawSession();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("initiate")]
        public ActionResult<DrawDto> InitiateDraw()
        {
            try
            {
                service.InitiateDraw();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}