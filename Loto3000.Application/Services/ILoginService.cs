using Loto3000.Application.Dto.Login;

namespace Loto3000.Application.Services
{
    public interface ILoginService
    {
        TokenDto Authenticate(LoginDto loginDto);
    }
}