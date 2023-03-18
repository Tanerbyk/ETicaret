using ETicaret.Shared.Application.Features.Category.Commands;
using FluentValidation;

namespace ETicaret.Management.Validators
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Kategori alanı boş bırakılamaz.");
            RuleFor(c => c.Name).MinimumLength(2).WithMessage("Kategori adı min 3 karakter olmalıdır.");
            RuleFor(c => c.Name).MaximumLength(10).WithMessage("Kategori adı max 15 karakter olmalıdır.");
        }
    }
}
