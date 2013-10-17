using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Speculous.Examples
{
    public class Person
    {

        protected IDateProvider DateProvider { get; private set; }

        public Person()
            : this(string.Empty)
        {
        }

        public Person(string name)
            : this(name, new DefaultDateProvider())
        {
        }

        public Person(IDateProvider dateProvider)
            : this(string.Empty, dateProvider)
        {
        }

        public Person(string name, IDateProvider dateProvider)
        {
            DateProvider = dateProvider;

            Name = name;
            CreatedDateUtc = DateProvider.UtcNow();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime CreatedDateUtc { get; set; }


        public void Process()
        {
            
        }

        public void Cleanup()
        { 
        
        }
    }
}
