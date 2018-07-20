<%@ Page Title="" Language="C#" MasterPageFile="~/StandardForm.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="FrameworkAnaliticaVisual.Home" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">

        function ValidarFormulario() {
            if (document.getElementById("ContentPlaceHolder1_Wizard1_txtServidor").value === '') {
                alert("Debe llenar todos los campos");
                return false;
            } else if (document.getElementById("ContentPlaceHolder1_Wizard1_txtBaseDatos").value === '') {
                alert("Debe llenar todos los campos");
                return false;
            } else if (document.getElementById("ContentPlaceHolder1_Wizard1_txtUser").value === '') {
                alert("Debe llenar todos los campos");
                return false;
            } else if (document.getElementById("ContentPlaceHolder1_Wizard1_txtPassword").value === '') {
                alert("Debe llenar todos los campos");
                return false;
            }
            return true;
        }

        function enable_startbutton() {
            $("#ContentPlaceHolder1_Wizard1_StartNavigationTemplateContainerID_StartNextButton").prop("disabled", false);
        }

        function enable_stepbutton() {
            $("#ContentPlaceHolder1_Wizard1_StepNavigationTemplateContainerID_StepNextButton").prop("disabled", false);
        }

        function enable_finishbutton() {
            $("#ContentPlaceHolder1_Wizard1_FinishNavigationTemplateContainerID_FinishButton").prop("disabled", false);
        }

        function QuitarTabla(id) {
            window.location("Home.aspx?ddlid=" + id);
        }


    </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:Wizard ID="Wizard1" runat="server" ActiveStepIndex="0" DisplaySideBar="False" CssClass="laclase">
        <FinishNavigationTemplate>
            <asp:Button ID="FinishPreviousButton" CssClass="btn btn-default" runat="server" CausesValidation="False" CommandName="MovePrevious" Text="Anterior" />
            <asp:Button ID="FinishButton" CssClass="btn btn-default" runat="server" CommandName="MoveComplete" Text="Finalizar" />
        </FinishNavigationTemplate>
        <StartNavigationTemplate>
            <br />
            <asp:Button ID="StartNextButton" CssClass="btn btn-default" runat="server" CommandName="MoveNext" Text="Siguiente" Enabled="false" OnClick="StartNextButton_Click" />
        </StartNavigationTemplate>
        <StepNavigationTemplate>
            <asp:Button ID="StepPreviousButton" CssClass="btn btn-default" runat="server" CausesValidation="False" CommandName="MovePrevious" Text="Anterior" />
            <asp:Button ID="StepNextButton" CssClass="btn btn-default" runat="server" CommandName="MoveNext" Text="Siguiente" Enabled="false" />
        </StepNavigationTemplate>
        <WizardSteps>
            <asp:WizardStep ID="WizardStep1" runat="server" Title="Conexión a la base de datos" OnLoad="WizardStep1_Load">
                <div align="center">
                    <h1>Conexi&oacute;n Base de Datos</h1>
                    <br />
                    <fieldset>
                        <legend>Datos del Servidor:</legend>
                        Selecciones el tipo de conexion que desea establecer:<br>
                        <asp:DropDownList ID="ddlConexion" runat="server">
                            <asp:ListItem Value="SQL Server">SQL Server</asp:ListItem>
                            <asp:ListItem Value="Oracle">Oracle</asp:ListItem>
                            <asp:ListItem Value="MySQL">MySQL</asp:ListItem>
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
                        <asp:Button ID="btnProbarConexion" CssClass="btn btn-default" runat="server" Text="Probar Conexion" OnClick="btnProbarConexion_Click" OnClientClick="return ValidarFormulario()" />
                    </fieldset>
                    <br />
                    <br />
                    <p>NOTA: Si tiene problemas de acceso pongase en contacto con nosotros</p>
                </div>
            </asp:WizardStep>

            <asp:WizardStep ID="WizardStep2" runat="server" Title="Selección de tablas" OnLoad="WizardStep2_Load">
                <div align="center">
                    <h1>Selecci&oacute;n de Tablas</h1>
                    <br />
                    <fieldset>
                        <legend>Tablas de la Base de Datos:</legend>
                        Seleccione la tabla principal:<br>
                        <asp:DropDownList ID="ddlTablas" runat="server" OnSelectedIndexChanged="ddlTablas_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        <asp:Button ID="btnEstablecerTablaPrincipal" CssClass="btn btn-default" runat="server" Text="Establecer Tabla Principal" OnClick="btnEstablecerTablaPrincipal_Click" /><br />
                        <br />
                        <asp:Panel ID="pNuevaTabla" runat="server"></asp:Panel>
                        <br />
                        <asp:Button ID="btnAgregarTabla" CssClass="btn btn-default" runat="server" Text="Agregar Nueva Tabla" OnClick="btnAgregarTabla_Click" />
                    </fieldset>
                    <br />
                    <br />
                    <p>
                        NOTA: Debe de establecer una tabla principal antes de agregar tablas secundarias.<br />
                        Las tablas secundarias deben de estar entrelasadas con la princiapl por constraints
                    </p>
                </div>
            </asp:WizardStep>

            <asp:WizardStep ID="WizardStep3" runat="server" Title="Asociación de las columnas" OnLoad="WizardStep3_Load">
                <div align="center">
                    <h1>Establecer Enlaces de CanvasJS</h1>
                    <br />
                    <fieldset>
                        <legend>Tablas:</legend>

                        <asp:Panel ID="pGridsTablas" runat="server"></asp:Panel>

                        Nombre Tabla:<br>
                        <asp:GridView ID="GridView1" runat="server" Enabled="true">
                            <Columns>
                                <asp:TemplateField HeaderText="Datos_Canvas">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlCamposCanvas" runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                    </fieldset>
                    <br />
                    <br />
                    <p>
                        NOTA: Los enlaces no deben de repetirce por tabla, si hay repetidos se tomara en cuenta el primer enlace unicamente.
                    </p>
                </div>

                <asp:Label ID="lblCamposTabla" runat="server" Text="Campos de la Tabla: "></asp:Label>
                <br />
                <asp:DropDownList ID="ddlEnlaceTabla" runat="server"></asp:DropDownList>
                <br />
                <asp:Label ID="lblCamposCanvas" runat="server" Text="Campos del Canvas: "></asp:Label>
                <br />
                <asp:DropDownList ID="ddlEnlaceCanvas" runat="server"></asp:DropDownList>
                <br />
                <asp:Button ID="btnAgregarEnlace" runat="server" Text="Agregar Elance" Enabled="false" />
                <asp:GridView ID="gvDatos" runat="server" Enabled="true" OnRowDeleting="gvDatos_RowDeleting" OnRowCreated="gvDatos_RowCreated">
                    <Columns>
                        <asp:CommandField ButtonType="Link" ShowDeleteButton="True" />
                        <asp:TemplateField HeaderText="Datos_Canvas">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlCamposCanvas" runat="server">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:Button ID="btnGenrarCodigo" CssClass="btn btn-default" runat="server" Text="Generar Codigo" OnClick="btnGenrarCodigo_Click" />
            </asp:WizardStep>
        </WizardSteps>
    </asp:Wizard>
</asp:Content>
