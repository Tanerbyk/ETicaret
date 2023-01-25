using ETicaret.Management.Models;
using ETicaret.Shared.Application.Features.Category.Queries;
using ETicaret.Shared.Application.Features.Product.Commands;
using ETicaret.Shared.Application.Features.Product.Queries;
using ETicaret.Shared.Repository.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.Management.Controllers
{
    public class ProductController : Controller
    {

        private readonly IMediator _mediator;


        public ProductController(IMediator mediator)
        {

            _mediator = mediator;
        }

        public async Task<IActionResult> ListProduct()
        {

            var values = await _mediator.Send(new GetAllProductQuery());
            return View(values);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var values = await _mediator.Send(new GetAllCategoryQuery());
            return View(values);


        }

        [HttpPost]
        public async Task<string> Create(CreateProductCommand c)
        {

            var data = await _mediator.Send(c);
            return data;


        }


        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int Id)
        {
            CategoriesWithProduct categoriesWithProduct = new CategoriesWithProduct
            {
                Categories = await _mediator.Send(new GetAllCategoryQuery()),
                Product = await _mediator.Send(new GetByIdProductQuery { Id = Id })
            };

            return View(categoriesWithProduct);

        }
        [HttpPost]
        public async Task<string> UpdateProduct(UpdateProductCommand p)
        {

           var data =  await _mediator.Send(p);
            return data;

        }


        
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _mediator.Send(new DeleteProductCommand { Id = id });
            return RedirectToAction("ListProduct");
        }





    }
}
