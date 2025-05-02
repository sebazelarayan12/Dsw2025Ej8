using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Ej8.Exceptions
{
    public class SaldoInsuficiente : Exception
    {
        public SaldoInsuficiente(string msg) : base(msg) { }
    }
}
