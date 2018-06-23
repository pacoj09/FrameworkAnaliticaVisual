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
        public List<string> ListaTablas { get; set; }

        public clsEsquema() { }

        public bool CargarEsquema()
        {
            ListaTablas = new List<string>();
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
                    exito = true;
                }
                objConexion.cerrarConexion();
            }
            return exito;
        }


        ~clsEsquema()
        {
            ListaTablas = null;
        }
    }
}
