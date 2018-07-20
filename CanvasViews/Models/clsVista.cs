using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CanvasViews.Models
{
    public class clsVista
    {
        private static clsVista objVista = new clsVista();
        private static List<clsEsquemaVista> ListaEsquema;
        private static DataTable dtColumnas;
        private static string Tabla;
        private static string cadenaConexion;

        private SqlConnection CNX = null;

        #region Constructor
        private clsVista()
        {
            cadenaConexion = "Server=tcp:frameworkanaliticavisual.database.windows.net,1433;Initial Catalog=frameworkanaliticavisual;Persist Security Info=False;User ID=frameworkanaliticavisual;Password=Seminario123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            setTabla("Persona");
            ListaEsquema = new List<clsEsquemaVista>();
            dtColumnas = new DataTable();
            dtColumnas.Columns.Add("Columnas_Tabla", typeof(String));
            dtColumnas.Columns.Add("Datos_Canvas", typeof(String));
            DataRow NewRow = dtColumnas.NewRow();
            NewRow[0] = "id";
            NewRow[1] = "Posicion X";
            dtColumnas.Rows.Add(NewRow);
            DataRow NewRow1 = dtColumnas.NewRow();
            NewRow1[0] = "id_carrera";
            NewRow1[1] = "Posicion Y";
            dtColumnas.Rows.Add(NewRow1);
            cargarListas();
        }
        #endregion

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

        public bool abrirConexion()
        {
            bool Exito = true;
            try
            {
                CNX.Open();
            }
            catch (Exception)
            {
                Exito = false;
            }
            return Exito;
        }

        public bool cerrarConexion()
        {
            bool Exito = true;
            try
            {
                CNX.Close();
            }
            catch (Exception)
            {
                Exito = false;
            }
            return Exito;
        }

        public DataTable consultar(string query)
        {
            DataTable dtResultado = new DataTable();
            try
            {
                SqlCommand Comando = new SqlCommand(query, CNX);
                Comando.CommandType = CommandType.Text;

                SqlDataAdapter Adapter = new SqlDataAdapter(Comando);
                Adapter.Fill(dtResultado);
            }
            catch
            {
                dtResultado = null;
            }
            return dtResultado;
        }

        public void cargarListas()
        {
            CNX = new SqlConnection(cadenaConexion);
            if (abrirConexion())
            {
                if (dtColumnas != null)
                {
                    for (int i = 0; i < dtColumnas.Rows.Count; i++)
                    {
                        DataTable dtRows = new DataTable();
                        string query = string.Format("select {0} from {1};", dtColumnas.Rows[i][0].ToString(), Tabla);
                        dtRows = consultar(query);

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
                cerrarConexion();
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