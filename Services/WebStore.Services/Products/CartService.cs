﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebStore.Domain.Entities;
using WebStore.Domain.Models;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Services.Products
{
   public class CartService: ICartService
    {
        private readonly IProductData _ProductData;
        private readonly ICartStore _CartStore;
        public Cart Cart { get => _CartStore.Cart; set => _CartStore.Cart = value; }
        //HttpContextAccessor - извлекаем cookies
        public CartService(IProductData ProductData, ICartStore CartStore)
        {
            _ProductData = ProductData;
            _CartStore = CartStore;
        }

        /// <summary>
        /// Добавление в корзину продуктов
        /// </summary>
        /// <param name="id"></param>
        public void AddToCart(int id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(c => c.ProductId == id);
            if (item is null)
            {
                cart.Items.Add(new CartItem { ProductId = id, Quantity = 1 });
            }
            else
            {
                item.Quantity++;
            }
            Cart = cart;
        }

        /// <summary>
        /// Уменьшение кол-ва товара
        /// </summary>
        /// <param name="id"></param>
        public void DecrementFromCart(int id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null)
                return;
            if (item.Quantity > 0)
                item.Quantity--;
            if (item.Quantity == 0)
                cart.Items.Remove(item);
            Cart = cart;
        }

        public void RemoveFromCart(int id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null)
                return;
            cart.Items.Remove(item);
            Cart = cart;
        }

        public void RemoveAll()
        {
            var cart = Cart;
            cart.Items.Clear();
            Cart = cart;
        }

        public CartViewModel TransformFromCart()
        {
            var products = _ProductData.GetProducts(new ProductFilter
            {
                Ids = Cart.Items.Select(p => p.ProductId).ToArray()
            });

            var product_view_models = products.Products.Select(p => p.FromDTO()).ToView().ToDictionary(p => p.Id);
            return new CartViewModel
            {
                Items = Cart.Items.Select(item => (product_view_models[item.ProductId], item.Quantity))
            };
        }
    }
}
