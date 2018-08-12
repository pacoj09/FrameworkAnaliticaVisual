using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class clsGenerador
    {
        private List<DataTable> ListadtEnlacesTablas;
        private string TablaPrincipal;

        public void setListadtEnlacesTablas(List<DataTable> _ListadtEnlacesTablas)
        {
            this.ListadtEnlacesTablas = new List<DataTable>();
            this.ListadtEnlacesTablas = _ListadtEnlacesTablas;
        }

        public void setTablaPrincipal(string _TablaPrincipal)
        {
            this.TablaPrincipal = _TablaPrincipal;
        }

        public clsGenerador() { }

        public void GenerateMethod(string path)
        {
            File.WriteAllLines(path, WriteMethodClaseVista());
            List<string> readText = new List<string>();
            readText = File.ReadAllLines(path).ToList<string>();
            for (int i = 0; i < readText.Count(); i++)
            {
                if (readText[i].Trim().Equals("#region Constructor"))
                {
                    bool enable = false;
                    List<string> readTextRest = new List<string>();
                    readTextRest = readText.GetRange((i + 2), (readText.Count - (i + 2)));
                    readText.RemoveRange((i + 2), (readText.Count - (i + 2)));
                    for (int j = 0; j < readTextRest.Count; j++)
                    {
                        if (readTextRest[j + 2].Trim().Equals("#endregion Constructor"))
                        {
                            readTextRest.RemoveRange(0, j);
                            readTextRest[0] = WriteMethodConstructor(ListadtEnlacesTablas.Count);
                            enable = true;
                            break;
                        }
                    }

                    if (enable)
                    {
                        var ListaCombinada = readText.Concat(readTextRest);
                        File.WriteAllLines(path, ListaCombinada);
                        break;
                    }
                }
            }

            int contador = 1;
            foreach (DataTable dt in this.ListadtEnlacesTablas)
            {
                readText.Clear();
                readText = File.ReadAllLines(path).ToList<string>();
                for (int i = 0; i < readText.Count(); i++)
                {
                    if (readText[i].Trim().Equals("#region Vista" + contador))
                    {
                        bool enable = false;
                        List<string> readTextRest = new List<string>();
                        readTextRest = readText.GetRange((i + 2), (readText.Count - (i + 2)));
                        readText.RemoveRange((i + 2), (readText.Count - (i + 2)));
                        for (int j = 0; j < readTextRest.Count; j++)
                        {
                            if (readTextRest[j + 2].Trim().Equals("#endregion Vista" + contador))
                            {
                                readTextRest.RemoveRange(0, j);
                                readTextRest[0] = WriteMethodVistas(dt);
                                enable = true;
                                break;
                            }
                        }

                        if (enable)
                        {
                            var ListaCombinada = readText.Concat(readTextRest);
                            File.WriteAllLines(path, ListaCombinada);
                            break;
                        }
                    }
                }
                contador++;
            }
        }

        private string WriteMethodConstructor(int _numeroVistas)
        {
            clsEsquema objEsquema = clsEsquema.obtenerclsEsquema();
            string Datos = string.Format("\t\tListadtEnlacesTablas = new List<DataTable>();"
                + "\r\n\t\tListaEnlaceColumnas = new List<clsEnlaceColumna>();"
                + "\r\n\t\tTablaPrincipal = \"{0}\";\r\n\t\tCadenaConexion = \"{1}\";", this.TablaPrincipal, objEsquema.getConnectionString());
            for (int i = 0; i < _numeroVistas; i++)
            {
                Datos += "\r\n\t\tcargarVista" + (i + 1) + "();";
            }
            return Datos;
        }

        private string WriteMethodVistas(DataTable _dtEnlacesTablas)
        {
            string Datos = "\t\tDataTable dt = new DataTable();"
                + "\r\n\t\tDataColumn column;"
                + "\r\n\t\tcolumn = new DataColumn();"
                + "\r\n\t\tcolumn.DataType = Type.GetType(\"System.String\");"
                + "\r\n\t\tcolumn.ColumnName = \"Tabla\";"
                + "\r\n\t\tdt.Columns.Add(column);"
                + "\r\n\t\tcolumn = new DataColumn();"
                + "\r\n\t\tcolumn.DataType = Type.GetType(\"System.String\");"
                + "\r\n\t\tcolumn.ColumnName = \"Columna\";"
                + "\r\n\t\tdt.Columns.Add(column);"
                + "\r\n\t\tcolumn = new DataColumn();"
                + "\r\n\t\tcolumn.DataType = Type.GetType(\"System.String\");"
                + "\r\n\t\tcolumn.ColumnName = \"Enlace\";"
                + "\r\n\t\tdt.Columns.Add(column);";
            int contador = 1;
            foreach (DataRow row in _dtEnlacesTablas.Rows)
            {
                Datos += string.Format("\r\n\t\tDataRow newRow" + contador + " = dt.NewRow();"
                    + "\r\n\t\tnewRow" + contador + "[\"Tabla\"] = \"{0}\";"
                    + "\r\n\t\tnewRow" + contador + "[\"Columna\"] = \"{1}\";"
                    + "\r\n\t\tnewRow" + contador + "[\"Enlace\"] = \"{2}\";"
                    + "\r\n\t\tdt.Rows.Add(newRow" + contador + ");", row[0].ToString(), row[1].ToString(), row[2].ToString());
                contador++;
            }
            Datos += "\r\n\t\tListadtEnlacesTablas.Add(dt);";
            return Datos;
        }

        private string[] WriteMethodClaseVista()
        {
            string[] claseVista = {"using System;"+
                                "\r\nusing System.Collections.Generic;"+
                                "\r\nusing System.Data;"+
                                "\r\nusing System.Data.SqlClient;"+
                                "\r\nusing System.Linq;"+
                                "\r\nusing System.Text;"+
                                "\r\n\r\nnamespace CanvasViews.Models"+
                                "\r\n{"+
                                "\r\n\tpublic class clsVista"+
                                "\r\n\t{"+
                                "\r\n\t\tprivate List<DataTable> ListadtEnlacesTablas;"+
                                "\r\n\t\tprivate List<clsEnlaceColumna> ListaEnlaceColumnas;"+
                                "\r\n\t\tprivate string CadenaConexion;"+
                                "\r\n\t\tprivate string TablaPrincipal;"+
                                "\r\n\t\tprivate SqlConnection CNX = null;"+
                                "\r\n"+
                                "\r\n\t\t#region Constructor"+
                                "\r\n\t\tpublic clsVista(){"+
                                "\r\n\r\n\r\n\r\n\r\n"+
                                "\r\n\t\t}"+
                                "\r\n\t\t#endregion Constructor"+
                                "\r\n"+
                                "\r\n\t\tpublic List<clsEnlaceColumna> getListaEnlaceColumnas()"+
                                "\r\n\t\t{"+
                                "\r\n\t\t\treturn this.ListaEnlaceColumnas;"+
                                "\r\n\t\t}"+
                                "\r\n"+
                                "\r\n\t\tpublic List<DataTable> getListadtEnlacesTablas()"+
                                "\r\n\t\t{"+
                                "\r\n\t\t\treturn this.ListadtEnlacesTablas;"+
                                "\r\n\t\t}"+
                                "\r\n"+
                                "\r\n\t\t#region Vista1"+
                                "\r\n\t\tpublic void cargarVista1(){"+
                                "\r\n\r\n\r\n\r\n\r\n"+
                                "\r\n\t\t}"+
                                "\r\n\t\t#endregion Vista1"+
                                "\r\n"+
                                "\r\n\t\t#region Vista2"+
                                "\r\n\t\tpublic void cargarVista2(){"+
                                "\r\n\r\n\r\n\r\n\r\n"+
                                "\r\n\t\t}"+
                                "\r\n\t\t#endregion Vista2"+
                                "\r\n"+
                                "\r\n\t\t#region Vista3"+
                                "\r\n\t\tpublic void cargarVista3(){"+
                                "\r\n\r\n\r\n\r\n\r\n"+
                                "\r\n\t\t}"+
                                "\r\n\t\t#endregion Vista3"+
                                "\r\n"+
                                "\r\n\t\t#region Vista4"+
                                "\r\n\t\tpublic void cargarVista4(){"+
                                "\r\n\r\n\r\n\r\n\r\n"+
                                "\r\n\t\t}"+
                                "\r\n\t\t#endregion Vista4"+
                                "\r\n"+
                                "\r\n\t\t#region FuncionCargar"+
                                "\r\n\t\tpublic void cargarListas()"+
                                "\r\n\t\t{"+
                                "\r\n\t\t\tCNX = new SqlConnection(CadenaConexion);"+
                                "\r\n\t\t\tif (abrirConexion())"+
                                "\r\n\t\t\t{"+
                                "\r\n\t\t\t\tint contador = 0;"+
                                "\r\n\t\t\t\tforeach (DataTable dt in this.ListadtEnlacesTablas)"+
                                "\r\n\t\t\t\t{"+
                                "\r\n\t\t\t\t\tList<string> Tablas = new List<string>();"+
                                "\r\n\t\t\t\t\tList<string> Keys = new List<string>();"+
                                "\r\n\t\t\t\t\tforeach (DataRow row in dt.Rows)"+
                                "\r\n\t\t\t\t\t{"+
                                "\r\n\t\t\t\t\t\tif (Tablas.Count <= 0)"+
                                "\r\n\t\t\t\t\t\t{"+
                                "\r\n\t\t\t\t\t\t\tTablas.Add(row[0].ToString());"+
                                "\r\n\t\t\t\t\t\t}"+
                                "\r\n\t\t\t\t\t\telse"+
                                "\r\n\t\t\t\t\t\t{"+
                                "\r\n\t\t\t\t\t\t\tbool exito = true;"+
                                "\r\n\t\t\t\t\t\t\tforeach (string item in Tablas)"+
                                "\r\n\t\t\t\t\t\t\t{"+
                                "\r\n\t\t\t\t\t\t\t\tif (item.Equals(row[0].ToString()))"+
                                "\r\n\t\t\t\t\t\t\t\t{"+
                                "\r\n\t\t\t\t\t\t\t\t\texito = false;"+
                                "\r\n\t\t\t\t\t\t\t\t\tbreak;"+
                                "\r\n\t\t\t\t\t\t\t\t}"+
                                "\r\n\t\t\t\t\t\t\t}"+
                                "\r\n\t\t\t\t\t\t\tif (exito)"+
                                "\r\n\t\t\t\t\t\t\t{"+
                                "\r\n\t\t\t\t\t\t\t\tTablas.Add(row[0].ToString());"+
                                "\r\n\t\t\t\t\t\t\t}"+
                                "\r\n\t\t\t\t\t\t}"+
                                "\r\n\t\t\t\t\t}"+
                                "\r\n"+
                                "\r\n\t\t\t\t\tbool MainTable = false;"+
                                "\r\n\t\t\t\t\tfor (int i = 0; i < Tablas.Count; i++)"+
                                "\r\n\t\t\t\t\t{"+
                                "\r\n\t\t\t\t\t\tif (Tablas.ElementAt(i).Equals(this.TablaPrincipal))"+
                                "\r\n\t\t\t\t\t\t{"+
                                "\r\n\t\t\t\t\t\t\tMainTable = true;"+
                                "\r\n\t\t\t\t\t\t\tKeys.Add(getPrimaryKey(Tablas.ElementAt(i)));"+
                                "\r\n\t\t\t\t\t\t}"+
                                "\r\n\t\t\t\t\t\telse"+
                                "\r\n\t\t\t\t\t\t{"+
                                "\r\n\t\t\t\t\t\t\tKeys.Add(getForeignKey(Tablas.ElementAt(i)));"+
                                "\r\n\t\t\t\t\t\t}"+
                                "\r\n\t\t\t\t\t}"+
                                "\r\n"+
                                "\r\n\t\t\t\t\tif (Tablas.Count() == 1)"+
                                "\r\n\t\t\t\t\t{"+
                                "\r\n\t\t\t\t\t\tstring y = \"null\";"+
                                "\r\n\t\t\t\t\t\tstring label_x = \"null\";"+
                                "\r\n\t\t\t\t\t\tstring index_label = \"null\";"+
                                "\r\n\t\t\t\t\t\tstring name = \"null\";"+
                                "\r\n\t\t\t\t\t\tforeach (DataRow row in dt.Rows)"+
                                "\r\n\t\t\t\t\t\t{"+
                                "\r\n\t\t\t\t\t\t\tif (row[2].ToString().Equals(\"Posicion Y\"))"+
                                "\r\n\t\t\t\t\t\t\t{"+
                                "\r\n\t\t\t\t\t\t\t\ty = row[1].ToString();"+
                                "\r\n\t\t\t\t\t\t\t}"+
                                "\r\n\t\t\t\t\t\t\telse if (row[2].ToString().Equals(\"Posicion X / Label\"))"+
                                "\r\n\t\t\t\t\t\t\t{"+
                                "\r\n\t\t\t\t\t\t\t\tlabel_x = row[1].ToString();"+
                                "\r\n\t\t\t\t\t\t\t}"+
                                "\r\n\t\t\t\t\t\t\telse if (row[2].ToString().Equals(\"Index Label\"))"+
                                "\r\n\t\t\t\t\t\t\t{"+
                                "\r\n\t\t\t\t\t\t\t\tindex_label = row[1].ToString();"+
                                "\r\n\t\t\t\t\t\t\t}"+
                                "\r\n\t\t\t\t\t\t\telse if (row[2].ToString().Equals(\"Name\"))"+
                                "\r\n\t\t\t\t\t\t\t{"+
                                "\r\n\t\t\t\t\t\t\t\tname = row[1].ToString();"+
                                "\r\n\t\t\t\t\t\t\t}"+
                                "\r\n\t\t\t\t\t\t}"+
                                "\r\n"+
                                "\r\n\t\t\t\t\t\tDataTable dtRows = new DataTable();"+
                                "\r\n\t\t\t\t\t\tstring query = string.Format(\"select {0} as 'id', {1} as 'Posicion Y', {2} as 'Posicion X / Label', {3} as 'Index Label', {4} as 'Name' from {5};\", this.TablaPrincipal + \".\" + getPrimaryKey(this.TablaPrincipal), y, label_x, index_label, name, Tablas.FirstOrDefault());"+
                                "\r\n\t\t\t\t\t\tdtRows = consultar(query);"+
                                "\r\n\t\t\t\t\t\tclsEnlaceColumna objEnlaceColumna = new clsEnlaceColumna(dtRows, contador, dtRows.Rows.Count);"+
                                "\r\n\t\t\t\t\t\tcontador++;"+
                                "\r\n\t\t\t\t\t\tthis.ListaEnlaceColumnas.Add(objEnlaceColumna);"+
                                "\r\n\t\t\t\t\t}"+
                                "\r\n\t\t\t\t\telse"+
                                "\r\n\t\t\t\t\t{"+
                                "\r\n\t\t\t\t\t\tList<string> y = new List<string>();"+
                                "\r\n\t\t\t\t\t\tList<string> label_x = new List<string>();"+
                                "\r\n\t\t\t\t\t\tList<string> index_label = new List<string>();"+
                                "\r\n\t\t\t\t\t\tList<string> name = new List<string>();"+
                                "\r\n\t\t\t\t\t\tforeach (DataRow row in dt.Rows)"+
                                "\r\n\t\t\t\t\t\t{"+
                                "\r\n\t\t\t\t\t\t\tif (row[2].ToString().Equals(\"Posicion Y\"))"+
                                "\r\n\t\t\t\t\t\t\t{"+
                                "\r\n\t\t\t\t\t\t\t\ty.Add(row[0].ToString());"+
                                "\r\n\t\t\t\t\t\t\t\ty.Add(row[1].ToString());"+
                                "\r\n\t\t\t\t\t\t\t}"+
                                "\r\n\t\t\t\t\t\t\telse if (row[2].ToString().Equals(\"Posicion X / Label\"))"+
                                "\r\n\t\t\t\t\t\t\t{"+
                                "\r\n\t\t\t\t\t\t\t\tlabel_x.Add(row[0].ToString());"+
                                "\r\n\t\t\t\t\t\t\t\tlabel_x.Add(row[1].ToString());"+
                                "\r\n\t\t\t\t\t\t\t}"+
                                "\r\n\t\t\t\t\t\t\telse if (row[2].ToString().Equals(\"Index Label\"))"+
                                "\r\n\t\t\t\t\t\t\t{"+
                                "\r\n\t\t\t\t\t\t\t\tindex_label.Add(row[0].ToString());"+
                                "\r\n\t\t\t\t\t\t\t\tindex_label.Add(row[1].ToString());"+
                                "\r\n\t\t\t\t\t\t\t}"+
                                "\r\n\t\t\t\t\t\t\telse if (row[2].ToString().Equals(\"Name\"))"+
                                "\r\n\t\t\t\t\t\t\t{"+
                                "\r\n\t\t\t\t\t\t\t\tname.Add(row[0].ToString());"+
                                "\r\n\t\t\t\t\t\t\t\tname.Add(row[1].ToString());"+
                                "\r\n\t\t\t\t\t\t\t}"+
                                "\r\n\t\t\t\t\t\t}"+
                                "\r\n"+
                                "\r\n\t\t\t\t\t\tDataTable dtRows = new DataTable();"+
                                "\r\n\t\t\t\t\t\tstring query = string.Format(\"select {0} as 'id', {1} as 'Posicion Y', {2} as 'Posicion X / Label', {3} as 'Index Label', {4} as 'Name' from {5};\", this.TablaPrincipal + \".\" + getPrimaryKey(this.TablaPrincipal), getColumnas(y), getColumnas(label_x), getColumnas(index_label), getColumnas(name), getTablas(Tablas, Keys, MainTable));"+
                                "\r\n\t\t\t\t\t\tdtRows = consultar(query);"+
                                "\r\n\t\t\t\t\t\tclsEnlaceColumna objEnlaceColumna = new clsEnlaceColumna(dtRows, contador, dtRows.Rows.Count);"+
                                "\r\n\t\t\t\t\t\tcontador++;"+
                                "\r\n\t\t\t\t\t\tthis.ListaEnlaceColumnas.Add(objEnlaceColumna);"+
                                "\r\n\t\t\t\t\t}"+
                                "\r\n\t\t\t\t}"+
                                "\r\n\t\t\t\tcerrarConexion();"+
                                "\r\n\t\t\t}"+
                                "\r\n\t\t}"+
                                "\r\n\t\t#endregion FuncionCargar"+
                                "\r\n"+
                                "\r\n\t\tprivate string getTablas(List<string> Tablas, List<string> Keys, bool MainTable)"+
                                "\r\n\t\t{"+
                                "\r\n\t\t\tstring innerJoin = \"\";"+
                                "\r\n\t\t\tif (Tablas.Count == 1)"+
                                "\r\n\t\t\t{"+
                                "\r\n\t\t\t\tinnerJoin = Tablas.FirstOrDefault();"+
                                "\r\n\t\t\t}"+
                                "\r\n\t\t\telse"+
                                "\r\n\t\t\t{"+
                                "\r\n\t\t\t\tif (MainTable)"+
                                "\r\n\t\t\t\t{"+
                                "\r\n\t\t\t\t\tstring PrimaryKey = \"\";"+
                                "\r\n\t\t\t\t\tint IndexMainTable = 0;"+
                                "\r\n\t\t\t\t\tfor (int i = 0; i < Tablas.Count; i++)"+
                                "\r\n\t\t\t\t\t{"+
                                "\r\n\t\t\t\t\t\tif (Tablas.ElementAt(i).Equals(this.TablaPrincipal))"+
                                "\r\n\t\t\t\t\t\t{"+
                                "\r\n\t\t\t\t\t\t\tIndexMainTable = i;"+
                                "\r\n\t\t\t\t\t\t\tbreak;"+
                                "\r\n\t\t\t\t\t\t}"+
                                "\r\n\t\t\t\t\t}"+
                                "\r\n\t\t\t\t\tPrimaryKey = Keys.ElementAt(IndexMainTable);"+
                                "\r\n\t\t\t\t\tTablas.RemoveAt(IndexMainTable);"+
                                "\r\n\t\t\t\t\tKeys.RemoveAt(IndexMainTable);"+
                                "\r\n\t\t\t\t\tinnerJoin = this.TablaPrincipal;"+
                                "\r\n\t\t\t\t\tfor (int i = 0; i < Tablas.Count; i++)"+
                                "\r\n\t\t\t\t\t{"+
                                "\r\n\t\t\t\t\t\tinnerJoin += string.Format(\" inner join {0} on {1}.{2} = {3}.{4}\", Tablas.ElementAt(i), this.TablaPrincipal, PrimaryKey, Tablas.ElementAt(i), Keys.ElementAt(i));"+
                                "\r\n\t\t\t\t\t}"+
                                "\r\n\t\t\t\t}"+
                                "\r\n\t\t\t\telse"+
                                "\r\n\t\t\t\t{"+
                                "\r\n\t\t\t\t\tstring PrimaryKey = getPrimaryKey(this.TablaPrincipal);"+
                                "\r\n\t\t\t\t\tinnerJoin = this.TablaPrincipal;"+
                                "\r\n\t\t\t\t\tfor (int i = 0; i < Tablas.Count; i++)"+
                                "\r\n\t\t\t\t\t{"+
                                "\r\n\t\t\t\t\t\tinnerJoin += string.Format(\" inner join {0} on {1}.{2} = {3}.{4}\", Tablas.ElementAt(i), this.TablaPrincipal, PrimaryKey, Tablas.ElementAt(i), Keys.ElementAt(i));"+
                                "\r\n\t\t\t\t\t}"+
                                "\r\n\t\t\t\t}"+
                                "\r\n\t\t\t}"+
                                "\r\n\t\t\treturn innerJoin;"+
                                "\r\n\t\t}"+
                                "\r\n"+
                                "\r\n"+
                                "\r\n\t\tprivate string getColumnas(List<string> Campos)"+
                                "\r\n\t\t{"+
                                "\r\n\t\t\tstring columnas = \"null\";"+
                                "\r\n\t\t\tif (Campos.Count > 0)"+
                                "\r\n\t\t\t{"+
                                "\r\n\t\t\t\tcolumnas = Campos.ElementAt(0) + \".\" + Campos.ElementAt(1);"+
                                "\r\n\t\t\t}"+
                                "\r\n\t\t\treturn columnas;"+
                                "\r\n\t\t}"+
                                "\r\n"+
                                "\r\n\t\tprivate string getPrimaryKey(string tabla)"+
                                "\r\n\t\t{"+
                                "\r\n\t\t\tstring primaryKey = \"\";"+
                                "\r\n\t\t\tCNX = new SqlConnection(CadenaConexion);"+
                                "\r\n\t\t\tif (abrirConexion())"+
                                "\r\n\t\t\t{"+
                                "\r\n\t\t\t\tDataTable dt = new DataTable();"+
                                "\r\n\t\t\t\tstring query = string.Format(\"SELECT FK_Table = FK.TABLE_NAME, FK_Column = CU.COLUMN_NAME, PK_Table = PK.TABLE_NAME, PK_Column = PT.COLUMN_NAME, Constraint_Name = C.CONSTRAINT_NAME \""+
                                "\r\n\t\t\t\t\t+ \"FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK \""+
                                "\r\n\t\t\t\t\t+ \"ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME INNER JOIN(SELECT i1.TABLE_NAME, i2.COLUMN_NAME \""+
                                "\r\n\t\t\t\t\t+ \"FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1 INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2 ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME WHERE i1.CONSTRAINT_TYPE = 'PRIMARY KEY') PT \""+
                                "\r\n\t\t\t\t\t+ \"ON PT.TABLE_NAME = PK.TABLE_NAME where PK.TABLE_NAME = '{0}';\", tabla);"+
                                "\r\n\t\t\t\tdt = consultar(query);"+
                                "\r\n\t\t\t\tif (dt.Rows.Count > 0)"+
                                "\r\n\t\t\t\t{"+
                                "\r\n\t\t\t\t\tprimaryKey = dt.Rows[0][3].ToString();"+
                                "\r\n\t\t\t\t}"+
                                "\r\n\t\t\t\telse"+
                                "\r\n\t\t\t\t{"+
                                "\r\n\t\t\t\t\tprimaryKey = string.Empty;"+
                                "\r\n\t\t\t\t}"+
                                "\r\n\t\t\t\tcerrarConexion();"+
                                "\r\n\t\t\t}"+
                                "\r\n\t\t\treturn primaryKey;"+
                                "\r\n\t\t}"+
                                "\r\n"+
                                "\r\n\t\tprivate string getForeignKey(string tabla)"+
                                "\r\n\t\t{"+
                                "\r\n\t\t\tstring foreignKey = \"\";"+
                                "\r\n\t\t\tCNX = new SqlConnection(CadenaConexion);"+
                                "\r\n\t\t\tif (abrirConexion())"+
                                "\r\n\t\t\t{"+
                                "\r\n\t\t\t\tDataTable dt = new DataTable();"+
                                "\r\n\t\t\t\tstring query = string.Format(\"SELECT FK_Table = FK.TABLE_NAME, FK_Column = CU.COLUMN_NAME, PK_Table = PK.TABLE_NAME, PK_Column = PT.COLUMN_NAME, Constraint_Name = C.CONSTRAINT_NAME \""+
                                "\r\n\t\t\t\t\t+ \"FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK \""+
                                "\r\n\t\t\t\t\t+ \"ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME INNER JOIN(SELECT i1.TABLE_NAME, i2.COLUMN_NAME \""+
                                "\r\n\t\t\t\t\t+ \"FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1 INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2 ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME WHERE i1.CONSTRAINT_TYPE = 'PRIMARY KEY') PT \""+
                                "\r\n\t\t\t\t\t+ \"ON PT.TABLE_NAME = PK.TABLE_NAME where FK.TABLE_NAME = '{0}' and PK.TABLE_NAME = '{1}';\", tabla, this.TablaPrincipal);"+
                                "\r\n\t\t\t\tdt = consultar(query);"+
                                "\r\n\t\t\t\tif (dt.Rows.Count > 0)"+
                                "\r\n\t\t\t\t{"+
                                "\r\n\t\t\t\t\tforeignKey = dt.Rows[0][1].ToString();"+
                                "\r\n\t\t\t\t}"+
                                "\r\n\t\t\t\telse"+
                                "\r\n\t\t\t\t{"+
                                "\r\n\t\t\t\t\tforeignKey = string.Empty;"+
                                "\r\n\t\t\t\t}"+
                                "\r\n\t\t\t\tcerrarConexion();"+
                                "\r\n\t\t\t}"+
                                "\r\n\t\t\treturn foreignKey;"+
                                "\r\n\t\t}"+
                                "\r\n"+
                                "\r\n\t\tpublic bool abrirConexion()"+
                                "\r\n\t\t{"+
                                "\r\n\t\t\tbool Exito = true;"+
                                "\r\n\t\t\ttry"+
                                "\r\n\t\t\t{"+
                                "\r\n\t\t\t\tCNX.Open();"+
                                "\r\n\t\t\t}"+
                                "\r\n\t\t\tcatch (Exception)"+
                                "\r\n\t\t\t{"+
                                "\r\n\t\t\t\tExito = false;"+
                                "\r\n\t\t\t}"+
                                "\r\n\t\t\treturn Exito;"+
                                "\r\n\t\t}"+
                                "\r\n"+
                                "\r\n\t\tpublic bool cerrarConexion()"+
                                "\r\n\t\t{"+
                                "\r\n\t\t\tbool Exito = true;"+
                                "\r\n\t\t\ttry"+
                                "\r\n\t\t\t{"+
                                "\r\n\t\t\t\tCNX.Close();"+
                                "\r\n\t\t\t}"+
                                "\r\n\t\t\tcatch (Exception)"+
                                "\r\n\t\t\t{"+
                                "\r\n\t\t\t\tExito = false;"+
                                "\r\n\t\t\t}"+
                                "\r\n\t\t\treturn Exito;"+
                                "\r\n\t\t}"+
                                "\r\n"+
                                "\r\n\t\tpublic DataTable consultar(string query)"+
                                "\r\n\t\t{"+
                                "\r\n\t\t\tDataTable dtResultado = new DataTable();"+
                                "\r\n\t\t\ttry"+
                                "\r\n\t\t\t{"+
                                "\r\n\t\t\t\tSqlCommand Comando = new SqlCommand(query, CNX);"+
                                "\r\n\t\t\t\tComando.CommandType = CommandType.Text;"+
                                "\r\n"+
                                "\r\n\t\t\t\tSqlDataAdapter Adapter = new SqlDataAdapter(Comando);"+
                                "\r\n\t\t\t\tAdapter.Fill(dtResultado);"+
                                "\r\n\t\t\t}"+
                                "\r\n\t\t\tcatch"+
                                "\r\n\t\t\t{"+
                                "\r\n\t\t\t\tdtResultado = null;"+
                                "\r\n\t\t\t}"+
                                "\r\n\t\t\treturn dtResultado;"+
                                "\r\n\t\t}"+
                                "\r\n"+
                                "\r\n\t\t~clsVista(){"+
                                "\r\n\t\t\tListadtEnlacesTablas = null;"+
                                "\r\n\t\t\tCadenaConexion = string.Empty;"+
                                "\r\n\t\t}"+
                                "\r\n\t}"+
                                "\r\n\tpublic class clsEnlaceColumna"+
                                "\r\n\t{"+
                                "\r\n\t\tprivate DataTable dtColumnas;"+
                                "\r\n\t\tprivate int Vista;"+
                                "\r\n\t\tprivate int MaxRows;"+
                                "\r\n"+
                                "\r\n\t\tpublic DataTable getdtColumnas()"+
                                "\r\n\t\t{"+
                                "\r\n\t\t\treturn this.dtColumnas;"+
                                "\r\n\t\t}"+
                                "\r\n"+
                                "\r\n\t\tpublic int getVista()"+
                                "\r\n\t\t{"+
                                "\r\n\t\t\treturn this.Vista;"+
                                "\r\n\t\t}"+
                                "\r\n"+
                                "\r\n\t\tpublic int getMaxRows()"+
                                "\r\n\t\t{"+
                                "\r\n\t\t\treturn this.MaxRows;"+
                                "\r\n\t\t}"+
                                "\r\n"+
                                "\r\n\t\tpublic clsEnlaceColumna(DataTable _dtColumnas, int _Vista, int _MaxRows)"+
                                "\r\n\t\t{"+
                                "\r\n\t\t\tthis.dtColumnas = _dtColumnas;"+
                                "\r\n\t\t\tthis.Vista = _Vista;"+
                                "\r\n\t\t\tthis.MaxRows = _MaxRows;"+
                                "\r\n\t\t}"+
                                "\r\n\t}"+
                                "\r\n}" };
            return claseVista;
        }
    }
}
