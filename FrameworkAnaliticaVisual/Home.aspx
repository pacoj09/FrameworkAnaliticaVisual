<%@ Page Title="" Language="C#" MasterPageFile="~/StandardForm.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="FrameworkAnaliticaVisual.Home" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <link rel="stylesheet" href="css/styles.css" />
    <script src="https://canvasjs.com/assets/script/canvasjs.min.js"> </script>
    <script type="text/javascript">

        var diagrama = "column";

        function cargarDiagrama() {
            CanvasJS.addColorSet("customColorSet1",
                [//colorSet Array
                    "#000000",
                    "#F11112",
                    "#1BCDD1",
                    "#8FAABB",
                    "#B08BEB",
                    "#3EA0DD",
                    "#F5A52A",
                    "#23BFAA",
                    "#FAA586",
                    "#EB8CC6",
                ]);
            var chart = new CanvasJS.Chart("chartContainer", {
                animationEnabled: true,
                colorSet: "customColorSet1",
                theme: "light1", // "light1", "light2", "dark1", "dark2"
                title: {
                    text: "Simple Column Chart with Index Labels"
                },
                data: [{
                    type: diagrama, //change type to bar, line, area, pie, etc
                    //indexLabel: "{y}", //Shows y value on all Data Points
                    indexLabelFontColor: "#5A5757",
                    indexLabelPlacement: "outside",
                    dataPoints: [
                        { label: "Pablo", x: 10, y: 10, indexLabel: "label1" },
                        { x: 20, y: 55 },
                        { x: 30, y: 50 },
                        { x: 40, y: 65 },
                        { x: 50, y: 92, indexLabel: "Highest" },
                        { x: 60, y: 68 },
                        { x: 70, y: 38 },
                        { x: 80, y: 71 },
                        { x: 90, y: 54 },
                        { x: 100, y: 60 },
                        { x: 110, y: 36 },
                        { x: 120, y: 49 },
                        { x: 130, y: 21, indexLabel: "Lowest" }
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


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="DoubleColumn" align="center">
        <div align="center" class="Content">

            <asp:DropDownList ID="ddlTablas" runat="server" OnSelectedIndexChanged="ddlTablas_SelectedIndexChanged">
            </asp:DropDownList>
            <hr />
            <asp:DropDownList ID="ddlColumnas" runat="server">
            </asp:DropDownList>
            <br />
            <asp:Button ID="btnAgregarColumnas" runat="server" Text="Agregar Columnas" OnClick="btnAgregarColumnas_Click" />
        </div>
        <div align="center" class="Content">

            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" OnLoad="upCanvas_Load">
                <ContentTemplate>
                    <asp:DropDownList ID="ddlGraficos" runat="server" AutoPostBack="True">
                        <asp:ListItem>bar</asp:ListItem>
                        <asp:ListItem>spline</asp:ListItem>
                        <asp:ListItem>area</asp:ListItem>
                        <asp:ListItem>pie</asp:ListItem>
                    </asp:DropDownList>
                    <br />

                    <div id="chartContainer" class="Content" style="height: 370px; width: 100%;"></div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>

    <br />
    <br />

    <div align="center">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always" OnLoad="upCanvas_Load">
            <ContentTemplate>
                <div class="DoubleColumn" align="center">
                    <div align="center" class="Content">
                        <h3>Campos de la Tabla</h3>
                        <asp:DropDownList ID="ddlEnlaceTabla" runat="server"></asp:DropDownList>
                    </div>
                    <div align="center" class="Content">
                        <h3>Campos del Canvas</h3>
                        <asp:DropDownList ID="ddlEnlaceCanvas" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <br />
                <asp:Button ID="btnAgregarEnlace" runat="server" Text="Agregar Elance" OnClick="btnAgregarEnlace_Click" Enabled="false" />
                <br />
                <hr />
                <asp:GridView ID="gvDatos" runat="server" Enabled="false" BorderStyle="Solid" BorderColor="Black"></asp:GridView>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>
