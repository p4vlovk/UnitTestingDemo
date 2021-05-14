namespace DemoCode.Tests
{
    using System;
    using AutoFixture;
    using Xunit;

    public class GuidEnumDemos
    {
        [Fact]
        public void Guid()
        {
            var fixture = new Fixture();
            var sut = new EmailMessage(fixture.Create<string>(), fixture.Create<string>(), fixture.Create<bool>())
            {
                Id = fixture.Create<Guid>()
            };

            // etc.
        }

        [Fact]
        public void Enum()
        {
            var fixture = new Fixture();
            var sut = new EmailMessage(fixture.Create<string>(), fixture.Create<string>(), fixture.Create<bool>())
            {
                MessageType = fixture.Create<EmailMessageType>()
            };

            // etc.
        }
    }
}
