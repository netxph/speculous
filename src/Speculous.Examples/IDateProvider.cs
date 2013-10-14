using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speculous.Examples
{
    public interface IDateProvider
    {

        DateTime UtcNow();

    }
}
