using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FrameworkAnaliticaVisual
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //clsGenerador objg = new clsGenerador();
            //objg.WriteMethod();

            cargarListas();
            ClientScript.RegisterStartupScript(GetType(), "ddlGraficos", "tipoDiagrama('" + ddlGraficos.SelectedValue + "')", true);
        }

        protected void ddlTablas_SelectedIndexChanged(object sender, EventArgs e)
        {
            clsEsquema objEsquema = clsEsquema.obtenerclsEsquema();
            var query = from dtRow in objEsquema.getListaColumnas() where dtRow.NombreTabla.StartsWith(ddlTablas.SelectedValue) select dtRow.NombreColumna;
            if (query.ToList().Count > 0)
            {
                ddlColumnas.DataSource = query.ToList();
                ddlColumnas.DataBind();
            }
        }

        private void cargarListas()
        {
            clsEsquema objEsquema = clsEsquema.obtenerclsEsquema();
            if (objEsquema.getListaTablas().Count <= 0 && objEsquema.getListaColumnas().Count <= 0)
            {
                if (objEsquema.CargarEsquema())
                {
                    ddlTablas.DataSource = objEsquema.getListaTablas();
                    ddlTablas.DataBind();
                    var query = from dtRow in objEsquema.getListaColumnas() where dtRow.NombreTabla.StartsWith(ddlTablas.SelectedValue) select dtRow.NombreColumna;
                    if (query.ToList().Count > 0)
                    {
                        ddlColumnas.DataSource = query.ToList();
                        ddlColumnas.DataBind();
                    }
                }
            }
            else
            {
                ddlTablas.DataSource = objEsquema.getListaTablas();
                ddlTablas.DataBind();
                var query = from dtRow in objEsquema.getListaColumnas() where dtRow.NombreTabla.StartsWith(ddlTablas.SelectedValue) select dtRow.NombreColumna;
                if (query.ToList().Count > 0)
                {
                    ddlColumnas.DataSource = query.ToList();
                    ddlColumnas.DataBind();
                }
            }
        }

        protected void ddlGraficos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "ddlGraficos", "tipoDiagrama('" + ddlGraficos.SelectedValue + "')", true);
        }
    }
}