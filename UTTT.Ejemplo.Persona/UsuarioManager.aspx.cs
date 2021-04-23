using EASendMail;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;

namespace UTTT.Ejemplo.Persona
{
    public partial class UsuarioManager : System.Web.UI.Page
    {
        private SessionManager session = new SessionManager();
        private int idUsuario = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Usuario baseEntity;

        private DataContext dcGlobal = new DcGeneralDataContext();
        private int tipoAccion = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["strNombre"] == null)
            {
                Response.Redirect("LoginUser.aspx");
                lblUsuarioDetalle.Text = "strNombre : " + Session["strNombre"];
            }
            try
            {
                this.Response.Buffer = true;
                this.session = (SessionManager)this.Session["SessionManager"];
                this.idUsuario = this.session.Parametros["idUsuario"] != null ?
                    int.Parse(this.session.Parametros["idUsuario"].ToString()) : 0;
                if (this.idUsuario == 0)
                {
                    this.baseEntity = new Linq.Data.Entity.Usuario();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Usuario>().First(c => c.id == this.idUsuario);
                    this.tipoAccion = 2;
                }
                if (!this.IsPostBack)
                {
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }

                    List<CatUsuario> lista = dcGlobal.GetTable<CatUsuario>().ToList();
                    CatUsuario catTemp = new CatUsuario();

                    this.ddlCatUsuario.DataTextField = "strValor";
                    this.ddlCatUsuario.DataValueField = "id";
                    this.ddlCatUsuario.DataSource = lista;
                    this.ddlCatUsuario.DataBind();
                    UnirDropDownL(); 
                    this.ddlCatUsuario.SelectedIndexChanged += new EventHandler(ddlCatUsuario_SelectedIndexChanged);
                    this.ddlCatUsuario.AutoPostBack = true;

                    if (this.idUsuario == 0)
                    {
                        this.lblAccion.Text = "Agregar";
                        DateTime tiempo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                        this.txtDate.Text = Convert.ToString(tiempo.ToShortDateString());
                        this.CalendarExtender.SelectedDate = tiempo;

                        lblEstado.Visible = false;
                        ddlCatUsuario.Visible = false;
                        RfvCatUsuario.Visible = false;
                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";
                        ddlPersona.Enabled = false;
                        this.txtUser.Text = this.baseEntity.strNombre;
                        this.txtPass1.Text = this.baseEntity.strPassword;
                        this.txtPass2.Text = this.baseEntity.strPassword;

                        DateTime time = (DateTime)this.baseEntity.dteFecha;
                        if (time != null)
                        {
                            txtDate.Text = time.Date.ToString("MM/dd/yyyy");
                        }

                        this.setItem(ref this.ddlCatUsuario, baseEntity.CatUsuario.strValor);
                        this.setItemEdith(ref this.ddlPersona, baseEntity.Persona.strNombre);
                    }
                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha Ocurrido un error en la aplicasion");
                this.Response.Redirect("~/PageError.aspx", false);
                this.showMessageException(_e.Message);

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

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsValid)
                {
                    return;
                }
                DataContext dcGuardar = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Usuario usuario = new Linq.Data.Entity.Usuario();
                if (this.idUsuario == 0)
                {
                    if (!UsuarioExistente(Convert.ToInt32(ddlPersona.Text)))
                    {
                        usuario.idPersona = int.Parse(this.ddlPersona.Text.Trim());
                    }
                    else
                    {
                        this.showMessage("Esta persona ya esta en uso");
                        this.lblMensaje.Text = "Persona en uso";
                    }

                    usuario.strPassword = Encrypt.GetSHA256(txtPass1.Text.Trim());

                    DateTime ingresoFecha = Convert.ToDateTime(txtDate.Text);
                    usuario.dteFecha = ingresoFecha;
                    usuario.idCatUsuario = 1;

                    var comprobar = dcGlobal.GetTable<Usuario>().Where(a => a.strNombre == txtUser.Text).FirstOrDefault();
                    if (comprobar != null)
                    {
                        this.showMessage("Usuario Repetido por favor ingrese un nuevo Usuario");
                        this.lblMensaje.Visible = true;
                        this.lblMensaje.Text = "Nombre de Usuario en uso";
                    }
                    else
                    {
                        usuario.strNombre = this.txtUser.Text.Trim();
                        String message = String.Empty;
                        if (!this.txtValidacion(usuario, ref message))
                        {
                            this.lblMensaje.Text = message;
                            this.lblMensaje.Visible = true;
                            return;
                        }
                        if (!this.validaSql(ref message))
                        {
                            this.lblMensaje.Text = message;
                            this.lblMensaje.Visible = true;
                            return;
                        }
                        if (!this.validaHTML(ref message))
                        {
                            this.lblMensaje.Text = message;
                            this.lblMensaje.Visible = true;
                            return;
                        }
                        dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Usuario>().InsertOnSubmit(usuario);
                        dcGuardar.SubmitChanges();
                        this.showMessage("El registro se agrego corrctamente");
                        this.Response.Redirect("~/UsuarioPrincipal.aspx", false);
                    }

                }
                if (this.idUsuario > 0)
                {

                    usuario = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Usuario>().First(u => u.id == idUsuario);
                    usuario.strPassword = Encrypt.GetSHA256(txtPass1.Text.Trim());
                    usuario.idCatUsuario = int.Parse(this.ddlCatUsuario.Text);
                    DateTime fechaIngreso = Convert.ToDateTime(txtDate.Text);
                    usuario.dteFecha = fechaIngreso;
                    //var coprove = dcGlobal.GetTable<Usuario>().Where(u => u.strNombre ==txtUser.Text).FirstOrDefault();
                    //if (coprove != null)
                    //{
                    //    this.showMessage("Usuario ya registrado");
                    //}
                    //else
                    //{
                        usuario.strNombre = this.txtUser.Text.Trim();
                        dcGuardar.SubmitChanges();
                        this.showMessage("El registro se edito correctamente");
                        this.Response.Redirect("~/UsuarioPrincipal.aspx", false);
                    //}
                }

            }
            catch (Exception _e)
            {
                this.showMessageException(_e.Message);

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
        public bool UsuarioExistente(int id)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DBPersona3.mssql.somee.com;Initial Catalog=DBPersona3;User ID=DanielPerez_SQLLogin_4;Password=1cablwmiwf"))
            {
                string query = "SELECT COUNT(*) FROM Usuario WHERE idPersona=@IdPersona";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdPersona", id);
                conn.Open();

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == 0)
                    return false;
                else
                    return true;
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
        private void UnirDropDownL()
        {
            SqlConnection con = new SqlConnection("Data Source=DBPersona3.mssql.somee.com;Initial Catalog=DBPersona3;User ID=DanielPerez_SQLLogin_4;Password=1cablwmiwf");
            SqlCommand cmd = new SqlCommand("select strNombre,id from Persona", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();


            sda.Fill(ds);



            ddlPersona.DataSource = ds;

            ddlPersona.DataTextField = "strNombre"; // FieldName of Table in DataBase
            ddlPersona.DataValueField = "id";

            //ddlPersona.DisplayMember = "strNombre";

            ddlPersona.DataBind();
            ddlPersona.Items.Insert(0, new ListItem("Seleccionar", String.Empty));

            //this.ddZona.Items.Insert(0, new ListItem("Elija una Opcion..", "0"));

        }
        protected void ddlCatUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idCatUsuario = int.Parse(this.ddlCatUsuario.Text);
                Expression<Func<CatUsuario, bool>> predicateCatU = c => c.id == idCatUsuario;
                predicateCatU.Compile();
                List<CatUsuario> lista = dcGlobal.GetTable<CatUsuario>().Where(predicateCatU).ToList();

                CatSexo catTemp = new CatSexo();
                this.ddlCatUsuario.DataTextField = "strValor";
                this.ddlCatUsuario.DataValueField = "id";
                this.ddlCatUsuario.DataSource = lista;
                this.ddlCatUsuario.DataBind();
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
                this.showMessageException(_e.Message);

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

        public void setItem(ref DropDownList _control, String _value)
        {
            foreach (ListItem item in _control.Items)
            {
                if (item.Value == _value)
                {
                    item.Selected = true;
                    break;
                }
            }
            _control.Items.FindByText(_value).Selected = true;
        }

        public void setItemEdith(ref DropDownList _control, String _value)
        {
            foreach (ListItem item in _control.Items)
            {
                if (item.Value != _value)
                {
                    item.Enabled = false;

                    break;
                }
            }
            _control.Items.FindByText(_value).Selected = true;
        }
        public bool txtValidacion(UTTT.Ejemplo.Linq.Data.Entity.Usuario _usuario, ref String _mensaje)
        {
            if (_usuario.idPersona != Convert.ToInt32(ddlPersona.Text))

            {
                _mensaje = "Persona repetida ";
                return false;
            }

            string APaterno = _usuario.strPassword.Trim();
            if (APaterno.Length < 3)
            {
                _mensaje = "Debe de tener mas de 3 caracteres en el campo contraseña";
                return false;
            }

            if (_usuario.strPassword.Equals(String.Empty))
            {
                _mensaje = "El campo contraseña esta vacio verifique porvafor";
                return false;
            }
            if (_usuario.strPassword.Length > 200)
            {
                _mensaje = "Rebasa el numero de caracteres en el campo contraseña";
                return false;
            }

            return true;
        }
        private bool validaSql(ref String _mensaje)
        {
            CtrValidaInyeccion valida = new CtrValidaInyeccion();

            string mensajeFuncion = string.Empty;

            if (valida.sqlInyectionValida(this.txtUser.Text.Trim(), ref mensajeFuncion, "Usuario", ref this.txtUser))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.sqlInyectionValida(this.txtPass1.Text.Trim(), ref mensajeFuncion, "Contraseña", ref this.txtPass1))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.sqlInyectionValida(this.txtPass2.Text.Trim(), ref mensajeFuncion, "Contraseña", ref this.txtPass2))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.sqlInyectionValida(this.txtDate.Text.Trim(), ref mensajeFuncion, "Fecha de Ingreso", ref this.txtDate))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            
            return true;
        }
        private bool validaHTML(ref String _mensaje)
        {
            CtrValidaInyeccion valida = new CtrValidaInyeccion();
            string mensajeFuncion = string.Empty;
            if (valida.htmlInyectionValida(this.txtUser.Text.Trim(), ref mensajeFuncion, "Usuario", ref this.txtUser))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInyectionValida(this.txtPass1.Text.Trim(), ref mensajeFuncion, "Contraseña", ref this.txtPass1))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInyectionValida(this.txtPass2.Text.Trim(), ref mensajeFuncion, "Contraseña", ref this.txtPass2))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInyectionValida(this.txtDate.Text.Trim(), ref mensajeFuncion, "Fecha de Ingreso", ref this.txtDate))
            {
                _mensaje = mensajeFuncion;
                return false;
            }

            return true;
        }

        protected void btnCerrarS_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("LoginUser.aspx");
        }
    }
}