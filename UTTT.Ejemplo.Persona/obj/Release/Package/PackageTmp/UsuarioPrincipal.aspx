<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsuarioPrincipal.aspx.cs" Inherits="UTTT.Ejemplo.Persona.UsuarioPrincipal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta2/dist/css/bootstrap.min.css" rel="stylesheet"
        integrity="sha384-BmbxuPwQa2lc/FVzBcNJ7UAyJxM6wuqIj61tLrc4wSX0szH/Ev+nYRRuWlolflfl" crossorigin="anonymous">
    <title>Usuario</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" rel="stylesheet" />
</head>

<body style="background-color:#EAFCF7">
    <div class="container">
        <nav class="navbar navbar-light" style="background-color: #e3f2fd;">
             <div class="container-fluid">
                      <span class="navbar-brand">Administracion de Usuarios</span>
                          <a class="nav-link active" aria-current="page" href="PersonaPrincipal.aspx">Ir a Personas</a>
                    </div>
         </nav>
        <br />
        <form id="form1" runat="server">
            <div class="row">
                <asp:ScriptManager ID="ScriptManager2" runat="server" />
            </div>
            
            <div class="align-content-end d-grid gap-2 d-md-flex justify-content-md-end">
                <asp:Label ID="lblUsuarioDetalle" runat="server" Text=""></asp:Label>
                <asp:Button ID="btnCerrarS" runat="server" Text="Cerrar sesion" class="btn btn-outline-info" OnClick="btnCerrarS_Click"/>
            </div>
            
            <div
                style="color: #000000; font-size: x-large; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-weight: bold">
                <center>
                    <asp:Label ID="LaTitleUsuario" runat="server" Text="Usuarios"></asp:Label>
                </center>
            </div>
            <br />

            <div class="row">

                <div class="form-group col-auto">
                    <div class="row">
                        <div class="form-group col-auto">
                            <asp:DropDownList class="btn btn-outline-primary dropdown-toggle" data-bs-toggle="dropdown" ID="ddCatEstado" Width="177px" Height="35px" runat="server"></asp:DropDownList><br />
                            <asp:Label class="col-form-label" ID="LaUsuario" runat="server" Text="Usuario"></asp:Label>
                        </div>
                        <asp:UpdatePanel ID="uPUsu" runat="server">
                            <ContentTemplate>
                                <input type="submit" name="btnTrick" id="btnTrick" style="display:none;" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                            <asp:TextBox ID="txtUser" runat="server" class="form-control" AutoPostBack="true" Width="250px" OnTextChanged="txtUser_TextChanged" ViewStateMode="Disabled"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group col-auto">
                    <div class="row">
                        <div class="form-group col-auto">
                            <asp:Button ID="btnBuscarUsuario" runat="server" Text="Buscar" type="button" ViewStateMode="Disabled"
                                class="btn btn-outline-primary" BorderStyle="Double" OnClick="btnBuscarUsuario_Click" />
                        </div>
                    </div>
                </div>

                <div class="form-group col-auto">
                    <div class="row">
                        <div class="form-group col-auto">
                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar Usuario" type="button"
                                OnClick="btnAgregar_Click" ViewStateMode="Disabled" class="btn btn-outline-primary"
                                BorderStyle="Double" />
                        </div>
                    </div>
                </div>
                <br />
            </div>
                <div
                    style="font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: x-large;">
                    <center>
                        <asp:Label ID="LabelDetalle" runat="server" Text="Detalle"></asp:Label>
                    </center>
                </div>
            <div class="table-responsive">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                    <asp:GridView ID="dgvUsuarios" runat="server" class="table table-info"
                    AllowPaging="True" AutoGenerateColumns="false" DataSourceID="DSUsuario" CellPadding="4"
                    GridLines="None" Width="930px" ViewStateMode="Disabled" OnRowCommand="dgvUsuarios_RowCommand">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>

                        <asp:BoundField DataField="strNombre" HeaderText="Nombre Usuario" ReadOnly="True"
                            SortExpression="strNombre" />

                        <asp:BoundField DataField="Persona" HeaderText="Persona" ReadOnly="True"
                            SortExpression="strNombre" />
                        <asp:BoundField DataField="CatUsuario" HeaderText="Estado" ReadOnly="True"
                            SortExpression="strValor" />

                        <asp:TemplateField HeaderText="Editar">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="imgEditar" CommandName="Editar"
                                    CommandArgument='<%#Bind("id") %>'
                                    ImageUrl="~/Images/icons8-editar-archivo-64.png" width="25"
                                                    height="25" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Eliminar" Visible="True">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="imgEliminar" CommandName="Eliminar"
                                    CommandArgument='<%#Bind("id") %>'
                                    ImageUrl="~/Images/icons8-eliminar-archivo-64.png" width="25"
                                                    height="25"
                                    OnClientClick="javascript:return confirm('¿Está seguro de querer eliminar el registro seleccionado?', 'Mensaje de sistema')" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                    </Columns>
                    </asp:GridView>
               
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnBuscarUsuario" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <asp:LinqDataSource ID="DSUsuario" runat="server" ContextTypeName="UTTT.Ejemplo.Linq.Data.Entity.DcGeneralDataContext" EntityTypeName="" Select="new (id, strNombre, dteFecha, Persona, CatUsuario)" TableName="Usuario" OnSelecting="DSUsuario_Selecting">
                        </asp:LinqDataSource>
                        
        </form>
        <script type="text/javascript">
            var nombre = document.getElementById("txtUser").value;
            document.querySelector('#txtUser').addEventListener('keyup', function () {
                const btnTrick = document.querySelector('#btnTrick');
                btnTrick.click();
            });
        </script>
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