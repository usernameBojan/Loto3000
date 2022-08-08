using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000.Domain.Models
{
    public class Draw
    {
        public Draw() { }
        public Draw(int id, int drawOrderNum, IList<Ticket> tickets, IList<User> users, DateTime drawTime)
        {
            DrawNumbers drawNums = new DrawNumbers();

            Id = id;
            DrawSequenceNumber = drawOrderNum;
            Tickets = tickets;
            Users = users;
            DrawTime = drawTime;
            DrawNumbers = drawNums.DrawNums();
        }
        public int Id { get; set; }
        public int DrawSequenceNumber { get; set; }
        public IList<DrawNumbers> DrawNumbers { get; set; } = new List<DrawNumbers>();
        public IList<Ticket> Tickets { get; set; } = new List<Ticket>();    
        public IList<User> Users { get; set; } = new List<User>();
        public DateTime DrawTime { get; set; }  
    }
}
