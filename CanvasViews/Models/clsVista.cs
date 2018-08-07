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
        private List<clsEnlaceColumna> ListaEnlaceColumnas;
        private List<DataTable> ListadtEnlacesTablas;
        private string CadenaConexion;
        private SqlConnection CNX = null;

        #region Constructor
        public clsVista()
        {
            ListaEnlaceColumnas = new List<clsEnlaceColumna>();
            ListadtEnlacesTablas = new List<DataTable>();
            CadenaConexion = "Server=tcp:frameworkanaliticavisual.database.windows.net,1433;Initial Catalog=frameworkanaliticavisual;Persist Security Info=False;User ID=frameworkanaliticavisual;Password=Seminario123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            cargarVista1();
            cargarVista2();
            cargarVista3();
        }
        #endregion Constructor

        public List<clsEnlaceColumna> getListaEnlaceColumnas()
        {
            return this.ListaEnlaceColumnas;
        }

        public List<DataTable> getListadtEnlacesTablas()
        {
            return this.ListadtEnlacesTablas;
        }

        #region Vista1
        public void cargarVista1()
        {
            DataTable dt = new DataTable();
            DataColumn column;
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Tabla";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Columna";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Enlace";
            dt.Columns.Add(column);
            DataRow newRow1 = dt.NewRow();
            newRow1["Tabla"] = "carrera";
            newRow1["Columna"] = "id";
            newRow1["Enlace"] = "Posicion Y";
            dt.Rows.Add(newRow1);
            DataRow newRow2 = dt.NewRow();
            newRow2["Tabla"] = "carrera";
            newRow2["Columna"] = "nombre";
            newRow2["Enlace"] = "Posicion X";
            dt.Rows.Add(newRow2);
            ListadtEnlacesTablas.Add(dt);
        }
        #endregion Vista1

        #region Vista2
        public void cargarVista2()
        {
            DataTable dt = new DataTable();
            DataColumn column;
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Tabla";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Columna";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Enlace";
            dt.Columns.Add(column);
            DataRow newRow1 = dt.NewRow();
            newRow1["Tabla"] = "persona";
            newRow1["Columna"] = "id";
            newRow1["Enlace"] = "Posicion Y";
            dt.Rows.Add(newRow1);
            DataRow newRow2 = dt.NewRow();
            newRow2["Tabla"] = "carrera";
            newRow2["Columna"] = "nombre";
            newRow2["Enlace"] = "Posicion X";
            dt.Rows.Add(newRow2);
            ListadtEnlacesTablas.Add(dt);
        }
        #endregion Vista2

        #region Vista3
        public void cargarVista3()
        {
            DataTable dt = new DataTable();
            DataColumn column;
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Tabla";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Columna";
            dt.Columns.Add(column);
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "Enlace";
            dt.Columns.Add(column);
            DataRow newRow1 = dt.NewRow();
            newRow1["Tabla"] = "profesor";
            newRow1["Columna"] = "id";
            newRow1["Enlace"] = "Posicion Y";
            dt.Rows.Add(newRow1);
            DataRow newRow2 = dt.NewRow();
            newRow2["Tabla"] = "persona";
            newRow2["Columna"] = "nombre";
            newRow2["Enlace"] = "Posicion X";
            dt.Rows.Add(newRow2);
            ListadtEnlacesTablas.Add(dt);
        }
        #endregion Vista3

        #region Vista4
        public void cargarVista4()
        {





        }
        #endregion Vista4

        public void cargarListas()
        {
            CNX = new SqlConnection(CadenaConexion);
            if (abrirConexion())
            {
                int MaxRows = 0;
                int contador = 0;
                foreach (DataTable dt in this.ListadtEnlacesTablas)
                {
                    List<DataTable> ListadtEnlaces = new List<DataTable>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataTable dtRows = new DataTable();
                        string query = string.Format("select {0} from {1};", dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString());
                        dtRows = consultar(query);
                        if (dtRows.Rows.Count > 0)
                        {
                            ListadtEnlaces.Add(dtRows);
                        }

                        if (contador == 0)
                        {
                            MaxRows = dtRows.Rows.Count;
                        }
                        else if (MaxRows < dtRows.Rows.Count)
                        {
                            MaxRows = dtRows.Rows.Count;
                        }
                    }
                    clsEnlaceColumna objEnlaceColumna = new clsEnlaceColumna(ListadtEnlaces, contador, MaxRows);
                    contador++;
                    this.ListaEnlaceColumnas.Add(objEnlaceColumna);
                }
                cerrarConexion();
            }
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

        ~clsVista()
        {

        }
    }

    public class clsEnlaceColumna
    {
        private List<DataTable> dtColumnas;
        private int Vista;
        private int MaxRows;

        public List<DataTable> getdtColumnas()
        {
            return this.dtColumnas;
        }

        public int getVista()
        {
            return this.Vista;
        }

        public int getMaxRows()
        {
            return this.MaxRows;
        }

        public clsEnlaceColumna(List<DataTable> _dtColumnas, int _Vista, int _MaxRows)
        {
            this.dtColumnas = _dtColumnas;
            this.Vista = _Vista;
            this.MaxRows = _MaxRows;
        }
    }
}