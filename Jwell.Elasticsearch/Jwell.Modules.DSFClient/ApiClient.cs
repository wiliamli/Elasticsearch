using System;
using System.Net.Http;
using Jwell.Framework.Extensions;
using System.Threading.Tasks;

namespace Jwell.Modules.DSFClient
{
    internal static class ApiClient
    {
        private static HttpClient HttpClient { get; set; }

        static ApiClient()
        {
            HttpClient = new HttpClient();
        }

        internal static string Request<T>(string url, MethodEnum method, T value = default(T))
        {
            string response = string.Empty;
            if (!string.IsNullOrWhiteSpace(url))
            {
                try
                {
                    switch (method.EnumDesc().ToUpper(System.Globalization.CultureInfo.CurrentCulture))
                    {
                        case "GET":
                            response = GetAsync(url).Result;
                            break;
                        case "POST":
                            response = PostAsync(url, value).Result;
                            break;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                throw new Exception("url不能为空");
            }

            return response;
        }

        private static async Task<string> GetAsync(string url)
        {
            HttpResponseMessage content = await HttpClient.GetAsync(url,HttpCompletionOption.ResponseContentRead);

            string body = await content.Content.ReadAsStringAsync();

            return body;
        }

        private static async Task<string> PostAsync<T>(string url, T value)
        {
            HttpResponseMessage content = await HttpClient.PostAsJsonAsync(url, value);

            string body = await content.Content.ReadAsStringAsync();

            return body;
        }
    }
}
