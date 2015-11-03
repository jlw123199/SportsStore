using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
  

        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        public int PageSize = 4;

        public ViewResult List( string category, int page = 1)
        {
            //return View(repository.Products
            //.OrderBy(p => p.ProductID)
            //.Skip((page - 1) * PageSize)
            //.Take(PageSize));


            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.Products
                .Where(p => p.Category == category || category == null)
.OrderBy(p => p.ProductID)
.Skip((page - 1) * PageSize)
.Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Where(p => p.Category == category || category == null).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }

        //public ViewResult List()
        //{
        //    return View(repository.Products);
        //}
    }
}