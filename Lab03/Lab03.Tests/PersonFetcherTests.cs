using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Autofac.Extras.Moq;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Moq.Protected;
using System.Threading;
using System.Net;

namespace Lab03.Tests
{
    public class PersonFetcherTests
    {
        private Mock<HttpMessageHandler> GetHttpMock(string content)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Loose);
            handlerMock
               .Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(content),
               })
               .Verifiable();

            return handlerMock;
        }

        [Fact]
        public async void FetchPerson_ReturnsData()
        {
            string personJson = "{'results': [{'name': {'first': 'test', 'last': 'user'}}]}";
            var handlerMock = GetHttpMock(personJson);

            JToken expected = JObject.Parse(personJson)["results"][0];

            JToken actual = await PersonFetcher.FetchPerson(new HttpClient(handlerMock.Object));
            Assert.Equal(actual, expected);
            
        }
    }
}
