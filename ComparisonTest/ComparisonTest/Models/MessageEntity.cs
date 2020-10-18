using System;

namespace ComparisonTest.Models
{
    public class MessageEntity
    {
        public DateTime DateTime { get; set; }

        public string Author { get; set; }

        public string Body { get; set; }

        public Guid? ParentMessageId { get; set; }
    }
}