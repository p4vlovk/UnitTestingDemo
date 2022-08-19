namespace DemoCode.Tests;

using System.Net.Mail;
using AutoFixture;
using Xunit;

public class EmailAddressDemos
{
    [Fact]
    public void Email()
    {
        var fixture = new Fixture();
        //var localPart = fixture.Create<EmailAddressLocalPart>().LocalPart;
        //var domain = fixture.Create<DomainName>().Domain;
        //var fullAddress = $"{localPart}@{domain}";
        var fullAddress = fixture.Create<MailAddress>().Address;
        var sut = new EmailMessage(fullAddress, fixture.Create<string>(), fixture.Create<bool>());

        // etc.
    }
}