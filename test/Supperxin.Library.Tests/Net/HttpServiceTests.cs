using System;
using Supperxin.Net;
using Xunit;

namespace Supperxin.Library.Tests.Net
{
    public class HttpServiceTests
    {
        [Fact]
        public async System.Threading.Tasks.Task TestNameAsync()
        {
            //Given
            string url = null;
            string jsonString = null;

            //When

            //Then

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await HttpService.FuncJsonObjectAsync(url, jsonString, (httpClient, u, c) => { return httpClient.PostAsync(u, c); }));
        }
    }
}
