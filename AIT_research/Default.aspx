<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AIT_research._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <div class="leftItem">
            <div class="contentLeftItem">
                <h1>Welcome to AIT research</h1>
                <h4>Are you interested in participating in market researches? If yes, we need to know a little bit about you and your habits. Please take our survey, it will only take a few minutes and you can be anonymus if you'd wish.</h4>
                <a href="question.aspx">
                    <input type="button" class="defaultBtnAit homeBtn yellow" value="take survey"/>
                </a>
                <h4>Are you working at AIT Research and would like to login?</h4>
                <a href="loginStaff.aspx">
                    <input type="button" class="defaultBtnAit homeBtn yellow" value="Login as staff"/>
                </a>
            </div>
        </div>
        <script type="text/javascript">
    //set page title
    document.getElementById("navTitle").innerHTML = "Welcome";
</script>
</asp:Content>
