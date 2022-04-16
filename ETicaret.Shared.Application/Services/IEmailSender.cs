using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.Services
{
    public interface IEmailSender
    {
        Task SendAsync(MailRequest request);
    }
}
