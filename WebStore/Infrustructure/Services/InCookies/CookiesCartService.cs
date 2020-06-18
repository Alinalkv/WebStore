using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrustructure.Interfaces;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Infrustructure.Services.InCookies
{
    public class CookiesCartService : ICartService
    {
        private readonly IProductData _ProductData;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly string _CartName;

        //HttpContextAccessor - извлекаем cookies
        public CookiesCartService(IProductData ProductData, IHttpContextAccessor HttpContextAccessor)
        {
            _ProductData = ProductData;
            _HttpContextAccessor = HttpContextAccessor;

            var user = _HttpContextAccessor.HttpContext.User;
            var user_name = user.Identity.IsAuthenticated ? user.Identity.Name : null;

            _CartName = $"WebStore.Cart[{user_name}]";
        }

        public Cart Cart
        {
            get
            {
                var context = _HttpContextAccessor.HttpContext;
                var cookies = context.Response.Cookies;
                var cart_cookie = context.Request.Cookies[_CartName];
                
                if(cart_cookie is null)
                {
                    var cart = new Cart();
                    cookies.Append(_CartName, JsonConvert.SerializeObject(cart));
                    return cart;
                }

                ReplaceCookie(cookies, cart_cookie);
                return JsonConvert.DeserializeObject<Cart>(cart_cookie);
            }
            set
            {
                ReplaceCookie(_HttpContextAccessor.HttpContext.Response.Cookies, JsonConvert.SerializeObject(value));
            }
        }

        private void ReplaceCookie(IResponseCookies cookies, string cookie)
        {
            cookies.Delete(_CartName);
            cookies.Append(_CartName, cookie);
        }
       
        
       /// <summary>
       /// Добавление в корзину продуктов
       /// </summary>
       /// <param name="id"></param>
        public void AddToCart(int id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(c => c.ProductId == id);
            if(item is null)
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

        public void RemoveAll()
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null)
                return;
            cart.Items.Remove(item);
            Cart = cart;
        }

        public void RemoveFromCart(int id)
        {
            var cart = Cart;
            cart.Items.Clear();
            Cart = cart;
        }

        public CartViewModel TransformFromCart()
        {
            throw new NotImplementedException();
        }
    }
}
