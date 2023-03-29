
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.Features.Account.Commands
{
    public class UpdateAccountCommand : IRequest<bool>
    {
        public int userId { get; set; }
        public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, bool>
        {
            public Task<bool> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
