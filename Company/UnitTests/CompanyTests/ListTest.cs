using Application.Companies;
using System.Threading;
using Xunit;

namespace UnitTests
{
    public class ListTest : TestBase
    {
        [Fact]
        public void Should_Return_CompanyList()
        {
            var context = GetDbContext();

            var sut = new List.Handler(context);

            var resultString = sut.Handle(new List.Query(), CancellationToken.None).Result;

            Assert.Equal("Apple Inc", resultString[0].Name);
            Assert.Equal("NASDAQ", resultString[0].Exchange);
            Assert.Equal("AAPL", resultString[0].Ticker);
            Assert.Equal("US03738831005", resultString[0].Isin);
            Assert.Equal("http://www.apple.com", resultString[0].Website);
        }
    }
}