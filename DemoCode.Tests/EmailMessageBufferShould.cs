namespace DemoCode.Tests
{
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using AutoFixture.Xunit2;
    using DemoCode.Tests.Attributes;
    using Moq;
    using Xunit;

    public class EmailMessageBufferShould
    {
        //[Fact]
        //public void AddMessageToBuffer()
        //{
        //    var fixture = new Fixture();
        //    var sut = new EmailMessageBuffer();

        //    sut.Add(fixture.Create<EmailMessage>());

        //    Assert.Equal(1, sut.UnsentMessagesCount);
        //}

        //[Fact]
        //public void RemoveMessageFromBufferWhenSent()
        //{
        //    var fixture = new Fixture();
        //    var sut = new EmailMessageBuffer();
        //    sut.Add(fixture.Create<EmailMessage>());

        //    sut.SendAll();

        //    Assert.Equal(0, sut.UnsentMessagesCount);
        //}

        //[Fact]
        //public void SendOnlySpecifiedNumberOfMessages()
        //{
        //    var fixture = new Fixture();
        //    var sut = new EmailMessageBuffer();
        //    sut.Add(fixture.Create<EmailMessage>());
        //    sut.Add(fixture.Create<EmailMessage>());
        //    sut.Add(fixture.Create<EmailMessage>());

        //    sut.SendLimited(2);

        //    Assert.Equal(1, sut.UnsentMessagesCount);
        //}

        [Fact]
        public void SendEmailToGateway_Manual_Moq()
        {
            var fixture = new Fixture();
            var mockGateway = new Mock<IEmailGateway>();
            var sut = new EmailMessageBuffer(mockGateway.Object);
            sut.Add(fixture.Create<EmailMessage>());

            sut.SendAll();

            mockGateway.Verify(x => x.Send(It.IsAny<EmailMessage>()), Times.Once);
        }

        [Fact]
        public void SendEmailToGateway_AutoMoq()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization()); // provides mock objects of interfaces
            var mockGateway = fixture.Freeze<Mock<IEmailGateway>>();
            var sut = fixture.Create<EmailMessageBuffer>();
            sut.Add(fixture.Create<EmailMessage>());

            sut.SendAll();

            mockGateway.Verify(x => x.Send(It.IsAny<EmailMessage>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public void SendEmailToGateway_AutoMoq_Theory(
            EmailMessage message,
            [Frozen]Mock<IEmailGateway> mockGateway,
            EmailMessageBuffer sut) // Notice the order of the parameters; the mock should be frozen before the SUT is created
        {
            sut.Add(message);

            sut.SendAll();

            mockGateway.Verify(x => x.Send(It.IsAny<EmailMessage>()), Times.Once);
        }
    }
}
