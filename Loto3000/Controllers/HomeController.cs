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

        [HttpPost("login")]
        public ActionResult<TokenDto> Login([FromBody] LoginDto login)
        {
            var token = loginService.Authenticate(login);

            return Ok(token.Token);
        }
        
        [HttpGet("winners-board")]
        public ActionResult<IEnumerable<WinnersDto>> WinnersBoard()
        {
            return Ok(drawService.DisplayWinners());
        }
    }
}