using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Ej8.Exceptions
{
    public class CuentaNoActiva : Exception
    {
        public CuentaNoActiva(string msg) : base(msg) { }
    }
}
