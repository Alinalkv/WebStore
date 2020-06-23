using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Services.Data
{
    public class WebStoreDBInitialiser
    {
        private readonly WebStoreDB _db;
        private readonly UserManager<User> _UserManager;
        private readonly RoleManager<Role> _RoleManager;

        public WebStoreDBInitialiser(WebStoreDB db, UserManager<User> UserManager, RoleManager<Role> RoleManager)
        {
            _db = db;
            _UserManager = UserManager;
            _RoleManager = RoleManager;
        }

        public void Initialise()
        {
            var db = _db.Database;
            db.Migrate();

            InitialiseProducts();

            InitialiseEmployees();

            InitialiseIdentityAsync().Wait();

        }

        private void InitialiseProducts()
        {
            var db = _db.Database;

            //если есть хоть один товар, то таблица считается проинициализированной
            if (_db.Products.Any())
                return;

            //добавляем бд транзакциями: если будет ошибка, всё откатится назад
            using (db.BeginTransaction())
            {
                _db.AddRange(TestData.Sections);
                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ProductSection] ON");
                _db.SaveChanges();
                db.ExecuteSqlRaw("SET IDENTITY_INSERT  [dbo].[ProductSection] OFF");

                db.CommitTransaction();
            }

            using (var transaction = db.BeginTransaction())
            {
                _db.AddRange(TestData.Brands);
                db.ExecuteSqlRaw("SET IDENTITY_INSERT  [dbo].[ProductBrand] ON");
                _db.SaveChanges();
                db.ExecuteSqlRaw("SET IDENTITY_INSERT  [dbo].[ProductBrand] OFF");
                transaction.Commit();
            }

            using (db.BeginTransaction())
            {
                _db.AddRange(TestData.Products);
                db.ExecuteSqlRaw("SET IDENTITY_INSERT  [dbo].[Products] ON");
                _db.SaveChanges();
                db.ExecuteSqlRaw("SET IDENTITY_INSERT  [dbo].[Products] OFF");
                db.CommitTransaction();
            }
        }

        private void InitialiseEmployees()
        {
            var db = _db.Database;
            if (_db.Employees.Any())
                return;

            using (db.BeginTransaction())
            {
                _db.AddRange(TestData.Employees);
                db.ExecuteSqlRaw("SET IDENTITY_INSERT  [dbo].[Employees] ON");
                _db.SaveChanges();
                db.ExecuteSqlRaw("SET IDENTITY_INSERT  [dbo].[Employees] OFF");
                db.CommitTransaction();
            }

        }

        private async Task InitialiseIdentityAsync()
        {
            //если нет роли администратора, то создаём её
            if (!await _RoleManager.RoleExistsAsync(Role.Administrator))
                await _RoleManager.CreateAsync(new Role
                {
                    Name = Role.Administrator
                });

            //если нет роли пользователя, то создаём её
            if (!await _RoleManager.RoleExistsAsync(Role.User))
                await _RoleManager.CreateAsync(new Role
                {
                    Name = Role.User
                });

            //проверяем, есть ли пользователь с этой ролью. Создаём, если нет
            if (await _UserManager.FindByNameAsync(User.Administrator) is null)
            {
                var admin = new User { UserName = User.Administrator };
                var result = await _UserManager.CreateAsync(admin, User.DefaultAdminPassword);
                if (result.Succeeded)
                {
                    await _UserManager.AddToRoleAsync(admin, Role.Administrator);
                }
                else
                {
                    var errors = result.Errors.Select(e => e.Description);
                    throw new InvalidOperationException($"Ошибка при создании пользователя Администратор: {string.Join(", ", errors)}");
                }
            }
        }

    }
}
