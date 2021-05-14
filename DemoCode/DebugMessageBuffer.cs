namespace DemoCode
{
    using System.Collections.Generic;

    public class DebugMessageBuffer
    {
        public List<string> Messages { get; set; } = new List<string>();

        public int MessagesWritten { get; private set; }

        public void WriteMessage()
        {
            foreach (var message in this.Messages)
            {
                // Do something with message...
                this.MessagesWritten++;
            }
        }
    }
}
