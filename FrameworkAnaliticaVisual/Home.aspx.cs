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
        private List<clsTablesColumnsSelected> ListaclsTablesColumnsSelected;
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
                ListaddlColumnCanvas.Add("Posicion Y");
                ListaddlColumnCanvas.Add("Posicion X / Label");
                ListaddlColumnCanvas.Add("Index Label");
                ListaddlColumnCanvas.Add("Name");
                Session["ListaddlColumnCanvas"] = ListaddlColumnCanvas;
            }

            if (Session["ListaclsTablesColumnsSelected"] == null)
            {
                ListaclsTablesColumnsSelected = new List<clsTablesColumnsSelected>();
                Session["ListaclsTablesColumnsSelected"] = ListaclsTablesColumnsSelected;
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
            if (gvVista_1.Rows.Count < 0)
            {
                ListaclsTablesColumnsSelected = Session["ListaclsTablesColumnsSelected"] as List<clsTablesColumnsSelected>;
                ListaNombresTablasSeleccionadas = Session["ListaNombresTablasSeleccionadas"] as List<string>;
                if (ListaclsTablesColumnsSelected.Count > 0 && ListaNombresTablasSeleccionadas.Count > 0)
                {
                    DataTable dtnewColumns = new DataTable();
                    dtnewColumns = Session["dtnewColumns"] as DataTable;
                    for (int i = 0; i < 4; i++)
                    {
                        if (i == 0)
                        {
                            gvVista_1.DataSource = dtnewColumns;
                            gvVista_1.DataBind();
                        }
                        else if (i == 1)
                        {
                            gvVista_2.DataSource = dtnewColumns;
                            gvVista_2.DataBind();
                        }
                        else if (i == 2)
                        {
                            gvVista_3.DataSource = dtnewColumns;
                            gvVista_3.DataBind();
                        }
                        else if (i == 3)
                        {
                            gvVista_4.DataSource = dtnewColumns;
                            gvVista_4.DataBind();
                        }
                    }
                }
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
            Session["TablaPrincipal"] = ddlTablaPrincipal.Text;
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
            ListaclsTablesColumnsSelected = Session["ListaclsTablesColumnsSelected"] as List<clsTablesColumnsSelected>;
            ListaNombresTablasSeleccionadas = Session["ListaNombresTablasSeleccionadas"] as List<string>;
            if (ListaclsTablesColumnsSelected.Count > 0 && ListaNombresTablasSeleccionadas.Count > 0)
            {
                ListaclsTablesColumnsSelected.Clear();
                ListaNombresTablasSeleccionadas.Clear();
                Session["ListaclsTablesColumnsSelected"] = ListaclsTablesColumnsSelected;
                Session["ListaNombresTablasSeleccionadas"] = ListaNombresTablasSeleccionadas;

            }
            crearGridViews_Step3();
        }

        private void crearGridViews_Step3()
        {
            ListaControlesDDLTablas = Session["ListaControlesDDLTablas"] as List<clsControlesDDLTablas>;
            ListaclsTablesColumnsSelected = Session["ListaclsTablesColumnsSelected"] as List<clsTablesColumnsSelected>;
            ListaNombresTablasSeleccionadas = Session["ListaNombresTablasSeleccionadas"] as List<string>;
            clsEsquema objEsquema = clsEsquema.obtenerclsEsquema();
            for (int i = 0; i < ListaControlesDDLTablas.Count + 1; i++)
            {
                clsTablesColumnsSelected objTablesColumnsSelected;
                DataTable dtColumnasxTable = new DataTable();
                if (i == 0)
                {
                    dtColumnasxTable = objEsquema.getColumnasXTabla(ddlTablaPrincipal.Text);
                    ListaNombresTablasSeleccionadas.Add(ddlTablaPrincipal.Text);
                    objTablesColumnsSelected = new clsTablesColumnsSelected(ddlTablaPrincipal.Text, dtColumnasxTable);
                }
                else
                {
                    dtColumnasxTable = objEsquema.getColumnasXTabla(ListaControlesDDLTablas[i - 1].getDDL().Text);
                    ListaNombresTablasSeleccionadas.Add(ListaControlesDDLTablas[i - 1].getDDL().Text);
                    objTablesColumnsSelected = new clsTablesColumnsSelected(ListaControlesDDLTablas[i - 1].getDDL().Text, dtColumnasxTable);
                }
                ListaclsTablesColumnsSelected.Add(objTablesColumnsSelected);
            }

            Session["ListaclsTablesColumnsSelected"] = ListaclsTablesColumnsSelected;
            Session["ListaNombresTablasSeleccionadas"] = ListaNombresTablasSeleccionadas;

            DataTable dtnewColumns = new DataTable();
            DataColumn column;
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Consecutivo";
            dtnewColumns.Columns.Add(column);
            for (int i = 0; i < 4; i++)
            {
                DataRow newRow = dtnewColumns.NewRow();
                newRow["Consecutivo"] = (i + 1).ToString();
                dtnewColumns.Rows.Add(newRow);
            }
            Session["dtnewColumns"] = dtnewColumns;
            
            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                {
                    gvVista_1.DataSource = dtnewColumns;
                    gvVista_1.DataBind();
                }
                else if (i == 1)
                {
                    gvVista_2.DataSource = dtnewColumns;
                    gvVista_2.DataBind();
                }
                else if (i == 2)
                {
                    gvVista_3.DataSource = dtnewColumns;
                    gvVista_3.DataBind();
                }
                else if (i == 3)
                {
                    gvVista_4.DataSource = dtnewColumns;
                    gvVista_4.DataBind();
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "HabilitarVistas_True", "habilitarGrid(" + ddlNumeroVistas.SelectedValue + ");", true);
        }

        protected void gvVista_1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[4].Visible = false;
        }

        protected void gvVista_2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[4].Visible = false;
        }

        protected void gvVista_3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[4].Visible = false;
        }

        protected void gvVista_4_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[4].Visible = false;
        }

        protected void gvVista_1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            ListaddlColumnCanvas = Session["ListaddlColumnCanvas"] as List<string>;
            ListaNombresTablasSeleccionadas = Session["ListaNombresTablasSeleccionadas"] as List<string>;
            ListaclsTablesColumnsSelected = Session["ListaclsTablesColumnsSelected"] as List<clsTablesColumnsSelected>;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddl1 = (DropDownList)e.Row.FindControl("ddlVista_1_CamposCanvas");
                ddl1.DataSource = ListaddlColumnCanvas;
                ddl1.DataBind();

                DropDownList ddl2 = (DropDownList)e.Row.FindControl("ddlVista_1_Tablas");
                ddl2.DataSource = ListaNombresTablasSeleccionadas;
                ddl2.DataBind();

                DropDownList ddl3 = (DropDownList)e.Row.FindControl("ddlVista_1_Columnas");
                ddl3.DataSource = ListaclsTablesColumnsSelected.First().getColumns_names();
                ddl3.DataTextField = "COLUMN_NAME";
                ddl3.DataValueField = "COLUMN_NAME";
                ddl3.DataBind();
            }
        }

        protected void gvVista_2_RowCreated(object sender, GridViewRowEventArgs e)
        {
            ListaddlColumnCanvas = Session["ListaddlColumnCanvas"] as List<string>;
            ListaNombresTablasSeleccionadas = Session["ListaNombresTablasSeleccionadas"] as List<string>;
            ListaclsTablesColumnsSelected = Session["ListaclsTablesColumnsSelected"] as List<clsTablesColumnsSelected>;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddl1 = (DropDownList)e.Row.FindControl("ddlVista_2_CamposCanvas");
                ddl1.DataSource = ListaddlColumnCanvas;
                ddl1.DataBind();

                DropDownList ddl2 = (DropDownList)e.Row.FindControl("ddlVista_2_Tablas");
                ddl2.DataSource = ListaNombresTablasSeleccionadas;
                ddl2.DataBind();

                DropDownList ddl3 = (DropDownList)e.Row.FindControl("ddlVista_2_Columnas");
                ddl3.DataSource = ListaclsTablesColumnsSelected.First().getColumns_names();
                ddl3.DataTextField = "COLUMN_NAME";
                ddl3.DataValueField = "COLUMN_NAME";
                ddl3.DataBind();
            }
        }

        protected void gvVista_3_RowCreated(object sender, GridViewRowEventArgs e)
        {
            ListaddlColumnCanvas = Session["ListaddlColumnCanvas"] as List<string>;
            ListaNombresTablasSeleccionadas = Session["ListaNombresTablasSeleccionadas"] as List<string>;
            ListaclsTablesColumnsSelected = Session["ListaclsTablesColumnsSelected"] as List<clsTablesColumnsSelected>;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddl1 = (DropDownList)e.Row.FindControl("ddlVista_3_CamposCanvas");
                ddl1.DataSource = ListaddlColumnCanvas;
                ddl1.DataBind();

                DropDownList ddl2 = (DropDownList)e.Row.FindControl("ddlVista_3_Tablas");
                ddl2.DataSource = ListaNombresTablasSeleccionadas;
                ddl2.DataBind();

                DropDownList ddl3 = (DropDownList)e.Row.FindControl("ddlVista_3_Columnas");
                ddl3.DataSource = ListaclsTablesColumnsSelected.First().getColumns_names();
                ddl3.DataTextField = "COLUMN_NAME";
                ddl3.DataValueField = "COLUMN_NAME";
                ddl3.DataBind();
            }
        }

        protected void gvVista_4_RowCreated(object sender, GridViewRowEventArgs e)
        {
            ListaddlColumnCanvas = Session["ListaddlColumnCanvas"] as List<string>;
            ListaNombresTablasSeleccionadas = Session["ListaNombresTablasSeleccionadas"] as List<string>;
            ListaclsTablesColumnsSelected = Session["ListaclsTablesColumnsSelected"] as List<clsTablesColumnsSelected>;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddl1 = (DropDownList)e.Row.FindControl("ddlVista_4_CamposCanvas");
                ddl1.DataSource = ListaddlColumnCanvas;
                ddl1.DataBind();

                DropDownList ddl2 = (DropDownList)e.Row.FindControl("ddlVista_4_Tablas");
                ddl2.DataSource = ListaNombresTablasSeleccionadas;
                ddl2.DataBind();

                DropDownList ddl3 = (DropDownList)e.Row.FindControl("ddlVista_4_Columnas");
                ddl3.DataSource = ListaclsTablesColumnsSelected.First().getColumns_names();
                ddl3.DataTextField = "COLUMN_NAME";
                ddl3.DataValueField = "COLUMN_NAME";
                ddl3.DataBind();
            }
        }

        protected void gvVista_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListaclsTablesColumnsSelected = Session["ListaclsTablesColumnsSelected"] as List<clsTablesColumnsSelected>;
                DropDownList ddl1 = (DropDownList)gvVista_1.Rows[gvVista_1.SelectedIndex].FindControl("ddlVista_1_Tablas");
                DropDownList ddl2 = (DropDownList)gvVista_1.Rows[gvVista_1.SelectedIndex].FindControl("ddlVista_1_Columnas");
                for (int i = 0; i < ListaclsTablesColumnsSelected.Count; i++)
                {
                    if (ListaclsTablesColumnsSelected.ElementAt(i).getTable_name().Equals(ddl1.SelectedValue.ToString()))
                    {
                        ddl2.DataSource = ListaclsTablesColumnsSelected.ElementAt(i).getColumns_names();
                        ddl2.DataTextField = "COLUMN_NAME";
                        ddl2.DataValueField = "COLUMN_NAME";
                        ddl2.DataBind();
                        break;
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "HabilitarVistas_True", "habilitarGrid(" + ddlNumeroVistas.SelectedValue + ");", true);
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error_Columnas.Bind", "alert('No se ha podido cargar las columnas correctamente, favor volver a intentar');", true);
            }
        }

        protected void gvVista_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListaclsTablesColumnsSelected = Session["ListaclsTablesColumnsSelected"] as List<clsTablesColumnsSelected>;
                DropDownList ddl1 = (DropDownList)gvVista_2.Rows[gvVista_2.SelectedIndex].FindControl("ddlVista_2_Tablas");
                DropDownList ddl2 = (DropDownList)gvVista_2.Rows[gvVista_2.SelectedIndex].FindControl("ddlVista_2_Columnas");
                for (int i = 0; i < ListaclsTablesColumnsSelected.Count; i++)
                {
                    if (ListaclsTablesColumnsSelected.ElementAt(i).getTable_name().Equals(ddl1.SelectedValue.ToString()))
                    {
                        ddl2.DataSource = ListaclsTablesColumnsSelected.ElementAt(i).getColumns_names();
                        ddl2.DataTextField = "COLUMN_NAME";
                        ddl2.DataValueField = "COLUMN_NAME";
                        ddl2.DataBind();
                        break;
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "HabilitarVistas_True", "habilitarGrid(" + ddlNumeroVistas.SelectedValue + ");", true);
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error_Columnas.Bind", "alert('No se ha podido cargar las columnas correctamente, favor volver a intentar');", true);
            }
        }

        protected void gvVista_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListaclsTablesColumnsSelected = Session["ListaclsTablesColumnsSelected"] as List<clsTablesColumnsSelected>;
                DropDownList ddl1 = (DropDownList)gvVista_3.Rows[gvVista_3.SelectedIndex].FindControl("ddlVista_3_Tablas");
                DropDownList ddl2 = (DropDownList)gvVista_3.Rows[gvVista_3.SelectedIndex].FindControl("ddlVista_3_Columnas");
                for (int i = 0; i < ListaclsTablesColumnsSelected.Count; i++)
                {
                    if (ListaclsTablesColumnsSelected.ElementAt(i).getTable_name().Equals(ddl1.SelectedValue.ToString()))
                    {
                        ddl2.DataSource = ListaclsTablesColumnsSelected.ElementAt(i).getColumns_names();
                        ddl2.DataTextField = "COLUMN_NAME";
                        ddl2.DataValueField = "COLUMN_NAME";
                        ddl2.DataBind();
                        break;
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "HabilitarVistas_True", "habilitarGrid(" + ddlNumeroVistas.SelectedValue + ");", true);
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error_Columnas.Bind", "alert('No se ha podido cargar las columnas correctamente, favor volver a intentar');", true);
            }
        }

        protected void gvVista_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListaclsTablesColumnsSelected = Session["ListaclsTablesColumnsSelected"] as List<clsTablesColumnsSelected>;
                DropDownList ddl1 = (DropDownList)gvVista_4.Rows[gvVista_4.SelectedIndex].FindControl("ddlVista_4_Tablas");
                DropDownList ddl2 = (DropDownList)gvVista_4.Rows[gvVista_4.SelectedIndex].FindControl("ddlVista_4_Columnas");
                for (int i = 0; i < ListaclsTablesColumnsSelected.Count; i++)
                {
                    if (ListaclsTablesColumnsSelected.ElementAt(i).getTable_name().Equals(ddl1.SelectedValue.ToString()))
                    {
                        ddl2.DataSource = ListaclsTablesColumnsSelected.ElementAt(i).getColumns_names();
                        ddl2.DataTextField = "COLUMN_NAME";
                        ddl2.DataValueField = "COLUMN_NAME";
                        ddl2.DataBind();
                        break;
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "HabilitarVistas_True", "habilitarGrid(" + ddlNumeroVistas.SelectedValue + ");", true);
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Error_Columnas.Bind", "alert('No se ha podido cargar las columnas correctamente, favor volver a intentar');", true);
            }
        }

        protected void btnGenrarCodigo_Click(object sender, EventArgs e)
        {
            ListaTablasEnlasadas = Session["ListaTablasEnlasadas"] as List<DataTable>;
            for (int i = 0; i < Convert.ToInt32(ddlNumeroVistas.SelectedValue); i++)
            {
                List<string> ListaVerificarEnlacesTablas = new List<string>();
                Session["ListaVerificarEnlacesTablas"] = ListaVerificarEnlacesTablas;
                DataTable dtTablasEnlaces = new DataTable();
                DataColumn column;
                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.ColumnName = "Tabla";
                dtTablasEnlaces.Columns.Add(column);
                column = new DataColumn();
                column.DataType = Type.GetType("System.String");
                column.ColumnName = "Columna";
                dtTablasEnlaces.Columns.Add(column);
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "Enlace";
                dtTablasEnlaces.Columns.Add(column);
                if (i == 0)
                {
                    foreach (GridViewRow row in gvVista_1.Rows)
                    {
                        DropDownList ddl1 = (DropDownList)row.Cells[1].FindControl("ddlVista_1_Tablas");
                        DropDownList ddl2 = (DropDownList)row.Cells[2].FindControl("ddlVista_1_Columnas");
                        DropDownList ddl3 = (DropDownList)row.Cells[3].FindControl("ddlVista_1_CamposCanvas");
                        if (ValidarCamposCanvas(ddl3.Text))
                        {
                            DataRow newRow = dtTablasEnlaces.NewRow();
                            newRow["Tabla"] = ddl1.Text;
                            newRow["Columna"] = ddl2.Text;
                            newRow["Enlace"] = ddl3.Text;
                            dtTablasEnlaces.Rows.Add(newRow);
                        }
                    }
                }
                else if (i == 1)
                {
                    foreach (GridViewRow row in gvVista_2.Rows)
                    {
                        DropDownList ddl1 = (DropDownList)row.Cells[1].FindControl("ddlVista_2_Tablas");
                        DropDownList ddl2 = (DropDownList)row.Cells[2].FindControl("ddlVista_2_Columnas");
                        DropDownList ddl3 = (DropDownList)row.Cells[3].FindControl("ddlVista_2_CamposCanvas");
                        if (ValidarCamposCanvas(ddl3.Text))
                        {
                            DataRow newRow = dtTablasEnlaces.NewRow();
                            newRow["Tabla"] = ddl1.Text;
                            newRow["Columna"] = ddl2.Text;
                            newRow["Enlace"] = ddl3.Text;
                            dtTablasEnlaces.Rows.Add(newRow);
                        }
                    }
                }
                else if (i == 2)
                {
                    foreach (GridViewRow row in gvVista_3.Rows)
                    {
                        DropDownList ddl1 = (DropDownList)row.Cells[1].FindControl("ddlVista_3_Tablas");
                        DropDownList ddl2 = (DropDownList)row.Cells[2].FindControl("ddlVista_3_Columnas");
                        DropDownList ddl3 = (DropDownList)row.Cells[3].FindControl("ddlVista_3_CamposCanvas");
                        if (ValidarCamposCanvas(ddl3.Text))
                        {
                            DataRow newRow = dtTablasEnlaces.NewRow();
                            newRow["Tabla"] = ddl1.Text;
                            newRow["Columna"] = ddl2.Text;
                            newRow["Enlace"] = ddl3.Text;
                            dtTablasEnlaces.Rows.Add(newRow);
                        }
                    }
                }
                else if (i == 3)
                {
                    foreach (GridViewRow row in gvVista_4.Rows)
                    {
                        DropDownList ddl1 = (DropDownList)row.Cells[1].FindControl("ddlVista_4_Tablas");
                        DropDownList ddl2 = (DropDownList)row.Cells[2].FindControl("ddlVista_4_Columnas");
                        DropDownList ddl3 = (DropDownList)row.Cells[3].FindControl("ddlVista_4_CamposCanvas");
                        if (ValidarCamposCanvas(ddl3.Text))
                        {
                            DataRow newRow = dtTablasEnlaces.NewRow();
                            newRow["Tabla"] = ddl1.Text;
                            newRow["Columna"] = ddl2.Text;
                            newRow["Enlace"] = ddl3.Text;
                            dtTablasEnlaces.Rows.Add(newRow);
                        }
                    }
                }

                if (dtTablasEnlaces.Rows.Count > 0)
                {
                    ListaTablasEnlasadas.Add(dtTablasEnlaces);
                }
            }

            clsFactory objFactory = new clsFactory();
            if (objFactory.FactoryMethod(ListaTablasEnlasadas, Session["TablaPrincipal"].ToString()))
            {
                Limpiar();
                Response.Redirect("Home.aspx");
                ///no tira el mensaje
                ScriptManager.RegisterStartupScript(this, GetType(), "Create_Class_True", "alert('Se ha escrito la clase');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Create_Class_False", "alert('Error al escribir la clase');", true);
            }
        }

        private bool ValidarCamposCanvas(string _Campo)
        {
            bool exito = true;
            if (!_Campo.Equals("No Seleccionado"))
            {
                List<string> ListaVerificarEnlacesTablas = Session["ListaVerificarEnlacesTablas"] as List<string>;
                if (ListaVerificarEnlacesTablas.Count <= 0)
                {
                    ListaVerificarEnlacesTablas.Add(_Campo);
                }
                else
                {
                    foreach (string item in ListaVerificarEnlacesTablas)
                    {
                        if (item.Equals(_Campo))
                        {
                            exito = false;
                            break;
                        }
                    }
                    if (exito)
                    {
                        ListaVerificarEnlacesTablas.Add(_Campo);
                    }
                }
            }
            else
            {
                exito = false;
            }
            return exito;
        }

        private void Limpiar()
        {
            Session["ListaObjetosCadeConexion"] = null;
            Session["ContadorDDLTablas"] = null;
            Session["ListaControlesDDLTablas"] = null;
            Session["ListaTablasConstraint"] = null;
            Session["ListaddlColumnCanvas"] = null;
            Session["ListaclsTablesColumnsSelected"] = null;
            Session["ListaNombresTablasSeleccionadas"] = null;
            Session["ListaTablasEnlasadas"] = null;
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

    public class clsTablesColumnsSelected
    {
        private string table_name;
        private DataTable columns_names;

        public string getTable_name()
        {
            return this.table_name;
        }

        public DataTable getColumns_names()
        {
            return this.columns_names;
        }

        public clsTablesColumnsSelected(string _table_name, DataTable _columns_names)
        {
            this.columns_names = new DataTable();
            this.table_name = _table_name;
            this.columns_names = _columns_names;
        }
    }
}