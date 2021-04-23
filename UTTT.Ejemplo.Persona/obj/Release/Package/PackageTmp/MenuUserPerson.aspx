<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuUserPerson.aspx.cs" Inherits="UTTT.Ejemplo.Persona.MenuUserPerson" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Menu</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-BmbxuPwQa2lc/FVzBcNJ7UAyJxM6wuqIj61tLrc4wSX0szH/Ev+nYRRuWlolflfl" crossorigin="anonymous">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" rel="stylesheet" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
</head>
<body style="background-color:#EAFCF7">
    <div class="container">
    <nav class="navbar navbar-light" style="background-color: #e3f2fd;">
        <div class="container-fluid">
            <span class="navbar-brand mb-0 h1">Menu Principal</span>
        </div>
    </nav>
    <form id="form1" runat="server">
        <div class="align-content-end d-grid gap-2 d-md-flex justify-content-md-end">
            <asp:Label ID="lblUsuarioDetalle" runat="server" Text=""></asp:Label>
            <asp:Button ID="btnCerrarS" runat="server" Text="Cerrar sesion"   class="btn btn-outline-info" OnClick="btnCerrarS_Click"/>
        </div>
        <br />
        <br />
        <br />
        <div class="container">
            <asp:label ID="lblPersona" runat="server" Text="Detalle Persona"></asp:label><br />
            <asp:ImageButton ID="ibtnPersona" ImageUrl="~/Images/buyer-persona-termino.jpg" width="300px" height="200px" AlternateText="No Image available" OnClick="btnDetallePersona_Click" runat="server" /><br />
            <asp:label ID="lblUsuario" runat="server" Text="Detalle Usuario"></asp:label><br />
            <asp:ImageButton ID="ibtnUsuario" ImageUrl="~/Images/buyer-persona.png" width="300px" height="200px" AlternateText="No Image available" OnClick="btnDetalleUsuario_Click" runat="server" />
        </div>
    </form>
  </div>
</body>
</html>
