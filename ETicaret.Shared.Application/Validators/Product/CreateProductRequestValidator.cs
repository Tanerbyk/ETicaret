using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.Validators.Product
{
    public class CreateProductRequestValidator : AbstractValidator<Dal.Concrete.Product>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ürün Adı Kısmı Boş Geçilemez");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama Kısmı Boş Geçilemez"); 
            RuleFor(x => x.Discount).IsInEnum().WithMessage("İndirim oranı sadece rakamlardan oluşmalıdır.");
            RuleFor(x => x.Stock).GreaterThan(-1).WithMessage("Stok miktarı 0 veya 0'dan büyük olmalıdır.");
        }
    }
}
