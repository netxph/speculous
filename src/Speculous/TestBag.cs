using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speculous
{
    public abstract class TestBag : ITestExtension
    {

        /// <summary>
        /// Test container. Contains mocked objects and other non-subject items
        /// </summary>
        protected Dictionary<string, Func<object>> Container { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestBag"/> class.
        /// </summary>
        public TestBag()
        {
            Container = new Dictionary<string, Func<object>>();
        }

        /// <summary>
        /// Defines a specific object in testbag.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="objectDef">The object definition.</param>
        protected virtual void Define(string key, Func<object> objectDef)
        {
            Container[key] = objectDef;
        }

        /// <summary>
        /// Creates object based on definition.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected virtual TObject New<TObject>(string key)
        {
            if (Container.ContainsKey(key))
            {
                return (TObject)Container[key]();
            }
            else
            {
                throw new KeyNotFoundException(
                    string.Format("Object not found in test container. Define the object in Initialize method using:\r\n Define(\"{0}\", () => //the object definition)", key));
            }
        }

        /// <summary>
        /// Inherits test bag of different test case
        /// </summary>
        /// <param name="testCase">The test case.</param>
        protected virtual void UseContext(ITestExtension testCase)
        {
            Container = testCase.InheritContainer();
        }

        /// <summary>
        /// Inherits the test container.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Func<object>> InheritContainer()
        {
            return Container;
        }

    }
}
