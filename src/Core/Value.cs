using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class Value
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return this.Description;
        }
    }
}
