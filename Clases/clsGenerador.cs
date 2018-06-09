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
        StreamReader sr = new StreamReader("C:\\Test.txt");
        StreamWriter sw = new StreamWriter("C:\\Test.txt");
        string linew;
        string liner;

        public void GenerateMethod()
        {
            ///Este metodo debe de generar el string completo con el codigo del Metodo
            linew = "El codigo generado";
        }

        public void WriteMethod()
        {
            liner = sr.ReadLine();

            while (liner != null)
            {
                if (liner.Equals("#region View"))
                {
                    sw.WriteLine(linew);
                    while (liner.Equals("#endregion View"))
                    {
                        linew = "";
                        sw.WriteLine(linew);
                        liner = sr.ReadLine();
                    }
                    linew = "} \n #endregion View";
                    sw.WriteLine(linew);
                }
                liner = sr.ReadLine();
            }
        }
    }
}
