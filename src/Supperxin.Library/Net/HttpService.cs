using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Supperxin.Net
{
    public class HttpService
    {
        private static HttpClient httpClient;

        static HttpService()
        {
            httpClient = new HttpClient();
        }

        /// <summary>
        /// Post json string
        /// </summary>
        /// <param name="jsonString">json string</param>
        /// <param name="url">url to post</param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task<NetResponse> FuncJsonStringAsync(string url, string jsonString, Func<HttpClient, string, StringContent, Task<HttpResponseMessage>> func)
        {
            if (null == jsonString || null == url)
            {
                throw new ArgumentNullException();
            }

            var acceptHeader = new MediaTypeWithQualityHeaderValue("application/json");
            httpClient.DefaultRequestHeaders.Accept.Add(acceptHeader);

            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            try
            {

                var result = await func(httpClient, url, content);
                //var result = await httpClient.PostAsync(url, content);
                var response = new NetResponse() { Successful = result.IsSuccessStatusCode, Message = await result.Content.ReadAsStringAsync() };

                return response;
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                httpClient.DefaultRequestHeaders.Accept.Remove(acceptHeader);
            }
        }

        /// <summary>
        /// Func json object
        /// </summary>
        /// <param name="url">url to post</param>
        /// <param name="jsonObject"></param>
        /// <param name="func">delegate function</param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task<NetResponse> FuncJsonObjectAsync(string url, object jsonObject, Func<HttpClient, string, StringContent, Task<HttpResponseMessage>> func)
        {
            if (null == jsonObject || null == url)
            {
                throw new ArgumentNullException();
            }

            var jsonString = JsonConvert.SerializeObject(jsonObject);

            return await FuncJsonStringAsync(jsonString, url, func);
        }
    }
}
