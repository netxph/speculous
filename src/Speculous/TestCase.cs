using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speculous
{

    /// <summary>
    /// Base class for tests, follows RSpec style
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class TestCase<T> : IDisposable, ITestExtend
    {

        #region Declarations

        ITestExtend _parent = null;

        #endregion

        #region Properties

        /// <summary>
        /// Object storage, used for storing supporting classes and mocks for the subject
        /// </summary>
        protected Dictionary<string, Func<object>> TestBag { get; set; }

        #endregion

        #region Subject Properties

        /// <summary>
        /// Gets the Subject context
        /// </summary>
        /// <value>
        /// The subject context.
        /// </value>
        protected Func<T> Subject
        {
            get
            {
                Initialize();
                return Given();
            }
        }

        /// <summary>
        /// Gets the subject, aliased as "It" for english readability. Use for writing tests related to itself.
        /// </summary>
        /// <value>
        /// The subject context.
        /// </value>
        protected T It
        {
            get
            {
                return Subject();
            }
        }

        /// <summary>
        /// Gets the subject, aliased as "Its" for english readability. Use for writing tests related to its properties
        /// </summary>
        /// <value>
        /// The subject context.
        /// </value>
        protected T Its
        {
            get
            {
                return Subject();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase{T}"/> class.
        /// </summary>
        public TestCase()
        {
            TestBag = new Dictionary<string, Func<object>>();
        }

        #endregion

        #region Storage Methods

        /// <summary>
        /// Defines a test object.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="initObject">The test object.</param>
        protected void Define(string key, Func<object> initObject)
        {
            TestBag[key] = initObject;
        }

        /// <summary>
        /// Gets the test object
        /// </summary>
        /// <typeparam name="TObject">The type of the test object.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected TObject Get<TObject>(string key)
        {
            if (TestBag.ContainsKey(key))
            {
                return (TObject)TestBag[key]();
            }

            return default(TObject);
        }

        #endregion

        #region Test Methods

        /// <summary>
        /// Initializes objects for test. Place here non-operational components such as mocks and stubs.
        /// </summary>
        protected virtual void Initialize()
        {
            IncludeParent();
        }

        /// <summary>
        /// Defines how you are going to use the Subject. Place here operational components on how to use your subject.
        /// </summary>
        /// <returns></returns>
        protected virtual Func<T> Given()
        {
            return IncludeSubject();
        }

        /// <summary>
        /// Disposes objects used in test.
        /// </summary>
        protected virtual void Destroy()
        {

        }

        #endregion

        #region Test Inherit Methods

        /// <summary>
        /// Returns the current test store object.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Func<object>> InheritStore()
        {
            Initialize();
            return TestBag;
        }

        

        /// <summary>
        /// Gets the declaring parent class.
        /// </summary>
        /// <value>
        /// The declaring parent class.
        /// </value>
        protected ITestExtend Parent
        {
            get
            {
                if (_parent == null)
                {
                    var parentType = this.GetType().DeclaringType;
                    _parent = Activator.CreateInstance(parentType) as ITestExtend;
                }

                return _parent;
            }
        }

        /// <summary>
        /// Includes the declaring parent class' Test Store.
        /// </summary>
        protected void IncludeParent()
        {
            if (Parent != null)
            {
                TestBag = Parent.InheritStore();
            }
        }

        /// <summary>
        /// Includes the subject defined in declaring parent class.
        /// </summary>
        /// <returns></returns>
        protected Func<T> IncludeSubject()
        {
            if (Parent != null)
            {
                return () => (T)Parent.InheritSubject();
            }

            return null;
        }

        /// <summary>
        /// Returns the current subject context.
        /// </summary>
        /// <returns></returns>
        public object InheritSubject()
        {
            //Executes the code in Given
            return Given()();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Destroy();
        }

        #endregion

    }
    
}
