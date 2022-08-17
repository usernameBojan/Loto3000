using Loto3000.Application.Dto.Player;
using Loto3000.Application.Services;
using Loto3000.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loto3000.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService service;

        public PlayerController(IPlayerService service)
        {
            this.service = service;
        }
        [HttpGet("players/{id:int}")]
        public ActionResult<PlayerDto> GetPlayer(int id)
        {
            try
            {
                return Ok(service.GetPlayer(id));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("players")]
        public ActionResult<IEnumerable<PlayerDto>> GetPlayers()
        {
            return Ok(service.GetPlayers());
        }

        [HttpPost]
        public ActionResult <PlayerDto> RegisterPlayer([FromBody] RegisterPlayerDto dto) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                service.RegisterPlayer(dto);
                return Created($"api/v1/login/", dto);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            //ДА СЕ НАПРАА СОПСТВЕНИ ЕКСЕПШНИ ШО ЌЕ КАЖУА КОЕ ЗА ШТО Е 
        }

        [HttpDelete("players/{id:int}")]
        public ActionResult DeletePlayer(int id)
        {
            try
            {
                service.DeletePlayer(id);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
