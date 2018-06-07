<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="loginStaff.aspx.cs" Inherits="AIT_research.loginStaff" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container loginDiv">
        <h2>Welcome back staff member, please sign in!</h2>
        <img src="images/logo.png" />
        <div class="loginInputs">
            <asp:TextBox runat="server" CssClass="input" ID="usernameTextbox" placeholder="Username"></asp:TextBox>
            <asp:RequiredFieldValidator ID="usernameValidator"
                    runat="server"
                    Display = "Dynamic"
                    ErrorMessage="Please enter your username"
                    ControlToValidate="usernameTextbox"
                    ForeColor="#EB0144">
            </asp:RequiredFieldValidator>
            <asp:Textbox runat="server" CssClass="input" ID="passwordTextbox" placeholder="Password"></asp:Textbox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                    runat="server"
                    Display = "Dynamic"
                    ErrorMessage="Please enter your password"
                    ControlToValidate="passwordTextbox"
                    ForeColor="#EB0144">
            </asp:RequiredFieldValidator>
            <asp:Button runat="server" CssClass="defaultBtnAit yellow" ID="loginBtn" Text="Sign in" OnClick="loginBtn_Click"/>
        </div>
            <a href="Default.aspx">Are you not a staff member and wanna go back to the home page? <u>Click here.</u></a>
    </div>
        <script type="text/javascript">
        //set page title
        document.getElementById("navTitle").innerHTML = "sign in staff";
    </script>
</asp:Content>