<%@ Page enableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="PersonaPrincipal.aspx.cs" Inherits="UTTT.Ejemplo.Persona.PersonaPrincipal" Debug="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-BmbxuPwQa2lc/FVzBcNJ7UAyJxM6wuqIj61tLrc4wSX0szH/Ev+nYRRuWlolflfl" crossorigin="anonymous">
    <title>Persona Principal</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" rel="stylesheet" />
</head>
    
<script src="Scripts/jquery-3.5.1.min.js"></script>

<body style="background-color:#EAFCF7">
    
    <div class="container well">
        <nav class="navbar navbar-light" style="background-color: #e3f2fd;">
             <div class="container-fluid">
                      <span class="navbar-brand">Administracion de Personas</span>
                          <a class="nav-link active" aria-current="page" href="UsuarioPrincipal.aspx">Ir a Usuarios</a>
                    </div>
         </nav>
    <form id="form1" runat="server">
        <div class="align-content-end d-grid gap-2 d-md-flex justify-content-md-end">
            <asp:Label ID="lblUsuarioDetalle" runat="server" Text=""></asp:Label>
            <asp:Button ID="btnCerrarS" runat="server" Text="Cerrar sesion" class="btn btn-outline-info" OnClick="btnCerrarS_Click"/>
        </div>
        <asp:ScriptManager ID="ScriptManager2" runat="server" />
        <div style="color: #000000; font-size: x-large; font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-weight: bold">
            <center>
                <asp:Label ID="LaPersona" runat="server" Text="Persona"></asp:Label></center>
        </div>
        <br />
        <div class="row">
            <%---------------------------------------------------------TXT BUSCAR -----------------------------------------------------------------%>
            <div class="form-group col-auto">
                <div class="row">
                    <div class="form-group col-auto">
                        <asp:Label class="col-form-label" ID="lLaNombre" runat="server" Text="Nombre:"></asp:Label>
                    </div>
                    <div class="form-group col-auto">
                        <asp:UpdatePanel ID="PtxtName" runat="server">
                            <ContentTemplate>
                                <input type="submit" name="btnTrick" id="btnTrick" style="display:none;"/>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:TextBox class="form-control" ID="txtNombre" runat="server" Width="177px" Height="35px" AutoPostBack="true" ViewStateMode="Disabled" OnTextChanged="txtNombre_TextChanged"></asp:TextBox>
                    </div>
                </div>
            </div>
            <%-- ------------------------------------------------------TXT SEXO------------------------------------------------------------------- --%>
            <div class="form-group col-auto">
                <div class="row">
                    <div class="form-group col-auto">
                        <asp:Label ID="LaSexo" runat="server" Text="Sexo:  "></asp:Label>
                    </div>
                    <div class="form-group col-auto">
                        <asp:DropDownList class="btn btn-outline-primary dropdown-toggle" data-bs-toggle="dropdown" ID="ddlSexo" Width="177px" Height="35px" runat="server"></asp:DropDownList>
                    </div>
                </div>
            </div>

            <%-- ------------------------------------------------------TXT ESTADOCIVIl------------------------------------------------------------------- --%>
            <div class="form-group col-auto">
                <div class="row">
                    <div class="form-group col-auto">
                        <asp:Label ID="LaCatEsC" runat="server" Text="Estado Civil:"></asp:Label>
                    </div>
                    <div class="form-group col-auto">
                        <asp:DropDownList class="btn btn-outline-primary dropdown-toggle" data-bs-toggle="dropdown" ID="ddlEstadoCivil" Width="177px" Height="35px" runat="server"></asp:DropDownList>
                    </div>
                </div>
            </div>

            <%-- ------------------------------------------------------BOTONES------------------------------------------------------------------- --%>
            <div class="form-group col-auto">
                <div class="row">
                    <div class="form-group col-auto">
                        <asp:Button ID="btnBuscar" CssClass="btn btn-outline-primary" runat="server" Text="Buscar" OnClick="btnBuscar_Click" ViewStateMode="Disabled" />
                    </div>
                    <div class="form-group col-auto">
                        <asp:Button ID="btnAgregar" CssClass="btn btn-outline-primary" runat="server" Text="Agregar" OnClick="btnAgregar_Click" ViewStateMode="Disabled" />
                    </div>
                </div>
            </div>

        </div>
        <%-- ----------------------------------------------DETALLE ------------------------------------------------------------------------%>
        <div style="font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif; font-size: x-large;">
            <center>
                <asp:Label ID="LabelDetalle" runat="server" Text="Detalle"></asp:Label></center>
        </div>

        <div>
        </div>

        <div class="table-responsive ">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <center>
                        <asp:GridView ID="dgvPersonas" runat="server" class="table table-info"
                            AllowPaging="True" AutoGenerateColumns="False" DataSourceID="DataSourcePersona"
                            Width="1067px" CellPadding="3" GridLines="Horizontal"
                            OnRowCommand="dgvPersonas_RowCommand" BackColor="White"
                            ViewStateMode="Disabled">
                            <AlternatingRowStyle />
                            <Columns>
                                <asp:BoundField DataField="strClaveUnica" HeaderText="Clave Unica"
                                    ReadOnly="True" SortExpression="strClaveUnica" />
                                <asp:BoundField DataField="strNombre" HeaderText="Nombre" ReadOnly="True"
                                    SortExpression="strNombre" />
                                <asp:BoundField DataField="strAPaterno" HeaderText="APaterno" ReadOnly="True"
                                    SortExpression="strAPaterno" />
                                <asp:BoundField DataField="strAMaterno" HeaderText="AMaterno" ReadOnly="True"
                                    SortExpression="strAMaterno" />
                                <asp:BoundField DataField="CatSexo" HeaderText="Sexo"
                                    SortExpression="CatSexo" />
                                <asp:TemplateField HeaderText="Editar">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="imgEditar" CommandName="Editar" CommandArgument='<%#Bind("id") %>' ImageUrl="~/Images/icons8-editar-archivo-64.png" width="25" height="25" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Eliminar" Visible="True">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="imgEliminar" CommandName="Eliminar" CommandArgument='<%#Bind("id") %>' ImageUrl="~/Images/icons8-eliminar-archivo-64.png" width="25" height="25" OnClientClick="javascript:return confirm('¿Está seguro de querer eliminar el registro seleccionado?', 'Mensaje de sistema')" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Direccion">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="imgDireccion" CommandName="Direccion" CommandArgument='<%#Bind("id") %>' ImageUrl="~/Images/icons8-dirección-64.png" width="25" height="25" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />

                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </center>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>

        </div>
        <asp:LinqDataSource ID="DataSourcePersona" runat="server"
            ContextTypeName="UTTT.Ejemplo.Linq.Data.Entity.DcGeneralDataContext"
            OnSelecting="DataSourcePersona_Selecting"
            Select="new (strNombre, strAPaterno, strAMaterno, CatSexo, strClaveUnica,id)"
            TableName="Persona" EntityTypeName="">
        </asp:LinqDataSource>
    </form>
        <script type="text/javascript">
            var nombre = document.getElementById("txtNombre").value;
            document.querySelector('#txtNombre').addEventListener('keyup', function () {
                const btnTrick = document.querySelector('#btnTrick');
                btnTrick.click();
            });
        </script>
        </div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta2/dist/js/bootstrap.bundle.min.js" integrity="sha384-b5kHyXgcpbZJO/tY9Ul7kGkf1S0CWuKcCD38l8YkeH8z8QjE0GmW1gYU5S9FOnJ0" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.6.0/dist/umd/popper.min.js" integrity="sha384-KsvD1yqQ1/1+IA7gi3P0tyJcT3vR+NdBTt13hSJ2lnve8agRGXTTyNaBYmCR/Nwi" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta2/dist/js/bootstrap.min.js" integrity="sha384-nsg8ua9HAw1y0W1btsyWgBklPnCUAFLuTMS2G72MMONqmOymq585AcH49TLBQObG" crossorigin="anonymous"></script>
</body>
</html>
