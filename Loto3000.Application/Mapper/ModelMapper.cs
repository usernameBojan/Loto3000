using AutoMapper;
using Loto3000.Application.Dto.Admin;
using Loto3000.Application.Dto.Player;
using Loto3000.Application.Dto.Transactions;
using Loto3000.Application.Dto.PlayerAccountManagment;
using Loto3000.Application.Dto.Tickets;
using Loto3000.Domain.Entities;
using Loto3000.Application.Dto.Draw;
using Loto3000.Application.Dto.Statistics;

namespace Loto3000.Application.Mapper
{
    public static class ModelMapper
    {          
        public static MapperConfiguration GetConfiguration()
        {

            return new(x =>
            {
                x.CreateMap<Admin, AdminDto>();

                x.CreateMap<AdminDto, Admin>()
                .ForMember(m => m.Password, m => m.Ignore());

                x.CreateMap<CreateAdminDto, Admin>()
                .ForMember(m => m.Password, m => m.Ignore());

                x.CreateMap<CreateAdminDto, AdminDto>();

                x.CreateMap<Player, PlayerDto>();

                x.CreateMap<RegisterPlayerDto, Player>()
                .ForMember(m => m.Password, m => m.Ignore());

                x.CreateMap<RegisterPlayerDto, PlayerDto>();

                x.CreateMap<BuyCreditsDto, TransactionTracker>();            

                x.CreateMap<TransactionTracker, TransactionTrackerDto>().ReverseMap();

                x.CreateMap<CreateTicketDto, Ticket>();

                x.CreateMap<CreateTicketDto, TicketDto>();

                x.CreateMap<Ticket, TicketDto>();

                x.CreateMap<Draw, DrawDto>();

                x.CreateMap<Player, PlayerTicketsStatisticsDto>();

                x.CreateMap<Player, PlayerTransactionsStatisticsDto>();
            });
        }
    }
}