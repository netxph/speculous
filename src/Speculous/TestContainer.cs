using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speculous
{
    public abstract class TestContainer : ITestContainer
    {

        /// <summary>
        /// Gets or sets the object definition container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        protected Dictionary<string, Func<object>> Container { get; set; }

        /// <summary>
        /// Gets or sets the object container for tests.
        /// </summary>
        /// <value>
        /// The object container.
        /// </value>
        protected Dictionary<string, object> ObjectContainer { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestContainer"/> class.
        /// </summary>
        public TestContainer()
        {
            Container = new Dictionary<string, Func<object>>();
            ObjectContainer = new Dictionary<string, object>();
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
        /// Defines a specific object in testing, uses type as key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectDef">The object definition.</param>
        protected void Define<T>(Func<T> objectDef)
        {
            var key = typeof(T).Name;
            Define(key, () => objectDef());
        }

        /// <summary>
        /// Creates object based on definition.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected virtual T New<T>(string key)
        {
            if (Container.ContainsKey(key))
            {
                return (T)Container[key]();
            }
            else
            {
                throw new KeyNotFoundException(
                    string.Format("Object not found in test container. Define the object in Initialize method using:\r\n Define(\"{0}\", () => //the object definition)", key));
            }
        }

        /// <summary>
        /// Gets or creates the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException"></exception>
        protected virtual T Get<T>(string key)
        {
            if (Container.ContainsKey(key))
            {
                if (!ObjectContainer.ContainsKey(key))
                {
                    ObjectContainer[key] = New<T>(key);
                }

                return (T)ObjectContainer[key];
            }
            else
            {
                throw new KeyNotFoundException(
                    string.Format("Object not found in test container. Define the object in Initialize method using:\r\n Define(\"{0}\", () => //the object definition)", key));
            }
        }

        /// <summary>
        /// Creates object based on definition, uses type as key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T New<T>()
        {
            var key = typeof(T).Name;

            return New<T>(key);
        }

        protected T Get<T>()
        {
            var key = typeof(T).Name;

            return Get<T>(key);
        }

        /// <summary>
        /// Inherits test bag of different test case
        /// </summary>
        /// <param name="testCase">The test case.</param>
        protected virtual void UseContext(ITestContainer testCase)
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
