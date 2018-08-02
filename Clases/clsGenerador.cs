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

        private void GenerateMethod()
        {
            foreach (DataTable dt in this.ListadtEnlacesTablas)
            {
                string path = $"C:\\Test\\clsPrueba.cs";
                List<string> readText = new List<string>();
                readText = File.ReadAllLines(path).ToList<string>();
                for (int i = 0; i < readText.Count(); i++)
                {
                    if (readText[i].Equals("#region Vista1"))
                    {
                        List<string> readTextRest = new List<string>();
                        readTextRest = readText.GetRange((i + 1), (readText.Count - i));
                        //int contador = 0;
                        //for (int j = 0; j < len; j++)
                        //{

                        //}
                        //Se debe de remplasar el string por una propiedad que se carge en otro metodo
                        //for (int i = 0; i < length; i++)
                        //{

                        //}
                        //readText[i + 1] = Metodo;
                        //int rest = readText.Length - (i + 2);
                        //Array.Clear(readText, i + 2, rest);
                        //break;
                    }
                }
            }

            ///Este metodo debe de generar el string completo con el codigo del Metodo
            ///Debe haber un for para recorrer por tablas y despeus por columnas, en caso de que sean varias tablas

            //List<string> listaQuerys = new List<string>();
            //if (dtColumnas != null)
            //{
            //    for (int i = 0; i < dtColumnas.Rows.Count; i++)
            //    {
            //        string query = string.Format("select {0} from {1};", dtColumnas.Rows[i][0].ToString(), Tablas);
            //        listaQuerys.Add("listaQuerys.add(" + query + ")");
            //    }
            //}
            //Metodo = string.Format("public void Function(){0}\r\nstring numeroColumnas = {1};\r\n{2}\r\n#endregion View", "{", "10", "}");
        }

        private void WriteMethod(DataTable _dtEnlacesTablas)
        {
            //for (int i = 0; i < readText.Count(); i++)
            //{
            //    if (readText[i].Equals("#region Vista1"))
            //    {
            //        //Se debe de remplasar el string por una propiedad que se carge en otro metodo
            //        //for (int i = 0; i < length; i++)
            //        //{
            //        //}
            //        //readText[i + 1] = Metodo;
            //        int rest = readText.Length - (i + 2);
            //        Array.Clear(readText, i + 2, rest);
            //        break;
            //    }

            //    if (readText[i].Equals("#region View"))
            //    {
            //        //Se debe de remplasar el string por una propiedad que se carge en otro metodo
            //        //readText[i + 1] = Metodo;
            //        int rest = readText.Length - (i + 2);
            //        Array.Clear(readText, i + 2, rest);
            //        break;
            //    }
            //}
            //File.WriteAllLines(path, readText);
        }
    }
}
