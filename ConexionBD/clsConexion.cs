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
        #region Atributos

        /// <summary>
        /// Representa la conexión con la base de datos SQL Server 
        /// </summary>
        private SqlConnection CNX = null;
        /// <summary>
        /// Representa las Transacciones en SQL Server
        /// </summary>
        private SqlTransaction Transaction = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Se inicializa una conexión a la base de datos
        /// </summary>
        public clsConexion()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["CNX"].ToString();
            CNX = new SqlConnection(ConnectionString);

        }

        #endregion

        #region Funciones

        /// <summary>
        /// Esta función intenta abrir la conexión con la base de datos
        /// </summary>
        /// <returns>True si la conexión se logró abrir False si hubo un error al abrir la conexión</returns>
        public bool abrirConexion()
        {
            bool Exito = true;
            try
            {
                CNX.Open();
            }
            catch (Exception)
            {
                Exito = false;
            }
            return Exito;
        }

        /// <summary>
        ///  Esta función cierra la conexión a la base de datos
        /// </summary>
        /// <returns>True si logró cerrar la correctamente False si hubo un error al cerrar la conexión</returns>
        public bool cerrarConexion()
        {
            bool Exito = true;
            try
            {
                CNX.Close();
            }
            catch (Exception)
            {
                Exito = false;
            }
            return Exito;
        }

        /// <summary>
        /// Esta función se encarga de ejecutar cualquier consulta (SELECT) en la base de datos
        /// </summary>
        /// <param name="query">Representa el select genérico para cualquier tabla</param>
        /// <returns>Retorna una Tabla con el resultado del select</returns>
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

        /// <summary>
        /// Esta función se encarga de Ejecutar cualquier Insert, Delete or Update en la base de datos
        /// </summary>
        /// <param name="statement">representa el sql del insert,delete or update</param>
        /// <returns>Devuelve el número de registros que afectó durante la ejecución</returns>
        public int gestion(string statement)
        {
            int RegistroAfectado = 0;
            SqlCommand Comando = new SqlCommand(statement, CNX);
            try
            {
                RegistroAfectado = Comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                RegistroAfectado = 0;
            }
            return RegistroAfectado;
        }

        //Transaction
        /// <summary>
        /// Esta Funcion Inicia la Transaccion en la BD
        /// </summary>
        /// <returns></returns>
        public bool beginTransaction()
        {
            bool bandera = false;

            try
            {
                this.Transaction = this.CNX.BeginTransaction();
                bandera = true;
            }
            catch (Exception)
            {
                bandera = false;
            }

            return bandera;
        }

        /// <summary>
        /// Esta Funcion realiza un Hit a la BD con Transaccion
        /// </summary>
        /// <param name="Statment"></param>
        /// <returns></returns>
        public int gestionTransaction(string Statment)
        {
            int RegistrosAfectados = 0;
            SqlCommand Gestion = new SqlCommand(Statment, CNX, Transaction);
            try
            {
                RegistrosAfectados = Gestion.ExecuteNonQuery();
            }
            catch (Exception)
            {

                RegistrosAfectados = 0;
            }
            return RegistrosAfectados;
        }

        /// <summary>
        /// Esta funcion hace consultas en Transacciones
        /// </summary>
        /// <param name="Statment"></param>
        /// <returns></returns>
        public DataTable consultaTransaction(string Statment)
        {
            DataTable RegistrosAfectados = new DataTable();
            SqlCommand Gestion = new SqlCommand(Statment, CNX, Transaction);
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(Gestion);
                adapter.Fill(RegistrosAfectados);
            }
            catch (Exception)
            {

                RegistrosAfectados = null;
            }
            return RegistrosAfectados;
        }

        /// <summary>
        /// Esta Funcion realiza un rollback en la BD
        /// </summary>
        /// <returns></returns>
        public bool rollback()
        {
            bool bandera = false;
            try
            {
                this.Transaction.Rollback();
                bandera = true;
            }
            catch (Exception)
            {
                bandera = false;
            }

            return bandera;
        }

        /// <summary>
        /// Esta Funcion hace un commit en la BD
        /// </summary>
        /// <returns></returns>
        public bool commit()
        {
            bool bandera = false;
            try
            {
                this.Transaction.Commit();
                bandera = true;
            }
            catch (Exception)
            {
                bandera = false;
            }

            return bandera;
        }

        #endregion

        #region Destructor

        /// <summary>
        /// Este es el Destructor de la Clase
        /// </summary>
        ~clsConexion()
        {
            CNX = null;
            Transaction = null;
        }

        #endregion
    }
}
