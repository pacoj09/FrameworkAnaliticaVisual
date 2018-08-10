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
        private string TablaPrincipal;
        private SqlConnection CNX = null;

        #region Constructor
        public clsVista()
        {
            ListaEnlaceColumnas = new List<clsEnlaceColumna>();
            ListadtEnlacesTablas = new List<DataTable>();
            CadenaConexion = "Server=tcp:frameworkanaliticavisual.database.windows.net,1433;Initial Catalog=frameworkanaliticavisual;Persist Security Info=False;User ID=frameworkanaliticavisual;Password=Seminario123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            TablaPrincipal = "persona";
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
            newRow1["Tabla"] = "persona";
            newRow1["Columna"] = "cedula";
            newRow1["Enlace"] = "Posicion Y";
            dt.Rows.Add(newRow1);
            DataRow newRow2 = dt.NewRow();
            newRow2["Tabla"] = "persona";
            newRow2["Columna"] = "id";
            newRow2["Enlace"] = "Posicion X / Label";
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
            newRow1["Tabla"] = "alumno";
            newRow1["Columna"] = "id";
            newRow1["Enlace"] = "Posicion Y";
            dt.Rows.Add(newRow1);
            DataRow newRow2 = dt.NewRow();
            newRow2["Tabla"] = "persona";
            newRow2["Columna"] = "edad";
            newRow2["Enlace"] = "Posicion X / Label";
            dt.Rows.Add(newRow2);
            DataRow newRow3 = dt.NewRow();
            newRow3["Tabla"] = "contacto";
            newRow3["Columna"] = "descripcion";
            newRow3["Enlace"] = "Index Label";
            dt.Rows.Add(newRow3);
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
            newRow1["Tabla"] = "persona";
            newRow1["Columna"] = "edad";
            newRow1["Enlace"] = "Posicion Y";
            dt.Rows.Add(newRow1);
            DataRow newRow2 = dt.NewRow();
            newRow2["Tabla"] = "profesor";
            newRow2["Columna"] = "id";
            newRow2["Enlace"] = "Posicion X / Label";
            dt.Rows.Add(newRow2);
            DataRow newRow3 = dt.NewRow();
            newRow3["Tabla"] = "persona";
            newRow3["Columna"] = "nombre";
            newRow3["Enlace"] = "Index Label";
            dt.Rows.Add(newRow3);
            DataRow newRow4 = dt.NewRow();
            newRow4["Tabla"] = "contacto";
            newRow4["Columna"] = "descripcion";
            newRow4["Enlace"] = "Name";
            dt.Rows.Add(newRow4);
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
                int contador = 0;
                foreach (DataTable dt in this.ListadtEnlacesTablas)
                {
                    List<string> Tablas = new List<string>();
                    List<string> Keys = new List<string>();
                    foreach (DataRow row in dt.Rows)
                    {
                        if (Tablas.Count <= 0)
                        {
                            Tablas.Add(row[0].ToString());
                        }
                        else
                        {
                            bool exito = true;
                            foreach (string item in Tablas)
                            {
                                if (item.Equals(row[0].ToString()))
                                {
                                    exito = false;
                                    break;
                                }
                            }
                            if (exito)
                            {
                                Tablas.Add(row[0].ToString());
                            }
                        }
                    }

                    bool MainTable = false;
                    for (int i = 0; i < Tablas.Count; i++)
                    {
                        if (Tablas.ElementAt(i).Equals(this.TablaPrincipal))
                        {
                            MainTable = true;
                            Keys.Add(getPrimaryKey(Tablas.ElementAt(i)));
                        }
                        else
                        {
                            Keys.Add(getForeignKey(Tablas.ElementAt(i)));
                        }
                    }

                    if (Tablas.Count() == 1)
                    {
                        string y = "null";
                        string label_x = "null";
                        string index_label = "null";
                        string name = "null";
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row[2].ToString().Equals("Posicion Y"))
                            {
                                y = row[1].ToString();
                            }
                            else if (row[2].ToString().Equals("Posicion X / Label"))
                            {
                                label_x = row[1].ToString();
                            }
                            else if (row[2].ToString().Equals("Index Label"))
                            {
                                index_label = row[1].ToString();
                            }
                            else if (row[2].ToString().Equals("Name"))
                            {
                                name = row[1].ToString();
                            }
                        }

                        DataTable dtRows = new DataTable();
                        string query = string.Format("select {0} as 'Posicion Y', {1} as 'Posicion X / Label', {2} as 'Index Label', {3} as 'Name' from {4};", y, label_x, index_label, name, Tablas.FirstOrDefault());
                        dtRows = consultar(query);
                        clsEnlaceColumna objEnlaceColumna = new clsEnlaceColumna(dtRows, contador, dtRows.Rows.Count);
                        contador++;
                        this.ListaEnlaceColumnas.Add(objEnlaceColumna);
                    }
                    else
                    {
                        List<string> y = new List<string>();
                        List<string> label_x = new List<string>();
                        List<string> index_label = new List<string>();
                        List<string> name = new List<string>();
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row[2].ToString().Equals("Posicion Y"))
                            {
                                y.Add(row[0].ToString());
                                y.Add(row[1].ToString());
                            }
                            else if (row[2].ToString().Equals("Posicion X / Label"))
                            {
                                label_x.Add(row[0].ToString());
                                label_x.Add(row[1].ToString());
                            }
                            else if (row[2].ToString().Equals("Index Label"))
                            {
                                index_label.Add(row[0].ToString());
                                index_label.Add(row[1].ToString());
                            }
                            else if (row[2].ToString().Equals("Name"))
                            {
                                name.Add(row[0].ToString());
                                name.Add(row[1].ToString());
                            }
                        }

                        DataTable dtRows = new DataTable();
                        string query = string.Format("select {0} as 'Posicion Y', {1} as 'Posicion X / Label', {2} as 'Index Label', {3} as 'Name' from {4};", getColumnas(y), getColumnas(label_x), getColumnas(index_label), getColumnas(name), getTablas(Tablas, Keys, MainTable));
                        dtRows = consultar(query);
                        clsEnlaceColumna objEnlaceColumna = new clsEnlaceColumna(dtRows, contador, dtRows.Rows.Count);
                        contador++;
                        this.ListaEnlaceColumnas.Add(objEnlaceColumna);
                    }
                }
                cerrarConexion();
            }
        }

        private string getTablas(List<string> Tablas, List<string> Keys, bool MainTable)
        {
            string innerJoin = "";
            if (Tablas.Count == 1)
            {
                innerJoin = Tablas.FirstOrDefault();
            }
            else
            {
                if (MainTable)
                {
                    string PrimaryKey = "";
                    int IndexMainTable = 0;
                    for (int i = 0; i < Tablas.Count; i++)
                    {
                        if (Tablas.ElementAt(i).Equals(this.TablaPrincipal))
                        {
                            IndexMainTable = i;
                            break;
                        }
                    }
                    PrimaryKey = Keys.ElementAt(IndexMainTable);
                    Tablas.RemoveAt(IndexMainTable);
                    Keys.RemoveAt(IndexMainTable);
                    innerJoin = this.TablaPrincipal;
                    for (int i = 0; i < Tablas.Count; i++)
                    {
                        innerJoin += string.Format(" inner join {0} on {1}.{2} = {3}.{4}", Tablas.ElementAt(i), this.TablaPrincipal, PrimaryKey, Tablas.ElementAt(i), Keys.ElementAt(i));
                    }
                }
                else
                {
                    string PrimaryKey = getPrimaryKey(this.TablaPrincipal);
                    innerJoin = this.TablaPrincipal;
                    for (int i = 0; i < Tablas.Count; i++)
                    {
                        innerJoin += string.Format(" inner join {0} on {1}.{2} = {3}.{4}", Tablas.ElementAt(i), this.TablaPrincipal, PrimaryKey, Tablas.ElementAt(i), Keys.ElementAt(i));
                    }
                }
            }
            return innerJoin;
        }

        private string getColumnas(List<string> Campos)
        {
            string columnas = "null";
            if (Campos.Count > 0)
            {
                columnas = Campos.ElementAt(0) + "." + Campos.ElementAt(1);
            }
            return columnas;
        }

        private string getPrimaryKey(string tabla)
        {
            string primaryKey = "";
            CNX = new SqlConnection(CadenaConexion);
            if (abrirConexion())
            {
                DataTable dt = new DataTable();
                string query = string.Format("SELECT FK_Table = FK.TABLE_NAME, FK_Column = CU.COLUMN_NAME, PK_Table = PK.TABLE_NAME, PK_Column = PT.COLUMN_NAME, Constraint_Name = C.CONSTRAINT_NAME "
                    + "FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK "
                    + "ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME INNER JOIN(SELECT i1.TABLE_NAME, i2.COLUMN_NAME "
                    + "FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1 INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2 ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME WHERE i1.CONSTRAINT_TYPE = 'PRIMARY KEY') PT "
                    + "ON PT.TABLE_NAME = PK.TABLE_NAME where PK.TABLE_NAME = '{0}';", tabla);
                dt = consultar(query);
                if (dt.Rows.Count > 0)
                {
                    primaryKey = dt.Rows[0][3].ToString();
                }
                else
                {
                    primaryKey = string.Empty;
                }
                cerrarConexion();
            }
            return primaryKey;
        }

        private string getForeignKey(string tabla)
        {
            string foreignKey = "";
            CNX = new SqlConnection(CadenaConexion);
            if (abrirConexion())
            {
                DataTable dt = new DataTable();
                string query = string.Format("SELECT FK_Table = FK.TABLE_NAME, FK_Column = CU.COLUMN_NAME, PK_Table = PK.TABLE_NAME, PK_Column = PT.COLUMN_NAME, Constraint_Name = C.CONSTRAINT_NAME "
                    + "FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK "
                    + "ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME INNER JOIN(SELECT i1.TABLE_NAME, i2.COLUMN_NAME "
                    + "FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1 INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2 ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME WHERE i1.CONSTRAINT_TYPE = 'PRIMARY KEY') PT "
                    + "ON PT.TABLE_NAME = PK.TABLE_NAME where FK.TABLE_NAME = '{0}' and PK.TABLE_NAME = '{1}';", tabla, this.TablaPrincipal);
                dt = consultar(query);
                if (dt.Rows.Count > 0)
                {
                    foreignKey = dt.Rows[0][1].ToString();
                }
                else
                {
                    foreignKey = string.Empty;
                }
                cerrarConexion();
            }
            return foreignKey;
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
        private DataTable dtColumnas;
        private int Vista;
        private int MaxRows;

        public DataTable getdtColumnas()
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

        public clsEnlaceColumna(DataTable _dtColumnas, int _Vista, int _MaxRows)
        {
            this.dtColumnas = _dtColumnas;
            this.Vista = _Vista;
            this.MaxRows = _MaxRows;
        }
    }
}