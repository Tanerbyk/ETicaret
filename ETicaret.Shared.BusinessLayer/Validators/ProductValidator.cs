﻿using ETicaret.Shared.Dal.Concrete;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.BusinessLayer.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("xd");
            RuleFor(x => x.Stock).LessThan(100).WithMessage("Stok miktarı 100 den çok olamaz");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Fiyat boş geçilemez");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Ürün Açıklaması Boş geçilemez");
            RuleFor(x => x.Stock).NotEmpty().WithMessage("Stok Miktarı Boş geçilemez");





        }
    }
}
