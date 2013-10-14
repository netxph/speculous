using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Speculous.Examples
{
    public class Person
    {
        private string p;


        public static IDateProvider DateProvider { get; set; }

        public Person()
        {
            Name = string.Empty;
            CreatedDateUtc = DateProvider.UtcNow();
        }

        public Person(string name)
            : this()
        {
            Name = name;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime CreatedDateUtc { get; set; }

    }
}
