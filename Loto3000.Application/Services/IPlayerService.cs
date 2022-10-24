using Loto3000.Application.Dto.Player;
using Loto3000.Application.Dto.PlayerAccountManagment;

namespace Loto3000.Application.Services
{
    public interface IPlayerService
    {
        PlayerDto GetPlayer(int id);
        IEnumerable<PlayerDto> GetPlayers();
        PlayerDto RegisterPlayer(RegisterPlayerDto dto);
        void VerifyPlayer(string code);
        void ChangePassword(ChangePasswordDto dto, int id);
        void ForgotPassword(ForgotPasswordDto dto);
        void UpdatePasswordByCode(UpdatePasswordDto dto);
        void DeletePlayer(int id);
    }
}