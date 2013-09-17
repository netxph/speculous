using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speculous.Tests
{
    public class SampleObject
    {

        public IDependentObject Dependent { get; set; }

        public string GetMessage(string message)
        {
            var baseMessage = "Hello";

            if (Dependent != null)
            {
                baseMessage = Dependent.GetBaseMessage();
            }

            return string.Format("{0}, {1}", baseMessage, message);
        }

    }
}
