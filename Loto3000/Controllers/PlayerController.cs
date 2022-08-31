using HashidsNet;
using Loto3000.Application.Dto.Player;
using Loto3000.Application.Dto.Transactions;
using Loto3000.Application.Dto.PlayerAccountManagment;
using Loto3000.Application.Dto.Tickets;
using Loto3000.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Loto3000.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService service;

        public PlayerController(IPlayerService service)
        {
            this.service = service;
        }

        [HttpGet("{id:int}")]
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

        [HttpPost("register")]
        public ActionResult<PlayerDto> RegisterPlayer([FromBody] RegisterPlayerDto dto)
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
        }

        [HttpPatch("{id:int}/deposit")]
        public ActionResult BuyCredits([FromBody] BuyCreditsDto dto, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                service.BuyCredits(dto, id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id:int}/transactions")]
        public ActionResult<IEnumerable> GetPlayerTransactions(int id)
        {
            try
            {
                return Ok(service.GetPlayerTransactions(id));
            }
            catch (Exception)
            {
                return NotFound();
            }
            
        }
        [HttpPost("{id:int}/create-ticket")]
        public ActionResult<TicketDto> CreateTicket([FromBody] CreateTicketDto dto, int id)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState); 
            }

            try 
            { 
                var ticket = service.CreateTicket(dto, id);
                return Created($"api/v1/player/{id}/ticket/{ticket.Id}/", ticket);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex}");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeletePlayer(int id)
        {
            try
            {
                service.DeletePlayer(id);
                return Ok();
            }
            catch(Exception)
            {
                return NotFound();
            }
        }
    }
}