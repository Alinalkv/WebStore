using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace WebStore.Clients.Base
{
    public abstract class BaseClient : IDisposable
    {
        protected readonly HttpClient _Client;
        protected readonly string _ServiceAddress;

        protected BaseClient(IConfiguration Configuration, string ServiceAddress)
        {
            _ServiceAddress = ServiceAddress;
            _Client = new HttpClient { 
                BaseAddress = new Uri(Configuration["WebApiURL"]),
                DefaultRequestHeaders =
                {
                    Accept = { new MediaTypeWithQualityHeaderValue("application/json")}
                }
            };
        }

        public T Get<T>(string url) => GetAsync<T>(url).Result;

        /// <summary>
        /// Получить данные по адресу
        /// </summary>
        /// <typeparam name="T">Тип данных, которые будут получены</typeparam>
        /// <param name="url">Адрес, по которому будет поиск данных</param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string url, CancellationToken Cancel = default)
        {
            //полуаем данные по адресу
            var response = await _Client.GetAsync(url, Cancel);
            //если ок, то десериализуем в нужный нам формат
            return await response.EnsureSuccessStatusCode().Content.ReadAsAsync<T>(Cancel);
        }

        public HttpResponseMessage Post<T>(string url, T item) => PostAsync<T>(url, item).Result;
        /// <summary>
        /// Создать новые данные внутри сервиса
        /// </summary>
        /// <typeparam name="T">Тип данных</typeparam>
        /// <param name="url">адрес</param>
        /// <param name="item">сущность, которую надо создат</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostAsync<T>(string url, T item, CancellationToken Cancel = default)
        {
            var response = await _Client.PostAsJsonAsync(url, item, Cancel);
            return response.EnsureSuccessStatusCode();
        }

        public HttpResponseMessage Put<T>(string url, T item) => PutAsync<T>(url, item).Result;
        /// <summary>
        /// Обновить данные внутри сервиса / создать новый
        /// </summary>
        /// <typeparam name="T">Тип данных</typeparam>
        /// <param name="url">адрес</param>
        /// <param name="item">сущность, которую надо изменить</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PutAsync<T>(string url, T item, CancellationToken Cancel = default)
        {
            var response = await _Client.PutAsJsonAsync(url, item, Cancel);
            return response.EnsureSuccessStatusCode();
        }


        public HttpResponseMessage Delete(string url) => DeleteAsync(url).Result;
       /// <summary>
       /// Удаление объекта
       /// </summary>
       /// <param name="url"></param>
       /// <returns></returns>
        public async Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken Cancel = default) => await _Client.DeleteAsync(url, Cancel);

        #region IDisposabe

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                //финализация управляемых ресурсов
                _Client.Dispose();
            }
            //финализация неуправляемых ресурсов
        }
        #endregion
    }
}
