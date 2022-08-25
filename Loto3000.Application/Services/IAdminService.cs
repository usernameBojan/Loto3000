using Loto3000.Application.Dto.Admin;
using Loto3000.Application.Dto.Player;

namespace Loto3000.Application.Services
{
    public interface IAdminService
    {
        AdminDto GetAdmin(int id);
        IEnumerable<AdminDto> GetAdmins();
        IEnumerable<TransactionTrackerDto> GetAllTransactions();
        AdminDto CreateAdmin(CreateAdminDto model);
        AdminDto EditAdmin(EditAdminDto model, int id);
        void DeleteAdmin(int id);
    }
}