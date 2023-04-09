using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Web.Application.Features.Order.Commands
{
    public class CreateOrderCommand :  IRequest<bool>
    {
        public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
        {
            public Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
