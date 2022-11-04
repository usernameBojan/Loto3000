using Loto3000.Application.Utilities;
using Loto3000.Application.Dto.Admin;
using Loto3000.Application.Dto.Transactions;
using Loto3000.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Loto3000.Application.Dto.Tickets;
using System.Security.Claims;

namespace Loto3000.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService service;
        private readonly IPlayerService playerService;
        private readonly ITicketService ticketService;
        private readonly ITransactionService transactionService;
        public AdminController(IAdminService service, IPlayerService playerService, ITicketService ticketService, ITransactionService transactionService)
        {
            this.service = service;
            this.playerService = playerService;
            this.ticketService = ticketService;
            this.transactionService = transactionService;
        }

        [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdmin}")]
        [HttpGet]
        public ActionResult Manage()
        {
            return Ok($"Admin: {User.FindFirstValue(ClaimTypes.Name)}");
        }

        [Authorize(Roles = SystemRoles.SuperAdmin)]
        [HttpGet("{id:int}")]
        public ActionResult<AdminDto> GetAdmin([FromRoute] int id)
        {
            return Ok(service.GetAdmin(id));
        }

        [Authorize(Roles = SystemRoles.SuperAdmin)]
        [HttpGet("admins")]
        public ActionResult<IEnumerable<AdminDto>> GetAdmins()
        {
            return Ok(service.GetAdmins());
        }

        [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdmin}")]
        [HttpGet("player/{id:int}")]
        public ActionResult<AdminDto> GetPlayerByAdmin([FromRoute] int id)
        {
            return Ok(playerService.GetPlayer(id));
        }

        [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdmin}")]
        [HttpGet("show-transactions")]
        public ActionResult<IList<TransactionTrackerDto>> GetAllTransactions()
        {
            return Ok(transactionService.GetAllTransactions());
        }

        [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdmin}")]
        [HttpGet("registered-transactions")]
        public ActionResult<IList<TransactionTrackerDto>> GetRegisteredPlayerTransactions()
        {
            return Ok(transactionService.GetRegisteredPlayersTransactions());
        }

        [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdmin}")]
        [HttpGet("nonregistered-transactions")]
        public ActionResult<IList<TransactionTrackerDto>> GetNonregisteredPlayersTransactions()
        {
            return Ok(transactionService.GetNonregisteredPlayersTransactions());
        }

        [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdmin}")]
        [HttpGet("nonregistered-transactions/{transactionId:int}")]
        public ActionResult<IList<TransactionTrackerDto>> GetNonregisteredPlayerTransaction([FromRoute] int transactionId)
        {
            return Ok(transactionService.GetNonregisteredPlayerTransaction(transactionId));
        }

        [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdmin}")]
        [HttpGet("show-tickets")]
        public ActionResult<IList<TicketDto>> GetAllTickets()
        {
            return Ok(ticketService.GetAllTickets());
        }

        [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdmin}")]
        [HttpGet("active-tickets")]
        public ActionResult<IList<TicketDto>> GetActiveTickets()
        {
            return Ok(ticketService.GetActiveTickets());
        }

        [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdmin}")]
        [HttpGet("past-tickets")]
        public ActionResult<IList<TicketDto>> GetPastTickets()
        {
            return Ok(ticketService.GetPastTickets());
        }
        [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdmin}")]
        [HttpGet("registered-tickets")]
        public ActionResult<IList<TicketDto>> GetRegisteredPlayersTickets()
        {
            return Ok(ticketService.GetRegisteredPlayersTickets());
        }
        [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdmin}")]
        [HttpGet("nonregistered-tickets")] 
        public ActionResult<IList<TransactionTrackerDto>> GetNonregisteredPlayersTickets()
        {
            return Ok(ticketService.GetNonregisteredPlayersTickets());
        }

        [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdmin}")]
        [HttpGet("nonregistered-tickets/{ticketId:int}")]
        public ActionResult<IList<TransactionTrackerDto>> GetNonregisteredPlayerTickets([FromRoute] int ticketId)
        {
            return Ok(ticketService.GetNonregisteredPlayerTicket(ticketId));
        }

        [Authorize(Roles = SystemRoles.SuperAdmin)]
        [HttpPost("create-admin")]
        public ActionResult<AdminDto> CreateAdmin([FromBody] CreateAdminDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var admin = service.CreateAdmin(dto);
            return Created($"api/v1/home/login/", admin);
        }

        [Authorize(Roles = SystemRoles.SuperAdmin)]
        [HttpDelete("delete-admin/{id:int}")]
        public ActionResult DeleteAdmin(int id)
        {
            service.DeleteAdmin(id);
            return Ok();
        }
    }
}