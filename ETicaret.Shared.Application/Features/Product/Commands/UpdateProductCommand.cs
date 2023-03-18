using ETicaret.Shared.Dal;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Shared.Application.Features.Product.Commands
{
    public class UpdateProductCommand : IRequest<string>

    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int Stock { get; set; }

        public string Path { get; set; }

        public int CategoryID { get; set; }

        public int Price { get; set; }
        public int Discount { get; set; }

        public IFormFile fileImage { get; set; }
        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, string>
        {
            private readonly MarketPlaceDbContext _db;
            private readonly string _phsyicalPath;


            public UpdateProductCommandHandler(MarketPlaceDbContext db, IOptions<FilePathOptions> options)
            {
                _db = db;
                _phsyicalPath = System.IO.Path.Combine(options.Value.RootPath, options.Value.GetByKey(FileKeys.Products).Key);
            }

            public async Task<string> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var data = _db.Products.Where(p => p.Id == request.Id).FirstOrDefault();


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

                    data.Name = request.Name;
                    data.Description = request.Description;
                    data.Stock = request.Stock;
                    data.Price = request.Price;
                    data.Path = request.Path;
                    data.Discount = request.Discount;
                    data.CategoryID = request.CategoryID;
                   await  _db.SaveChangesAsync ();

                    return "success";
                    
                }
                else
                {
                    return "error";
                    
                }

                

                


            }
        }
    }
}
