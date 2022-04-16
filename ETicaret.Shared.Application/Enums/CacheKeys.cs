using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.Enums
{
    public enum CacheKeys
    {
        [Display(Name="Kategori")]
        Category,
        [Display(Name = "Kategoriler")]
        CategoryList
    }
}
