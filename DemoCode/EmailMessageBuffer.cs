namespace DemoCode;

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class EmailMessageBuffer
{
    private readonly List<EmailMessage> emails = new();

    public EmailMessageBuffer(IEmailGateway emailGateway) => this.EmailGateway = emailGateway;

    public IEmailGateway EmailGateway { get; }

    public int UnsentMessagesCount => this.emails.Count;

    public void Add(EmailMessage message) => this.emails.Add(message);

    public void SendAll()
    {
        for (int i = this.emails.Count - 1; i >= 0; i--)
        {
            var email = this.emails[i];
            this.Send(email);
            this.emails.Remove(email);
        }
    }

    public void SendLimited(int maximumMessagesToSend)
    {
        var limitedBatchOfMessages = this.emails.Take(maximumMessagesToSend).ToArray();
        foreach (var email in limitedBatchOfMessages)
        {
            this.Send(email);
            this.emails.Remove(email);
        }
    }

    private void Send(EmailMessage email)
    {
        // Simulate sending email
        // Debug.WriteLine($"Sending email to: {email.ToAddress}");

        this.EmailGateway.Send(email);
    }
}