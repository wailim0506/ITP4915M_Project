using Xunit;

namespace TestController
{
    public class Test
    {
        [Fact]
        public void Test1()
        {
            int a = 1;
            int b = 2;
            int c = a + b;
            Assert.Equal(c, 3);
        }
    }
}