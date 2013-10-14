using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Speculous
{
    /// <summary>
    /// Base class for test cases
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class TestCase<T> : ITestExtension, IDisposable
    {
        /// <summary>
        /// Test container. Contains mocked objects and other non-subject items
        /// </summary>
        protected Dictionary<string, Func<object>> TestBag { get; set; }
        
        /// <summary>
        /// The subject context
        /// </summary>
        protected Func<T> Subject
        {
            get { return Given(); }
        }

        /// <summary>
        /// The executed subject context
        /// </summary>
        protected T Its
        {
            get { return Subject(); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase{T}"/> class.
        /// </summary>
        public TestCase()
        {
            TestBag = new Dictionary<string, Func<object>>();

            Initialize();
        }

        /// <summary>
        /// Defines a specific object in testbag.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="objectDef">The object definition.</param>
        protected virtual void Define(string key, Func<object> objectDef)
        {
            TestBag[key] = objectDef;
        }

        /// <summary>
        /// Creates object based on definition.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected virtual TObject New<TObject>(string key)
        {
            if (TestBag.ContainsKey(key))
            {
                return (TObject)TestBag[key]();
            }
            else
            { 
                throw new KeyNotFoundException(
                    string.Format("Object not found in test container. Define the object in Initialize method using:\r\n Define(\"{0}\", () => //the object definition)", key));
            }
        }

        /// <summary>
        /// Defines the subject and how it is executed.
        /// </summary>
        /// <returns></returns>
        protected abstract Func<T> Given();

        /// <summary>
        /// Handles initialization of non-subject items
        /// </summary>
        protected virtual void Initialize()
        {
        }

        /// <summary>
        /// Inherits test bag of different test case
        /// </summary>
        /// <param name="testCase">The test case.</param>
        protected virtual void UseContext(ITestExtension testCase)
        {
            TestBag = testCase.InheritContainer();
        }

        public Dictionary<string, Func<object>> InheritContainer()
        {
            //Run all define operations first
            Initialize();
            return TestBag;
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
        protected virtual void Destroy()
        {
        }
    }

}
