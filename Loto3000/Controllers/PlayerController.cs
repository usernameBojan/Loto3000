using Loto3000.Application.Dto.Player;
using Loto3000.Application.Dto.Transactions;
using Loto3000.Application.Dto.Tickets;
using Loto3000.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Microsoft.AspNetCore.Authorization;
using Loto3000.Application.Utilities;
using System.Security.Claims;

namespace Loto3000.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService service;
        public PlayerController(IPlayerService service)
        {
            this.service = service;
        }

        [Authorize(Policy = SystemPolicies.MustHaveId)]
        [HttpGet()]
        public ActionResult<PlayerDto> GetPlayer()
        {
            var id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(service.GetPlayer(id));
        }

        [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdmin}")]
        [HttpGet("players")]
        public ActionResult<IEnumerable<PlayerDto>> GetPlayers()
        {
            return Ok(service.GetPlayers());
        }

        [Authorize(Roles = SystemRoles.Player)]
        [HttpPatch("deposit")]
        public ActionResult BuyCredits([FromBody] BuyCreditsDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            service.BuyCredits(dto, id);
            return Ok("Deposit successful");
        }

        [Authorize(Policy = SystemPolicies.MustHaveId)]
        [HttpGet("transactions/{transactionId:int}")]
        public ActionResult<IEnumerable> GetPlayerTransaction([FromRoute] int transactionId)
        {
            var id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(service.GetPlayerTransaction(id, transactionId));
        }

        [Authorize(Policy = SystemPolicies.MustHaveId)]
        [HttpGet("transactions")]
        public ActionResult<IEnumerable> GetPlayerTransactions()
        {
            var id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(service.GetPlayerTransactions(id));
        }

        [Authorize(Roles = SystemRoles.Player)]
        [HttpPost("create-ticket")]
        public ActionResult<TicketDto> CreateTicket([FromBody] CreateTicketDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var ticket = service.CreateTicket(dto, id);
            return Created($"api/v1/player/ticket/{ticket.Id}/", ticket);
        }

        [Authorize(Policy = SystemPolicies.MustHaveId)]
        [HttpGet("/ticket/{ticketId:int}")]
        public ActionResult<IEnumerable> GetPlayerTicket([FromRoute] int ticketId)
        {
            var id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(service.GetPlayerTicket(id, ticketId));
        }

        [Authorize(Policy = SystemPolicies.MustHaveId)]
        [HttpGet("tickets")]
        public ActionResult<IEnumerable<TicketDto>> GetPlayerTickets()
        {
            var id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(service.GetPlayerTickets(id));
        }

        [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdmin}")]
        [HttpDelete("{id:int}")]
        public ActionResult DeletePlayer(int id)
        {
            service.DeletePlayer(id);
            return Ok();
        }
    }
}