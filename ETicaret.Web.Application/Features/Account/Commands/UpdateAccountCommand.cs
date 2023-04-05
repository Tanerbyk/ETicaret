
using ETicaret.Shared.Dal;
using ETicaret.Shared.Dal.Concrete;
using ETicaret.Shared.Dal.Web;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.Features.Account.Commands
{
    public class UpdateAccountCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, bool>
        {

            private readonly UserManager<WebUser> _userManager;

            public UpdateAccountCommandHandler( UserManager<WebUser> userManager)
            {
                
                _userManager = userManager;
            }

            public async Task<bool> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
            {
                var user =  _userManager.Users.FirstOrDefault(x => x.Id == request.UserId);

                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Email = request.Email;
                await _userManager.UpdateAsync(user);
                return true;
            }
        }
    }
}
