using Loto3000.Application.Dto.Draw;

namespace Loto3000.Application.Services
{
    public interface IDrawService
    {
        DrawDto ActivateDrawSession();
        DrawDto InitiateDraw();
        DrawDto GetDraw(int id);
    }
}