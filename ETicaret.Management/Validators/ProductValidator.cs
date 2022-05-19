using ETicaret.Shared.Dal.Concrete;
using FluentValidation;

namespace ETicaret.Management.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Description).NotEmpty();
        }
    }
}
