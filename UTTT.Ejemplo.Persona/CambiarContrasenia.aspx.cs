using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Model;

namespace UTTT.Ejemplo.Persona
{
    public partial class CambiarContrasenia : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        DBPersona3Entities bd = new DBPersona3Entities();
        DataContext dcGuardar = new DcGeneralDataContext();
        private UTTT.Ejemplo.Linq.Data.Entity.Usuario baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        

        protected void btnPass_Click(object sender, EventArgs e)
        {
            try
            {
                var usuario = bd.Persona.FirstOrDefault(x => x.strCorreoElecrronico == txtEmail.Text);
                if (usuario != null)
                {
                    var usu2 = bd.Usuario.FirstOrDefault(u => u.idPersona == usuario.id);
                    string correo = usuario.strCorreoElecrronico.ToString();
                    MD5("120500");
                    string tak = Token();
                    CorreoE(tak, correo);
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Usuario>().First(c => c.id == usu2.id);
                    DataContext dcGuardar = new DcGeneralDataContext();
                    UTTT.Ejemplo.Linq.Data.Entity.Usuario user = new Linq.Data.Entity.Usuario();
                    if (dcGlobal != null)
                    {
                        user = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Usuario>().First(u=> u.id == usu2.id);
                        var tok = tak;
                        Session["open"] = tok.ToString().Trim();
                        user.token = tok.ToString().Trim();
                        var net = Session["open"].ToString();
                        dcGuardar.SubmitChanges();
                        this.lblMessage.Text = "Se le ha enviado un correo por favor revise su bandeja";
                    }

                }
                else
                {
                    this.lblMessage.Text = "Correo no registrado";
                }
                
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = ex.Message;
            }
        }
        public static string MD5(string word)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(word));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
        public static string Token()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray()) i *= ((int)b + 1);
            return MD5(string.Format("{0:x}", i - DateTime.Now.Ticks));
        }
        public new void CorreoE(string error, string correo)
        {
            string EmailOrigen = "pbdaniel768@gmail.com";
            string EmailDestino = correo;
            string contra = "Velkoz#7";

            MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino, "Buen Dia usted a solisitado un Cambio de contraseña por favor dirijase al siguiente link: ", "http://www.appperson.somee.com/RestablecerContrasenia.aspx" + "?token=" + error);

            oMailMessage.IsBodyHtml = true;
            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false;
            oSmtpClient.Port = 587;
            oSmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, contra);

            oSmtpClient.Send(oMailMessage);

            oMailMessage.Dispose();
        }
    }
}