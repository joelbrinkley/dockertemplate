using System;

namespace Messaging 
{
    public class AddValueCommand : Message
    {
        public AddValueCommand() { }
        
        public static string _subject = "dockertemplate.commands.add_value";

        public override string Subject => _subject;

        public string Value { get; set; }

    }
}