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

        public bool FactoryMethod(List<string> _ListaNombresTablasSeleccionadas, List<DataTable> _ListaTablasEnlasadas)
        {
            bool exito = false;
            if (_ListaNombresTablasSeleccionadas.Count > 0 && _ListaTablasEnlasadas.Count > 0 && _ListaNombresTablasSeleccionadas.Count == _ListaTablasEnlasadas.Count)
            {
                clsGenerador objGenerador = new clsGenerador();
            }
            return exito;
        }

        //public List<clsEsquemaVista> getListaEsquema()
        //{
        //    clsVista objVista = clsVista.obtenerclsVista();
        //    return objVista.getListaEsquema();
        //}

        //public DataTable getEnlaces()
        //{
        //    clsVista objVista = clsVista.obtenerclsVista();
        //    return objVista.getdtColumnas();
        //}

        //public int getNumeroFilas()
        //{
        //    clsVista objVista = clsVista.obtenerclsVista();
        //    return objVista.obtenerNumeroFilas();
        //}
    }
}
