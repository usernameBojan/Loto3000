﻿using Loto3000.Application.Dto.Player;

namespace Loto3000.Application.Services
{
    public interface IPlayerService
    {
        PlayerDto GetPlayer(int id);
        IEnumerable<PlayerDto> GetPlayers();
        PlayerDto RegisterPlayer(RegisterPlayerDto dto);
        void DeletePlayer(int id);
    }
}