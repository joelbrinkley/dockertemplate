using Core;
using System;

namespace Persistance
{
    public class ValueEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public Value ToDomain()
        {
            return new Value()
            {
                Id = Id,
                Description = Description
            };
        }
    }

    public static class ValueExtensions
    {
        public static ValueEntity ToEntity(this Value value)
        {
            return new ValueEntity()
            {
                Id = value.Id,
                Description = value.Description
            };
        }
    }
}
