using Loto3000.Application.Utilities;
using Loto3000.Application.Dto.Draw;
using Loto3000.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Loto3000.Controllers
{
    [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdmin}")]
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
            return Ok(service.GetDraw(id));
        }

        [HttpGet("draws")]
        public ActionResult<IEnumerable<DrawDto>> GetAllDraws()
        {
            return Ok(service.GetDraws());
        }

        [HttpPost("initiate-draw")]
        public ActionResult<DrawDto> InitiateDraw()
        {
            return Ok(service.InitiateDraw());
        }
    }
}