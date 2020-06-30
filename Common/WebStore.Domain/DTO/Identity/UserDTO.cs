using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Domain.DTO.Identity
{
 
    public abstract class UserDTO
    {
        //пользователь, всегда будет сохранён
        public User User { get; set; }
    }

   
    public class AddLoginDTO : UserDTO
    {
        //хранит факт входа пользоателя в системы
        public UserLoginInfo UserLoginInfo { get; set; }
    }

    
    public class PasswordHashDTO : UserDTO
    {
        //возможность читать и изменять пароль. Передаём только хэш пароля
        public string Hash { get; set; }
    }
     //передача инфо о времени блокировки
     public class SetLockoutDTO : UserDTO
    {
        //инфо об окончании блокировки. если пусто, то не заблокирован
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}
