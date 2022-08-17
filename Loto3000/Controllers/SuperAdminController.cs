//using Loto3000.Application.Dto.Admin;
//using Loto3000.Application.Services;
//using Loto3000.Domain.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace Loto3000.Controllers
//{
//    [Route("api/v1/[controller]")]
//    [ApiController]
//    public class SuperAdminController : ControllerBase
//    {
//        private readonly ISuperAdminService service;
//        public SuperAdminController(ISuperAdminService service)
//        {
//            this.service = service;
//        }

//        [HttpGet("admins")]
//        public ActionResult<IEnumerable<Admin>> GetAdmins()
//        {
//            return Ok(service.GetAdmins());
//        }

//        [HttpGet("players")]
//        public ActionResult<IEnumerable<Player>> GetPlayers()
//        {
//            return Ok(service.GetPlayers());
//        }

//        [HttpGet("admins/{id:int}")]
//        public ActionResult<Admin> GetAdmin(int id)
//        {
//            try
//            {
//                return Ok(service.GetAdmin(id));
//            }
//            catch (Exception)
//            {
//                return NotFound();
//            }
//        }

//        [HttpGet("players/{id:int}")]
//        public ActionResult<Player> GetPlayer(int id)
//        {
//            try
//            {
//                return Ok(service.GetPlayer(id));
//            }
//            catch (Exception)
//            {
//                return NotFound();
//            }
//        }

//        [HttpPost("create-admin")]
//        public ActionResult CreateAdmin([FromBody] CreateAdminDto dto)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            var admin = service.CreateAdmin(dto);
//            return Created($"api/v1/login/", admin);
//        }

//        [HttpPut("update-admin/{id:int}")]
//        public ActionResult<EditAdminDto> EditAdmin([FromBody] EditAdminDto dto, int id)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(dto);
//            }
//            try
//            {
//                var note = service.EditAdmin(dto, id);
//                return Ok(note);
//            }
//            catch (Exception)
//            {
//                return NotFound();
//            }
//        }

//        [HttpDelete("players/{id:int}")]
//        public ActionResult DeletePlayer(int id)
//        {
//            try
//            {
//                service.DeletePlayer(id);
//                return Ok();
//            }
//            catch
//            {
//                return NotFound();
//            }
//        }

//        [HttpDelete("admins/{id:int}")]
//        public ActionResult DeleteAdmin(int id)
//        {
//            try
//            {
//                service.DeleteAdmin(id);
//                return Ok();
//            }
//            catch
//            {
//                return NotFound();
//            }
//        }
//    }
//}