<%@ Page Title="Vistas" Language="C#" MasterPageFile="~/StandardForm.Master" AutoEventWireup="true" CodeBehind="ExView.aspx.cs" Inherits="Prueba.About" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
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
                        { x: 20, y: 55 },
                        { x: 30, y: 50 },
                        { x: 40, y: 65 },
                        { x: 60, y: 68 },
                        { x: 70, y: 38 },
                        { x: 80, y: 71 },
                        { x: 90, y: 54 },
                        { x: 100, y: 60 },
                        { x: 110, y: 36 },
                        { x: 120, y: 49 }
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
            <asp:DropDownList ID="ddlGraficos" runat="server" AutoPostBack="True">
                <asp:ListItem>bar</asp:ListItem>
                <asp:ListItem>spline</asp:ListItem>
                <asp:ListItem>area</asp:ListItem>
                <asp:ListItem>pie</asp:ListItem>
            </asp:DropDownList>
            <br />
            <div id="chartContainer"></div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
