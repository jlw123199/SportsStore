﻿using System.Linq;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        public CartController(IProductRepository repo)
        {
            repository = repo;
   
        }

        public ViewResult Index(Cart cart,string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart,int productId, string
        returnUrl)
        {
            Product product = repository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                //GetCart().AddItem(product, 1);
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string
        returnUrl)
        {
            Product product = repository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                //GetCart().RemoveLine(product);
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
    }
}