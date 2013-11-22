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
    public abstract class TestCase<T> : TestContainer, IDisposable
    {
        
        
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
        /// Handles initialization of non-subject items
        /// </summary>
        protected virtual void Initialize() { }

        /// <summary>
        /// Defines the subject and how it is executed.
        /// </summary>
        /// <returns></returns>
        protected abstract Func<T> Given();

        /// <summary>
        /// Performs clean up for test
        /// </summary>
        protected virtual void Destroy() { }
        
    }

    /// <summary>
    /// Base class for test cases that returns void
    /// </summary>
    public abstract class TestCase : TestContainer, IDisposable
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        public TestCase()
            : base()
        {
            
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Destroy();
        }

        /// <summary>
        /// The subject context
        /// </summary>
        protected Action Subject
        {
            get { return Given(); }
        }

        /// <summary>
        /// Handles initialization of non-subject items
        /// </summary>
        protected virtual void Initialize() { }

        /// <summary>
        /// Defines the subject and how it is executed.
        /// </summary>
        /// <returns></returns>
        protected abstract Action Given();

        /// <summary>
        /// Performs clean up for test
        /// </summary>
        protected virtual void Destroy() { } 
        
    }


}
