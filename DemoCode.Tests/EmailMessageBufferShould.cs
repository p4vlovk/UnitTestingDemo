namespace DemoCode.Tests;

using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using DemoCode.Tests.Attributes;
using Moq;
using Xunit;

public class EmailMessageBufferShould
{
    private readonly Fixture fixture = new();
    private readonly EmailMessageBuffer sut;
        
    public EmailMessageBufferShould()
    {
        this.fixture.Customize(new AutoMoqCustomization()); // provides mock objects of interfaces
        this.fixture.Freeze<Mock<IEmailGateway>>();
        this.sut = this.fixture.Create<EmailMessageBuffer>();
    }
        
    [Fact]
    public void AddMessageToBuffer()
    {
        this.sut.Add(this.fixture.Create<EmailMessage>());

        Assert.Equal(1, this.sut.UnsentMessagesCount);
    }

    [Fact]
    public void RemoveMessageFromBufferWhenSent()
    {
        this.sut.Add(this.fixture.Create<EmailMessage>());

        this.sut.SendAll();

        Assert.Equal(0, this.sut.UnsentMessagesCount);
    }

    [Fact]
    public void SendOnlySpecifiedNumberOfMessages()
    {
        this.sut.Add(this.fixture.Create<EmailMessage>());
        this.sut.Add(this.fixture.Create<EmailMessage>());
        this.sut.Add(this.fixture.Create<EmailMessage>());
            
        this.sut.SendLimited(2);

        Assert.Equal(1, this.sut.UnsentMessagesCount);
    }

    [Fact]
    public void SendEmailToGateway_Manual_Moq()
    {
        var mockGateway = new Mock<IEmailGateway>();
        var localSut = new EmailMessageBuffer(mockGateway.Object);
        localSut.Add(this.fixture.Create<EmailMessage>());
        
        localSut.SendAll();
        
        mockGateway.Verify(x => x.Send(It.IsAny<EmailMessage>()), Times.Once);
    }

    [Fact]
    public void SendEmailToGateway_AutoMoq()
    {
        var mockGateway = this.fixture.Create<Mock<IEmailGateway>>();
        this.sut.Add(this.fixture.Create<EmailMessage>());

        this.sut.SendAll();

        mockGateway.Verify(x => x.Send(It.IsAny<EmailMessage>()), Times.Once);
    }

    [Theory]
    [AutoMoqData]
    public void SendEmailToGateway_AutoMoq_Theory(
        EmailMessage message,
        [Frozen]Mock<IEmailGateway> mockGateway,
        EmailMessageBuffer localSut) // Notice the order of the parameters; the mock should be frozen before the SUT is created
    {
        localSut.Add(message);

        localSut.SendAll();

        mockGateway.Verify(x => x.Send(It.IsAny<EmailMessage>()), Times.Once);
    }
}