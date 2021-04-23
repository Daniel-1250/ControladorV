using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UTTT.Ejemplo.Persona
{
    public partial class MenuUserPerson : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["strNombre"] == null)
            {
                Response.Redirect("LoginUser.aspx");
                lblUsuarioDetalle.Text = "strNombre : " + Session["strNombre"];
            }
        }

        protected void btnCerrarS_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("LoginUser.aspx");
        }

        protected void btnDetallePersona_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("PersonaPrincipal.aspx");
        }

        protected void btnDetalleUsuario_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("UsuarioPrincipal.aspx");
        }
    }

}