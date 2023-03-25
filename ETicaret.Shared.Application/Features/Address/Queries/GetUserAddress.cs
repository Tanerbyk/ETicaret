using ETicaret.Shared.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.Features.Address.Queries
{
    public class GetUserAddress : IRequest<AddressDto>
    {
    }
}
