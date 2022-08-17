namespace DemoCode
{
    using System;

    public class EmailMessage
    {
        public EmailMessage(string toAddress, string messageBody, bool isImportant)
        {
            this.ToAddress = toAddress;
            this.MessageBody = messageBody;
            this.IsImportant = isImportant;
        }

        public Guid Id { get; set; }

        public string ToAddress { get; }

        public string Subject { get; set; }

        public string MessageBody { get; }

        public bool IsImportant { get; }

        public EmailMessageType MessageType { get; set; }
    }
}
