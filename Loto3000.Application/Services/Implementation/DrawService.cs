using AutoMapper;
using Loto3000.Application.Dto.Draw;
using Loto3000.Application.Repositories;
using Loto3000.Domain.Entities;

namespace Loto3000.Application.Services.Implementation
{
    public class DrawService : IDrawService
    {
        private readonly IRepository<Draw> drawRepository;
        //private readonly IRepository<Session> sessionRepository;
        private readonly IMapper mapper;
        public DrawService(IRepository<Draw> drawRepository/*,IRepository<Session> sessionRepository*/, IMapper mapper)
        {
            this.drawRepository = drawRepository;
            //this.sessionRepository = sessionRepository;
            this.mapper = mapper;
        }
        public DrawDto GetDraw(int id)
        {
            var draw = drawRepository.GetById(id);
            if (draw == null)
            {
                throw new Exception("draw not found");
            }

            return mapper.Map<DrawDto>(draw);
        }
        public DrawDto ActivateDrawSession() 
        {
            //int sequence = 1;
            //if(drawRepository.GetAll().Any())
            //{
            //    sequence += drawRepository.GetAll().LastOrDefault(x => x.DrawSequenceNumber != sequence).DrawSequenceNumber;
            //}

            //var session = new Session(new DateTime(2022, 7, 31), new DateTime(2022, 8, 31)); 

            var draw = new Draw(new List<Ticket>(), new DateTime(2022, 8, 31), new DateTime(2022, 7, 31), new DateTime(2022, 8, 31)); // засеа со закуцани и едноставни вредности, понатаму да се автоматизира!!!

            //sessionRepository.Create(session);
            drawRepository.Create(draw);

            return mapper.Map<DrawDto>(draw);
        }
        public DrawDto InitiateDraw()
        {
            var draw = drawRepository.GetAll().WhereActiveDraw().FirstOrDefault() ?? throw new Exception("No draws yet.");
            //var session = sessionRepository.GetAll().LastOrDefault() ?? throw new Exception("Session not found.");

            if ((DateTime.Today.Day) != draw.SessionEnd.Day)
            {
                throw new Exception("Draw can't be initiated before the draw session ends.");
            }

            drawRepository.Update(draw);
            //sessionRepository.Delete(session);

            return mapper.Map<DrawDto>(draw);   
        } 
    }
}