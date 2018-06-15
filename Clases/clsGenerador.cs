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
        public string linew { get; set; }
        //string linew;
        string liner;

        public void GenerateMethod()
        {
            ///Este metodo debe de generar el string completo con el codigo del Metodo
            linew = "El codigo generado";
        }

        public void WriteMethod()
        {
            StreamReader sr = new StreamReader("C:\\Test\\clsPrueba.cs");
            liner = sr.ReadLine();

            while (liner != null)
            {
                if (liner.Equals("#region View"))
                {
                    long lineposition = sr.BaseStream.Position + 1;
                    sr.Close();
                    StreamWriter sw = new StreamWriter("C:\\Test\\clsPrueba.cs");
                    sw.BaseStream.Position = lineposition;
                    sw.WriteLine(linew);
                    //while (liner.Equals("#endregion View"))
                    //{
                    //    linew = "";
                    //    sw.WriteLine(linew);
                    //    liner = sr.ReadLine();
                    //}
                    //linew = "} \n #endregion View";
                    //sw.WriteLine(linew);
                }
                liner = sr.ReadLine();
            }
        }
    }
}
