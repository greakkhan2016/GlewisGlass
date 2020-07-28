using Domain;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class CompanyControllerTests:IntegrationTest
    {

        [Fact]
        public async Task Get_ReturnsGet_AllCompanies()
        {
            //Act
            var response = await TestClient.GetAsync("/api/company");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var resultString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Company>>(resultString);
            Assert.Equal(5, result.Count);
        }


        [Fact]
        public async Task POST_ReturnsTrue_WhenCompanyIsSuccessful()
        {
            //Arrange
            var request = new Company
            {
                Name = "ssss",
                Exchange="sadasd",
                Ticker="adasd",
                Isin="dasdasd",
                Website="asdasd"
            };

            var requestJson = JsonConvert.SerializeObject(request);
            var body = new StringContent(requestJson, Encoding.UTF8, "application/json");

            //Act
            var response = await TestClient.PostAsync("/api/company", body);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task POST_Returnsfalse_Incorrect_Isin()
        {
            //Arrange
            var request = new Company
            {
                Name = "ssss",
                Exchange = "sadasd",
                Ticker = "adasd",
                Isin = "11dasdasd",
                Website = "asdasd"
            };

            var requestJson = JsonConvert.SerializeObject(request);
            var body = new StringContent(requestJson, Encoding.UTF8, "application/json");

            //Act
            var response = await TestClient.PostAsync("/api/company", body);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

       
    }
}
