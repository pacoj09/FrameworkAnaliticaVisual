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

        public void FactoryMethod(string _TableName, DataTable _dt)
        {
            clsVista objVista = clsVista.obtenerclsVista();
            objVista.setdtColumnas(_dt);
            objVista.setTabla(_TableName);
            objVista.cargarListas();
        }
        
        public List<clsEsquemaVista> getListaEsquema()
        {
            clsVista objVista = clsVista.obtenerclsVista();
            return objVista.getListaEsquema();
        }

        public DataTable getEnlaces()
        {
            clsVista objVista = clsVista.obtenerclsVista();
            return objVista.getdtColumnas();
        }

        public int getNumeroFilas()
        {
            clsVista objVista = clsVista.obtenerclsVista();
            return objVista.obtenerNumeroFilas();
        }
    }
}
