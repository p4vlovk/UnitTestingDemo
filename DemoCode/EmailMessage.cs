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

        public string ToAddress { get; set; }

        public string Subject { get; set; }

        public string MessageBody { get; set; }

        public bool IsImportant { get; set; }

        public EmailMessageType MessageType { get; set; }
    }
}
