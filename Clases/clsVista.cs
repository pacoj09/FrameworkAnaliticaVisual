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
        private static clsVista objVista = new clsVista();
        private static List<clsEsquemaVista> ListaEsquema;
        private static DataTable dtColumnas;
        private static string Tabla;

        private clsVista()
        {
            ListaEsquema = new List<clsEsquemaVista>();
            dtColumnas = new DataTable();
        }

        public static clsVista obtenerclsVista()
        {
            return objVista;
        }

        public void setdtColumnas(DataTable _dtColumnas)
        {
            dtColumnas = _dtColumnas;
        }

        public void setTabla(string _Tabla)
        {
            Tabla = _Tabla;
        }

        public List<clsEsquemaVista> getListaEsquema()
        {
            return ListaEsquema;
        }

        public DataTable getdtColumnas()
        {
            return dtColumnas;
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
            int numero = 0;
            for (int i = 0; i < ListaEsquema.ElementAt(0).ListaDetalleColumnas.Count; i++)
            {
                numero++;
            }
            return numero;
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
}
