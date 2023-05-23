using TIME;

namespace Testy
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
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
        [Fact]
        public void Test2()
        {
            var time = new Time(15, 10, 30);
            Assert.Equal("15:10:30", time.ToString());
        }
        [Fact]
        public void Test3()
        {
            var time1 = new Time(12, 30, 0);
            var time2 = new Time(12, 30);
            var time3 = new Time(12);
            var time4 = new Time(12, 30, 1);

            Assert.True(time1 == time2);
            Assert.True(time2 == time3);
            Assert.True(time1 != time4);
        }
        [Fact]
        public void Test4()
        {
            var time1 = new Time(8, 30, 0);
            var time2 = new Time(12, 0);
            var time3 = new Time(12, 0, 1);
            var time4 = new Time(15, 0);

            Assert.True(time1 < time2);
            Assert.True(time2 < time3);
            Assert.True(time3 < time4);
            Assert.True(time4 > time1);
            Assert.True(time3 <= time4);
            Assert.True(time2 >= time1);
        }
        
        [Fact]
        public void Test7()
        {
            var period = new TimePeriod(3, 30, 15);
            Assert.Equal("03:30:15", period.ToString());
        }
        [Fact]
        public void Test8()
        {
            var period1 = new TimePeriod(2, 30, 0);
            var period2 = new TimePeriod(2, 30);
            var period3 = new TimePeriod(2);
            var period4 = new TimePeriod(2, 30, 1);

            Assert.True(period1 == period2);
            Assert.True(period2 == period3);
            Assert.True(period1 != period4);
        }
        [Fact]
        public void Test9()
        {
            var period1 = new TimePeriod(3, 30, 0);
            var period2 = new TimePeriod(5, 0);
            var period3 = new TimePeriod(5, 0, 1);
            var period4 = new TimePeriod(6, 30);

            Assert.True(period1 < period2);
            Assert.True(period2 < period3);
            Assert.True(period3 < period4);
            Assert.True(period4 > period1);
            Assert.True(period3 <= period4);
            Assert.True(period2 >= period1);
        }
        [Fact]
        public void Test10()
        {
            var period1 = new TimePeriod(2, 15);
            var period2 = new TimePeriod(0, 45, 30);
            var period3 = new TimePeriod(1, 30, 45);

            var result1 = period1 + period2;
            Assert.Equal(3, result1.Hours);
            Assert.Equal(0, result1.Minutes);
            Assert.Equal(0, result1.Seconds);

            var result2 = TimePeriod.Plus(period2, period3);
            Assert.Equal(2, result2.Hours);
            Assert.Equal(16, result2.Minutes);
            Assert.Equal(15, result2.Seconds);
        }

    }
}