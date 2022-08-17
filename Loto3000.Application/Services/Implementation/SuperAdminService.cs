//using AutoMapper;
//using Loto3000.Application.Dto.Admin;
//using Loto3000.Application.Repositories;
//using Loto3000.Application.Services.Implementation;
//using Loto3000.Domain.Models;

//namespace Loto3000.Application.Services
//{
//    public class SuperAdminService : AdminService, ISuperAdminService
//    {
//        private readonly IRepository<Admin> adminRepository;
//        private readonly IMapper mapper;

//        public SuperAdminService(IRepository<Admin> adminRepository, IRepository<Player> playerRepository, IMapper mapper)
//        : base(playerRepository, mapper)
//        {
//            this.adminRepository = adminRepository;
//            this.mapper = mapper;
//        }
//        public AdminDto GetAdmin(int id)
//        {
//            var admin = adminRepository.GetById(id);
//            if (admin == null)
//            {
//                throw new Exception("Admin not found"); //напрај сопствени ексепшни
//            }

//            return mapper.Map<AdminDto>(admin);
//        }
//        public IEnumerable<AdminDto> GetAdmins()
//        {
//            var admins = adminRepository.GetAll().ToList();
//            return adminRepository.GetAll()
//                                  .Select(a => mapper.Map<AdminDto>(a))
//                                  .ToList();
//        }
//        public AdminDto CreateAdmin(CreateAdminDto dto)
//        {
//            var admin = mapper.Map<Admin>(dto);
//            adminRepository.Create(admin);

//            return mapper.Map<AdminDto>(dto);
//        }
//        public AdminDto EditAdmin(EditAdminDto dto, int id)
//        {
//            var admin = adminRepository.GetById(id);
//            if(admin == null)
//            {
//                throw new Exception("Admin was not found");  //напрај сопствени ексепшни!!!!!
//            }

//            admin = mapper.Map<Admin>(dto);
//            adminRepository.Update(admin);

//            return mapper.Map<AdminDto>(dto);
//        }
//        public void DeleteAdmin(int id)
//        {
//            var admin = adminRepository.GetById(id);
//            if (admin == null)
//            { 
//                throw new Exception("Admin not found"); //напрај сопствени ексепшни!!!!!
//            }

//            adminRepository.Delete(admin);
//        }
//    }
//}