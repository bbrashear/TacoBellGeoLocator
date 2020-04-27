using System;
using Xunit;
using LoggingKata;

namespace LoggingKata.Test
{
    public class TacoParserTests
    {
        [Fact]
        public void ShouldDoSomething()
        {
            string csvData = "34.272015,-85.229599, Taco Bell Rome";

            var tacoParser = new TacoParser();

            var actual = tacoParser.Parse(csvData);

            Assert.NotNull(actual);
        }

        [Theory]
        [InlineData("34.272015,-85.229599, Taco Bell Rome")]
        [InlineData("33.715798, -84.215646, Taco Bell Decatur")]
        
        //information should parse if the correct amount is present and it is within bounds
        //test passes if the method does not return null
        public void ShouldParse(string csvData)
        {
            var tacoParser = new TacoParser();

            var actual = tacoParser.Parse(csvData);

            Assert.NotNull(actual);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("33.715798, Taco Bell Decatur")]
        [InlineData("Taco Bell Decatur")]
        [InlineData("-86, -85.229599, Taco Bell Nope")]
        [InlineData("86, -85.229599, Taco Bell Nope")]        
        [InlineData("33.715798, -181, Taco Bell Nope")]
        [InlineData("33.715798, 181, Taco Bell Nope")]

        //information should fail to parse if the correct amount of data is not present or is out of bounds,
        //method returns null for those conditions, test passes if null is returned
        public void ShouldFailParse(string csvData)
        {
            var tacoParser = new TacoParser();

            var actual = tacoParser.Parse(csvData);

            Assert.Null(actual);
        }
    }
}
