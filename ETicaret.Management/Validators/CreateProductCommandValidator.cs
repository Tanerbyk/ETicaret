using ETicaret.Management.Models;
using ETicaret.Shared.Application.DTOs;
using FluentValidation;

namespace ETicaret.Management.Validators
{
    public class CreateProductCommandValidator :  AbstractValidator<CategoriesWithProductDTO>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(c => c.Name).MinimumLength(2).WithMessage("Ürün adı alanı min 2 karakter olmalıdır.");
            RuleFor(c => c.Name).MaximumLength(100).WithMessage("Ürün adı alanı max 100 karakter olmalıdır.");
            RuleFor(c => c.Description).MinimumLength(10).WithMessage("Ürün açıklaması min 10 karakter olmalıdır.");
            RuleFor(c => c.Description).MaximumLength(200).WithMessage("Ürün açıklaması max 200 karakter olmalıdır.");
            RuleFor(x => x.fileImage).NotNull().WithMessage("Ürün görseli seçimi yapmalısınız.");
            RuleFor(x => x.fileImage)
    .NotNull().WithMessage("Dosya yükleyiniz.")
    .Must(file => file.Length < 1024 * 1024).WithMessage("Dosya boyutu 1 MB'dan küçük olmalıdır.");
            RuleFor(x => x.fileImage).SetValidator(new FileValidator());



        }
    }
}
