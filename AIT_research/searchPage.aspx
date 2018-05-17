<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="searchPage.aspx.cs" Inherits="AIT_research.searchPage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid searchDiv">
        <input type="button" id="showAll" class="longButton blue" value="show all respondants"/>
    <div id="showAllDiv" class="foldOutContainer" style="display:none">
        hello
    </div>
        <input type="button" id="searchFor" class="longButton yellow" value="search for respondants"/>
    <div id="searchForDiv" class="foldOutContainer" style="display:none">
        hello
    </div>
    </div>
    
    <script type="text/javascript">
        //set page title
        document.getElementById("navTitle").innerHTML = "find respondants";

        //jquery toggle function to show divs when clicked
        $(document).ready(function () {
            $("#showAll").click(function () {
                $("#showAllDiv").toggle();
            });
        });
        $(document).ready(function () {
            $("#searchFor").click(function () {
                $("#searchForDiv").toggle();
            })
        });
    </script>
</asp:Content>