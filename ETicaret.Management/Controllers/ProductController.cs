using ETicaret.Management.Models;
using ETicaret.Shared.Application.DTOs;
using ETicaret.Shared.Application.Features.Category.Queries;
using ETicaret.Shared.Application.Features.Product.Commands;
using ETicaret.Shared.Application.Features.Product.Queries;
using FormHelper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nest;

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
            CategoriesWithProductDTO categoriesWithProduct = new();
       var data = await _mediator.Send(new GetAllCategoryQuery());
            categoriesWithProduct.Categories = data;
            return View(categoriesWithProduct);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand c)
        {

            if(!ModelState.IsValid)
            {
                return FormResult.CreateErrorResult("An error occurred!");
            }
            var data = await _mediator.Send(c);
            return Ok(data);

        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int Id)
        {         
            ViewBag.categories = await _mediator.Send(new GetAllCategoryQuery());          
            var product = await _mediator.Send(new GetByIdProductQuery { Id=Id});           
            return View(product);
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
