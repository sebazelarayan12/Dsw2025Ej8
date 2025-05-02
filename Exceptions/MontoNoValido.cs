using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Ej8.Exceptions
{
    public class MontoNoValido : Exception
    {
        public MontoNoValido(string msg) : base(msg) { }
    }
}
