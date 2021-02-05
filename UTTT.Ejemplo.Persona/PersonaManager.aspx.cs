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
    public partial class PersonaManager : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Persona baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        private int tipoAccion = 0;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.Response.Buffer = true;
                this.session = (SessionManager)this.Session["SessionManager"];
                this.idPersona = this.session.Parametros["idPersona"] != null ?
                    int.Parse(this.session.Parametros["idPersona"].ToString()) : 0;
                if (this.idPersona == 0)
                {
                    this.baseEntity = new Linq.Data.Entity.Persona();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Persona>().First(c => c.id == this.idPersona);
                    this.tipoAccion = 2;
                }

                if (!this.IsPostBack)
                {
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }
                    List<CatSexo> lista = dcGlobal.GetTable<CatSexo>().ToList();
                    CatSexo catTemp = new CatSexo();
                    this.ddlSexo.DataTextField = "strValor";
                    this.ddlSexo.DataValueField = "id";
                    this.ddlSexo.DataSource = lista;
                    this.ddlSexo.DataBind();

                    this.ddlSexo.SelectedIndexChanged += new EventHandler(ddlSexo_SelectedIndexChanged);
                    this.ddlSexo.AutoPostBack = true;
                    if (this.idPersona == 0)
                    {
                        this.lblAccion.Text = "Agregar";
                        DateTime tiempo = new DateTime((DateTime.Now.Year)-20, DateTime.Now.Month, DateTime.Now.Day); 
                        this.dteCalendar.TodaysDate = tiempo; 
                        this.dteCalendar.SelectedDate = tiempo;
                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";
                        this.txtNombre.Text = this.baseEntity.strNombre;
                        this.txtAPaterno.Text = this.baseEntity.strAPaterno;
                        this.txtAMaterno.Text = this.baseEntity.strAMaterno;
                        this.txtClaveUnica.Text = this.baseEntity.strClaveUnica;
                        this.txtCorreoElectronico.Text = this.baseEntity.strCorreoElecrronico;
                        this.txtCodigoPostal.Text = this.baseEntity.strCodigoPostal;
                        this.txtRfc.Text = this.baseEntity.strRfc;
                        this.setItem(ref this.ddlSexo, baseEntity.CatSexo.strValor);
                        DateTime fechaNacimiento = (DateTime)this.baseEntity.dteFechaNacimiento;
                        if (fechaNacimiento !=null)
                        {
                            this.dteCalendar.TodaysDate = fechaNacimiento;
                            this.dteCalendar.SelectedDate = fechaNacimiento;
                        }
                    }                
                }

            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/PersonaPrincipal.aspx", false);
                // Qué ha sucedido
                var mensaje = "Error message: " + _e.Message;
                // Información sobre la excepción interna
                if (_e.InnerException != null)
                {
                    mensaje = mensaje + " Inner exception: " + _e.InnerException.Message;
                }
                // Dónde ha sucedido
                mensaje = mensaje + " Stack trace: " + _e.StackTrace;
                

                this.EnviarCorreo("18300997@uttt.edu.mx", "Exception", mensaje);
            }

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fechaNacimiento1 = this.dteCalendar.SelectedDate.Date;
                DateTime fechaHoy = DateTime.Today;
                int edad = fechaHoy.Year - fechaNacimiento1.Year;
                if (fechaHoy < fechaNacimiento1.AddYears(edad)) edad--;

                
                if (edad < 18)
                {
                    this.lblCalendario.Visible = true;
                    this.lblCalendario.Text = "Eres menor de edad no puedes completar el registro";
                }
                else
                {
                    this.lblCalendario.Visible = false;
                    if (!Page.IsValid)
                    {
                        return;
                    }
                    DataContext dcGuardar = new DcGeneralDataContext();
                    UTTT.Ejemplo.Linq.Data.Entity.Persona persona = new Linq.Data.Entity.Persona();
                    if (this.idPersona == 0)
                    {
                        persona.strClaveUnica = this.txtClaveUnica.Text.Trim();
                        persona.strNombre = this.txtNombre.Text.Trim();
                        persona.strAMaterno = this.txtAMaterno.Text.Trim();
                        persona.strAPaterno = this.txtAPaterno.Text.Trim();
                        persona.idCatSexo = int.Parse(this.ddlSexo.Text);
                        DateTime fechaNacimineto = this.dteCalendar.SelectedDate.Date;
                        persona.dteFechaNacimiento = fechaNacimineto;
                        persona.strCorreoElecrronico = this.txtCorreoElectronico.Text.Trim();
                        persona.strCodigoPostal = this.txtCodigoPostal.Text.Trim();
                        persona.strRfc = this.txtRfc.Text.Trim();


                        String mensaje = String.Empty;
                        String mensajeN = String.Empty;
                        String mensajeAP = String.Empty;
                        String mensajeAM = String.Empty;
                        if (!this.validacion(persona,ref mensaje, ref mensajeN, ref mensajeAP, ref mensajeAM))
                        {
                            this.lblMensaje.Text = mensaje;
                            this.lblMensaje.Visible = true;
                            this.lblNombre.Text = mensajeN;
                            this.lblApaterno.Text = mensajeAP;
                            this.lblAmaterno.Text = mensajeAM;
                            return;
                        }

                        if (!this.validaSql(ref mensaje))
                        {
                            this.lblMensaje.Text = mensaje;
                            this.lblMensaje.Visible = true;
                            return;
                        }
                        if (!this.validaHTML(ref mensaje))
                        {
                            this.lblMensaje.Text = mensaje;
                            this.lblMensaje.Visible = true;
                            return;
                        }

                        dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Persona>().InsertOnSubmit(persona);
                        dcGuardar.SubmitChanges();
                        this.showMessage("El registro se agrego correctamente.");
                        this.Response.Redirect("~/PersonaPrincipal.aspx", false);

                    }
                    if (this.idPersona > 0)
                    {
                        
                        persona = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Persona>().First(c => c.id == idPersona);
                        persona.strClaveUnica = this.txtClaveUnica.Text.Trim();
                        persona.strNombre = this.txtNombre.Text.Trim();
                        persona.strAMaterno = this.txtAMaterno.Text.Trim();
                        persona.strAPaterno = this.txtAPaterno.Text.Trim();
                        persona.idCatSexo = int.Parse(this.ddlSexo.Text);
                        DateTime fechaNacimineto = this.dteCalendar.SelectedDate.Date;
                        persona.dteFechaNacimiento = fechaNacimineto;
                        persona.strCorreoElecrronico = this.txtCorreoElectronico.Text.Trim();
                        persona.strCodigoPostal = this.txtCodigoPostal.Text.Trim();
                        persona.strRfc = this.txtRfc.Text.Trim();

                        String mensaje = String.Empty;
                        String mensajeN = String.Empty;
                        String mensajeAP = String.Empty;
                        String mensajeAM = String.Empty;
                        if (!this.validacion(persona, ref mensaje,ref mensajeN,ref mensajeAP,ref mensajeAM))
                        {
                            this.lblMensaje.Text = mensaje;
                            this.lblNombre.Text = mensajeN;
                            this.lblApaterno.Text = mensajeAP;
                            this.lblAmaterno.Text = mensajeAM;
                            this.lblMensaje.Visible = true;
                            return;
                        }

                        if (!this.validaSql(ref mensaje))
                        {
                            this.lblMensaje.Text = mensaje;
                            this.lblMensaje.Visible = true;
                            return;
                        }
                        if (!this.validaHTML(ref mensaje))
                        {
                            this.lblMensaje.Text = mensaje;
                            this.lblMensaje.Visible = true;
                            return;
                        }


                        dcGuardar.SubmitChanges();
                        this.showMessage("El registro se edito correctamente.");
                        this.Response.Redirect("~/PersonaPrincipal.aspx", false);
                    }
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
                this.Response.Redirect("~/PageError.aspx",false);

                this.EnviarCorreo("18300997@uttt.edu.mx", "Exception", mensaje);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {              
                this.Response.Redirect("~/PersonaPrincipal.aspx", false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        protected void ddlSexo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idSexo = int.Parse(this.ddlSexo.Text);
                Expression<Func<CatSexo, bool>> predicateSexo = c => c.id == idSexo;
                predicateSexo.Compile();
                List<CatSexo> lista = dcGlobal.GetTable<CatSexo>().Where(predicateSexo).ToList();
                CatSexo catTemp = new CatSexo();            
                this.ddlSexo.DataTextField = "strValor";
                this.ddlSexo.DataValueField = "id";
                this.ddlSexo.DataSource = lista;
                this.ddlSexo.DataBind();
            }
            catch (Exception)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        #endregion

        #region Metodos

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

        #endregion

        #region Metodos
        /// <summary>
        /// Valida datos basicos
        /// </summary>
        /// <param name="_persona"></param>
        /// <param name="_mensaje"></param>
        /// <param name="_mensajeN"></param>
        /// <param name="_mensajeAP"></param>
        /// <param name="_mensajeAM"></param>
        /// <returns></returns>

        public bool validacion(UTTT.Ejemplo.Linq.Data.Entity.Persona _persona, ref String _mensaje,ref String _mensajeN
                                                                             , ref String _mensajeAP, ref String _mensajeAM)
        {

            // Sexo
            if (_persona.idCatSexo == -1)
            {
                _mensaje = "Seleccione un sexo";
                return false;
            }

            //Clave Unica Verifica si solo se utilizan numeros
            int i = 0;
            if (int.TryParse(_persona.strClaveUnica, out i) == false)
            {
                _mensaje = "La clave unica no es un numero";
                return false;
            }
            if (int.Parse(_persona.strClaveUnica) < 100 || int.Parse(_persona.strClaveUnica) > 999)
            {
                _mensaje = "La clave unica Esta fuera de rango";
                return false;
            }

            // Valida nombre
            if (_persona.strNombre.Equals(String.Empty))
            {
                _mensaje = "Nombre vacio";
                return false;
            }
            if (_persona.strNombre.Length > 50)
            {
                _mensaje = "Los caracteres permitidos para nombre rebasan lo establecido de 50";
                return false;
            }

            string nombre = _persona.strNombre.Trim();

            if (nombre.Length < 3 )
            {
                _mensajeN = "Escriba mas de 3 digitos en Nombre";
                return false;
            }

            // Valida APaterno
            if (_persona.strAPaterno.Equals(String.Empty))
            {
                _mensaje = "Apellido paterno vacio";
                return false;
            }
            if (_persona.strAPaterno.Length > 50)
            {
                _mensaje = "Los caracteres permitidos para nombre rebasan lo establecido de 50";
                return false;
            }

            string apaterno = _persona.strAPaterno.Trim();

            if (apaterno.Length < 3)
            {
                _mensajeAP = "Escriba mas de 3 digitos en Apellido paterno";
                return false;
            }

            //Valida Amaterno
            if (_persona.strAMaterno.Equals(String.Empty))
            {
                _mensaje = "Apellido materno vacio";
                return false;
            }
            if (_persona.strAMaterno.Length > 50)
            {
                _mensaje = "Los caracteres permitidos para nombre rebasan lo establecido de 50";
                return false;
            }

            string amaterno = _persona.strAMaterno.Trim();
            if (amaterno.Length < 3)
            {
                _mensajeAM = "Escriba mas de 3 digitos en apellido Materno";
                return false;
            }

            return true;

        }

        private bool validaSql(ref String _mensaje)
        {
            CtrValidaInyeccion valida = new CtrValidaInyeccion();

            string mensajeFuncion = string.Empty;

            if (valida.sqlInyectionValida(this.txtClaveUnica.Text.Trim(), ref mensajeFuncion, "Clave Unica", ref this.txtClaveUnica))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.sqlInyectionValida(this.txtNombre.Text.Trim(), ref mensajeFuncion, "Nombre", ref this.txtNombre))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.sqlInyectionValida(this.txtAPaterno.Text.Trim(), ref mensajeFuncion, "A Paterno", ref this.txtAPaterno))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.sqlInyectionValida(this.txtAMaterno.Text.Trim(), ref mensajeFuncion, "A Materno", ref this.txtAMaterno))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.sqlInyectionValida(this.txtCorreoElectronico.Text.Trim(), ref mensajeFuncion, "Correo Electronico", ref this.txtCorreoElectronico))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.sqlInyectionValida(this.txtCodigoPostal.Text.Trim(), ref mensajeFuncion, "Codigo Postal", ref this.txtCodigoPostal))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.sqlInyectionValida(this.txtRfc.Text.Trim(), ref mensajeFuncion, "RFC", ref this.txtRfc))
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
            if (valida.htmlInyectionValida(this.txtNombre.Text.Trim(), ref mensajeFuncion, "Nombre", ref this.txtNombre))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInyectionValida(this.txtAPaterno.Text.Trim(), ref mensajeFuncion, "A paterno",ref this.txtAPaterno))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInyectionValida(this.txtAMaterno.Text.Trim(), ref mensajeFuncion, "A Materno", ref this.txtAMaterno))
            {
                _mensaje = mensajeFuncion;
                return false;
            }

            return true;
        }

        public void EnviarCorreo(string correoDestino, string asunto, string mensajeCorreo)
        {
            string mensaje = "Error al enviar correo.";

            try
            {
                SmtpMail objetoCorreo = new SmtpMail("TryIt");

                objetoCorreo.From = "TUCORREO";
                objetoCorreo.To = correoDestino;
                objetoCorreo.Subject = asunto;
                objetoCorreo.TextBody = mensajeCorreo;

                SmtpServer objetoServidor = new SmtpServer("smtp.gmail.com");//servidor proporcionado desde la configuracion de google

                objetoServidor.User = "TUCORREO";
                objetoServidor.Password = "TUCONTRASEÑA";
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

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {

        }
    }
}