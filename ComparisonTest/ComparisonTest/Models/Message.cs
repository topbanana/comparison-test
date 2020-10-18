using System;

namespace ComparisonTest.Models
{
    public class Message
    {
        public DateTime DateTime { get; set; }

        public string Author { get; set; }

        public string Body { get; set; }

        public Guid? ParentMessageId { get; set; }
    }
}