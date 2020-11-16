using System;
using Xunit;

namespace LibraryApiIntegrationTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            int a = 10, b = 20, answer;

            answer = a + b;

            Assert.Equal(30, answer);
        }

        [Theory]
        [InlineData(10,10, 20)]
        [InlineData(2,2,4)]
        [InlineData(10,2, 12)]
        public void CanAdd(int a, int b, int expected)
        {
            var answer = a + b;
            Assert.Equal(expected, answer);
        }
    }
}
