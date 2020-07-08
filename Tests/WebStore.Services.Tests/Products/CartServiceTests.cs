using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Domain.Models;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Products;
using Assert = Xunit.Assert;

namespace WebStore.Services.Tests.Products
{
    [TestClass]
    public class CartServiceTests
    {
        private Cart _Cart;
        private Mock<IProductData> _ProductDataMock;
        private Mock<ICartStore> _CartStoreMock;
        private Mock<ICartService> __CartService;

        [TestInitialize]
        public void TestInitialize()
        {
            _Cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem { ProductId = 1, Quantity = 1 },
                    new CartItem { ProductId = 2, Quantity = 3 }
                }
            };
            _ProductDataMock = new Mock<IProductData>();
            _ProductDataMock
               .Setup(c => c.GetProducts(It.IsAny<ProductFilter>()))
               .Returns(new List<ProductDTO>
                {
                    new ProductDTO
                    {
                        Id = 1,
                        Name = "Product 1",
                        Price = 1.1m,
                        Order = 0,
                        ImageUrl = "Product1.png",
                        Brand = new BrandDTO { Id = 1, Name = "Brand 1" },
                        Section = new SectionDTO { Id = 1, Name = "Section 1"}
                    },
                    new ProductDTO
                    {
                        Id = 2,
                        Name = "Product 2",
                        Price = 2.2m,
                        Order = 0,
                        ImageUrl = "Product2.png",
                        Brand = new BrandDTO { Id = 2, Name = "Brand 2" },
                        Section = new SectionDTO { Id = 2, Name = "Section 2"}
                    },
                });
            _CartStoreMock = new Mock<ICartStore>();
            _CartStoreMock.Setup(c => c.Cart).Returns(_Cart);
            _CartService = new CartService(_ProductDataMock.Object, _CartStoreMock.Object);

        }

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
