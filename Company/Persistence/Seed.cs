using Domain;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;

namespace Persistence
{
    public class Seed
    {
        public static void SeedData(DataContext context)
        {
            if (!context.Companies.Any())
            {
                var companies = new List<Company>
                {
                    new Company { Id=1, Name = "Apple Inc", Exchange = "NASDAQ", Ticker = "AAPL", Isin = "US03738831005", Website = "http://www.apple.com" },
                    new Company { Id=2, Name = "British Airways Plc", Exchange = "Pink Sheets", Ticker = "BAIRY", Isin = "NL0000009165" },
                    new Company { Id=3, Name = "Heineken NV", Exchange = "Euronext Amsterdam", Ticker = "HEIA", Isin = "NL0000009165" },
                    new Company { Id=4, Name = "Panasonic Corp", Exchange = "Tokyo Stock Exchange", Ticker = "6752", Isin = "JP3866800000", Website = "http://www.panasonic.co.jp" },
                    new Company { Id=5, Name = "Porsche Automobile", Exchange = "Deutsche Börse", Ticker = "PAH3", Isin = "DE000PAH0038", Website = "https://www.porsche.com/" }
                };
                context.Companies.AddRange(companies);
                context.SaveChanges();
            }
        }
    }
}
