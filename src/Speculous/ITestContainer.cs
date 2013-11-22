using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Speculous
{

    /// <summary>
    /// Defines a test case
    /// </summary>
    public interface ITestContainer
    {

        /// <summary>
        /// Inherits the test bag.
        /// </summary>
        /// <returns></returns>
        Dictionary<string, Func<object>> InheritContainer();

    }
}
