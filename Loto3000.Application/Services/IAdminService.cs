using Loto3000.Application.Dto.Admin;
using Loto3000.Application.Dto.Transactions;
using Loto3000.Application.Dto.Tickets;

namespace Loto3000.Application.Services
{
    public interface IAdminService
    {
        AdminDto GetAdmin(int id);
        IEnumerable<AdminDto> GetAdmins();
        AdminDto CreateAdmin(CreateAdminDto model);
        void DeleteAdmin(int id);
    }
}