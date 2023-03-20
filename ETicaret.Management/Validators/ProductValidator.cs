using ETicaret.Shared.Application.DTOs;
using ETicaret.Shared.Application.Features.Product.Commands;
using ETicaret.Shared.Dal.Concrete;
using FluentValidation;

namespace ETicaret.Management.Validators
{
    public class ProductValidator : AbstractValidator<UProductDto>
    {
        public ProductValidator()
        {
            RuleFor(c => c.Name).MinimumLength(2).WithMessage("Ürün adı alanı min 2 karakter olmalıdır.");
            RuleFor(c => c.Name).MaximumLength(100).WithMessage("Ürün adı alanı max 100 karakter olmalıdır.");
            RuleFor(c => c.Price).GreaterThan(0).WithMessage("Ürünün fiyatı 0 dan büyük olmalıdır ");
            RuleFor(c => c.Description).MinimumLength(10).WithMessage("Ürün açıklaması min 10 karakter olmalıdır.");
            RuleFor(c => c.Description).MaximumLength(200).WithMessage("Ürün açıklaması max 200 karakter olmalıdır.");
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Description).NotEmpty();

        }
    }
}
