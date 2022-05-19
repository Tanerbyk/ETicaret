using ETicaret.Shared.Application.Features.Category.Commands;
using ETicaret.Shared.Dal;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.Validators.Category
{
    public class CreateCategoryRequestValidator: AbstractValidator<CreateCategoryCommand>
    {
       
        public CreateCategoryRequestValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Kategori alanı boş bırakılamaz.");
        }

    }
}
