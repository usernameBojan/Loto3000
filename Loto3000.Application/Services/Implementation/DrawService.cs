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
        private readonly IRepository<NonregisteredPlayer> nonregisteredPlayerRepository;
        private readonly IMapper mapper;
        public DrawService(
            IRepository<Draw> drawRepository,
            IRepository<DrawNumbers> drawNumbersRepository,
            IRepository<Ticket> ticketRepository,
            IRepository<Combination> combinationRepository,
            IRepository<NonregisteredPlayerTicket> nonregisteredPlayerTicketsRepository,
            IRepository<NonregisteredPlayer> nonregisteredPlayerRepository,
            IMapper mapper
            )
        {
            this.drawRepository = drawRepository;
            this.drawNumbersRepository = drawNumbersRepository;
            this.ticketRepository = ticketRepository;
            this.combinationRepository = combinationRepository;
            this.nonregisteredPlayerTicketsRepository = nonregisteredPlayerTicketsRepository;
            this.nonregisteredPlayerRepository = nonregisteredPlayerRepository;
            this.mapper = mapper;
        }
        public DrawDto GetDraw(int id)
        {
            var draw = drawRepository.GetById(id) ?? throw new NotFoundException();   

            return mapper.Map<DrawDto>(draw);
        }
        public DrawDto GetActiveDraw()
        {
            var draw = drawRepository.Query().WhereActiveDraw().FirstOrDefault() ?? throw new NotFoundException();

            return mapper.Map<DrawDto>(draw);
        }
        public IEnumerable<DrawDto> GetDraws()
        {
            var draws = drawRepository.Query().Select(d => mapper.Map<DrawDto>(d));

            return draws.ToList();
        }
        public void InitiateDemoDraw()
        {
            var date = DateTime.Now;

            var demoDraw = new Draw(date, date, date);
            drawRepository.Create(demoDraw);


            for (int i=1; i<=51; i++)
            {
                Random random = new();
                var nums = Enumerable.Range(1, 37).OrderBy(x => random.Next()).Take(7).ToArray();

                var player = new NonregisteredPlayer($"Demo Player {i}", $"player{i}@mail.com", 5);
                player.CreateTicket(nums, demoDraw);

                nonregisteredPlayerRepository.Create(player);
            }

            demoDraw.DrawNums();

            var validTickets = ticketRepository.Query()
                                                    .Include(x => x.Draw)
                                                    .Where(x => x.Draw!.Id == demoDraw.Id)
                                                    .ToList();

            var combinations = combinationRepository.Query();

            foreach (var ticket in validTickets)
            {
                ticket.NumbersGuessed = demoDraw.DrawNumbers.Select(x => x.Number)
                                                        .Intersect(combinations
                                                            .Where(comb => comb.TicketId == ticket.Id)
                                                            .Select(comb => comb.Number))
                                                        .ToList()
                                                        .Count;

                ticket.AssignPrize(ticket.NumbersGuessed);
                ticketRepository.Update(ticket);
            }

            drawRepository.Update(demoDraw);
        }
        public IEnumerable<DateTime> GetConcludedDrawsDates()
        {
            return drawRepository.Query().WhereConcludedDraw().Select(x => x.DrawTime);
        }
        public IEnumerable<WinnersDto> DisplayWinners(DateTime drawTime = default)
        {
            const int LowestPrizeValue = 3;

            var winners = new List<WinnersDto>();
            var draw = drawTime == default ? drawRepository.Query().WhereConcludedDraw().OrderBy(x => x.Id).LastOrDefault()
                : drawRepository.Query().Where(x => x.DrawTime == drawTime).FirstOrDefault();

            if(draw == null)
            {
                throw new Exception("There are no concluded draws yet.");
            }

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