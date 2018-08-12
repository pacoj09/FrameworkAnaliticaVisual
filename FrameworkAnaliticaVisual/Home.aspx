<%@ Page Title="" Language="C#" MasterPageFile="~/StandardForm.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="FrameworkAnaliticaVisual.Home" MaintainScrollPositionOnPostback="true" %>

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

        function ValidarDirectorio() {
            if (document.getElementById("ContentPlaceHolder1_Wizard1_txtDirectorio").value === '') {
                alert("Debe de escoger un directorio para crear la clase");
                return false;
            }
            return true;
        }

        function directoryfileInfo() {
            document.getElementById('ContentPlaceHolder1_Wizard1_txtDirectorio').value = document.getElementById('ContentPlaceHolder1_Wizard1_fuDirectorio').value;
        }

        function enable_startbutton() {
            $("#ContentPlaceHolder1_Wizard1_StartNavigationTemplateContainerID_StartNextButton").prop("disabled", false);
        }

        function QuitarTabla(id) {
            ///Revisar funcion para chrome
            window.location.assign("Home.aspx?ddlid=" + id);
            return true;
        }

        function habilitarGrid(count) {
            count = parseInt(count);
            if (count === 1) {
                document.getElementById("Vista_1").style.display = "block";
                document.getElementById("Vista_2").style.display = "none";
                document.getElementById("Vista_3").style.display = "none";
                document.getElementById("Vista_4").style.display = "none";
            } else if (count === 2) {
                document.getElementById("Vista_1").style.display = "block";
                document.getElementById("Vista_2").style.display = "block";
                document.getElementById("Vista_3").style.display = "none";
                document.getElementById("Vista_4").style.display = "none";
            } else if (count === 3) {
                document.getElementById("Vista_1").style.display = "block";
                document.getElementById("Vista_2").style.display = "block";
                document.getElementById("Vista_3").style.display = "block";
                document.getElementById("Vista_4").style.display = "none";
            } else if (count === 4) {
                document.getElementById("Vista_1").style.display = "block";
                document.getElementById("Vista_2").style.display = "block";
                document.getElementById("Vista_3").style.display = "block";
                document.getElementById("Vista_4").style.display = "block";
            }
        }


    </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:Wizard ID="Wizard1" runat="server" ActiveStepIndex="0" DisplaySideBar="False" CssClass="laclase">
        <FinishNavigationTemplate>
            <asp:Button ID="FinishPreviousButton" CssClass="btn btn-default" runat="server" CausesValidation="False" CommandName="MovePrevious" Text="Anterior" />
            <asp:Button ID="FinishButton" CssClass="btn btn-default" runat="server" CommandName="MoveComplete" Text="Finalizar" OnClick="FinishButton_Click" OnClientClick="return ValidarDirectorio()" />
        </FinishNavigationTemplate>
        <StartNavigationTemplate>
            <br />
            <asp:Button ID="StartNextButton" CssClass="btn btn-default" runat="server" CommandName="MoveNext" Text="Siguiente" Enabled="false" OnClick="StartNextButton_Click" />
        </StartNavigationTemplate>
        <StepNavigationTemplate>
            <asp:Button ID="StepPreviousButton" CssClass="btn btn-default" runat="server" CausesValidation="False" CommandName="MovePrevious" Text="Anterior" />
            <asp:Button ID="StepNextButton" CssClass="btn btn-default" runat="server" CommandName="MoveNext" Text="Siguiente" OnClick="StepNextButton_Click" />
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
                        <asp:TextBox ID="txtServidor" runat="server" Style="width: 430px;"></asp:TextBox>
                        <br>
                        Nombre de la Base de Datos:<br>
                        <asp:TextBox ID="txtBaseDatos" runat="server" Style="width: 430px;"></asp:TextBox>
                        <br>
                        Nombre de Usario:<br>
                        <asp:TextBox ID="txtUser" runat="server" Style="width: 430px;"></asp:TextBox>
                        <br>
                        Contrase&ntilde;a:<br>
                        <asp:TextBox ID="txtPassword" runat="server" Style="width: 430px;"></asp:TextBox>
                        <br>
                        <br>
                        <asp:Button ID="btnProbarConexion" CssClass="btn btn-default" runat="server" Text="Probar Conexion" OnClick="btnProbarConexion_Click" OnClientClick="return ValidarFormulario()" />
                    </fieldset>
                    <br />
                    <hr />
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
                        <asp:DropDownList ID="ddlTablaPrincipal" runat="server"></asp:DropDownList>
                        <asp:Button ID="btnEstablecerTablaPrincipal" CssClass="btn btn-default" runat="server" Text="Establecer Tabla Principal" OnClick="btnEstablecerTablaPrincipal_Click" /><br />
                        <br />
                        <asp:Panel ID="pNuevaTabla" runat="server"></asp:Panel>
                        <br />
                        <asp:Button ID="btnAgregarTabla" CssClass="btn btn-default" runat="server" Text="Agregar Nueva Tabla" OnClick="btnAgregarTabla_Click" Enabled="false" />
                    </fieldset>
                    <br />
                    <hr />
                    <br />
                    <p>
                        NOTA: Debe de establecer una tabla principal antes de agregar tablas secundarias.<br />
                        Las tablas secundarias deben de estar entrelasadas con la principal por constraints.
                    </p>
                </div>
            </asp:WizardStep>

            <asp:WizardStep ID="WizardStep3" runat="server" Title="Asociación de las columnas" OnLoad="WizardStep3_Load">
                <div align="center">
                    <h1>Establecer Enlaces de CanvasJS</h1>
                    <br />
                    <fieldset>
                        <legend>Vistas:</legend>
                        Seleccione el numero de vistas:<br>
                        <asp:DropDownList ID="ddlNumeroVistas" runat="server" onChange="habilitarGrid(this.value);">
                            <asp:ListItem Selected="True" Value="1">1</asp:ListItem>
                            <asp:ListItem Value="2">2</asp:ListItem>
                            <asp:ListItem Value="3">3</asp:ListItem>
                            <asp:ListItem Value="4">4</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <br />
                        <div id="Vista_1" style="display: none">
                            <asp:Label ID="lblVista_1" runat="server" Text="Label">VISTA 1</asp:Label><br>
                            <asp:GridView ID="gvVista_1" runat="server" Enabled="true" OnRowDataBound="gvVista_1_RowDataBound" OnRowCreated="gvVista_1_RowCreated" OnSelectedIndexChanged="gvVista_1_SelectedIndexChanged">
                                <Columns>
                                    <asp:CommandField CausesValidation="False" InsertVisible="False" SelectText="Actualizar Columnas" ShowCancelButton="False" ShowSelectButton="True" />
                                    <asp:TemplateField HeaderText="TABLAS">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlVista_1_Tablas" runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="COLUMNAS">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlVista_1_Columnas" runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="VALORES_CANVAS">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlVista_1_CamposCanvas" runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <br />
                            <br />
                        </div>

                        <div id="Vista_2" style="display: none">
                            <asp:Label ID="lblVista_2" runat="server" Text="Label">VISTA 2</asp:Label><br>
                            <asp:GridView ID="gvVista_2" runat="server" Enabled="true" OnRowDataBound="gvVista_2_RowDataBound" OnRowCreated="gvVista_2_RowCreated" OnSelectedIndexChanged="gvVista_2_SelectedIndexChanged">
                                <Columns>
                                    <asp:CommandField CausesValidation="False" InsertVisible="False" SelectText="Actualizar Columnas" ShowCancelButton="False" ShowSelectButton="True" />
                                    <asp:TemplateField HeaderText="TABLAS">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlVista_2_Tablas" runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="COLUMNAS">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlVista_2_Columnas" runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="VALORES_CANVAS">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlVista_2_CamposCanvas" runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <br />
                            <br />
                        </div>

                        <div id="Vista_3" style="display: none">
                            <asp:Label ID="lblVista_3" runat="server" Text="Label">VISTA 3</asp:Label><br>
                            <asp:GridView ID="gvVista_3" runat="server" Enabled="true" OnRowDataBound="gvVista_3_RowDataBound" OnRowCreated="gvVista_3_RowCreated" OnSelectedIndexChanged="gvVista_3_SelectedIndexChanged">
                                <Columns>
                                    <asp:CommandField CausesValidation="False" InsertVisible="False" SelectText="Actualizar Columnas" ShowCancelButton="False" ShowSelectButton="True" />
                                    <asp:TemplateField HeaderText="TABLAS">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlVista_3_Tablas" runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="COLUMNAS">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlVista_3_Columnas" runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="VALORES_CANVAS">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlVista_3_CamposCanvas" runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <br />
                            <br />
                        </div>

                        <div id="Vista_4" style="display: none">
                            <asp:Label ID="lblVista_4" runat="server" Text="Label">VISTA 4</asp:Label><br>
                            <asp:GridView ID="gvVista_4" runat="server" Enabled="true" OnRowDataBound="gvVista_4_RowDataBound" OnRowCreated="gvVista_4_RowCreated" OnSelectedIndexChanged="gvVista_4_SelectedIndexChanged">
                                <Columns>
                                    <asp:CommandField CausesValidation="False" InsertVisible="False" SelectText="Actualizar Columnas" ShowCancelButton="False" ShowSelectButton="True" />
                                    <asp:TemplateField HeaderText="TABLAS">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlVista_4_Tablas" runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="COLUMNAS">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlVista_4_Columnas" runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="VALORES_CANVAS">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlVista_4_CamposCanvas" runat="server">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <br />
                            <br />
                        </div>
                    </fieldset>
                    <br />
                    <hr />
                    <br />
                    <p>
                        NOTA: Los enlaces no deben de repetirce por vista, si hay repetidos se tomara en cuenta el primer enlace unicamente. Debe exitir la "Posicion Y" en todas las vistas.
                    </p>
                </div>
                <hr />
                <div align="center">
                    <asp:FileUpload ID="fuDirectorio" runat="server" style="display:none" onchange="directoryfileInfo()" />
                    <asp:Button ID="btnEstablecerDirectorio"  CssClass="btn btn-default" runat="server" Text="Establecer Directorio" /><br /><br />
                    <asp:TextBox ID="txtDirectorio" runat="server" Style="width: 430px;"></asp:TextBox>
                    <br />
                    <hr />
                    <br />
                    <p>
                        NOTA: Debe de existir el archivo clsVista.cs, de no existir debera crearlo en el directorio seleccionado.
                    </p>
                </div>
            </asp:WizardStep>
        </WizardSteps>
    </asp:Wizard>
</asp:Content>
