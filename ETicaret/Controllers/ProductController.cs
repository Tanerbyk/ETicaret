using AutoMapper;
using ETicaret.Shared.Application.DTOs;
using ETicaret.Shared.Application.Features.Product.Queries;
using ETicaret.Shared.Dal;
using ETicaret.Shared.Dal.Concrete;
using ETicaret.Shared.Repository.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient.Memcached;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaret.Web.Controllers
{
    [AllowAnonymous]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
       
        public ProductController(IMapper mapper, IMediator mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {    
            var products = await _mediator.Send(new GetAllProductQuery());
            var productDto = _mapper.Map<List<ProductDto>>(products);
            return View(productDto);
        }
        [HttpGet]
        public async Task<IActionResult> ProductGetByIdCategory(int id)
        {
            var products = await _mediator.Send(new GetAllProductQuery());
            var productDto = _mapper.Map<List<ProductDto>>(products);
            var filter = productDto.Where(x => x.Category.Id == id).ToList();
            return View(filter);
        }
     
        public async Task<IActionResult> ProductDetail(int id)
        {
            
            var product =  await _mediator.Send(new GetByIdProductQueryWeb{ Id = id });
            return View(product);

        }
       
    }
}
