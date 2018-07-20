<%@ Page Title="Vistas" Language="C#" MasterPageFile="~/StandardForm.Master" AutoEventWireup="true" CodeBehind="ExView.aspx.cs" Inherits="FrameworkAnaliticaVisual.ExView" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <link rel="stylesheet" href="css/styles.css" />
    <script src="https://canvasjs.com/assets/script/canvasjs.min.js"> </script>
    <script type="text/javascript">

        var diagrama = "column";

        function cargarDiagrama() {
            var chart = new CanvasJS.Chart("chartContainer", {
                animationEnabled: true,
                theme: "light1", // "light1", "light2", "dark1", "dark2"
                title: {
                    text: "Ejemplo de Gráfico"
                },
                data: [{
                    type: diagrama, //change type to bar, line, area, pie, etc
                    //indexLabel: "{y}", //Shows y value on all Data Points
                    indexLabelFontColor: "#5A5757",
                    indexLabelPlacement: "outside",
                    dataPoints: [
                        { y: 1, label: "Uno" },
                        { y: 2, label: "Dos" },
                        { y: 3, label: "Tres" },
                        { y: 4, label: "Cuatro" },
                        { y: 5, label: "Cinco" },
                        { y: 6, label: "Seis" },
                        { y: 7, label: "Siete" },
                        { y: 8, label: "Ocho" },
                        { y: 9, label: "Nueve" },
                        { y: 10, label: "Diez" }
                    ]
                }]
            });
            chart.render();

        }

        function tipoDiagrama(_tipoDiagrama) {
            diagrama = _tipoDiagrama;
            cargarDiagrama();
        }

    </script>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Ejemplos de Vistas</h2>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" OnLoad="UpdatePanel1_Load">
        <ContentTemplate>
            <div class="laclase">
                <asp:Label ID="lblCamposTabla" runat="server" Text="Seleccione un tipo de gráfico:"></asp:Label>
                <asp:DropDownList ID="ddlGraficos" runat="server" AutoPostBack="True">
                    <asp:ListItem>bar</asp:ListItem>
                    <asp:ListItem>spline</asp:ListItem>
                    <asp:ListItem>area</asp:ListItem>
                    <asp:ListItem>pie</asp:ListItem>
                    <asp:ListItem>doughnut</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div id="chartContainer" style="height: 370px; width: 100%;"></div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
