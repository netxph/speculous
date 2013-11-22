using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speculous
{

    /// <summary>
    /// Base for all test case
    /// </summary>
    public class TestBase : TestContainer, IDisposable
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        public TestBase()
            : base()
        {
            Initialize();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Destroy();
        }

        /// <summary>
        /// Performs clean up for test
        /// </summary>
        protected virtual void Destroy() { }

        /// <summary>
        /// Handles initialization of non-subject items
        /// </summary>
        protected virtual void Initialize() { }

    }
}
