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

        public void setListadtEnlacesTablas(List<DataTable> _ListadtEnlacesTablas)
        {
            this.ListadtEnlacesTablas = new List<DataTable>();
            this.ListadtEnlacesTablas = _ListadtEnlacesTablas;
        }

        public clsGenerador() { }

        public void GenerateMethod()
        {
            string path = System.IO.Directory.GetCurrentDirectory() + $"\\clsVista.cs";
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
                + "\r\n\t\tCadenaConexion = \"{0}\";", objEsquema.getConnectionString());
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
            string[] claseVista = { "using ConexionBD;"+
                                "\r\nusing System;"+
                                "\r\nusing System.Collections.Generic;"+
                                "\r\nusing System.Data;"+
                                "\r\nusing System.Linq;"+
                                "\r\nusing System.Text;"+
                                "\r\nusing System.Threading.Tasks;"+
                                "\r\n\r\nnamespace Clases"+
                                "\r\n{"+
                                "\r\n\tpublic class clsVista"+
                                "\r\n\t{"+
                                "\r\n\t\tprivate List<DataTable> ListadtEnlacesTablas;"+
                                "\r\n\t\tprivate string CadenaConexion;"+
                                "\r\n"+
                                "\r\n\t\t#region Constructor"+
                                "\r\n\t\tpublic clsVista(){"+
                                "\r\n\r\n\r\n\r\n\r\n"+
                                "\r\n\t\t}"+
                                "\r\n\t\t#endregion Constructor"+
                                "\r\n\t"+
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
                                "\r\n\t\tpublic void cargarListas(){"+
                                "\r\n\r\n\r\n\r\n\r\n"+
                                "\r\n\t\t}"+
                                "\r\n\t\t#endregion FuncionCargar"+
                                "\r\n"+
                                "\r\n\t\t~clsVista(){"+
                                "\r\n\t\t\tListadtEnlacesTablas = null;"+
                                "\r\n\t\t\tCadenaConexion = string.Empty;"+
                                "\r\n\t\t}"+
                                "\r\n\t}"+
                                "\r\n}" };
            return claseVista;
        }
    }
}
