﻿using ConexionBD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
   public class clsEsquema
    {
        private static clsEsquema objEsquema = new clsEsquema();
        private static string ConnectionString;
        private static List<string> ListaTablas;
        private static List<clsTabla> ListaColumnas;
        private static DataTable dtConstraints;

        private clsEsquema()
        {
            ListaTablas = new List<string>();
            ListaColumnas = new List<clsTabla>();
            dtConstraints = new DataTable();
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

        public string getConnectionString()
        {
            return ConnectionString;
        }

        public DataTable getdtConstraints()
        {
            return dtConstraints;
        }

        private void setConnectionString(string _ConnectionsString)
        {
            ConnectionString = _ConnectionsString;
        }

        public void NuevoEsquema()
        {
            ConnectionString = string.Empty;
            ListaTablas = new List<string>();
            ListaColumnas = new List<clsTabla>();
            dtConstraints = new DataTable();
        }

        public bool ProbarConexion(List<string> _ListaCadenaConexion)
        {
            bool exito = false;
            if (_ListaCadenaConexion.Count == 5)
            {
                if (_ListaCadenaConexion.First().Equals("SQL Server"))
                {
                    setConnectionString(string.Format("Server={0};Initial Catalog={1};Persist Security Info=False;User ID={2};Password={3};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;", 
                        _ListaCadenaConexion.ElementAt(1), _ListaCadenaConexion.ElementAt(2), _ListaCadenaConexion.ElementAt(3), _ListaCadenaConexion.ElementAt(4)));
                }
                else if (_ListaCadenaConexion.First().Equals("Oracle"))
                {
                    ///Hay que cambiar el string connection a Oracle
                    setConnectionString(string.Format("Server=tcp:frameworkanaliticavisual.database.windows.net,1433;Initial Catalog=frameworkanaliticavisual;Persist Security Info=False;User ID=frameworkanaliticavisual;Password=Seminario123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
                }
                else if (_ListaCadenaConexion.First().Equals("MySQL"))
                {
                    ///Hay que cambiar el string connection a MySQL
                    setConnectionString(string.Format("Server=tcp:frameworkanaliticavisual.database.windows.net,1433;Initial Catalog=frameworkanaliticavisual;Persist Security Info=False;User ID=frameworkanaliticavisual;Password=Seminario123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
                }

                clsConexion objConexion = clsConexion.obtenerclsConexion();
                objConexion.setStringConnection(getConnectionString());
                if (objConexion.abrirConexion())
                {
                    objConexion.cerrarConexion();
                    exito = true;
                }
                else
                {
                    setConnectionString(string.Empty);
                    objConexion.setStringConnection(string.Empty);
                }
            }
            return exito;
        }

        public bool CargarTablasBD()
        {
            bool exito = false;
            clsConexion objConexion = clsConexion.obtenerclsConexion();
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
                            if (CargarConstraints())
                            {
                                exito = true;
                            }
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
            clsConexion objConexion = clsConexion.obtenerclsConexion();
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
            return exito;
        }

        private bool CargarConstraints()
        {
            bool exito = false;
            clsConexion objConexion = clsConexion.obtenerclsConexion();
            string query = "SELECT FK_Table = FK.TABLE_NAME, FK_Column = CU.COLUMN_NAME, PK_Table = PK.TABLE_NAME, PK_Column = PT.COLUMN_NAME, Constraint_Name = C.CONSTRAINT_NAME FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME INNER JOIN(SELECT i1.TABLE_NAME, i2.COLUMN_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1 INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2 ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME WHERE i1.CONSTRAINT_TYPE = 'PRIMARY KEY') PT ON PT.TABLE_NAME = PK.TABLE_NAME;";
            dtConstraints = objConexion.consultar(query);
            if (dtConstraints != null)
            {
                exito = true;
            }
            return exito;
        }

        ~clsEsquema()
        {
            ListaTablas = null;
            ListaColumnas = null;
            ConnectionString = string.Empty;
            dtConstraints = null;
        }
    }
}
