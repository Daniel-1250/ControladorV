#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using System.Data.Linq;
using System.Linq.Expressions;
using System.Collections;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;
using EASendMail;

#endregion

namespace UTTT.Ejemplo.Persona
{
    public partial class PersonaPrincipal : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["strNombre"] == null)
            {
                Response.Redirect("LoginUser.aspx");
                lblUsuarioDetalle.Text = "strNombre : " + Session["strNombre"];
            }
            try
            {
                Response.Buffer = true;
                DataContext dcTemp = new DcGeneralDataContext();
                if (!this.IsPostBack)
                {
                    List<CatSexo> lista = dcTemp.GetTable<CatSexo>().ToList();
                    CatSexo catTemp = new CatSexo();
                    catTemp.id = -1;
                    catTemp.strValor = "Todos";
                    lista.Insert(0, catTemp);
                    this.ddlSexo.DataTextField = "strValor";
                    this.ddlSexo.DataValueField = "id";
                    this.ddlSexo.DataSource = lista;
                    this.ddlSexo.DataBind();

                    List<CatEstadoCivil> listaEstadoCivil = dcTemp.GetTable<CatEstadoCivil>().ToList();
                    CatEstadoCivil tempEstadoCivil = new CatEstadoCivil();
                    tempEstadoCivil.id = -1;
                    tempEstadoCivil.strValor = "Todos";
                    listaEstadoCivil.Insert(0, tempEstadoCivil);
                    this.ddlEstadoCivil.DataTextField = "strValor";
                    this.ddlEstadoCivil.DataValueField = "id";
                    this.ddlEstadoCivil.DataSource = listaEstadoCivil;
                    this.ddlEstadoCivil.DataBind();
                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                // Qué ha sucedido
                var mensaje = "Error message: " + _e.Message;
                // Información sobre la excepción interna
                if (_e.InnerException != null)
                {
                    mensaje = mensaje + " Inner exception: " + _e.InnerException.Message;
                }
                // Dónde ha sucedido
                mensaje = mensaje + " Stack trace: " + _e.StackTrace;
                this.Response.Redirect("~/PageError.aspx", false);

                this.EnviarCorreo("18300997@uttt.edu.mx", "Exception", mensaje);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                this.DataSourcePersona.RaiseViewChanged();
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al buscar");
                // Qué ha sucedido
                var mensaje = "Error message: " + _e.Message;
                // Información sobre la excepción interna
                if (_e.InnerException != null)
                {
                    mensaje = mensaje + " Inner exception: " + _e.InnerException.Message;
                }
                // Dónde ha sucedido
                mensaje = mensaje + " Stack trace: " + _e.StackTrace;
                this.Response.Redirect("~/PageError.aspx", false);

                this.EnviarCorreo("18300997@uttt.edu.mx", "Exception", mensaje);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                this.session.Pantalla = "~/PersonaManager.aspx";
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idPersona", "0");
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.Response.Redirect(this.session.Pantalla, false);               
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al agregar");
                // Qué ha sucedido
                var mensaje = "Error message: " + _e.Message;
                // Información sobre la excepción interna
                if (_e.InnerException != null)
                {
                    mensaje = mensaje + " Inner exception: " + _e.InnerException.Message;
                }
                // Dónde ha sucedido
                mensaje = mensaje + " Stack trace: " + _e.StackTrace;
                this.Response.Redirect("~/PageError.aspx", false);

                this.EnviarCorreo("18300997@uttt.edu.mx", "Exception", mensaje);
            }
        }

        protected void DataSourcePersona_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                DataContext dcConsulta = new DcGeneralDataContext();
                bool nombreBool = false;
                bool sexoBool = false;
                bool estadoCivil = false;
                if (!this.txtNombre.Text.Equals(String.Empty))
                {
                    nombreBool = true;
                }
                if (this.ddlSexo.Text != "-1")
                {
                    sexoBool = true;
                }

                if (this.ddlEstadoCivil.Text != "-1")
                {
                    estadoCivil = true;
                }

                Expression<Func<UTTT.Ejemplo.Linq.Data.Entity.Persona, bool>>
                    predicate =
                    (c =>
                    ((sexoBool) ? c.idCatSexo == int.Parse(this.ddlSexo.Text) : true) &&
                    ((estadoCivil) ? c.idCatEstadoCivil == int.Parse(this.ddlEstadoCivil.Text) : true) &&
                    ((nombreBool) ? (((nombreBool) ? c.strNombre.Contains(this.txtNombre.Text.Trim()) : false)) : true)
                    );
                //Expression<Func<UTTT.Ejemplo.Linq.Data.Entity.Persona, bool>> 
                //    predicate =
                //    (c =>
                //    ((sexoBool) ? c.idCatSexo == int.Parse(this.ddlSexo.Text) : true) &&             
                //    ((nombreBool) ? (((nombreBool) ? c.strNombre.Contains(this.txtNombre.Text.Trim()) : false)) : true)
                //    );

                predicate.Compile();

                List<UTTT.Ejemplo.Linq.Data.Entity.Persona> listaPersona =
                    dcConsulta.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Persona>().Where(predicate).ToList();
                e.Result = listaPersona;        
            }
            catch (Exception _e)
            {
                
                // Qué ha sucedido
                var mensaje = "Error message: " + _e.Message;
                // Información sobre la excepción interna
                if (_e.InnerException != null)
                {
                    mensaje = mensaje + " Inner exception: " + _e.InnerException.Message;
                }
                // Dónde ha sucedido
                mensaje = mensaje + " Stack trace: " + _e.StackTrace;
                this.Response.Redirect("~/PageError.aspx", false);

                this.EnviarCorreo("18300997@uttt.edu.mx", "Exception", mensaje);
            }
        }

        protected void dgvPersonas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int idPersona = int.Parse(e.CommandArgument.ToString());
                switch (e.CommandName)
                {
                    case "Editar":
                        this.editar(idPersona);
                        break;
                    case "Eliminar":
                        this.eliminar(idPersona);
                        break;
                    case "Direccion":
                        this.direccion(idPersona);
                        break;
                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al seleccionar");
                // Qué ha sucedido
                var mensaje = "Error message: " + _e.Message;
                // Información sobre la excepción interna
                if (_e.InnerException != null)
                {
                    mensaje = mensaje + " Inner exception: " + _e.InnerException.Message;
                }
                // Dónde ha sucedido
                mensaje = mensaje + " Stack trace: " + _e.StackTrace;
                this.Response.Redirect("~/PageError.aspx", false);

                this.EnviarCorreo("18300997@uttt.edu.mx", "Exception", mensaje);
            }
        }

        #endregion 

        #region Metodos

        private void editar(int _idPersona)
        {
            try
            {
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idPersona", _idPersona.ToString());
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.session.Pantalla = String.Empty;
                this.session.Pantalla = "~/PersonaManager.aspx";
                this.Response.Redirect(this.session.Pantalla, false);

            }
            catch (Exception _e)
            {
                throw _e;
                // Qué ha sucedido
                var mensaje = "Error message: " + _e.Message;
                // Información sobre la excepción interna
                if (_e.InnerException != null)
                {
                    mensaje = mensaje + " Inner exception: " + _e.InnerException.Message;
                }
                // Dónde ha sucedido
                mensaje = mensaje + " Stack trace: " + _e.StackTrace;
                this.Response.Redirect("~/PageError.aspx", false);

                this.EnviarCorreo("18300997@uttt.edu.mx", "Exception", mensaje);
            }
        }

        private void eliminar(int _idPersona)
        {
            try
            {
                DataContext dcDelete = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Persona persona = dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Persona>().First(
                    c => c.id == _idPersona);
                dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Persona>().DeleteOnSubmit(persona);
                dcDelete.SubmitChanges();
                this.showMessage("El registro se elimino correctamente.");
                this.DataSourcePersona.RaiseViewChanged();                
            }
            catch (Exception _e)
            {
                throw _e;
                // Qué ha sucedido
                var mensaje = "Error message: " + _e.Message;
                // Información sobre la excepción interna
                if (_e.InnerException != null)
                {
                    mensaje = mensaje + " Inner exception: " + _e.InnerException.Message;
                }
                // Dónde ha sucedido
                mensaje = mensaje + " Stack trace: " + _e.StackTrace;
                this.Response.Redirect("~/PageError.aspx", false);

                this.EnviarCorreo("18300997@uttt.edu.mx", "Exception", mensaje);
            }
        }

        private void direccion(int _idPersona)
        {
            try
            {
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idPersona", _idPersona.ToString());
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.session.Pantalla = String.Empty;
                this.session.Pantalla = "~/DireccionManager.aspx";
                this.Response.Redirect(this.session.Pantalla, false);
            }
            catch (Exception _e)
            {
                throw _e;
                // Qué ha sucedido
                var mensaje = "Error message: " + _e.Message;
                // Información sobre la excepción interna
                if (_e.InnerException != null)
                {
                    mensaje = mensaje + " Inner exception: " + _e.InnerException.Message;
                }
                // Dónde ha sucedido
                mensaje = mensaje + " Stack trace: " + _e.StackTrace;
                this.Response.Redirect("~/PageError.aspx", false);

                this.EnviarCorreo("18300997@uttt.edu.mx", "Exception", mensaje);
            }
        }

        public void EnviarCorreo(string correoDestino, string asunto, string mensajeCorreo)
        {
            string mensaje = "Error al enviar correo.";

            try
            {
                SmtpMail objetoCorreo = new SmtpMail("TryIt");

                objetoCorreo.From = "pbdaniel768@gmail.com";
                objetoCorreo.To = correoDestino;
                objetoCorreo.Subject = asunto;
                objetoCorreo.TextBody = mensajeCorreo;

                SmtpServer objetoServidor = new SmtpServer("smtp.gmail.com");//servidor proporcionado desde la configuracion de google

                objetoServidor.User = "pbdaniel768@gmail.com";
                objetoServidor.Password = "Velkoz#7";
                objetoServidor.Port = 587;
                objetoServidor.ConnectType = SmtpConnectType.ConnectSSLAuto;

                SmtpClient objetoCliente = new SmtpClient();
                objetoCliente.SendMail(objetoServidor, objetoCorreo);
                mensaje = "Correo Enviado Correctamente.";


            }
            catch (Exception ex)
            {
                mensaje = "Error al enviar correo." + ex.Message;
            }
        }

        #endregion

        protected void dgvPersonas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnCerrarS_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("LoginUser.aspx");
        }
    }
}