<%@ Page  enableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="PersonaManager.aspx.cs"
    Inherits="UTTT.Ejemplo.Persona.PersonaManager" Debug="false" %>

    <!DOCTYPE html
        PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta2/dist/css/bootstrap.min.css" rel="stylesheet"
            integrity="sha384-BmbxuPwQa2lc/FVzBcNJ7UAyJxM6wuqIj61tLrc4wSX0szH/Ev+nYRRuWlolflfl" crossorigin="anonymous">
        <title>Persona Manager</title>
        <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" rel="stylesheet" />

    </head>
    <script type="text/javascript">
        function caracteres(valor) {
            if (valor.value.length < 3) {
                alert('Escribe más de 3 carácteres');
                return false;
            }
        }
        function VerificarCantidad(sender, args) {
            args.IsValid = (args.Value.length >= 3);
        }
        function comprueba() {
             window.history.back();
        }

    </script>

    <body style="background-color:#EAFCF7">
        
        <nav class="navbar navbar-light" style="background-color: #e3f2fd;">
            <div class="container-fluid">
                <span class="navbar-brand mb-0 h1">Agregar Persona</span>
            </div>
        </nav>
        <div class="container">
            <form id="form1" runat="server">
                <div class="align-content-end d-grid gap-2 d-md-flex justify-content-md-end">
                    <asp:Label ID="lblUsuarioDetalle" runat="server" Text=""></asp:Label>
                     <asp:Button ID="btnCerrarS" runat="server" Text="Cerrar sesion"   class="btn btn-outline-info" OnClick="btnCerrarS_Click"/>
                </div>
                 <asp:ScriptManager ID="ScriptManager2" runat="server" />
                <div style="font-family: Verdana; font-size: large; font-weight: bold;" align="center">
                    <asp:Label ID="lblAccion" runat="server" Text="Accion" Font-Bold="True"></asp:Label>
                </div>
                <br />
                <div>
                </div>

                <div class="form-group"
                    style="font-family: 'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif">
                    <asp:UpdatePanel ID="UpddlSexo" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                            <br />

                            <asp:Label class="control-label col-md-2" ID="LaSexo" runat="server" Text="Sexo:   "></asp:Label>

                            <div class="col-md-4">
                                <asp:DropDownList class="btn btn-info dropdown-toggle" data-bs-toggle="dropdown"
                                    aria-expanded="false" ID="ddlSexo" runat="server">
                                </asp:DropDownList>
                            </div>


                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlSexo" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

                <br />

                <div class="form-group"
                    style="font-family:'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif">
                    <asp:Label class="control-label col-md-2" ID="LaClaveU" runat="server" Text="Clave Unica:   "></asp:Label>

                    <div class="col-md-4">

                        <asp:TextBox class="form-control" ID="txtClaveUnica" runat="server" type="number" MaxLength="3"
                            ViewStateMode="Disabled"></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RFClaveU" runat="server" ErrorMessage="Campo requerido"
                            ControlToValidate="txtClaveUnica"></asp:RequiredFieldValidator>

                        <br />
                        <asp:CustomValidator ID="CVClaveU" runat="server" ClientValidationFunction="VerificarCantidad"
                            ErrorMessage="Minimo 3 caracteres" ControlToValidate="txtClaveUnica" ForeColor="#0000CC">
                        </asp:CustomValidator>

                    </div>
                </div>

                <!-- DIV QUE CONTIENTE A EL NOMBRE APELLIDO PATERNO Y APELLIDO MATERNO-->
                <div class="form-group"
                    style="font-family:'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif">

                    <!-- LABEL NOMBRE -->
                    <asp:Label class="control-label col-md-2" ID="LaNombre" runat="server" Text="Nombre:"></asp:Label>

                    <div class="col-md-4">
                        <asp:TextBox class="form-control" ID="txtNombre" runat="server" MaxLength="15" 
                            ViewStateMode="Disabled">
                        </asp:TextBox>

                        <asp:RequiredFieldValidator ID="RFNombre" runat="server" ErrorMessage="Campo requerido"
                            ControlToValidate="txtNombre"></asp:RequiredFieldValidator>

                        <asp:RegularExpressionValidator ID="RENombre" runat="server" ErrorMessage="Solo ingrese letras"
                            ValidationExpression="^[a-z A-ZÁÉÍÓÚáéíóúñÑ]{1,50}" ControlToValidate="txtNombre"
                            ForeColor="#000066"></asp:RegularExpressionValidator>

                        <br />

                        <asp:CustomValidator ID="CVNombre" runat="server" ClientValidationFunction="VerificarCantidad"
                            ErrorMessage="Minimo 3 caracteres" ControlToValidate="txtNombre" ForeColor="#0000CC">
                        </asp:CustomValidator>
                        <br />
                        <asp:Label ID="lblNombre" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                    </div>

                    <!-- LABEL DE APATERNO -->
                    <div class="form-group"
                    style="font-family:'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif">
                    <asp:Label class="control-label col-md-2" ID="LaApaterno" runat="server" Text="Apellido Paterno:  ">
                    </asp:Label>
                    <div class="col-md-4">
                        <asp:TextBox class="form-control" ID="txtAPaterno" runat="server" MaxLength="15"
                            ViewStateMode="Disabled">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFApaterno" runat="server" ErrorMessage="Campo requerido"
                            ControlToValidate="txtAPaterno">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="REApaterno" runat="server"
                            ErrorMessage="Solo ingrese letras" ControlToValidate="txtAPaterno"
                            ValidationExpression="^[a-z A-ZÁÉÍÓÚáéíóúñÑ]{1,50}" ForeColor="#000066">
                        </asp:RegularExpressionValidator>
                        <br />
                        <asp:CustomValidator ID="CVApaterno" runat="server" ClientValidationFunction="VerificarCantidad"
                            ErrorMessage="Minimo 3 caracteres" ControlToValidate="txtAPaterno" ForeColor="#0000CC">
                        </asp:CustomValidator>
                        <br />
                        <asp:Label ID="lblApaterno" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                    </div>

                    <!-- AMATERNO -->
                    <div class="form-group"
                    style="font-family:'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif">
                    <asp:Label class="control-label col-md-2" ID="LaAmaterno" runat="server" Text="Apellido Materno:  ">
                    </asp:Label>

                    <div class="col-md-4">
                        <asp:TextBox class="form-control" ID="txtAMaterno" runat="server" MaxLength="15"
                            ViewStateMode="Disabled">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFAmaterno" runat="server" ErrorMessage="Campo requerido"
                            ControlToValidate="txtAMaterno">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="REAmaterno" runat="server"
                            ErrorMessage="Solo ingrese letras" ControlToValidate="txtAMaterno"
                            ValidationExpression="^[a-z A-ZÁÉÍÓÚáéíóúñÑ]{1,50}" ForeColor="#000066">
                        </asp:RegularExpressionValidator>

                        <br />
                        <asp:CustomValidator ID="CVAmaterno" runat="server" ClientValidationFunction="VerificarCantidad"
                            ErrorMessage="Minimo 3 caracteres" ControlToValidate="txtAMaterno" ForeColor="#0000CC">
                        </asp:CustomValidator>
                        <br />
                        <asp:Label ID="lblAmaterno" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                    </div>

                <!-- FIN DEL DIV QUE CONTIENE NOMBRE APATERNO Y AMATERNO-->

                <div class="form-group"
                    style="font-family: 'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif">
                    <%--                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>--%>
                    <div class="col-md-4">
                        <asp:TextBox class="form-control" ID="txtClendario" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="dteCalendar" runat="server" Format="MM/dd/yyyy" PopupPosition="BottomRight" BehaviorID="CalendarExtender" PopupButtonID="btnCa" TargetControlID="txtClendario" />
                        <asp:ImageButton ID="btnCa" runat="server" Height="25px" ImageUrl="~/Images/42416.png" Width="25px" />
                        <br />
                        <asp:RegularExpressionValidator ID="ReFecha" runat="server" ControlToValidate="txtClendario" ErrorMessage="Formato de fecha incorrecto" ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{2,4}$"></asp:RegularExpressionValidator>
                        <asp:Label ID="lblAlertaFecha" runat="server" ForeColor="#CC0000" ></asp:Label>
                    </div>
                </div>

                
                <!-- DIV QUE CONTIENE CORREO ELECTRONICO RFC Y CODIGO POSTAL -->

                <div class="form-group"
                    style="font-family:'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif">
                    
                    <!-- CORREO ELECTRONICO -->
                    <asp:Label class="control-label col-md-2" ID="LaCorreoE" runat="server" Text="Correo Electronico:  ">
                    </asp:Label>

                    <div class="col-md-4">
                        <asp:TextBox class="form-control" ID="txtCorreoElectronico" runat="server"
                            ViewStateMode="Disabled">
                        </asp:TextBox>

                        <asp:RegularExpressionValidator ID="RECorreo" runat="server"
                            ControlToValidate="txtCorreoElectronico" ErrorMessage="Correo Electronico No valido"
                            ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                            ForeColor="#0000CC"></asp:RegularExpressionValidator>

                        <br />
                        <asp:RequiredFieldValidator ID="RFCorreo" runat="server"
                            ControlToValidate="txtCorreoElectronico" ErrorMessage="Email Requerido">
                        </asp:RequiredFieldValidator>
                    </div>
                    <br />
                  </div>
                    <!-- CODIGO POSTAL -->
                <div class="form-group"
                    style="font-family:'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif">
                    <asp:Label class="control-label col-md-2" ID="LaCodigoP" runat="server" Text="Codigo Postal:">
                    </asp:Label>

                    <div class="col-md-4">
                        <asp:TextBox class="form-control" ID="txtCodigoPostal" runat="server" ViewStateMode="Disabled">
                        </asp:TextBox>

                        <asp:RegularExpressionValidator ID="RECodigoPostal" runat="server"
                            ControlToValidate="txtCodigoPostal" ErrorMessage="Codigo Postal no valido"
                            ValidationExpression="^([1-9]{2}|[0-9][1-9]|[1-9][0-9])[0-9]{3}$" ForeColor="#0000CC">
                        </asp:RegularExpressionValidator>

                        <br />
                        <asp:RequiredFieldValidator ID="RFCodigoPostal" runat="server"
                            ControlToValidate="txtCodigoPostal" ErrorMessage="Codigo Postal Requerido">
                        </asp:RequiredFieldValidator>
                    </div>
                    <br />
                   </div>
                 <%-- RFC --%>
                <div class="form-group"
                    style="font-family:'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif">
                        <asp:Label class="control-label col-md-2" ID="LaRfc" runat="server" Text="RFC:"></asp:Label>

                        <div class="col-md-4">
                            <asp:TextBox class="form-control" ID="txtRfc" runat="server" ViewStateMode="Disabled">
                            </asp:TextBox>

                            <asp:RegularExpressionValidator ID="RERfc" runat="server" ControlToValidate="txtRfc"
                                ErrorMessage="RFC no valido"
                                ValidationExpression="^(([A-Z]|[a-z]|\s){1})(([A-Z]|[a-z]){3})([0-9]{6})((([A-Z]|[a-z]|[0-9]){3}))"
                                ForeColor="#0000CC"></asp:RegularExpressionValidator>

                            <br />

                            <asp:RequiredFieldValidator ID="RFRfc" runat="server" ControlToValidate="txtRfc"
                                ErrorMessage="RFC requerido"></asp:RequiredFieldValidator>
                        </div>
                </div>
                 <%-- ESTADO CIVIL --%>
                <div class="form-group"
                    style="font-family: 'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif">
                    <asp:UpdatePanel ID="UpddlEstadoCivil" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="LaEstadoCivil" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                            <br />
                            <div >
                                <asp:Label class="control-label col-md-2" ID="LaEstadoC" runat="server" Text="Estado Civl:"></asp:Label>
                                <div class="form-group col">
                                    <asp:DropDownList class="btn btn-info dropdown-toggle" data-bs-toggle="dropdown"
                                        aria-expanded="false" ID="ddlEstadoCivil" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlEstadoCivil" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

                <!-- FIN DEL DIV QUE CONTIENE EL RFC CORREO ELECTRONICO Y CP -->
                <br />

                <div>
                    <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click"
                        ViewStateMode="Disabled" CssClass="btn btn-outline-primary" />
                    &nbsp;&nbsp;&nbsp;                 
                    <a href="PersonaPrincipal.aspx" class="btn btn-outline-info">Cancelar</a>
                </div><br />
            </form>
        </div>
    </body>

    </html>