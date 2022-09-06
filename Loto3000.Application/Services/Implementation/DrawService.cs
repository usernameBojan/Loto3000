using AutoMapper;
using Loto3000.Application.Dto.Draw;
using Loto3000.Application.Dto.Tickets;
using Loto3000.Application.Dto.Winners;
using Loto3000.Application.Repositories;
using Loto3000.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Loto3000.Application.Services.Implementation
{
    public class DrawService : IDrawService
    {
        private readonly IRepository<Draw> drawRepository;
        private readonly IRepository<DrawNumbers> drawNumbersRepository;
        private readonly IRepository<Player> playerRepository;
        private readonly IRepository<Ticket> ticketRepository;
        private readonly IRepository<Combination> combinationRepository;
        private readonly IMapper mapper;
        public DrawService(
            IRepository<Draw> drawRepository, 
            IRepository<DrawNumbers> drawNumbersRepository, 
            IRepository<Ticket> ticketRepository, 
            IRepository<Combination> combinationRepository, 
            IRepository<Player> playerRepository,
            IMapper mapper
            )
        {
            this.drawRepository = drawRepository;
            this.drawNumbersRepository = drawNumbersRepository;
            this.playerRepository = playerRepository;
            this.ticketRepository = ticketRepository;
            this.combinationRepository = combinationRepository;
            this.mapper = mapper;
        }
        public DrawDto GetDraw(int id)
        {
            var draw = drawRepository.GetById(id) ?? throw new Exception("draw not found");   

            return mapper.Map<DrawDto>(draw);
        }
        public DrawDto? GetActiveDraw()
        {
            var draw = drawRepository.Query().WhereActiveDraw().FirstOrDefault();

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
        //GUIDELINES FOR TESTING
        //MAKE SURE THERE ARE NO DRAWS IN THE DATABASE
        //COMMENT THE FIRST IF STATEMENT IN InitiateDraw() WHICH CHECKS IF DRAW DATE IS EQUAL TO TODAYS DATE
        //GO TO Draw.cs AND FOLLOW THE COMMENTS FOR GUIDELINES THERE
        public DrawDto InitiateDraw()
        {
            var draw = drawRepository.Query().WhereActiveDraw().FirstOrDefault() ?? throw new Exception("No draws yet.");

            if ((DateTime.Today.Day) != draw.DrawTime.Day)
            {
                throw new Exception("Draw can't be initiated before the draw session ends.");
            }

            if (drawNumbersRepository.Query().Count() == 8)
            {
                throw new Exception("One draw can only have one winning combination.");
            }

            draw.DrawNums();

            var validTickets = ticketRepository.Query()
                                               .Include(x => x.Draw)
                                               .Where(x => x.Draw.Id == draw.Id)
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

                ticket.GetPrize(ticket.NumbersGuessed);
            }

            foreach(var ticket in validTickets)
            {
                ticketRepository.Update(ticket);    
            }

            drawRepository.Update(draw);

            ActivateDrawSession();

            return mapper.Map<DrawDto>(draw);   
        }

        //public IEnumerable<WinnersDto> DisplayWinners()
        //{
        //    var winners = new List<WinnersDto>();

            
        //}
    }
}