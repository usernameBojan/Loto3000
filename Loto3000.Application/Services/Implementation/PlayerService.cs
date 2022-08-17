using AutoMapper;
using Loto3000.Application.Dto.Player;
using Loto3000.Application.Repositories;
using Loto3000.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000.Application.Services.Implementation
{
    public class PlayerService : IPlayerService
    {
        private readonly IRepository<Player> playerRepository;
        private readonly IMapper mapper; 
        public PlayerService(IRepository<Player> playerRepository, IMapper mapper)
        {
            this.playerRepository = playerRepository;
            this.mapper = mapper;   
        }
        public PlayerDto GetPlayer(int id)
        {
            var player = playerRepository.GetById(id);
            if (player == null)
            {
                throw new Exception("Player not found");
            }

            return mapper.Map<PlayerDto>(player);
        }
        public IEnumerable<PlayerDto> GetPlayers()
        {
            return playerRepository.GetAll()
                                   .Select(p => mapper.Map<PlayerDto>(p))
                                   .ToList();
        }
        public PlayerDto RegisterPlayer(RegisterPlayerDto dto)
        {
            var players = this.GetPlayers();
                
            foreach(var existingPlayer in players)
            {
                if(dto.Username == existingPlayer.Username)
                {
                    throw new Exception("Username already exists.");
                }
                if (dto.Email == existingPlayer.Email)
                {
                    throw new Exception("This email is connected with another account.");
                };
            }

            var player = mapper.Map<Player>(dto);
            playerRepository.Create(player);

            return mapper.Map<PlayerDto>(dto);
        }
        public void DeletePlayer(int id)
        {
            var player = playerRepository.GetById(id);
            if (player == null)
            {
                throw new Exception("Player not found");
            }

            playerRepository.Delete(player);
        }
    }
}
