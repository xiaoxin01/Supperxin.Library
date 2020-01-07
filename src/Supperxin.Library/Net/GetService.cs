using System;
using System.IO;
using System.Net.Http;

namespace Supperxin.Net
{
    public class GetService
    {
        private static HttpClient httpClient;

        static GetService()
        {
            httpClient = new HttpClient();
        }

        /// <summary>
        /// Get url's html string
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task<NetResponse> GetStringAsync(string url)
        {
            //httpClient.DefaultRequestHeaders.Accept.Clear();
            var result = await httpClient.GetAsync(url);
            var resultString = await result.Content.ReadAsStringAsync();

            return new NetResponse() { Successful = result.IsSuccessStatusCode, Message = resultString };
        }

        /// <summary>
        /// download file and save it
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fullPath">file's full path to save</param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task<NetResponse> DownloadFileAsync(string url, string fullPath)
        {
            //httpClient.DefaultRequestHeaders.Accept.Clear();
            var result = await httpClient.GetAsync(url);
            var response = new NetResponse();

            try
            {
                if (!result.IsSuccessStatusCode)
                {
                    response.Successful = false;
                    response.Message = await result.Content.ReadAsStringAsync();
                    return response;
                }
                string folder = Path.GetDirectoryName(fullPath);
                if (!string.IsNullOrWhiteSpace(folder))
                {
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                }
                File.WriteAllBytes(fullPath, await result.Content.ReadAsByteArrayAsync());

                response.Successful = true;
            }
            catch (Exception ex)
            {
                response.Successful = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
