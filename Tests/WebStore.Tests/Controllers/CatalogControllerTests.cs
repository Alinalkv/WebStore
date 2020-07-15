using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebStore.Controllers;
using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class CatalogControllerTests
    {
        [TestMethod]
        public void Details_Returns_Correct_View()
        {
            #region Assert-Подготовка данных
            const int exp_product_id = 1;
            const decimal exp_product_price = 10m;
            var exp_product_name = $"Product id {exp_product_id}";
            var exp_brand_name = $"Brand of product {exp_product_id}";

            var mock_product_data = new Mock<IProductData>();
            mock_product_data
                .Setup(s => s.GetProductById(It.IsAny<int>()))
                .Returns<int>(id => new ProductDTO 
                {
                    Id = id,
                    Name = $"Product id {id}",
                    ImageUrl = $"Image_id_{id}.png",
                    Order = 1,
                    Price = exp_product_price,
                    Brand = new BrandDTO
                    {
                        Id = 1,
                        Name = $"Brand of product {id}"
                    },
                    Section = new SectionDTO
                    {
                        Id = 1,
                        Name = $"Section of product {id}"
                    }
                });

            var controller = new CatalogController(mock_product_data.Object);
            #endregion

            #region Act - действия
            var result = controller.Details(exp_product_id);
            #endregion

            #region Assert - проверка
            var view_result = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductViewModel>(view_result.Model);
            Assert.Equal(exp_product_id, model.Id);
            Assert.Equal(exp_product_name, model.Name);
            Assert.Equal(exp_product_price, model.Price);
            Assert.Equal(exp_brand_name, model.Brand);
            #endregion
        }

        [TestMethod]
        public void Shop_Returns_Correct_View()
        {
            var products = new[]
            {
                new ProductDTO
                {
                    Id = 1,
                    Name = "Product 1",
                    Order = 0,
                    Price = 10m,
                    ImageUrl = "Product1.png",
                    Brand = new BrandDTO
                    {
                        Id = 1,
                        Name = "Brand of product 1"
                    },
                    Section = new SectionDTO
                    {
                        Id = 1,
                        Name = "Section of product 1"
                    }
                },
                new ProductDTO
                {
                    Id = 2,
                    Name = "Product 2",
                    Order = 0,
                    Price = 20m,
                    ImageUrl = "Product2.png",
                    Brand = new BrandDTO
                    {
                        Id = 2,
                        Name = "Brand of product 2"
                    },
                    Section = new SectionDTO
                    {
                        Id = 2,
                        Name = "Section of product 2"
                    }
                },
            };
           
            var mock_product_data = new Mock<IProductData>();
            mock_product_data
                .Setup(s => s.GetProducts(It.IsAny<ProductFilter>()))
                .Returns(new PageProductsDTO { 
                 Products = products,
                  TotalCount = products.Length
                });

            var controller = new CatalogController(mock_product_data.Object);
            const int exp_section_id = 1;
            const int exp_brand_id = 5;
            
            var mapper_mock = new Mock<IMapper>();
            mapper_mock.Setup(mapper => mapper.Map<ProductViewModel>(It.IsAny<Product>()))
               .Returns<Product>(p => new ProductViewModel
               {
                   Id = p.Id,
                   Name = p.Name,
                   Order = p.Order,
                   Price = p.Price,
                   Brand = p.Brand?.Name,
                   ImageUrl = p.ImageUrl
               });

            var result = controller.Shop(exp_section_id, exp_brand_id, mapper_mock.Object);
            var view_result = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CatalogViewModel>(view_result.ViewData.Model);
            Assert.Equal(exp_brand_id, model.BrandId);
            Assert.Equal(exp_section_id, model.SectionId);
            Assert.Equal(products.Length, model.Products.Count());
            Assert.Equal(products[0].Brand.Name, model.Products.First().Brand);
        }
    }
}
