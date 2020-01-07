using System;
using Supperxin.Net;
using Xunit;

namespace Supperxin.Library.Tests.Net
{
    public class PostServiceTests
    {
        [Fact]
        public async System.Threading.Tasks.Task TestArgumentIsNullAsync()
        {
            //Given
            string url = null;
            string jsonString = null;

            //When

            //Then
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await PostService.PostJsonStringAsync(jsonString, url));
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await PostService.PostJsonObjectAsync(jsonString, url));
        }
    }
}
