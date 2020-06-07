using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;

namespace WebStore.Data
{
    public class WebStoreDBInitialiser
    {
        private readonly WebStoreDB _db;

        public WebStoreDBInitialiser(WebStoreDB db)
        {
            _db = db;
        }

        public void Initialise()
        {
            var db = _db.Database;
            db.Migrate();
            //если есть хоть один товар, то таблица считается проинициализированной
            if (!_db.Products.Any())
            {
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




            if (!_db.Employees.Any())
            {
                using (db.BeginTransaction())
                {
                    _db.AddRange(TestData.Employees);
                    db.ExecuteSqlRaw("SET IDENTITY_INSERT  [dbo].[Employees] ON");
                    _db.SaveChanges();
                    db.ExecuteSqlRaw("SET IDENTITY_INSERT  [dbo].[Employees] OFF");
                    db.CommitTransaction();
                }
            }


        }
    }
}
