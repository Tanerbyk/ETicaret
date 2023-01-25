using ETicaret.Shared.Application.Validators.Product;
using ETicaret.Shared.Dal;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.Features.Product.Commands
{
    public class CreateProductCommand : IRequest<string>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Stock { get; set; }

        public string Path { get; set; }

        public int CategoryID { get; set; }

        public int Price { get; set; }
        public int Discount { get; set; }


        public   IFormFile fileImage { get; set; }


        
        

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, string>
        {
            private readonly MarketPlaceDbContext _db;
            private readonly string _phsyicalPath;


            public CreateProductCommandHandler(MarketPlaceDbContext db, IOptions<FilePathOptions> options)
            {
                _db = db;
                _phsyicalPath = System.IO.Path.Combine(options.Value.RootPath, options.Value.GetByKey(FileKeys.Products).Key);

            }
             
            public async Task<string> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var validator = new CreateProductRequestValidator();
                var result = validator.Validate(new Dal.Concrete.Product());
               
                try
                {
                    if (request.fileImage is not null)
                    {
                        var ex = System.IO.Path.GetExtension(request.fileImage.FileName);
                        var newname = Guid.NewGuid() + ex;
                        var filePath = System.IO.Path.Combine(_phsyicalPath, newname);
                        request.Path = newname;


                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            request.fileImage.CopyTo(fileStream);
                        }


                        await _db.Products.AddAsync(new Dal.Concrete.Product { Name = request.Name, Description = request.Description, Stock = request.Stock, Path = request.Path, CategoryID = request.CategoryID, Price = request.Price, Discount = request.Discount });
                        await _db.SaveChangesAsync();
                        return "success";
                    }
                    else
                    {
                        foreach (var failure in result.Errors)
                        {
                            Console.WriteLine($"Property: {failure.PropertyName} Error Code: {failure.ErrorCode}");
                        }
                        return "error"; ;
                    }
                }
                catch (Exception)
                {

                    return "error"; ;
                }
           

                
                    
                       


                
                
            }
        }

    }
}
