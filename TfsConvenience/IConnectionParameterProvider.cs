using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsConvenience
{
    public interface IConnectionParameterProvider
    {
        ConnectionParameters GetParameters();
    }
}
