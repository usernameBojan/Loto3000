﻿using AutoMapper;
using HashidsNet;
using Loto3000.Application.Dto.Tickets;
using Loto3000.Application.Repositories;
using Loto3000.Application.Utilities;
using Loto3000.Domain.Entities;
using Loto3000.Domain.Exceptions;

namespace Loto3000.Application.Services.Implementation
{
    public class TicketService : ITicketService
    {
        private readonly IEmailSender emailSender;
        private readonly IMapper mapper;
        private readonly IHashids hashids;
        private readonly IRepository<Ticket> ticketRepository;
        private readonly IRepository<NonregisteredPlayerTicket> nonregisteredPlayerTicketRepository;
        private readonly IRepository<Player> playerRepository;
        private readonly IRepository<Draw> drawRepository;
        private readonly IRepository<NonregisteredPlayer> nonregisteredPlayerRepository;

        public TicketService(
            IEmailSender emailSender,
            IMapper mapper,
            IHashids hashids,
            IRepository<Ticket> ticketRepository,
            IRepository<NonregisteredPlayerTicket> nonregisteredPlayerTicketRepository,
            IRepository<Player> playerRepository,
            IRepository<Draw> drawRepository,
            IRepository<NonregisteredPlayer> nonregisteredPlayerRepository
            )
        {
            this.emailSender = emailSender;
            this.mapper = mapper;
            this.hashids = hashids;
            this.ticketRepository = ticketRepository;
            this.nonregisteredPlayerTicketRepository = nonregisteredPlayerTicketRepository;
            this.playerRepository = playerRepository;
            this.drawRepository = drawRepository;
            this.nonregisteredPlayerRepository = nonregisteredPlayerRepository;
        }
        public TicketDto GetPlayerTicket(int id, int ticketId)
        {
            var ticket = ticketRepository.Query()
                                         .Where(t => t.PlayerId == id)
                                         .FirstOrDefault(t => t.Id == ticketId);

            return mapper.Map<TicketDto>(ticket);
        }
        public TicketDto GetNonregisteredPlayerTicket(int ticketId)
        {
            var ticket = nonregisteredPlayerTicketRepository.Query().FirstOrDefault(t => t.Id == ticketId);

            return mapper.Map<TicketDto>(ticket);
        }
        public IEnumerable<TicketDto> GetAllTickets()
        {
            var tickets = ticketRepository.Query().Select(t => mapper.Map<TicketDto>(t));

            return tickets.ToList();
        }
        public IEnumerable<TicketDto> GetRegisteredPlayersTickets()
        {
            var tickets = ticketRepository.Query()
                                          .Where(x => x.PlayerId != null)
                                          .Select(t => mapper.Map<TicketDto>(t));

            return tickets.ToList();
        }
        public IEnumerable<TicketDto> GetNonregisteredPlayersTickets()
        {
            var tickets = nonregisteredPlayerTicketRepository.Query().Select(t => mapper.Map<TicketDto>(t));

            return tickets.ToList();
        }
        public IEnumerable<TicketDto> GetPlayerTickets(int id)
        {
            var tickets = ticketRepository.Query()
                                          .Where(t => t.PlayerId == id)
                                          .Select(t => mapper.Map<TicketDto>(t));

            return tickets.ToList();
        }
        public TicketDto CreateTicket(CreateTicketDto dto, int id)
        {
            var player = playerRepository.GetById(id) ?? throw new NotFoundException();

            var draw = drawRepository.Query().WhereActiveDraw().FirstOrDefault() ?? throw new NotFoundException("No draws yet");

            var ticket = player.CreateTicket(dto.CombinationNumbers, draw);

            playerRepository.Update(player);

            return mapper.Map<TicketDto>(ticket);
        }
        public TicketDto CreateTicketNonregisteredPlayer(CreateTicketNonregisteredPlayerDto dto)
        {
            NonregisteredPlayer player = new(dto.Fullname, dto.Email, (int)dto.DepositAmount);

            var draw = drawRepository.Query().WhereActiveDraw().FirstOrDefault() ?? throw new NotFoundException("No draws yet");

            var ticket = player.CreateTicket(dto.CombinationNumbers, draw);

            nonregisteredPlayerRepository.Create(player);

            string idCodePattern = $"{hashids.Encode(dto.Fullname.Length)}{ticket.CombinationNumbersString.Length}{player.Ticket!.Id + 9999}{hashids.Encode(player.Ticket.Id)}";

            emailSender.SendEmail(
                EmailContents.NonregisteredPlayerTicketSubject,
                EmailContents.NonregisteredPlayerTicketBody(
                    player.FullName, player.Ticket!.CombinationNumbersString, idCodePattern, DateTime.Now.ToString(), draw.DrawTime.ToString()), 
                dto.Email
                );

            return mapper.Map<TicketDto>(ticket); 
        }
    }
}