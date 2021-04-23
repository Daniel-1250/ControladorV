using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UTTT.Ejemplo.Persona
{
    public partial class LoginUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrorM.Visible = false;
        }

        protected void btLogin_Click(object sender, EventArgs e)
        {
            using (SqlConnection sqlConnect = new SqlConnection("Data Source=DBPersona3.mssql.somee.com;Initial Catalog=DBPersona3;User ID=DanielPerez_SQLLogin_4;Password=1cablwmiwf"))
            {
                string query = "SELECT COUNT(1) FROM USUARIO WHERE strNombre=@strNombre AND strPassword=@strPassword AND idCatUsuario = 1";
                SqlCommand sqlCom = new SqlCommand(query, sqlConnect);
                sqlCom.Parameters.AddWithValue("@strNombre", txtUsuario.Text.Trim());
                string encriptPass = Encrypt.GetSHA256(txtPassword.Text.Trim());
                sqlCom.Parameters.AddWithValue("@strPassword", encriptPass);

                sqlConnect.Open();
                int cout = Convert.ToInt32(sqlCom.ExecuteScalar());
                if (cout==1)
                {
                    Session["strNombre"] = txtUsuario.Text.Trim();
                    Response.Redirect("MenuUserPerson.aspx");
                }
                else
                {
                    lblErrorM.Visible = true;
                }

            }
        }
        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("~/CambiarContrasenia.aspx");
        }

        protected void btnCerrarS_Click(object sender, EventArgs e)
        {

        }
    }
}