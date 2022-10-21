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
        public DrawDto ActivateDrawSession() 
        {
            var draw = new Draw();
            draw.SetDrawSession();
            
            drawRepository.Create(draw);

            return mapper.Map<DrawDto>(draw);
        }
        public DrawDto InitiateDraw()
        {
            var draw = drawRepository.Query().WhereActiveDraw().FirstOrDefault() ?? throw new NotFoundException("No draws yet.");

            if ((DateTime.Today.Day) != draw.DrawTime.Day)
            {
                throw new ValidationException("Draw can't be initiated before the draw session ends.");
            }

            if (drawNumbersRepository.Query().Count() == 8)
            {
                throw new ValidationException("One draw can only have one winning combination.");
            }

            draw.DrawNums();

            var validTickets = ticketRepository.Query()
                                               .Include(x => x.Draw)
                                               .Where(x => x.Draw!.Id == draw.Id)
                                               .ToList();

            var combinations = combinationRepository.Query();

            foreach(var ticket in validTickets)
            {
                ticket.NumbersGuessed = draw.DrawNumbers.Select(x => x.Number)
                                                        .Intersect(combinations
                                                            .Where(comb => comb.TicketId == ticket.Id)
                                                            .Select(comb => comb.Number))
                                                        .ToList()
                                                        .Count;
                
                ticket.AssignPrize(ticket.NumbersGuessed);
                ticketRepository.Update(ticket);
            }

            drawRepository.Update(draw);

            ActivateDrawSession();

            return mapper.Map<DrawDto>(draw);   
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
                if (((int)ticket.Prize) >= LowestPrizeValue)
                {
                    WinnersDto winner = new(ticket.Player!.FullName, ticket.CombinationNumbersString, ticket.Prize);

                    winners.Add(winner);
                }
            }

            foreach (var ticket in nonregisteredPlayersTickets)
            {
                if (((int)ticket.Prize) >= LowestPrizeValue)
                {
                    WinnersDto winner = new(ticket.NonregisteredPlayer!.FullName, ticket.CombinationNumbersString, ticket.Prize);

                    winners.Add(winner);
                }
            }

            return winners;
        }
    }
}