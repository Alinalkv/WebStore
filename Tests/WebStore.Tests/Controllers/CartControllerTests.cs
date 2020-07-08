using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebStore.Controllers;
using WebStore.Domain.DTO.Order;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class CartControllerTests
    {
        [TestMethod]
        public async Task CheckOut_ModelState_Invalid_Returns_ViewModel()
        {
            var mock_cart_service = new Mock<ICartService>();
            var mock_order_service = new Mock<IOrderService>();

            var controller = new CartController(mock_cart_service.Object);
            controller.ModelState.AddModelError("Error", "InvalidModel");
            const string expected_order_name = "Test order";

            var result = await controller.CheckOut(new OrderViewModel { Name = expected_order_name}, mock_order_service.Object);

            var view_result = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CartOrderViewModel>(view_result.Model);
            Assert.Equal(expected_order_name, model.Order.Name);
        }

        [TestMethod]
        public async Task CheckOut_Calls_Service_Returns_Redirrect()
        {
            var mock_cart_service = new Mock<ICartService>();
            mock_cart_service
                .Setup(c => c.TransformFromCart())
                .Returns(() => new CartViewModel
                {
                     Items = new[] { (new ProductViewModel {  Name = "Product"}, 1) }
                });

            const int exprcted_order_id = 1;
            var mock_order_service = new Mock<IOrderService>();
            mock_order_service
                .Setup(c => c.CreateOrder(It.IsAny<string>(), It.IsAny<CreateOrderModel>()))
                .ReturnsAsync(new OrderDTO
                {
                    Id = exprcted_order_id,
                });

            var controller = new CartController(mock_cart_service.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, "TestUser") }))
                    }
                }
            };

            var order_model = new OrderViewModel
            {
                Name = "TestUser",
                Address = "TestAddress",
                Phone = "TestPhone",
            };

            var result = await controller.CheckOut(order_model, mock_order_service.Object);
            var redirrect_to_act = Assert.IsType<RedirectToActionResult>(result);
            //имя контроллера не дб указано
            Assert.Null(redirrect_to_act.ControllerName);
            Assert.Equal(nameof(CartController.OrderConfirmed), redirrect_to_act.ActionName);

            Assert.Equal(exprcted_order_id, redirrect_to_act.RouteValues["id"]);
        }
    }
}
