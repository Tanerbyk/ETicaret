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
            RuleFor(x => x.fileImage)
           .NotNull().WithMessage("Lütfen dosya yükleyin.")
           .Must(file => file.Length > 0).WithMessage("Dosya boş olamaz.")
           .Must(file => file.Length <= 5 * 1024 * 1024).WithMessage("Dosya boyutu 5MB'tan küçük olmalıdır.");





        }

        private bool BeAValidPhoto(IFormFile file)
        {
         

            // Fotoğrafın türünü kontrol etmek için, dosya uzantısına bakabilirsiniz.
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".bmp" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(fileExtension))
            {
                return false;
            }

            // Fotoğrafın boyutunu kontrol etmek için, dosya boyutunu kontrol edebilirsiniz.
            var maxFileSizeInBytes = 10 * 1024 * 1024; // 10 MB
            if (file.Length > maxFileSizeInBytes)
            {
                return false;
            }

            // Fotoğraf geçerli ise true döndürün.
            return true;
        }
    }
}
