﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.DTO.Order;
using WebStore.Domain.Entities.Orders;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _CartService;

        public CartController(ICartService CartService)
        {
            _CartService = CartService;
        }

        public IActionResult Details() => View(new CartOrderViewModel
        {
             Cart = _CartService.TransformFromCart(),
              Order = new OrderViewModel()
        });

        public IActionResult AddToCart(int id)
        {
            _CartService.AddToCart(id);
            return RedirectToAction(nameof(Details));
        }

        public IActionResult DecrementFromCart(int id)
        {
            _CartService.DecrementFromCart(id);
            return RedirectToAction(nameof(Details));
        }

        public IActionResult RemoveFromCart(int id)
        {
            _CartService.RemoveFromCart(id);
            return RedirectToAction(nameof(Details));
        }

        public IActionResult RemoveAll()
        {
            _CartService.RemoveAll();
            return RedirectToAction(nameof(Details));
        }

        [HttpPost]
        public async Task<IActionResult> CheckOut(OrderViewModel Order, [FromServices] IOrderService OrderService)
        {
            if (!ModelState.IsValid)
                return View(nameof(Details), new CartOrderViewModel
                {
                    Cart = _CartService.TransformFromCart(),
                    Order = Order
                });

            var order_model = new CreateOrderModel
            {
                Order = Order,
                Items = _CartService.TransformFromCart().Items
                .Select(item => new OrderItemDTO
                { Id = item.product.Id,
                 Price = item.product.Price,
                  Quantity = item.Quantity,
                }).ToList()
            };

            var order = await OrderService.CreateOrder(User.Identity.Name, order_model);
            _CartService.RemoveAll();
            return RedirectToAction(nameof(OrderConfirmed), new { id = order.Id});
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

        #region WebAPI

        /// <summary>
        /// Получение компонента с кол-вом в корзине
        /// </summary>
        public IActionResult GetCartView => ViewComponent("Cart");


        public IActionResult AddToCartAPI(int id)
        {
            _CartService.AddToCart(id);
            return Json(new { id, message = $"Товар с id {id} добавлен в корзину"});
        }

        public IActionResult DecrementFromCartAPI(int id)
        {
            _CartService.DecrementFromCart(id);
            return Ok();
        }

        public IActionResult RemoveFromCartAPI(int id)
        {
            _CartService.RemoveFromCart(id);
            return Ok();
        }

        public IActionResult RemoveAllAPI()
        {
            _CartService.RemoveAll();
            return Ok();
        }


        #endregion
    }
}
