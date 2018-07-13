using ConexionBD;
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
        public List<string> ListaSelects { get; set; }
        private List<clsEsquemaVista> ListaEsquema;
        private DataTable dtColumnas;
        private string Tabla;

        public clsVista()
        {
            ///Aqui irian las lineas a agregar a la lista de querys 
            ///ListaEsquema.Add("select * from Persona;");
            ListaEsquema = new List<clsEsquemaVista>();
            dtColumnas = new DataTable();
        }

        public void cargarListas()
        {
            clsConexion objConexion = new clsConexion();
            if (objConexion.abrirConexion())
            {
                if (dtColumnas != null)
                {
                    for (int i = 0; i < dtColumnas.Rows.Count; i++)
                    {
                        DataTable dtRows = new DataTable();
                        string query = string.Format("select {0} from {1};", dtColumnas.Rows[i][0].ToString(), Tabla);
                        dtRows = objConexion.consultar(query);

                        List<clsColumna> ListaCampos = new List<clsColumna>();
                        foreach (DataRow item in dtRows.Rows)
                        {
                            clsColumna objColumna = new clsColumna(item[0].ToString(), dtColumnas.Rows[i][0].ToString());
                            ListaCampos.Add(objColumna);
                        }
                        clsEsquemaVista objEsquemaVista = new clsEsquemaVista(dtColumnas.Rows[i][0].ToString(), ListaCampos);
                        ListaEsquema.Add(objEsquemaVista);
                    }
                }
                objConexion.cerrarConexion();
            }
        }

        public int obtenerNumeroFilas()
        {
            
            return 12;
        }

        ~clsVista()
        {
            ListaEsquema = null;
            dtColumnas = null;
            Tabla = string.Empty;
        }
    }


    public class clsEsquemaVista
    {
        public string ColumnaxTabla { get; set; }
        public List<clsColumna> ListaDetalleColumnas { get; set; }

        public clsEsquemaVista() { }

        public clsEsquemaVista(string _ColumnaxTabla, List<clsColumna> _ListaDetalleColumnas)
        {
            this.ColumnaxTabla = _ColumnaxTabla;
            this.ListaDetalleColumnas = _ListaDetalleColumnas;
        }

        ~clsEsquemaVista()
        {
            ColumnaxTabla = string.Empty;
            ListaDetalleColumnas = null;
        }
    }

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
