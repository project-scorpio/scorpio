using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using Scorpio.AspNetCore.TestBase;
using Scorpio.Modularity;

using Shouldly;

namespace Scorpio.AspNetCore
{
    public class AspNetCoreTestBase : AspNetCoreTestBase<TestModule, Startup>
    {

    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S2743:Static fields should not be used in generic types", Justification = "<挂起>")]
    public abstract class AspNetCoreTestBase<TStartupModule, TStartup> : AspNetCoreIntegratedTestBase<TStartupModule, TStartup>
        where TStartupModule : IScorpioModule
        where TStartup : class
    {
        private static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
        private static readonly JsonSerializerSettings _sharedJsonSerializerSettings =
            _jsonSerializerSettings;

        protected virtual async Task<T> GetResponseAsObjectAsync<T>(string url, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            var strResponse = await GetResponseAsStringAsync(url, expectedStatusCode);
            return JsonConvert.DeserializeObject<T>(strResponse, _sharedJsonSerializerSettings);
        }

        protected virtual async Task<string> GetResponseAsStringAsync(string url, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            using (var response = await GetResponseAsync(url, expectedStatusCode))
            {
                return await response.Content.ReadAsStringAsync();
            }
        }

        protected virtual async Task<HttpResponseMessage> GetResponseAsync(string url, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url))
            {
                requestMessage.Headers.Add("Accept-Language", CultureInfo.CurrentUICulture.Name);
                var response = await Client.SendAsync(requestMessage);
                response.StatusCode.ShouldBe(expectedStatusCode);
                return response;
            }
        }
    }
}
