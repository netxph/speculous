using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Speculous
{
    public interface ITestExtension
    {

        Dictionary<string, Func<object>> GetTestBag();

    }
}
