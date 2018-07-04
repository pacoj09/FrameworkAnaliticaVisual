using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class clsColumna
    {
        public string Dato { get; set; }
        public string Columna { get; set; }

        public clsColumna() { }

        public clsColumna(string _Dato, string _Columna)
        {
            this.Dato = _Dato;
            this.Columna = _Columna;
        }

        ~clsColumna()
        {
            this.Dato = string.Empty;
            this.Columna = string.Empty;
        }
    }
}
