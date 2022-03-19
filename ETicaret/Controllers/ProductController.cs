﻿using ETicaret.Shared.Repository.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient.Memcached;
using System.IO;

namespace ETicaret.Web.Controllers
{
    public class ProductController : Controller
    {

        IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {

         var values =  _unitOfWork.Products.GetAll();        
            return View(values);

        }

        public IActionResult ProductDetail(int id)
        {

            var values = _unitOfWork.Products.Get(id);
            return View(values);

        }
    }
}
