using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class clsGenerador
    {

        public string Metodo { get; set; }

        public void GenerateMethod(string[] Columnas, string[] Grafico)
        {
            //Este metodo debe de generar el string completo con el codigo del Metodo

            Metodo = string.Format("public void Function(){0}\r\nstring numeroColumnas = {1};\r\n{2}\r\n#endregion View", "{", "10", "}");
        }

        public void WriteMethod()
        {
            
            Metodo = string.Format("public void Function(){0}\r\nstring numeroColumnas = {1};\r\n{2}\r\n#endregion View", "{", "10", "}");

            string path = $"C:\\Test\\clsPrueba.cs";
            string[] readText = File.ReadAllLines(path);
            for (int i = 0; i < readText.Count(); i++)
            {
                if (readText[i].Equals("#region View"))
                {
                    //Se debe de remplasar el string por una propiedad que se carge en otro metodo
                    readText[i + 1] = Metodo;
                    int rest = readText.Length - (i + 2);
                    Array.Clear(readText, i + 2, rest);
                    break;
                }
            }
            File.WriteAllLines(path, readText);
        }
    }
}
