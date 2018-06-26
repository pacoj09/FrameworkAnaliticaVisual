using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class clsTabla
    {
        public string NombreTabla { get; set; }
        public string NombreColumna { get; set; }

        public clsTabla() { }

        public clsTabla(string _NombreTabla, string _NombreColumna)
        {
            this.NombreTabla = _NombreTabla;
            this.NombreColumna = _NombreColumna;
        }

        ~clsTabla()
        {
            this.NombreTabla = string.Empty;
            this.NombreColumna = string.Empty;
        }
    }
}
