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
        private List<DataTable> ListadtEnlacesTablas;
        private string CadenaConexion;

        #region Constructor
        public clsVista()
        {
            ListadtEnlacesTablas = new List<DataTable>();
            CadenaConexion = "Server=tcp:frameworkanaliticavisual.database.windows.net,1433;Initial Catalog=frameworkanaliticavisual;Persist Security Info=False;User ID=frameworkanaliticavisual;Password=Seminario123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            cargarVista1();
            cargarVista2();
            cargarVista3();
            cargarVista4();
        }
        #endregion Constructor

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
            newRow1["Columna"] = "id";
            newRow1["Enlace"] = "Posicion Y";
            dt.Rows.Add(newRow1);
            DataRow newRow2 = dt.NewRow();
            newRow2["Tabla"] = "persona";
            newRow2["Columna"] = "cedula";
            newRow2["Enlace"] = "Posicion X";
            dt.Rows.Add(newRow2);
            DataRow newRow3 = dt.NewRow();
            newRow3["Tabla"] = "persona";
            newRow3["Columna"] = "nombre";
            newRow3["Enlace"] = "Label";
            dt.Rows.Add(newRow3);
            DataRow newRow4 = dt.NewRow();
            newRow4["Tabla"] = "persona";
            newRow4["Columna"] = "edad";
            newRow4["Enlace"] = "Index Label";
            dt.Rows.Add(newRow4);
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
            newRow1["Columna"] = "cedula";
            newRow1["Enlace"] = "Label";
            dt.Rows.Add(newRow1);
            DataRow newRow2 = dt.NewRow();
            newRow2["Tabla"] = "alumno";
            newRow2["Columna"] = "id";
            newRow2["Enlace"] = "Posicion Y";
            dt.Rows.Add(newRow2);
            DataRow newRow3 = dt.NewRow();
            newRow3["Tabla"] = "persona";
            newRow3["Columna"] = "profesion";
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
            newRow1["Columna"] = "nombre";
            newRow1["Enlace"] = "Label";
            dt.Rows.Add(newRow1);
            DataRow newRow2 = dt.NewRow();
            newRow2["Tabla"] = "contacto";
            newRow2["Columna"] = "tipo";
            newRow2["Enlace"] = "Posicion Y";
            dt.Rows.Add(newRow2);
            DataRow newRow3 = dt.NewRow();
            newRow3["Tabla"] = "contacto";
            newRow3["Columna"] = "descripcion";
            newRow3["Enlace"] = "Index Label";
            dt.Rows.Add(newRow3);
            ListadtEnlacesTablas.Add(dt);
        }
        #endregion Vista3

        #region Vista4
        public void cargarVista4()
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
            newRow2["Tabla"] = "alumno";
            newRow2["Columna"] = "beca";
            newRow2["Enlace"] = "Label";
            dt.Rows.Add(newRow2);
            DataRow newRow3 = dt.NewRow();
            newRow3["Tabla"] = "contacto";
            newRow3["Columna"] = "id_persona";
            newRow3["Enlace"] = "Index Label";
            dt.Rows.Add(newRow3);
            ListadtEnlacesTablas.Add(dt);
        }
        #endregion Vista4

        #region FuncionCargar
        public void cargarListas()
        {

        }
        #endregion FuncionCargar

        ~clsVista()
        {

        }
    }
}
