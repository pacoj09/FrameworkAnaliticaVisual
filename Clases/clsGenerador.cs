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
                if (contador == 1)
                {
                    for (int i = 0; i < readText.Count(); i++)
                    {
                        if (readText[i].Trim().Equals("#region Vista1"))
                        {
                            bool enable = false;
                            List<string> readTextRest = new List<string>();
                            readTextRest = readText.GetRange((i + 2), (readText.Count - (i + 2)));
                            readText.RemoveRange((i + 2), (readText.Count - (i + 2)));
                            for (int j = 0; j < readTextRest.Count; j++)
                            {
                                if (readTextRest[j + 2].Trim().Equals("#endregion Vista1"))
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
                }
                else if (contador == 2)
                {
                    for (int i = 0; i < readText.Count(); i++)
                    {
                        if (readText[i].Trim().Equals("#region Vista2"))
                        {
                            bool enable = false;
                            List<string> readTextRest = new List<string>();
                            readTextRest = readText.GetRange((i + 2), (readText.Count - (i + 2)));
                            readText.RemoveRange((i + 2), (readText.Count - (i + 2)));
                            for (int j = 0; j < readTextRest.Count; j++)
                            {
                                if (readTextRest[j + 2].Trim().Equals("#endregion Vista2"))
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
                }
                else if (contador == 3)
                {
                    for (int i = 0; i < readText.Count(); i++)
                    {
                        if (readText[i].Trim().Equals("#region Vista3"))
                        {
                            bool enable = false;
                            List<string> readTextRest = new List<string>();
                            readTextRest = readText.GetRange((i + 2), (readText.Count - (i + 2)));
                            readText.RemoveRange((i + 2), (readText.Count - (i + 2)));
                            for (int j = 0; j < readTextRest.Count; j++)
                            {
                                if (readTextRest[j + 2].Trim().Equals("#endregion Vista3"))
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
                }
                else if (contador == 4)
                {
                    for (int i = 0; i < readText.Count(); i++)
                    {
                        if (readText[i].Trim().Equals("#region Vista4"))
                        {
                            bool enable = false;
                            List<string> readTextRest = new List<string>();
                            readTextRest = readText.GetRange((i + 2), (readText.Count - (i + 2)));
                            readText.RemoveRange((i + 2), (readText.Count - (i + 2)));
                            for (int j = 0; j < readTextRest.Count; j++)
                            {
                                if (readTextRest[j + 2].Trim().Equals("#endregion Vista4"))
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
    }
}
