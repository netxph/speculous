using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Speculous
{

    /// <summary>
    /// Base class for test cases
    /// </summary>
    /// <typeparam name="T">Type of subject</typeparam>
    /// <typeparam name="U">Type of context parameter</typeparam>
    public abstract class TestCase<T, U> : TestBase
    {

        /// <summary>
        /// The subject context
        /// </summary>
        protected Func<U, T> Subject
        {
            get {  return Given(); }
        }

        /// <summary>
        /// Defines the subject and how it is executed
        /// </summary>
        /// <returns></returns>
        protected abstract Func<U, T> Given();

    }

    /// <summary>
    /// Base class for test cases
    /// </summary>
    /// <typeparam name="T">Type of subject</typeparam>
    public abstract class TestCase<T> : TestBase
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
        /// Defines the subject and how it is executed.
        /// </summary>
        /// <returns></returns>
        protected abstract Func<T> Given();

    }

    /// <summary>
    /// Base class for test cases that returns void
    /// </summary>
    public abstract class TestCase : TestBase
    {

        /// <summary>
        /// The subject context
        /// </summary>
        protected Action Subject
        {
            get { return Given(); }
        }

        /// <summary>
        /// Defines the subject and how it is executed.
        /// </summary>
        /// <returns></returns>
        protected abstract Action Given();

    }

}
