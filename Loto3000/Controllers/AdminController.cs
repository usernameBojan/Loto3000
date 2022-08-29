using Loto3000.Application.Dto.Admin;
using Loto3000.Application.Dto.Transactions;
using Loto3000.Application.Dto.PlayerAccountManagment;
using Loto3000.Application.Dto.Tickets;
using Loto3000.Application.Services;
using Loto3000.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loto3000.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService service;
        public AdminController(IAdminService service)
        {
            this.service = service;
        }

        [HttpGet("admins")]
        public ActionResult<IEnumerable<AdminDto>> GetAdmins()
        {
            return Ok(service.GetAdmins());
        }
        [HttpGet("show-transactions")]
        public ActionResult<IList<TransactionTrackerDto>> GetAllTransactions()
        {
            return Ok(service.GetAllTransactions());
        }

        [HttpGet("{id:int}")]
        public ActionResult<AdminDto> GetAdmin(int id)
        {
            try
            {
                return Ok(service.GetAdmin(id));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost("create-admin")]
        public ActionResult<AdminDto> CreateAdmin([FromBody] CreateAdminDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var admin = service.CreateAdmin(dto);
            return Created($"api/v1/login/", admin);
        }

        [HttpPatch("update-admin/{id:int}")]
        public ActionResult<EditAdminDto> EditAdmin([FromBody] EditAdminDto dto, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dto);
            }
            try
            {
                var note = service.EditAdmin(dto, id);
                return Ok(note);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("admins/{id:int}")]
        public ActionResult DeleteAdmin(int id)
        {
            try
            {
                service.DeleteAdmin(id);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}