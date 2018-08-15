using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionBD
{
    public class clsConexion
    {
        private static clsConexion objConexion = new clsConexion();
        private static SqlConnection CNX;

        private clsConexion()
        {
            CNX = null;
        }

        public static clsConexion obtenerclsConexion()
        {
            return objConexion;
        }

        public void setStringConnection(string _ConnectionString)
        {
            CNX = new SqlConnection(_ConnectionString);
        }

        public bool abrirConexion()
        {
            bool Exito = true;
            try
            {
                CNX.Open();
            }
            catch (Exception e)
            {
                Exito = false;
            }
            return Exito;
        }

        public bool cerrarConexion()
        {
            bool Exito = true;
            try
            {
                CNX.Close();
            }
            catch (Exception e)
            {
                Exito = false;
            }
            return Exito;
        }

        public DataTable consultar(string query)
        {
            DataTable dtResultado = new DataTable();
            try
            {
                SqlCommand Comando = new SqlCommand(query, CNX);
                Comando.CommandType = CommandType.Text;

                SqlDataAdapter Adapter = new SqlDataAdapter(Comando);
                Adapter.Fill(dtResultado);
            }
            catch
            {
                dtResultado = null;
            }
            return dtResultado;
        }

        ~clsConexion()
        {
            CNX = null;
        }
    }
}
