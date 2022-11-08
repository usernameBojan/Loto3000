using Loto3000.Application.Dto.Player;
using Loto3000.Application.Dto.Transactions;
using Loto3000.Application.Dto.Tickets;
using Loto3000.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using Microsoft.AspNetCore.Authorization;
using Loto3000.Application.Utilities;
using System.Security.Claims;
using Loto3000.Application.Dto.PlayerAccountManagment;
using Loto3000.Application.Dto.Statistics;

namespace Loto3000.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService playerService;
        private readonly ITicketService ticketService;
        private readonly ITransactionService transactionService;
        public PlayerController(IPlayerService playerService, ITicketService ticketService, ITransactionService transactionService)
        {
            this.playerService = playerService;
            this.ticketService = ticketService;
            this.transactionService = transactionService;
        }

        [Authorize(Roles = SystemRoles.Player)]
        [HttpGet]
        public ActionResult<PlayerDto> Player()
        {
            return Ok($"Player: {User.FindFirstValue(ClaimTypes.Name)} {User.FindFirstValue(ClaimTypes.Surname)}");
        }

        [HttpGet("get-player")]
        public ActionResult<PlayerDto> GetPlayer()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Ok(playerService.GetPlayer(id));
        }

        [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdmin}")]
        [HttpGet("players")]
        public ActionResult<IEnumerable<PlayerDto>> GetPlayers()
        {
            return Ok(playerService.GetPlayers());
        }

        [Authorize(Roles = SystemRoles.Player)]
        [HttpPatch("deposit")]
        public ActionResult BuyCredits([FromBody] BuyCreditsDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            transactionService.BuyCredits(dto, id);
            return Ok("Deposit successful");
        }

        [Authorize(Policy = SystemPolicies.MustHaveId)]
        [HttpGet("transactions/{transactionId:int}")]
        public ActionResult<IEnumerable> GetPlayerTransaction([FromRoute] int transactionId)
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(transactionService.GetPlayerTransaction(id, transactionId));
        }

        [Authorize(Policy = SystemPolicies.MustHaveId)]
        [HttpGet("transactions")]
        public ActionResult<IEnumerable> GetPlayerTransactions()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(transactionService.GetPlayerTransactions(id));
        }

        [Authorize(Roles = SystemRoles.Player)]
        [HttpPost("create-ticket")]
        public ActionResult<TicketDto> CreateTicket([FromBody] CreateTicketDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var ticket = ticketService.CreateTicket(dto, id);
            return Created($"api/v1/player/ticket/{ticket.Id}/", ticket);
        }

        [Authorize(Policy = SystemPolicies.MustHaveId)]
        [HttpGet("ticket/{ticketId:int}")]
        public ActionResult<IEnumerable> GetPlayerTicket([FromRoute] int ticketId)
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(ticketService.GetPlayerTicket(id, ticketId));
        }

        [Authorize(Policy = SystemPolicies.MustHaveId)]
        [HttpGet("tickets")]
        public ActionResult<IEnumerable<TicketDto>> GetPlayerTickets()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(ticketService.GetPlayerTickets(id));
        }

        [Authorize(Policy = SystemPolicies.MustHaveId)]
        [HttpGet("active-tickets")]
        public ActionResult<IEnumerable<TicketDto>> GetPlayerActiveTickets()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(ticketService.GetPlayerActiveTickets(id));
        }

        [Authorize(Policy = SystemPolicies.MustHaveId)]
        [HttpGet("past-tickets")]
        public ActionResult<IEnumerable<TicketDto>> GetPlayerPastTickets()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(ticketService.GetPlayerPastTickets(id));
        }

        [Authorize(Policy = SystemPolicies.MustHaveId)]
        [HttpPost("change-password")]
        public ActionResult ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            playerService.ChangePassword(dto, id);

            return Ok();
        }

        [Authorize(Roles = SystemRoles.Player)]
        [HttpGet("tickets-statistics")]
        public ActionResult<PlayerTicketsStatisticsDto> PlayerTicketsStatistics()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(playerService.PlayerTicketsStatistics(id));
        }

        [Authorize(Roles = SystemRoles.Player)]
        [HttpGet("transactions-statistics")]
        public ActionResult<PlayerTransactionsStatisticsDto> PlayerTransactionsStatistics()
        {
            var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(playerService.PlayerTransactionsStatistics(id));
        }

        [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdmin}")]
        [HttpDelete("delete-player/{id:int}")]
        public ActionResult DeletePlayer(int id)
        {
            playerService.DeletePlayer(id);
            return Ok();
        }
    }
}