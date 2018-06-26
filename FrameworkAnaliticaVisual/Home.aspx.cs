using Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FrameworkAnaliticaVisual
{
    public partial class Home : System.Web.UI.Page
    {

        private List<string> ListaddlCanvas;
        private List<string> ListaddlTablas;
        private DataTable dtCanvas;

        protected void Page_Load(object sender, EventArgs e)
        {
            //clsGenerador objg = new clsGenerador();
            //objg.WriteMethod();
            cargarListasBD();
            cargarGrid();
        }

        private void cargarListaCanvas()
        {
            ListaddlCanvas = new List<string>();
            ListaddlCanvas.Add("Color");
            ListaddlCanvas.Add("Label");
            ListaddlCanvas.Add("Index Label");
            ListaddlCanvas.Add("Posicion X");
            ListaddlCanvas.Add("Posicion Y");
            Session["ListaCanvas"] = ListaddlCanvas;
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
        
        private void cargarComboBoxListaCanvas()
        {
            cargarListaCanvas();
            ddlEnlaceCanvas.DataSource = ListaddlCanvas;
            ddlEnlaceCanvas.DataBind();
        }

        private void cargarGrid()
        {
            dtCanvas = new DataTable();
            dtCanvas.Columns.Add("Columnas_Tabla", typeof(String));
            dtCanvas.Columns.Add("Datos_Canvas", typeof(String));
            Session["dtCanvas"] = dtCanvas;
            gvDatos.DataSource = dtCanvas;
            gvDatos.DataBind();
        }

        private void cargarListasBD()
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

        protected void upCanvas_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "ddlGraficos", "tipoDiagrama('" + ddlGraficos.SelectedValue + "');", true);
        }

        protected void btnAgregarEnlace_Click(object sender, EventArgs e)
        {
            string campo_cvs = ddlEnlaceCanvas.SelectedValue;
            ListaddlCanvas = Session["ListaCanvas"] as List<string>;
            ListaddlCanvas.Remove(campo_cvs);
            Session["ListaCanvas"] = ListaddlCanvas;
            ddlEnlaceCanvas.DataSource = ListaddlCanvas;
            ddlEnlaceCanvas.DataBind();

            string campo_tbl = ddlEnlaceTabla.SelectedValue;
            ListaddlTablas = Session["ListaTablas"] as List<string>;
            ListaddlTablas.Remove(campo_tbl);
            Session["ListaTablas"] = ListaddlTablas;
            ddlEnlaceTabla.DataSource = ListaddlTablas;
            ddlEnlaceTabla.DataBind();


            dtCanvas = Session["dtCanvas"] as DataTable;
            DataRow NewRow = dtCanvas.NewRow();
            NewRow[0] = campo_tbl;
            NewRow[1] = campo_cvs;
            dtCanvas.Rows.Add(NewRow);
            gvDatos.DataSource = dtCanvas;
            gvDatos.DataBind();
            Session["dtCanvas"] = dtCanvas;
        }

        protected void btnAgregarColumnas_Click(object sender, EventArgs e)
        {
            ListaddlTablas = new List<string>();
            ListaddlTablas = ddlColumnas.DataSource as List<string>;
            Session["ListaTablas"] = ListaddlTablas;
            ddlEnlaceTabla.DataSource = ListaddlTablas;
            ddlEnlaceTabla.DataBind();
            cargarComboBoxListaCanvas();
            cargarGrid();
            btnAgregarEnlace.Enabled = true;
            gvDatos.Enabled = true;
        }
    }
}