using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Loto3000.Application.Repositories;
using Loto3000.Domain.Entities;
using Loto3000.Domain.Exceptions;
using Loto3000.Application.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Loto3000.Application.Services.Implementation
{
    public class DrawBackgroundWorker : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        public DrawBackgroundWorker(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                using var scope = serviceProvider.CreateScope();

                var drawRepository = scope.ServiceProvider.GetRequiredService<IRepository<Draw>>();
                var ticketsRepository = scope.ServiceProvider.GetRequiredService<IRepository<Ticket>>();
                var nonregisteredPlayersTicketsRepository = scope.ServiceProvider.GetRequiredService<IRepository<NonregisteredPlayerTicket>>();
                var drawNumbersRepository = scope.ServiceProvider.GetRequiredService<IRepository<DrawNumbers>>();
                var combinationRepository = scope.ServiceProvider.GetRequiredService<IRepository<Combination>>();
                var draw = drawRepository.Query().WhereActiveDraw().FirstOrDefault() ?? throw new NotFoundException("No draws yet.");

                TimeSpan drawTime = new(draw.DrawTime.Ticks - DateTime.Now.Ticks);
                
                if ((DateTime.Today.Day) != draw.DrawTime.Day)
                {
                    throw new ValidationException("Draw can't be initiated before the draw session ends.");
                }

                if (drawNumbersRepository.Query().Count() == 8)
                {
                    throw new ValidationException("One draw can only have one winning combination.");
                }

                draw.DrawNums();

                var validTickets = ticketsRepository.Query()
                                                    .Include(x => x.Draw)
                                                    .Where(x => x.Draw!.Id == draw.Id)
                                                    .ToList();

                var combinations = combinationRepository.Query();

                foreach (var ticket in validTickets)
                {
                    ticket.NumbersGuessed = draw.DrawNumbers.Select(x => x.Number)
                                                            .Intersect(combinations
                                                                .Where(comb => comb.TicketId == ticket.Id)
                                                                .Select(comb => comb.Number))
                                                            .ToList()
                                                            .Count;

                    ticket.AssignPrize(ticket.NumbersGuessed);
                    ticketsRepository.Update(ticket);
                }
             
                drawRepository.Update(draw);

                var newDraw = new Draw();
                newDraw.SetDrawSession();
                drawRepository.Create(newDraw);

                await Task.Delay(drawTime, stoppingToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}