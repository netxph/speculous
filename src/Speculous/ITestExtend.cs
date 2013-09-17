using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speculous
{
    public interface ITestExtend
    {
        Dictionary<string, Func<object>> InheritStore();
        object InheritSubject();
    }
}
