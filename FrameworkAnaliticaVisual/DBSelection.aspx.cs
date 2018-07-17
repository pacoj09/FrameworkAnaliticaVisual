using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FrameworkAnaliticaVisual
{
    public partial class DBSelection : System.Web.UI.Page
    {
        private List<string> ListaCadeConexion;
        protected void Page_Load(object sender, EventArgs e)
        {
            ListaCadeConexion = new List<string>();
            Session["CadenaConexion"] = ListaCadeConexion;
        }

        protected void btnProbarConexion_Click(object sender, EventArgs e)
        {
            if (Session["CadenaConexion"] != null)
            {
                ListaCadeConexion = Session["CadenaConexion"] as List<string>;
                ListaCadeConexion.Add(ddlConexion.SelectedValue);
                ListaCadeConexion.Add(txtServidor.Text);
                ListaCadeConexion.Add(txtBaseDatos.Text);
                ListaCadeConexion.Add(txtUser.Text);
                ListaCadeConexion.Add(txtPassword.Text);
                Session["CadenaConexion"] = ListaCadeConexion;
                clsEsquema objEsquema = clsEsquema.obtenerclsEsquema();
                if (objEsquema.ProbarConexion(ListaCadeConexion))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Conexion_True", "alert('Conexion establecida con exito');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Conexion_True", "alert('No se ha podido establecer conexion');", true);
                }
            }
        }
    }
}