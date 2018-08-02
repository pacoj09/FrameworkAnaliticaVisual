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
        private List<DataTable> ListaEsquema;
        private DataTable dtColumnas;
        private string Tabla;
        private List<DataTable> ListadtEnlacesTablas;

        public clsVista()
        {
            cargarVista1();
        }

        #region Vista1
        public void cargarVista1() {
            ListadtEnlacesTablas = new List<DataTable>();
            DataTable dt = new DataTable();
            DataColumn column;
            column = new DataColumn();
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Tabla";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Columna";
            dt.Columns.Add(column);
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Enlace";
            dt.Columns.Add(column);
            ///se recorre con for
            DataRow newRow = dt.NewRow();
            newRow["Tabla"] = "persona";
            newRow["Columna"] = "id";
            newRow["Enlace"] = "Posicion X";
            dt.Rows.Add(newRow);
            ///termina for
            ListadtEnlacesTablas.Add(dt);
        }
        #endregion Vista1

        public void cargarListas()
        {
            clsConexion objConexion = clsConexion.obtenerclsConexion();
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
                        //ListaEsquema.Add(objEsquemaVista);
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
