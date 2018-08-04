using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class clsFactory
    {
        public clsFactory() { }

        public bool FactoryMethod(List<DataTable> _ListaTablasEnlasadas)
        {
            bool exito = false;
            if (_ListaTablasEnlasadas.Count > 0)
            {
                clsGenerador objGenerador = new clsGenerador();
                objGenerador.setListadtEnlacesTablas(_ListaTablasEnlasadas);
                objGenerador.GenerateMethod();
                exito = true;
            }
            return exito;
        }
    }
}
