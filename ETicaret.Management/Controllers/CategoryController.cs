using ETicaret.Shared.Application.Enums;
using ETicaret.Shared.Application.Extensions;
using ETicaret.Shared.Application.Features.Category.Commands;
using ETicaret.Shared.Application.Features.Category.Queries;
using ETicaret.Shared.Application.Validators.Category;
using ETicaret.Shared.Dal.Concrete;
using ETicaret.Shared.Repository.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ETicaret.Management.Controllers
{
    public class CategoryController : Controller
    {

      
        private readonly IMediator _mediator;

        public CategoryController( IMediator mediator)
        {
            
            _mediator = mediator;
        }

        [HttpGet]

        public  IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> AddCategory(CreateCategoryCommand c)
        {
            CreateCategoryRequestValidator validator = new CreateCategoryRequestValidator();
            FluentValidation.Results.ValidationResult result = validator.Validate(c);
            if (result.Errors.Any())
            {
                var error = result.Errors.FirstOrDefault();
                return error.ToString();
            }
            var data = await _mediator.Send(c);
            return data;
        }
        public async Task<IActionResult> ListCategory()
        {
            var values = await _mediator.Send(new GetAllCategoryQuery());
            return View(values);
        }


        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id )
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery { Id = id});
            return View(category);

        }

        [HttpPost]
        public async Task<string> UpdateCategory(UpdateCategoryCommand c)
        {

            var data = await _mediator.Send(c);
            return data;

        }
        public async Task<IActionResult> DeleteCategory(DeleteCategoryCommand c)
        {
            var data = await _mediator.Send(c);
            return RedirectToAction("ListCategory", "Category", new { data });

        }

    }
}
