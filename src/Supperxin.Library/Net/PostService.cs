using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace Supperxin.Net
{
    public class PostService
    {
        private static HttpClient httpClient;

        static PostService()
        {
            httpClient = new HttpClient();
        }

        /// <summary>
        /// Post json string
        /// </summary>
        /// <param name="jsonString">json string</param>
        /// <param name="url">url to post</param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task<NetResponse> PostJsonStringAsync(string jsonString, string url)
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
                var result = await httpClient.PostAsync(url, content);
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
        /// Serialize object to json string and post
        /// </summary>
        /// <param name="jsonObject">Object to serialize</param>
        /// <param name="url">url to post</param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task<NetResponse> PostJsonObjectAsync(object jsonObject, string url)
        {
            if (null == jsonObject || null == url)
            {
                throw new ArgumentNullException();
            }

            var jsonString = JsonConvert.SerializeObject(jsonObject);

            return await PostJsonStringAsync(jsonString, url);
        }
    }
}
