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
            RuleFor(c => c.Name).MinimumLength(3).WithMessage("Kategori adı min 3 karakter olmalıdır.");
            RuleFor(c => c.Name).MaximumLength(15).WithMessage("Kategori adı max 15 karakter olmalıdır.");
        }

    }
}
