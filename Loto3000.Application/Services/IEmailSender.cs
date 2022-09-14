using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000.Application.Services
{
    public interface IEmailSender
    {
        void SendEmail(string subject, string body, string recieverEmailAddress);
    }
}
