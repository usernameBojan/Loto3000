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

                x.CreateMap<EditAdminDto, AdminDto>();

                x.CreateMap<Player, PlayerDto>();

                x.CreateMap<RegisterPlayerDto, Player>();

                x.CreateMap<RegisterPlayerDto, PlayerDto>();

                x.CreateMap<BuyCreditsDto, TransactionTracker>();            

                x.CreateMap<TransactionTracker, TransactionTrackerDto>();

                x.CreateMap<CreateTicketDto, Ticket>();

                x.CreateMap<CreateTicketDto, TicketDto>();
            });

            return cfg;
        }
    }
}
