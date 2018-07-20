using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FrameworkAnaliticaVisual
{
    public partial class ExView : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void UpdatePanel1_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "ddlGraficos", "tipoDiagrama('" + ddlGraficos.SelectedValue + "');", true);
        }
    }
}