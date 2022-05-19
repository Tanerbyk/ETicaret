using ETicaret.Management.Models;
using ETicaret.Management.Validators;
using ETicaret.Shared.Application;
using ETicaret.Shared.Application.Features.Category.Queries;
using ETicaret.Shared.Application.Features.Product.Commands;
using ETicaret.Shared.Application.Features.Product.Queries;
using ETicaret.Shared.Dal.Concrete;
using ETicaret.Shared.Repository.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace ETicaret.Management.Controllers
{
    public class ProductController : Controller
    {



        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;


        public ProductController(IUnitOfWork unitOfWork, IMediator mediator)
        {
            _unitOfWork = unitOfWork;

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
        public class Response
        {
            public object Data { get; set; }
            public StatusCode Status { get; set; }
        }
        public enum StatusCode
        {

            Error,
            Success
        }
        public class SharedErrors
        {
            public string Property { get; set; }
            public string Message { get; set; }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int Id)
        {
            CategoriesWithProduct categoriesWithProduct = new CategoriesWithProduct
            {
                Categories = await _mediator.Send(new GetAllCategoryQuery()),
                Product = await _mediator.Send(new GetByIdProductQuery { Id =Id })
            };
           
            return View(categoriesWithProduct);



        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand p)
        {

            await _mediator.Send(p);
            return RedirectToAction("ListProduct", "Product");

        }


        [HttpPost]
        public async Task<string> Test(UpdateProductCommand p,IFormFile fileImage)
        {

            await _mediator.Send(p);
            return "success";

        }
        public async Task< IActionResult> DeleteProduct(int id)
        {
            await _mediator.Send(new DeleteProductCommand { Id = id });
            return RedirectToAction("ListProduct");
        }





    }
}
