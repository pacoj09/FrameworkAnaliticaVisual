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
        private List<string> ListaTablas;
        private List<string> ListaEnlacesTablas;
        private DataTable dtCanvas;

        protected void Page_Load(object sender, EventArgs e)
        {
            cargarListasBD();
            if (Session["dtCanvas"] == null)
            {
                cargarGrid();
            }

            if (Session["Tablas"] == null)
            {
                ListaTablas = new List<string>();
                Session["Tablas"] = ListaTablas;
            }

            if (Session["ListaEnlacesTablas"] == null)
            {
                ListaEnlacesTablas = new List<string>();
                Session["ListaEnlacesTablas"] = ListaEnlacesTablas;
            }
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
            //dtCanvas.Columns.Add("Datos_Canvas", typeof(WebControl));
            Session["dtCanvas"] = dtCanvas;
            gvDatos.DataSource = dtCanvas;
            gvDatos.DataBind();
        }

        private void cargarListasBD()
        {
            if (string.IsNullOrEmpty(ddlTablas.SelectedValue) || string.IsNullOrEmpty(ddlColumnas.SelectedValue))
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
        }

        protected void upCanvas_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "ddlGraficos", "tipoDiagrama('" + ddlGraficos.SelectedValue + "');", true);
        }

        protected void btnAgregarEnlace_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlEnlaceCanvas.SelectedValue) || !string.IsNullOrEmpty(ddlEnlaceTabla.SelectedValue))
            {
                ///Elimina el campo de la lista
                string campo_cvs = ddlEnlaceCanvas.SelectedValue;
                ListaddlCanvas = Session["ListaCanvas"] as List<string>;
                ListaddlCanvas.Remove(campo_cvs);
                Session["ListaCanvas"] = ListaddlCanvas;
                ddlEnlaceCanvas.DataSource = ListaddlCanvas;
                ddlEnlaceCanvas.DataBind();

                ///Elimina el campo de la lista
                string campo_tbl = ddlEnlaceTabla.SelectedValue;
                ListaddlTablas = Session["ListaTablas"] as List<string>;
                ListaddlTablas.Remove(campo_tbl);
                Session["ListaTablas"] = ListaddlTablas;
                ddlEnlaceTabla.DataSource = ListaddlTablas;
                ddlEnlaceTabla.DataBind();

                ///Carga el row com los campos seleccionados
                dtCanvas = Session["dtCanvas"] as DataTable;
                DataRow NewRow = dtCanvas.NewRow();
                NewRow[0] = campo_tbl;
                //NewRow[1] = ddlEnlaceCanvas;
                dtCanvas.Rows.Add(NewRow);
                Session["dtCanvas"] = dtCanvas;
                gvDatos.DataSource = dtCanvas;
                gvDatos.DataBind();
            }
        }

        protected void gvDatos_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row

                DropDownList drpdwndec = (DropDownList)e.Row.FindControl("ddlCamposCanvas");

                drpdwndec.DataSource = ddlEnlaceCanvas.DataSource;

                drpdwndec.DataBind();

                drpdwndec.Items.Insert(0, new ListItem("No Seleccionado"));
            }
        }

        protected void btnAgregarColumnas_Click(object sender, EventArgs e)
        {
            ListaddlTablas = new List<string>();
            ListaTablas = (Session["Tablas"]) as List<string>;
            clsEsquema objEsquema = clsEsquema.obtenerclsEsquema();
            var query = from dtRow in objEsquema.getListaColumnas() where dtRow.NombreTabla.StartsWith(ddlTablas.SelectedValue) select dtRow.NombreColumna;
            ListaddlTablas = query.ToList();
            Session["ListaTablas"] = ListaddlTablas;
            ddlEnlaceTabla.DataSource = ListaddlTablas;
            ddlEnlaceTabla.DataBind();
            cargarComboBoxListaCanvas();
            cargarGrid();
            ListaTablas.Add(ddlTablas.SelectedValue);
            Session["Tablas"] = ListaTablas;
            btnAgregarEnlace.Enabled = true;
            gvDatos.Enabled = true;
        }

        protected void gvDatos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string campo_tbl = gvDatos.Rows[e.RowIndex].Cells[1].Text;
            string campo_cvs = gvDatos.Rows[e.RowIndex].Cells[2].Text;

            ListaddlCanvas = Session["ListaCanvas"] as List<string>;
            ListaddlCanvas.Add(campo_cvs);
            Session["ListaCanvas"] = ListaddlCanvas;
            ddlEnlaceCanvas.DataSource = ListaddlCanvas;
            ddlEnlaceCanvas.DataBind();

            ListaddlTablas = Session["ListaTablas"] as List<string>;
            ListaddlTablas.Add(campo_tbl);
            Session["ListaTablas"] = ListaddlTablas;
            ddlEnlaceTabla.DataSource = ListaddlTablas;
            ddlEnlaceTabla.DataBind();

            dtCanvas = Session["dtCanvas"] as DataTable;
            DataRow Row = dtCanvas.Rows[e.RowIndex];
            dtCanvas.Rows.Remove(Row);
            Session["dtCanvas"] = dtCanvas;
            gvDatos.DataSource = dtCanvas;
            gvDatos.DataBind();
        }

        protected void btnGenrarCodigo_Click(object sender, EventArgs e)
        {
            if ((Session["dtCanvas"] as DataTable).Rows.Count > 0)
            {
                DropDownList drpdwndec = (DropDownList)gvDatos.Rows[0].FindControl("ddlCamposCanvas");
                string test = drpdwndec.SelectedValue;


                //clsFactory objFactory = new clsFactory();
                //objFactory.FactoryMethod(Session["Tabla"].ToString(), Session["dtCanvas"] as DataTable);
                //Limpiar();
                ///Cambiar a MVC
                //Response.Redirect("~/Home/Index");
            }
        }

        private bool ValidarCamposCanvas(string _Campo)
        {
            bool exito = true;
            List<string> ListaEnlacesTablas = Session["ListaEnlacesTablas"] as List<string>;
            if (ListaEnlacesTablas.Count <= 0)
            {
                ListaEnlacesTablas.Add(_Campo);
            }
            else
            {
                foreach (string item in ListaEnlacesTablas)
                {
                    if (item.Equals(_Campo))
                    {
                        exito = false;
                        break;
                    }
                }
                if (exito)
                {
                    ListaEnlacesTablas.Add(_Campo);
                }
            }
            return exito;
        }

        private void Limpiar()
        {
            ListaddlCanvas = null;
            ListaddlTablas = null;
            dtCanvas = null;
            Session["dtCanvas"] = null;
            Session["ListaCanvas"] = null;
            Session["ListaTablas"] = null;
            Session["Tabla"] = null;
        }
    }
}