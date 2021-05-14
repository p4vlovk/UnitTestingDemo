namespace DemoCode.Tests
{
    using System;
    using AutoFixture;
    using Xunit;

    public class DateAndTimeDemos
    {
        [Fact]
        public void DateTimes()
        {
            //var logTime = new DateTime(2020, 10, 24);
            var fixture = new Fixture();
            var logTime = fixture.Create<DateTime>();

            var result = LogMessageCreator.Create(fixture.Create<string>(), logTime);

            //Assert.Equal(2020, result.Year);
            Assert.Equal(logTime.Year, result.Year);
        }
    }
}
