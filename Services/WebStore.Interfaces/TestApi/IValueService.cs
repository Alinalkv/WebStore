using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace WebStore.Interfaces.TestApi
{
    public interface IValueService
    {
        /// <summary>
        /// Выгрузка всех значений
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> Get();

       /// <summary>
       /// Извлечение значения по идентификатору
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        string Get(int id);
 
        /// <summary>
        /// Добавление нового значения
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Адрес указанного значения</returns>
        Uri Post(string value);

        /// <summary>
        /// Обновление значения
        /// </summary>
        /// <param name="id">То, что надо обновить</param>
        /// <param name="value">Новое значение</param>
        /// <returns>Статусный код - удалось или нет</returns>
        HttpStatusCode Update(int id, string value);

        /// <summary>
        /// Удаление значения
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        HttpStatusCode Delete(int id);
    }
}
