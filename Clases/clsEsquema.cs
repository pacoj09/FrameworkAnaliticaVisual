using ConexionBD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
   public class clsEsquema
    {
        private static clsEsquema objEsquema = new clsEsquema();
        private static List<string> ListaTablas;
        private static List<clsTabla> ListaColumnas;

        private clsEsquema()
        {
            ListaTablas = new List<string>();
            ListaColumnas = new List<clsTabla>();
        }

        public static clsEsquema obtenerclsEsquema()
        {
            return objEsquema;
        }

        public List<string> getListaTablas()
        {
            return ListaTablas;
        }

        public List<clsTabla> getListaColumnas()
        {
            return ListaColumnas;
        }

        public bool CargarEsquema()
        {
            bool exito = false;
            clsConexion objConexion = new clsConexion();
            if (objConexion.abrirConexion())
            {
                DataTable dtTablas = new DataTable();
                string query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE';";
                dtTablas = objConexion.consultar(query);
                if (dtTablas != null)
                {
                    for (int i = 0; i < dtTablas.Rows.Count; i++)
                    {
                        ListaTablas.Add(dtTablas.Rows[i][0].ToString());
                    }
                    if (ListaTablas.Count > 0)
                    {
                        if (CargarColumnasXTabla(dtTablas))
                        {
                            exito = true;
                        }
                    }
                }
                objConexion.cerrarConexion();
            }
            return exito;
        }

        private bool CargarColumnasXTabla(DataTable _dtTablas)
        {
            bool exito = false;
            clsConexion objConexion = new clsConexion();
            if (objConexion.abrirConexion())
            {
                DataTable dtColumnas = new DataTable();

                for (int i = 0; i < _dtTablas.Rows.Count; i++)
                {
                    string query = string.Format("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{0}';", _dtTablas.Rows[i][0].ToString());
                    dtColumnas = objConexion.consultar(query);
                    for (int j = 0; j < dtColumnas.Rows.Count; j++)
                    {
                        clsTabla objTabla = new clsTabla(_dtTablas.Rows[i][0].ToString(), dtColumnas.Rows[j][0].ToString());
                        ListaColumnas.Add(objTabla);
                    }
                }
                if (ListaColumnas.Count > 0)
                {
                    exito = true;
                }
                objConexion.cerrarConexion();
            }
            return exito;
        }


        ~clsEsquema()
        {
            ListaTablas = null;
            ListaColumnas = null;
        }
    }
}
