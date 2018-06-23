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
            clsGenerador objg = new clsGenerador();
            objg.WriteMethod();

            clsEsquema objEsquema = new clsEsquema();
            if (objEsquema.CargarEsquema())
            {
                DropDownList1.DataSource = objEsquema.ListaTablas;
                DropDownList1.DataBind();
            }
        }
    }
}