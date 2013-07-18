using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speculous
{
    public abstract class TestCase<T> : Base
    {

        protected virtual void PerformContext()
        {
            var parent = GetParent() as TestCase<T>;

            if (parent != null)
            {
                parent.Given();
            }
        }

        protected T Its
        {
            get { return Subject(); }
        }

        protected Func<T> Subject
        {
            get 
            {
                PerformContext();
                return Given(); 
            }
        }

        protected abstract Func<T> Given();

    }

    public abstract class TestCase : Base
    {
        protected virtual void PerformContext()
        {
            var parent = GetParent() as TestCase;

            if (parent != null)
            {
                parent.Given();
            }
        }

        protected Action Subject
        {
            get 
            {
                PerformContext();
                return Given(); 
            }
        }

        protected abstract Action Given();

    }

    public abstract class Base : IDisposable
    {

        public Base()
        {
            Initialize();
        }

        protected virtual void Initialize()
        { 
        }

        protected virtual void Destroy()
        { 
        }

        protected object GetParent()
        {
            var parentType = this.GetType().DeclaringType;

            return Activator.CreateInstance(parentType);
        }

        Action _destroyer = null;

        protected void With(Action initializer, Action destroyer)
        {
            initializer();
            _destroyer = destroyer;
        }

        public void Dispose()
        {
            if (_destroyer != null)
            {
                _destroyer();
                Destroy();
            }
        }
    }

}
