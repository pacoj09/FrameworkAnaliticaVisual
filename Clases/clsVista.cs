using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class clsVista
    {
        public DataTable dtVista { get; set; }

        public clsVista() { }

        public clsVista(DataTable _dtDatos)
        {
            dtVista = new DataTable();
            dtVista = _dtDatos;
        }
    }
}
