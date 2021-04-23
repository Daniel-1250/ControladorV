<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsuarioManager.aspx.cs" Inherits="UTTT.Ejemplo.Persona.UsuarioManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta2/dist/css/bootstrap.min.css" rel="stylesheet"
        integrity="sha384-BmbxuPwQa2lc/FVzBcNJ7UAyJxM6wuqIj61tLrc4wSX0szH/Ev+nYRRuWlolflfl" crossorigin="anonymous">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" rel="stylesheet" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>

<body style="background-color:#EAFCF7">
    
    <nav class="navbar navbar-light" style="background-color: #e3f2fd;">
        <div class="container-fluid">
            <span class="navbar-brand mb-0 h1">USUARIO MANAGER</span>
        </div>
    </nav>
    <div class="container">
        <form id="form1" runat="server">
            <div class="align-content-end d-grid gap-2 d-md-flex justify-content-md-end">
                <asp:Label ID="lblUsuarioDetalle" runat="server" Text=""></asp:Label>
                <asp:Button ID="btnCerrarS" runat="server" Text="Cerrar sesion"   class="btn btn-outline-info" OnClick="btnCerrarS_Click"/>
            </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
            <div>
                <center>
                    <asp:Label ID="lblAccion" runat="server" Text="Accion" Font-Bold="True" Height="32px" Width="165px"
                        Font-Size="X-Large"></asp:Label>
                </center>
                <center>
                    <asp:Label ID="lblMensaje" runat="server" BorderColor="Red" Visible="False" BackColor="#669999"
                        Font-Size="X-Large"></asp:Label>
                    <br />
                    <br />
                </center>
            </div>
            <div class="mb-3">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="Label3" runat="server" Text="Nombre de la persona"></asp:Label>
                        <div class="mb-4">
                            <asp:DropDownList ID="ddlPersona" runat="server" CssClass="btn btn-info dropdown-toggle"
                                Width="250px" AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                            ControlToValidate="ddlPersona" ErrorMessage="Selecione persona" InitialValue="" ForeColor="#0000CC"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlPersona" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="mb-3">
                <asp:Label runat="server" Text="Nombre del usuario"></asp:Label>
                <asp:TextBox ID="txtUser" runat="server" CssClass="form-control" Width="250px" MinLength="3"
                    MaxLength="15"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ErrorMessage="El nombre de Usuario es Obligatorio" ControlToValidate="txtUser"
                    ForeColor="#0000CC"></asp:RequiredFieldValidator>
                <br />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                    ErrorMessage="Solo ingrese letras" ControlToValidate="txtUser"
                    ValidationExpression="^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$" ForeColor="#0000CC"></asp:RegularExpressionValidator>
            </div>

            <div class="mb-3">
                <asp:Label ID="Label1" runat="server" Text="Contraseña"></asp:Label>
                <asp:TextBox ID="txtPass1" runat="server" CssClass="form-control" MinLength="3" MaxLength="15"
                    Width="250px" type="password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="La contraseña es Obligatoria" ControlToValidate="txtPass1" ForeColor="#0000CC"></asp:RequiredFieldValidator>
            </div>
            <div class="mb-3">
                <asp:Label ID="Label5" runat="server" Text="Verifique su Contraseña"></asp:Label>
                <asp:TextBox ID="txtPass2" runat="server" CssClass="form-control" MinLength="3" MaxLength="15"
                    Width="250px" type="password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="La contraseña es Obligatoria" ControlToValidate="txtPass2" ForeColor="#0000CC"></asp:RequiredFieldValidator>
                <br />
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPass1"
                    ControlToValidate="txtPass2" ErrorMessage="Las contraseñas no Coinciden" ForeColor="#0000CC"></asp:CompareValidator>
            </div>
            <div class="mb-3">
                <asp:Label ID="Label2" runat="server" Text="Fecha de ingreso"></asp:Label>
                <asp:TextBox ID="txtDate" runat="server" CssClass="form-control col-sm-auto" Width="250px">
                </asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                    ErrorMessage="Fecha Obligatoria" ControlToValidate="txtDate" ForeColor="#0000CC"></asp:RequiredFieldValidator>
                <br />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server"
                    ErrorMessage="Formato Incorrecto" ControlToValidate="txtDate"
                    ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{2,4}$" ForeColor="#0000CC"></asp:RegularExpressionValidator>

                <ajaxToolkit:CalendarExtender ID="CalendarExtender" runat="server" Format="MM/dd/yyyy"
                    PopupPosition="BottomRight" BehaviorID="CalendarExtender" PopupButtonID="txtDate"
                    TargetControlID="txtDate" />
            </div>
            <div class="mb-3">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblEstado" runat="server" Text="Estado"></asp:Label>
                        <div class="mb-4">
                            <asp:DropDownList ID="ddlCatUsuario" CssClass="btn btn-info dropdown-toggle" Width="250px"
                                runat="server">
                            </asp:DropDownList>
                        </div>
                        <asp:RequiredFieldValidator ID="RfvCatUsuario" runat="server"
                            ErrorMessage="Campo Obligatorio" ControlToValidate="ddlCatUsuario"
                            ForeColor="#0000CC" InitialValue="-1"></asp:RequiredFieldValidator>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlCatUsuario" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="mb-3">
                <asp:Button ID="btnAceptar" OnClick="btnAceptar_Click" runat="server" Text="Guardar" type="button"
                    ViewStateMode="Disabled" CssClass="btn btn-outline-primary" BorderStyle="Double" Font-Italic="False" />
                <a href="UsuarioPrincipal.aspx" class="btn btn-outline-info">Cancelar</a>
            </div>
        
    </form>
    </div>
    
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta2/dist/js/bootstrap.bundle.min.js"
        integrity="sha384-b5kHyXgcpbZJO/tY9Ul7kGkf1S0CWuKcCD38l8YkeH8z8QjE0GmW1gYU5S9FOnJ0"
        crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.6.0/dist/umd/popper.min.js"
        integrity="sha384-KsvD1yqQ1/1+IA7gi3P0tyJcT3vR+NdBTt13hSJ2lnve8agRGXTTyNaBYmCR/Nwi"
        crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta2/dist/js/bootstrap.min.js"
        integrity="sha384-nsg8ua9HAw1y0W1btsyWgBklPnCUAFLuTMS2G72MMONqmOymq585AcH49TLBQObG"
        crossorigin="anonymous"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
</body>

</html>