<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="loginStaff.aspx.cs" Inherits="AIT_research.loginStaff" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container loginDiv">
        <h2>Welcome back staff member, please sign in!</h2>
        <img src="images/logo.png" />
        <div class="loginInputs">
            <input type="text" class="input" id="inputUsernameId" name="inputUsernameName" placeholder="Username" />
            <input type="password" class="input" id="inputPasswordId" name="inputPasswordName" placeholder="Password" />
            <input type="button" class="defaultBtnAit yellow" id="loginBtn" value="Sign in"/>
        </div>
            <a href="Default.aspx">Are you not a staff member and wanna go back to the home page? <u>Click here.</u></a>
    </div>

</asp:Content>