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
        private List<string> ListaObjetosCadeConexion;
        private List<string> ListaTablas;
        private int ContadorDDLTablas;
        private List<clsControlesDDLTablas> ListaControlesDDLTablas;
        private List<string> ListaTablasConstraint;
        private List<string> ListaddlColumnCanvas;
        private List<DataTable> ListadtColumnas;
        private List<string> ListaNombresTablasSeleccionadas;
        private List<DataTable> ListaTablasEnlasadas;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ListaObjetosCadeConexion"] == null)
            {
                ListaObjetosCadeConexion = new List<string>();
                Session["ListaObjetosCadeConexion"] = ListaObjetosCadeConexion;
            }

            if (Session["ContadorDDLTablas"] == null)
            {
                ContadorDDLTablas = 1;
                Session["ContadorDDLTablas"] = ContadorDDLTablas;
            }

            if (Session["ListaControlesDDLTablas"] == null)
            {
                ListaControlesDDLTablas = new List<clsControlesDDLTablas>();
                Session["ListaControlesDDLTablas"] = ListaControlesDDLTablas;
            }

            if (Session["ListaTablasConstraint"] == null)
            {
                ListaTablasConstraint = new List<string>();
                Session["ListaTablasConstraint"] = ListaTablasConstraint;
            }

            if (Session["ListaddlColumnCanvas"] == null)
            {
                ListaddlColumnCanvas = new List<string>();
                ListaddlColumnCanvas.Add("No Seleccionado");
                ListaddlColumnCanvas.Add("Label");
                ListaddlColumnCanvas.Add("Index Label");
                ListaddlColumnCanvas.Add("Posicion X");
                ListaddlColumnCanvas.Add("Posicion Y");
                Session["ListaddlColumnCanvas"] = ListaddlColumnCanvas;
            }

            if (Session["ListadtColumnas"] == null)
            {
                ListadtColumnas = new List<DataTable>();
                Session["ListadtColumnas"] = ListadtColumnas;
            }

            if (Session["ListaNombresTablasSeleccionadas"] == null)
            {
                ListaNombresTablasSeleccionadas = new List<string>();
                Session["ListaNombresTablasSeleccionadas"] = ListaNombresTablasSeleccionadas;
            }

            if (Session["ListaTablasEnlasadas"] == null)
            {
                ListaTablasEnlasadas = new List<DataTable>();
                Session["ListaTablasEnlasadas"] = ListaTablasEnlasadas;
            }

            string ddlid = Page.Request.QueryString["ddlid"];
            if (!string.IsNullOrEmpty(ddlid))
            {
                QuitarTabla(Convert.ToInt32(ddlid));
            }
        }

        protected void WizardStep1_Load(object sender, EventArgs e)
        {
            clsEsquema objEsquema = clsEsquema.obtenerclsEsquema();
            if (!string.IsNullOrEmpty(objEsquema.getConnectionString()))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "enable_startbutton", "enable_startbutton();", true);
            }
        }

        protected void WizardStep2_Load(object sender, EventArgs e)
        {
            ListaControlesDDLTablas = Session["ListaControlesDDLTablas"] as List<clsControlesDDLTablas>;
            if (ListaControlesDDLTablas.Count > 0)
            {
                ddlTablaPrincipal.Enabled = false;
                btnEstablecerTablaPrincipal.Enabled = false;
            }
            else
            {
                ddlTablaPrincipal.Enabled = true;
                btnEstablecerTablaPrincipal.Enabled = true;
            }
            foreach (clsControlesDDLTablas control in ListaControlesDDLTablas)
            {
                this.pNuevaTabla.Controls.Add(control.getDDL());
                this.pNuevaTabla.Controls.Add(control.getJUMP());
                this.pNuevaTabla.Controls.Add(control.getBTN());
                this.pNuevaTabla.Controls.Add(control.getSPACE());
                if (ListaControlesDDLTablas.LastOrDefault().getDDL().ID.ToString().Equals(control.getDDL().ID.ToString()))
                {
                    if (control.getDDL().ID.ToString().Equals("ddlTabla1"))
                    {
                        (this.pNuevaTabla.Controls[0] as DropDownList).Enabled = true;
                        (this.pNuevaTabla.Controls[2] as Button).Enabled = true;
                    }
                    else if (control.getDDL().ID.ToString().Equals("ddlTabla2"))
                    {
                        (this.pNuevaTabla.Controls[4] as DropDownList).Enabled = true;
                        (this.pNuevaTabla.Controls[6] as Button).Enabled = true;
                    }
                    else if (control.getDDL().ID.ToString().Equals("ddlTabla3"))
                    {
                        (this.pNuevaTabla.Controls[8] as DropDownList).Enabled = true;
                        (this.pNuevaTabla.Controls[10] as Button).Enabled = true;
                    }
                }
            }
        }

        protected void WizardStep3_Load(object sender, EventArgs e)
        {
            ListadtColumnas = Session["ListadtColumnas"] as List<DataTable>;
            ListaNombresTablasSeleccionadas = Session["ListaNombresTablasSeleccionadas"] as List<string>;
            if (ListadtColumnas.Count > 0 && ListaNombresTablasSeleccionadas.Count > 0)
            {
                int Contador = 0;
                foreach (DataTable dt in ListadtColumnas)
                {
                    if (Contador == 0)
                    {
                        lblTablaPrincipal.Text = ListaNombresTablasSeleccionadas[Contador];
                        gvPrincipal.DataSource = dt;
                        gvPrincipal.DataBind();
                    }
                    else if (Contador == 1)
                    {
                        lblSecundaria1.Text = ListaNombresTablasSeleccionadas[Contador];
                        gvSecundario1.DataSource = dt;
                        gvSecundario1.DataBind();
                    }
                    else if (Contador == 2)
                    {
                        lblSecundaria2.Text = ListaNombresTablasSeleccionadas[Contador];
                        gvSecundario2.DataSource = dt;
                        gvSecundario2.DataBind();
                    }
                    else if (Contador == 3)
                    {
                        lblSecundaria3.Text = ListaNombresTablasSeleccionadas[Contador];
                        gvSecundario3.DataSource = dt;
                        gvSecundario3.DataBind();
                    }
                    Contador++;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "enable_gridview", "hablitarGrid(" + (Contador - 1) + ");", true);
            }
        }

        protected void btnProbarConexion_Click(object sender, EventArgs e)
        {
            if (Session["ListaObjetosCadeConexion"] != null)
            {
                ListaObjetosCadeConexion = Session["ListaObjetosCadeConexion"] as List<string>;
                if (ListaObjetosCadeConexion.Count ==  5)
                {
                    ListaObjetosCadeConexion.Clear();
                }
                ListaObjetosCadeConexion.Add(ddlConexion.SelectedValue);
                ListaObjetosCadeConexion.Add(txtServidor.Text);
                ListaObjetosCadeConexion.Add(txtBaseDatos.Text);
                ListaObjetosCadeConexion.Add(txtUser.Text);
                ListaObjetosCadeConexion.Add(txtPassword.Text);
                Session["ListaObjetosCadeConexion"] = ListaObjetosCadeConexion;
                clsEsquema objEsquema = clsEsquema.obtenerclsEsquema();
                objEsquema.NuevoEsquema();
                if (objEsquema.ProbarConexion(ListaObjetosCadeConexion))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "enable_startbutton", "enable_startbutton();", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "Conexion_True", "alert('Conexion establecida con exito');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Conexion_False", "alert('No se ha podido establecer conexion');", true);
                }
            }
        }

        protected void StartNextButton_Click(object sender, EventArgs e)
        {
            clsEsquema objEsquema = clsEsquema.obtenerclsEsquema();
            if (objEsquema.getListaTablas().Count == 0)
            {
                ListaTablas = new List<string>();
                Session["ListaTablas"] = ListaTablas;
                CargarTablasBD();
            }
            else
            {
                ListaTablas = Session["ListaTablas"] as List<string>;
                ddlTablaPrincipal.DataSource = ListaTablas;
                ddlTablaPrincipal.DataBind();
            }
        }

        private void CargarTablasBD()
        {
            clsEsquema objEsquema = clsEsquema.obtenerclsEsquema();
            if (objEsquema.CargarTablasBD())
            {
                ListaTablas = Session["ListaTablas"] as List<string>;
                ListaTablas = objEsquema.getListaTablas();
                Session["ListaTablas"] = ListaTablas;
                ddlTablaPrincipal.DataSource = ListaTablas;
                ddlTablaPrincipal.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error_Tablas.Bind", "alert('No se ha podido cargar las tablas de la BD');", true);
            }
        }

        protected void btnEstablecerTablaPrincipal_Click(object sender, EventArgs e)
        {
            clsEsquema objEsquema = clsEsquema.obtenerclsEsquema();
            if (objEsquema.getdtConstraints().Rows.Count > 0)
            {
                ListaTablasConstraint = Session["ListaTablasConstraint"] as List<string>;
                ListaTablasConstraint.Clear();
                foreach (DataRow row in objEsquema.getdtConstraints().Rows)
                {
                    if (row[2].ToString().Equals(ddlTablaPrincipal.Text))
                    {
                        ListaTablasConstraint.Add(row[0].ToString());
                    }
                }
                Session["ListaTablasConstraint"] = ListaTablasConstraint;
                btnAgregarTabla.Enabled = true;
            }
        }

        protected void btnAgregarTabla_Click(object sender, EventArgs e)
        {
            ListaTablasConstraint = Session["ListaTablasConstraint"] as List<string>;
            ContadorDDLTablas = Convert.ToInt32(Session["ContadorDDLTablas"]);
            if (ContadorDDLTablas <= 3)
            {
                if (ContadorDDLTablas >= 2 && ListaTablasConstraint.Count > 0)
                {
                    ListaControlesDDLTablas.Last().setEnabled(false);
                    ListaTablasConstraint.Remove(ListaControlesDDLTablas.Last().getDDL().Text);
                }

                if (ListaTablasConstraint.Count > 0)
                {
                    ListaControlesDDLTablas = Session["ListaControlesDDLTablas"] as List<clsControlesDDLTablas>;
                    if (ContadorDDLTablas == 1)
                    {
                        ddlTablaPrincipal.Enabled = false;
                        btnEstablecerTablaPrincipal.Enabled = false;
                        DropDownList ddl = new DropDownList();
                        ddl.ID = "ddlTabla" + ContadorDDLTablas;
                        ddl.DataSource = ListaTablasConstraint;
                        ddl.DataBind();
                        Button btn = new Button();
                        btn.Text = "Quitar Tabla";
                        btn.ID = "btnQuitarTabla" + ContadorDDLTablas;
                        btn.OnClientClick = "javascript:QuitarTabla(" + ContadorDDLTablas + ");";
                        btn.CssClass = "btn btn-default";
                        LiteralControl jump = new LiteralControl("&nbsp;");
                        LiteralControl space = new LiteralControl("<br /><br />");

                        clsControlesDDLTablas newControl = new clsControlesDDLTablas(ddl, jump, btn, space);
                        ListaControlesDDLTablas.Add(newControl);
                    }
                    else if (ContadorDDLTablas == 2)
                    {
                        DropDownList ddl = new DropDownList();
                        ddl.ID = "ddlTabla" + ContadorDDLTablas;
                        ddl.DataSource = ListaTablasConstraint;
                        ddl.DataBind();
                        Button btn = new Button();
                        btn.Text = "Quitar Tabla";
                        btn.ID = "btnQuitarTabla" + ContadorDDLTablas;
                        btn.OnClientClick = "javascript:QuitarTabla(" + ContadorDDLTablas + ");";
                        btn.CssClass = "btn btn-default";
                        LiteralControl jump = new LiteralControl("&nbsp;");
                        LiteralControl space = new LiteralControl("<br /><br />");

                        clsControlesDDLTablas newControl = new clsControlesDDLTablas(ddl, jump, btn, space);
                        ListaControlesDDLTablas.Add(newControl);
                    }
                    else if (ContadorDDLTablas == 3)
                    {
                        DropDownList ddl = new DropDownList();
                        ddl.ID = "ddlTabla" + ContadorDDLTablas;
                        ddl.DataSource = ListaTablasConstraint;
                        ddl.DataBind();
                        Button btn = new Button();
                        btn.Text = "Quitar Tabla";
                        btn.ID = "btnQuitarTabla" + ContadorDDLTablas;
                        btn.OnClientClick = "javascript:QuitarTabla(" + ContadorDDLTablas + ");";
                        btn.CssClass = "btn btn-default";
                        LiteralControl jump = new LiteralControl("&nbsp;");
                        LiteralControl space = new LiteralControl("<br /><br />");

                        clsControlesDDLTablas newControl = new clsControlesDDLTablas(ddl, jump, btn, space);
                        ListaControlesDDLTablas.Add(newControl);
                    }

                    foreach (clsControlesDDLTablas control in ListaControlesDDLTablas)
                    {
                        this.pNuevaTabla.Controls.Add(control.getDDL());
                        this.pNuevaTabla.Controls.Add(control.getJUMP());
                        this.pNuevaTabla.Controls.Add(control.getBTN());
                        this.pNuevaTabla.Controls.Add(control.getSPACE());
                        if (ListaControlesDDLTablas.LastOrDefault().getDDL().ID.ToString().Equals(control.getDDL().ID.ToString()))
                        {
                            if (control.getDDL().ID.ToString().Equals("ddlTabla1"))
                            {
                                (this.pNuevaTabla.Controls[0] as DropDownList).Enabled = true;
                                (this.pNuevaTabla.Controls[2] as Button).Enabled = true;
                            }
                            else if (control.getDDL().ID.ToString().Equals("ddlTabla2"))
                            {
                                (this.pNuevaTabla.Controls[4] as DropDownList).Enabled = true;
                                (this.pNuevaTabla.Controls[6] as Button).Enabled = true;
                            }
                            else if (control.getDDL().ID.ToString().Equals("ddlTabla3"))
                            {
                                (this.pNuevaTabla.Controls[8] as DropDownList).Enabled = true;
                                (this.pNuevaTabla.Controls[10] as Button).Enabled = true;
                            }
                        }
                    }

                    ContadorDDLTablas++;
                    Session["ContadorDDLTablas"] = ContadorDDLTablas;
                    Session["ListaTablasConstraint"] = ListaTablasConstraint;
                    Session["ListaControlesDDLTablas"] = ListaControlesDDLTablas;
                }
            }
        }

        private void QuitarTabla(int index)
        {
            try
            {
                ListaControlesDDLTablas = Session["ListaControlesDDLTablas"] as List<clsControlesDDLTablas>;
                ListaControlesDDLTablas.RemoveAt(index - 1);
                if (ListaControlesDDLTablas.Count() > 0)
                {
                    ListaTablasConstraint = Session["ListaTablasConstraint"] as List<string>;
                    ListaTablasConstraint.Add(ListaControlesDDLTablas.Last().getDDL().Text);

                    foreach (clsControlesDDLTablas control in ListaControlesDDLTablas)
                    {
                        if (control.getDDL().ID.ToString().Equals(ListaControlesDDLTablas.Last().getDDL().ID.ToString()))
                        {
                            control.setEnabled(true);
                            control.cargarDDL(ListaTablasConstraint);
                        }
                    }
                    Session["ListaTablasConstraint"] = ListaTablasConstraint;
                }
                Session["ListaControlesDDLTablas"] = ListaControlesDDLTablas;
                ContadorDDLTablas = Convert.ToInt32(Session["ContadorDDLTablas"]);
                ContadorDDLTablas--;
                Session["ContadorDDLTablas"] = ContadorDDLTablas;
                Response.Redirect("~/Home.aspx");
            }
            catch (Exception)
            {
                Response.Redirect("~/Home.aspx");
            }
        }

        protected void StepNextButton_Click(object sender, EventArgs e)
        {
            ListadtColumnas = Session["ListadtColumnas"] as List<DataTable>;
            ListaNombresTablasSeleccionadas = Session["ListaNombresTablasSeleccionadas"] as List<string>;
            if (ListadtColumnas.Count > 0 && ListaNombresTablasSeleccionadas.Count > 0)
            {
                ListadtColumnas.Clear();
                ListaNombresTablasSeleccionadas.Clear();
                Session["ListadtColumnas"] = ListadtColumnas;
                Session["ListaNombresTablasSeleccionadas"] = ListaNombresTablasSeleccionadas;

            }
            crearGridViews_Step3();
        }

        private void crearGridViews_Step3()
        {
            ListaControlesDDLTablas = Session["ListaControlesDDLTablas"] as List<clsControlesDDLTablas>;
            ListadtColumnas = Session["ListadtColumnas"] as List<DataTable>;
            ListaNombresTablasSeleccionadas = Session["ListaNombresTablasSeleccionadas"] as List<string>;
            clsEsquema objEsquema = clsEsquema.obtenerclsEsquema();
            for (int i = 0; i < ListaControlesDDLTablas.Count + 1; i++)
            {
                DataTable dtColumnasxTable = new DataTable();
                if (i == 0)
                {
                    dtColumnasxTable = objEsquema.getColumnasXTabla(ddlTablaPrincipal.Text);
                    ListaNombresTablasSeleccionadas.Add(ddlTablaPrincipal.Text);
                }
                else
                {
                    dtColumnasxTable = objEsquema.getColumnasXTabla(ListaControlesDDLTablas[i - 1].getDDL().Text);
                    ListaNombresTablasSeleccionadas.Add(ListaControlesDDLTablas[i - 1].getDDL().Text);
                }
                ListadtColumnas.Add(dtColumnasxTable);
            }

            Session["ListadtColumnas"] = ListadtColumnas;
            Session["ListaNombresTablasSeleccionadas"] = ListaNombresTablasSeleccionadas;

            int Contador = 0;
            foreach (DataTable dt in ListadtColumnas)
            {
                if (Contador == 0)
                {
                    lblTablaPrincipal.Text = ListaNombresTablasSeleccionadas[Contador];
                    gvPrincipal.DataSource = dt;
                    gvPrincipal.DataBind();
                }
                else if (Contador == 1)
                {
                    lblSecundaria1.Text = ListaNombresTablasSeleccionadas[Contador];
                    gvSecundario1.DataSource = dt;
                    gvSecundario1.DataBind();
                }
                else if (Contador == 2)
                {
                    lblSecundaria2.Text = ListaNombresTablasSeleccionadas[Contador];
                    gvSecundario2.DataSource = dt;
                    gvSecundario2.DataBind();
                }
                else if (Contador == 3)
                {
                    lblSecundaria3.Text = ListaNombresTablasSeleccionadas[Contador];
                    gvSecundario3.DataSource = dt;
                    gvSecundario3.DataBind();
                }
                Contador++;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "enable_gridview", "hablitarGrid(" + (Contador - 1) + ");", true);
        }

        protected void gvPrincipal_RowCreated(object sender, GridViewRowEventArgs e)
        {
            ListaddlColumnCanvas = Session["ListaddlColumnCanvas"] as List<string>;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList drpdwndec = (DropDownList)e.Row.FindControl("ddlCamposCanvasPrincipal");
                drpdwndec.DataSource = ListaddlColumnCanvas;
                drpdwndec.DataBind();
            }
        }

        protected void gvSecundario1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            ListaddlColumnCanvas = Session["ListaddlColumnCanvas"] as List<string>;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList drpdwndec = (DropDownList)e.Row.FindControl("ddlCamposCanvasSecundario1");
                drpdwndec.DataSource = ListaddlColumnCanvas;
                drpdwndec.DataBind();
            }
        }

        protected void gvSecundario2_RowCreated(object sender, GridViewRowEventArgs e)
        {
            ListaddlColumnCanvas = Session["ListaddlColumnCanvas"] as List<string>;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList drpdwndec = (DropDownList)e.Row.FindControl("ddlCamposCanvasSecundario2");
                drpdwndec.DataSource = ListaddlColumnCanvas;
                drpdwndec.DataBind();
            }
        }

        protected void gvSecundario3_RowCreated(object sender, GridViewRowEventArgs e)
        {
            ListaddlColumnCanvas = Session["ListaddlColumnCanvas"] as List<string>;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList drpdwndec = (DropDownList)e.Row.FindControl("ddlCamposCanvasSecundario3");
                drpdwndec.DataSource = ListaddlColumnCanvas;
                drpdwndec.DataBind();
            }
        }

        protected void btnGenrarCodigo_Click(object sender, EventArgs e)
        {
            ListadtColumnas = Session["ListadtColumnas"] as List<DataTable>;
            ListaNombresTablasSeleccionadas = Session["ListaNombresTablasSeleccionadas"] as List<string>;
            ListaTablasEnlasadas = Session["ListaTablasEnlasadas"] as List<DataTable>;
            for (int i = 0; i < ListadtColumnas.Count; i++)
            {
                List<string> ListaEnlacesTablas = new List<string>();
                Session["ListaEnlacesTablas"] = ListaEnlacesTablas;
                DataTable dtTablasEnlaces = new DataTable();
                DataColumn column;
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "Enlace";
                dtTablasEnlaces.Columns.Add(column);
                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.ColumnName = "Columna";
                dtTablasEnlaces.Columns.Add(column);
                if (i == 0)
                {
                    foreach (GridViewRow row in gvPrincipal.Rows)
                    {
                        DropDownList drpdwndec = (DropDownList)row.Cells[0].FindControl("ddlCamposCanvasPrincipal");
                        if (ValidarCamposCanvas(drpdwndec.Text))
                        {
                            DataRow newRow = dtTablasEnlaces.NewRow();
                            newRow["Enlace"] = drpdwndec.Text;
                            newRow["Columna"] = row.Cells[1].Text;
                            dtTablasEnlaces.Rows.Add(newRow);
                        }
                    }
                }
                else if (i == 1)
                {
                    foreach (GridViewRow row in gvSecundario1.Rows)
                    {
                        DropDownList drpdwndec = (DropDownList)row.Cells[0].FindControl("ddlCamposCanvasSecundario1");
                        if (ValidarCamposCanvas(drpdwndec.Text))
                        {
                            DataRow newRow = dtTablasEnlaces.NewRow();
                            newRow["Enlace"] = drpdwndec.Text;
                            newRow["Columna"] = row.Cells[1].Text;
                            dtTablasEnlaces.Rows.Add(newRow);
                        }
                    }
                }
                else if (i == 2)
                {
                    foreach (GridViewRow row in gvSecundario2.Rows)
                    {
                        DropDownList drpdwndec = (DropDownList)row.Cells[0].FindControl("ddlCamposCanvasSecundario2");
                        if (ValidarCamposCanvas(drpdwndec.Text))
                        {
                            DataRow newRow = dtTablasEnlaces.NewRow();
                            newRow["Enlace"] = drpdwndec.Text;
                            newRow["Columna"] = row.Cells[1].Text;
                            dtTablasEnlaces.Rows.Add(newRow);
                        }
                    }
                }
                else if (i == 3)
                {
                    foreach (GridViewRow row in gvSecundario3.Rows)
                    {
                        DropDownList drpdwndec = (DropDownList)row.Cells[0].FindControl("ddlCamposCanvasSecundario3");
                        if (ValidarCamposCanvas(drpdwndec.Text))
                        {
                            DataRow newRow = dtTablasEnlaces.NewRow();
                            newRow["Enlace"] = drpdwndec.Text;
                            newRow["Columna"] = row.Cells[1].Text;
                            dtTablasEnlaces.Rows.Add(newRow);
                        }
                    }
                }
                ///Revisar que no guarde una de mas
                ListaTablasEnlasadas.Add(dtTablasEnlaces);
            }

            ///Una vez cargado las tablas instanciar el factory y generar la clase
            clsFactory objFactory = new clsFactory();
            if (true)
            {

            }


            //if ((Session["dtCanvas"] as DataTable).Rows.Count > 0)
            //{
            //    //DropDownList drpdwndec = (DropDownList)gvDatos.Rows[0].FindControl("ddlCamposCanvas");
            //    //string test = drpdwndec.SelectedValue;


            //    //clsFactory objFactory = new clsFactory();
            //    //objFactory.FactoryMethod(Session["Tabla"].ToString(), Session["dtCanvas"] as DataTable);
            //    //Limpiar();
            //    ///Cambiar a MVC
            //    //Response.Redirect("~/Home/Index");
            //}
        }

        private bool ValidarCamposCanvas(string _Campo)
        {
            bool exito = true;
            if (!_Campo.Equals("No Seleccionado"))
            {
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
            }
            else
            {
                exito = false;
            }
            return exito;
        }




        /// Lo demas no va
        /// 

        //protected void StepPreviousButton_Click(object sender, EventArgs e)
        //{
        //    lcTablas = Session["lcTablas"] as List<LiteralControl>;
        //    lcTablas.First().ID = "NotReloadWizard2";
        //    Session["lcTablas"] = lcTablas;
        //}

        //No va ha hacer falta

        //No va ha hacer falta
        //protected void ddlTablas_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    clsEsquema objEsquema = clsEsquema.obtenerclsEsquema();
        //    var query = from dtRow in objEsquema.getListaColumnas() where dtRow.NombreTabla.StartsWith(ddlTablas.SelectedValue) select dtRow.NombreColumna;
        //    if (query.ToList().Count > 0)
        //    {
        //        //ddlColumnas.DataSource = query.ToList();
        //        //ddlColumnas.DataBind();
        //    }
        //}

        private void cargarComboBoxListaCanvas()
        {
            //cargarListaCanvas();
            //ddlEnlaceCanvas.DataSource = ListaddlCanvas;
            //ddlEnlaceCanvas.DataBind();
        }

        private void cargarGrid()
        {
            //dtCanvas = new DataTable();
            //dtCanvas.Columns.Add("Columnas_Tabla", typeof(String));
            ////dtCanvas.Columns.Add("Datos_Canvas", typeof(WebControl));
            //Session["dtCanvas"] = dtCanvas;
            //gvDatos.DataSource = dtCanvas;
            //gvDatos.DataBind();
        }

        protected void btnAgregarEnlace_Click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(ddlEnlaceCanvas.SelectedValue) || !string.IsNullOrEmpty(ddlEnlaceTabla.SelectedValue))
            //{
            //    ///Elimina el campo de la lista
            //    string campo_cvs = ddlEnlaceCanvas.SelectedValue;
            //    ListaddlCanvas = Session["ListaCanvas"] as List<string>;
            //    ListaddlCanvas.Remove(campo_cvs);
            //    Session["ListaCanvas"] = ListaddlCanvas;
            //    ddlEnlaceCanvas.DataSource = ListaddlCanvas;
            //    ddlEnlaceCanvas.DataBind();

            //    ///Elimina el campo de la lista
            //    string campo_tbl = ddlEnlaceTabla.SelectedValue;
            //    ListaddlTablas = Session["ListaTablas"] as List<string>;
            //    ListaddlTablas.Remove(campo_tbl);
            //    Session["ListaTablas"] = ListaddlTablas;
            //    ddlEnlaceTabla.DataSource = ListaddlTablas;
            //    ddlEnlaceTabla.DataBind();

            //    ///Carga el row com los campos seleccionados
            //    dtCanvas = Session["dtCanvas"] as DataTable;
            //    DataRow NewRow = dtCanvas.NewRow();
            //    NewRow[0] = campo_tbl;
            //    //NewRow[1] = ddlEnlaceCanvas;
            //    dtCanvas.Rows.Add(NewRow);
            //    Session["dtCanvas"] = dtCanvas;
            //    gvDatos.DataSource = dtCanvas;
            //    gvDatos.DataBind();
            //}
        }

        protected void gvDatos_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row

                DropDownList drpdwndec = (DropDownList)e.Row.FindControl("ddlCamposCanvas");

                //drpdwndec.DataSource = ddlEnlaceCanvas.DataSource;

                drpdwndec.DataBind();

                //drpdwndec.Items.Insert(0, new ListItem("No Seleccionado"));
            }
        }

        protected void btnAgregarColumnas_Click(object sender, EventArgs e)
        {
            //ListaddlTablas = new List<string>();
            //ListaTablas = (Session["Tablas"]) as List<string>;
            //clsEsquema objEsquema = clsEsquema.obtenerclsEsquema();
            //var query = from dtRow in objEsquema.getListaColumnas() where dtRow.NombreTabla.StartsWith(ddlTablas.SelectedValue) select dtRow.NombreColumna;
            //ListaddlTablas = query.ToList();
            //Session["ListaTablas"] = ListaddlTablas;
            //ddlEnlaceTabla.DataSource = ListaddlTablas;
            //ddlEnlaceTabla.DataBind();
            //cargarComboBoxListaCanvas();
            //cargarGrid();
            //ListaTablas.Add(ddlTablas.SelectedValue);
            //Session["Tablas"] = ListaTablas;
            //btnAgregarEnlace.Enabled = true;
            //gvDatos.Enabled = true;
        }

        private void Limpiar()
        {
            //ListaObjetosCadeConexion = null;
            //ListaTablas = null;
            //Session["ListaObjetosCadeConexion"] = null;
            //Session["ListaTablas"] = null;
        }
    }

    public class clsControlesDDLTablas
    {
        private DropDownList ddl;
        private LiteralControl space;
        private Button btn;
        private LiteralControl jump;

        public DropDownList getDDL()
        {
            return this.ddl;
        }

        public LiteralControl getSPACE()
        {
            return this.space;
        }

        public Button getBTN()
        {
            return this.btn;
        }

        public LiteralControl getJUMP()
        {
            return this.jump;
        }

        public void setEnabled(bool _enabled)
        {
            this.ddl.Enabled = _enabled;
            this.btn.Enabled = _enabled;
        }

        public void cargarDDL(List<string> values)
        {
            this.ddl.DataSource = values;
            this.ddl.DataBind();
        }

        public clsControlesDDLTablas(DropDownList _ddl, LiteralControl _jump, Button _btn, LiteralControl _space)
        {
            this.ddl = _ddl;
            this.jump = _jump;
            this.btn = _btn;
            this.space = _space;
        }
    }
}