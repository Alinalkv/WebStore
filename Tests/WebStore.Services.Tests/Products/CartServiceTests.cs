using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using WebStore.Domain.Models;
using WebStore.Domain.ViewModels;
using Assert = Xunit.Assert;

namespace WebStore.Services.Tests.Products
{
    [TestClass]
    public class CartServiceTests
    {
        [TestMethod]
        public void Cart_Class_ItemsCount_Returns_Correct_Quantity()
        {
            var Cart = new Cart
            {
                Items = new List<CartItem>
                {
                      new CartItem { ProductId = 1, Quantity = 1 },
                      new CartItem { ProductId = 2, Quantity = 3 },
                 }
            };

            const int exp_count = 4;
            var actual_count = Cart.ItemsCount;
            Assert.Equal(exp_count, actual_count);
        }

        [TestMethod]
        public void CartViewModel_Returns_Correct_Quantity()
        {
            var CartViewModel = new CartViewModel
            { 
             Items = new []
             {
                   ( new ProductViewModel {Id = 1, Name = "Product 1", Price = 0.5m}, 1 ),
                    ( new ProductViewModel {Id = 2, Name = "Product 2", Price = 1.5m}, 3 ),
             }
             };
            const int exp_count = 4;
            var actual_count = CartViewModel.ItemsCount;
            Assert.Equal(exp_count, actual_count);
        }
    }
}
