using Loto3000.Application.Utilities;
using Loto3000.Application.Dto.Admin;
using Loto3000.Application.Dto.Transactions;
using Loto3000.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Loto3000.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService service;
        public AdminController(IAdminService service)
        {
            this.service = service;
        }

        [Authorize(Roles = SystemRoles.SuperAdmin)]
        [HttpGet("{id:int}")]
        public ActionResult<AdminDto> GetAdmin(int id)
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
        [HttpGet("show-transactions")]
        public ActionResult<IList<TransactionTrackerDto>> GetAllTransactions()
        {
            return Ok(service.GetAllTransactions());
        }

        [Authorize(Roles = $"{SystemRoles.Administrator},{SystemRoles.SuperAdmin}")]
        [HttpGet("show-tickets")]
        public ActionResult<IList<TransactionTrackerDto>> GetAllTickets()
        {
            return Ok(service.GetAllTickets());
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
        [HttpDelete("admins/{id:int}")]
        public ActionResult DeleteAdmin(int id)
        {
            service.DeleteAdmin(id);
            return Ok();
        }
    }
}