<%@ Page Title="" Language="C#" MasterPageFile="~/StandardForm.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="FrameworkAnaliticaVisual.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        window.onload = function () {
            var chart = new CanvasJS.Chart("chartContainer", {
                theme: "light1", // "light2", "dark1", "dark2"
                animationEnabled: false, // change to true		
                title: {
                    text: "Basic Column Chart"
                },
                data: [
                    {
                        // Change type to "bar", "area", "spline", "pie",etc.
                        type: "bar",
                        dataPoints: [
                            { label: "Hola", y: 10 },
                            { label: "orange", y: 15 },
                            { label: "banana", y: 25 },
                            { label: "mango", y: 30 },
                            { label: "grape", y: 28 }
                        ]
                    }
                ]
            });
            chart.render();
        }
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div id="chartContainer" style="height: 370px; width: 100%;">
        <script src="https://canvasjs.com/assets/script/canvasjs.min.js"> </script>
    </div>

</asp:Content>
