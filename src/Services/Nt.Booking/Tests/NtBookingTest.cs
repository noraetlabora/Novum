using Xunit;

namespace Nt.Booking.Test
{
    public class NtBookingTest
    {
        [Fact]
        public void EmptyConfigurationFileTest()
        {
            Assert.Throws<System.ArgumentException>(()=> new ServerConfiguration(""));
        }
        [Fact]
        public void MissingConfigurationFileTest()
        {
            Assert.Throws<System.IO.FileNotFoundException>(() => new ServerConfiguration("..."));
        }

        [Fact]
        public void HelloWorld2()
        {
            Assert.Equal("HelloWorld","HelloWorld");
        }
        [Fact]
        public void HelloWorld3()
        {
            Assert.Equal("HelloWorld", "HelloWorld");
        }
    }
}
