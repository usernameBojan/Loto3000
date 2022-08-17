using AutoMapper;
using Loto3000.Application.Dto.Admin;
using Loto3000.Application.Dto.Player;
using Loto3000.Domain.Models;

namespace Loto3000.Application.Mapper
{
    public static class ModelMapper
    {          
        public static MapperConfiguration GetConfiguration()
        {

            MapperConfiguration cfg = new MapperConfiguration(x =>
            {
                x.CreateMap<Admin, AdminDto>();

                x.CreateMap<AdminDto, Admin>()
                .ForMember(m => m.Password, m => m.Ignore())
                .ForMember(y => y.AuthorizationCode, y => y.Ignore());

                x.CreateMap<CreateAdminDto, Admin>();

                x.CreateMap<CreateAdminDto, AdminDto>();

                x.CreateMap<EditAdminDto, Admin>();
                //.ForMember(m => m.FirstName, m => m.Ignore())
                //.ForMember(y => y.LastName, y => y.Ignore())
                //.ForMember(z => z.FullName, z => z.Ignore());

                x.CreateMap<EditAdminDto, AdminDto>();
                
                //.ForMember(m => m.FirstName, m => m.Ignore())
                //.ForMember(y => y.LastName, y => y.Ignore())
                //.ForMember(z => z.FullName, z => z.Ignore());

                x.CreateMap<Player, PlayerDto>();
                //.ForMember(m => m.Id, m => m.Ignore());
                //.ForMember(m => m.Password, m => m.Ignore())
                //.ForMember(y => y.AuthorizationCode, y => y.Ignore());

                x.CreateMap<CreateAdminDto, Admin>();

                x.CreateMap<CreateAdminDto, AdminDto>();
            });

            return cfg;
        }
    }
}
