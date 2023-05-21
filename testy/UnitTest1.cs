using System;
using Xunit;
namespace testy
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestTimeCreation()
        {
            var time1 = new Time(10, 30, 45);
            Assert.Equal(10, time1.Hours);
            Assert.Equal(30, time1.Minutes);
            Assert.Equal(45, time1.Seconds);

            var time2 = new Time(18, 15);
            Assert.Equal(18, time2.Hours);
            Assert.Equal(15, time2.Minutes);
            Assert.Equal(0, time2.Seconds);

            var time3 = new Time(23);
            Assert.Equal(23, time3.Hours);
            Assert.Equal(0, time3.Minutes);
            Assert.Equal(0, time3.Seconds);

            var time4 = new Time("08:45:20");
            Assert.Equal(8, time4.Hours);
            Assert.Equal(45, time4.Minutes);
            Assert.Equal(20, time4.Seconds);
        }
    }
}