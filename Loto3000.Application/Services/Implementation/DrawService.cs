using AutoMapper;
using Loto3000.Application.Dto.Draw;
using Loto3000.Application.Dto.Winners;
using Loto3000.Domain.Exceptions;
using Loto3000.Application.Repositories;
using Loto3000.Application.Utilities;
using Loto3000.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Loto3000.Application.Services.Implementation
{
    public class DrawService : IDrawService
    {
        private readonly IRepository<Draw> drawRepository;
        private readonly IRepository<DrawNumbers> drawNumbersRepository;
        private readonly IRepository<Ticket> ticketRepository;
        private readonly IRepository<Combination> combinationRepository;
        private readonly IRepository<NonregisteredPlayerTicket> nonregisteredPlayerTicketsRepository;
        private readonly IMapper mapper;
        public DrawService(
            IRepository<Draw> drawRepository,
            IRepository<DrawNumbers> drawNumbersRepository,
            IRepository<Ticket> ticketRepository,
            IRepository<Combination> combinationRepository,
            IRepository<NonregisteredPlayerTicket> nonregisteredPlayerTicketsRepository,
            IMapper mapper
            )
        {
            this.drawRepository = drawRepository;
            this.drawNumbersRepository = drawNumbersRepository;
            this.ticketRepository = ticketRepository;
            this.combinationRepository = combinationRepository;
            this.nonregisteredPlayerTicketsRepository = nonregisteredPlayerTicketsRepository;
            this.mapper = mapper;
        }
        public DrawDto GetDraw(int id)
        {
            var draw = drawRepository.GetById(id) ?? throw new NotFoundException();   

            return mapper.Map<DrawDto>(draw);
        }
        public IEnumerable<DrawDto> GetDraws()
        {
            var draws = drawRepository.Query().Select(d => mapper.Map<DrawDto>(d));

            return draws.ToList();
        }        
        public IEnumerable<WinnersDto> DisplayWinners()
        {
            const int LowestPrizeValue = 3;

            var winners = new List<WinnersDto>();
            var draw = drawRepository.Query().WhereConcludedDraw().FirstOrDefault() ?? throw new NotFoundException();

            var registeredPlayersTickets = ticketRepository.Query()
                                                           .Include(x => x.Player)
                                                           .Include(x => x.Draw)
                                                           .Where(x => x.Draw!.Id == draw.Id);

            var nonregisteredPlayersTickets = nonregisteredPlayerTicketsRepository.Query()
                                                                                  .Include(x => x.NonregisteredPlayer)
                                                                                  .Include(x => x.Draw)
                                                                                  .Where(x => x.Draw!.Id == draw.Id);

            foreach(var ticket in registeredPlayersTickets)
            {
                if (((int)ticket.Prize) >= LowestPrizeValue && ticket.PlayerId != null)
                {
                    WinnersDto winner = new(ticket.Player!.FullName, ticket.CombinationNumbersString, ticket.Prize);

                    winners.Add(winner);
                }
            }

            foreach (var ticket in nonregisteredPlayersTickets)
            {
                if (((int)ticket.Prize) >= LowestPrizeValue && ticket.NonregisteredPlayerId != null)
                {
                    WinnersDto winner = new(ticket.NonregisteredPlayer!.FullName, ticket.CombinationNumbersString, ticket.Prize);

                    winners.Add(winner);
                }
            }

            return winners;
        }
    }
}