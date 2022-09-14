using Loto3000.Application.Dto.Login;
using Loto3000.Application.Dto.Player;
using Loto3000.Application.Dto.PlayerAccountManagment;
using Loto3000.Application.Dto.Winners;
using Loto3000.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Loto3000.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IPlayerService playerService;
        private readonly ILoginService loginService;
        private readonly IDrawService drawService;
        public HomeController(IPlayerService playerService, IDrawService drawService, ILoginService loginService)
        {
            this.playerService = playerService;
            this.loginService = loginService;
            this.drawService = drawService;
        }

        [HttpPost("register")]
        public ActionResult<PlayerDto> RegisterPlayer([FromBody] RegisterPlayerDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            playerService.RegisterPlayer(dto);
            return Created($"api/v1/home/login/", dto);
        }

        [HttpPost("register/verify")]
        public ActionResult VerifyPlayer([FromBody] string code)
        {
            playerService.VerifyPlayer(code);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult<TokenDto> Login([FromBody] LoginDto login)
        {
            var token = loginService.Authenticate(login);

            return Ok(token.Token);
        }

        [HttpPost("forgot-password")]
        public ActionResult ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            playerService.ForgotPassword(dto);
            return Ok();
        }

        [HttpPost("change-password-with-email-code")]
        public ActionResult ChangePasswordByCode([FromBody] UpdatePasswordDto dto)
        {
            playerService.UpdatePasswordByCode(dto);
            return Ok();
        }
        [HttpGet("winners-board")]
        public ActionResult<IEnumerable<WinnersDto>> WinnersBoard()
        {
            return Ok(drawService.DisplayWinners());
        }
    }
}