﻿using Loto3000.Application.Dto.Draw;
using Loto3000.Application.Dto.Tickets;
using Loto3000.Domain.Entities;

namespace Loto3000.Application.Services
{
    public interface IDrawService
    {
        DrawDto GetDraw(int id);
        DrawDto? GetActiveDraw();
        IEnumerable<DrawDto> GetDraws();
        DrawDto ActivateDrawSession();
        DrawDto InitiateDraw();
    }
}