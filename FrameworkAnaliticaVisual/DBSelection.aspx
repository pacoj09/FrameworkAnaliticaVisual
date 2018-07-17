<%@ Page Title="" Language="C#" MasterPageFile="~/StandardForm.Master" AutoEventWireup="true" CodeBehind="DBSelection.aspx.cs" Inherits="FrameworkAnaliticaVisual.DBSelection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        function ValidarFormulario() {
            if (document.getElementById("ContentPlaceHolderSite_txtServidor").value === '') {
                alert("Debe llenar todos los campos");
                return false;
            } else if (document.getElementById("ContentPlaceHolderSite_txtBaseDatos").value === '') {
                alert("Debe llenar todos los campos");
                return false;
            } else if (document.getElementById("ContentPlaceHolderSite_txtUser").value === '') {
                alert("Debe llenar todos los campos");
                return false;
            } else if (document.getElementById("ContentPlaceHolderSite_txtPassword").value === '') {
                alert("Debe llenar todos los campos");
                return false;
            }
            return true;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align="center">
        <h1>Conexion Base de Datos</h1>
        <br />
        <fieldset>
            <legend>Datos del Servidor:</legend>
            Selecciones el tipo de conexion que desea establecer:<br>
            \
            <asp:DropDownList ID="ddlConexion" runat="server">
                <asp:ListItem Value="1">SQL Server</asp:ListItem>
                <asp:ListItem Value="2">Oracle</asp:ListItem>
                <asp:ListItem Value="3">MySQL</asp:ListItem>
            </asp:DropDownList>
            <br>
            Nombre del Servidor:<br>
            <asp:TextBox ID="txtServidor" runat="server"></asp:TextBox>
            <br>
            Nombre de la Base de Datos:<br>
            <asp:TextBox ID="txtBaseDatos" runat="server"></asp:TextBox>
            <br>
            Nombre de Usario:<br>
            <asp:TextBox ID="txtUser" runat="server"></asp:TextBox>
            <br>
            Contrase&ntilde;a:<br>
            <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
            <br>
            <br>
            <asp:Button ID="btnProbarConexion" runat="server" Text="Probar Conexion" OnClick="btnProbarConexion_Click" OnClientClick="return ValidarFormulario()" />
        </fieldset>
        <br />
        <br />
        <p>NOTA: Para poder obtener acceso a la plataforma pongase en contacto con nosostros</p>
    </div>
</asp:Content>
