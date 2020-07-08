using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Controllers;
using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_Returns_View()
        {
            //создаём объект тестирования
            var controller = new HomeController();

            //вызываем тестируемый метод
            var result = controller.Index();

            //проверка результатов - класс Assert
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Error404_Returns_View()
        {
            var controller = new HomeController();
            var result = controller.Error404();
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Blog_Returns_View()
        {
            var controller = new HomeController();
            var result = controller.Blog();
            Assert.IsType<ViewResult>(result);
        }
        [TestMethod]
        public void BlogSingle_Returns_View()
        {
            var controller = new HomeController();
            var result = controller.BlogSingle();
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void ContactUs_Returns_View()
        {
            var controller = new HomeController();
            var result = controller.ContactUs();
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Throw_thrown_Exception()
        {

            var controller = new HomeController();
            Exception exception = null;
            try
            {
                _ = controller.Throw("Error");
            }
            catch (Exception ex)
            {

                exception = ex;
            }

            Assert.IsType<ApplicationException>(exception);
        }


        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void Throw_thrown_ApplicationException()
        {

            var controller = new HomeController();
            _ = controller.Throw("Error");
        }

        [TestMethod]
        public void Throw_thrown_ApplicationException2()
        {

            var controller = new HomeController();
            const string expected_message = "Error";

            var exception = Assert.Throws<ApplicationException>(() => controller.Throw(expected_message));
            Assert.Equal(expected_message, exception.Message);

        }

        [TestMethod]
        public void ErrorStatus_404_RedirrectTo_Error404()
        {
            var controller = new HomeController();
            const string status_code_404 = "404";
            var result = controller.ErrorStatus(status_code_404);
            var redirrect_to_act = Assert.IsType<RedirectToActionResult>(result);
            //имя контроллера не дб указано
            Assert.Null(redirrect_to_act.ControllerName);
            Assert.Equal(nameof(HomeController.Error404), redirrect_to_act.ActionName);

        }
    }
}
