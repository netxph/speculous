using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speculous.Tests
{
    public class SampleObject
    {

        public static string BaseMessage { get; set; }

        public string GetMessage(string message)
        {
            return string.Format("{0}, {1}", BaseMessage, message);
        }

    }
}
