using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Speculous
{
    public abstract class TestCase<T> : ITestExtension, IDisposable
    {

        protected Dictionary<string, Func<object>> TestBag { get; set; }

        public TestCase()
        {
            TestBag = new Dictionary<string, Func<object>>();

            Initialize();
        }

        protected virtual void Define(string key, Func<object> objectDef)
        {
            TestBag[key] = objectDef;
        }

        protected virtual TObject Get<TObject>(string key)
        {
            if (TestBag.ContainsKey(key))
            {
                return (TObject)TestBag[key]();
            }

            return default(TObject);
        }

        protected Func<T> Subject 
        {
            get { return Given(); }
        }

        protected T It 
        {
            get { return Subject(); }
        }

        protected T Its
        {
            get { return Subject(); }
        }

        protected abstract Func<T> Given();

        protected virtual void Initialize()
        {
            InheritStore();
        }

        protected void InheritStore()
        {
            var parentType = this.GetType().DeclaringType;

            var parent = Activator.CreateInstance(parentType) as ITestExtension;

            if (parent != null)
            {
                TestBag = parent.GetTestBag();
            }
        }


        public Dictionary<string, Func<object>> GetTestBag()
        {
            return TestBag;
        }

        public void Dispose()
        {
            Destroy();
        }

        protected virtual void Destroy()
        {
            
        }
    }

}
