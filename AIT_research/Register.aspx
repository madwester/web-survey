<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="Register.aspx.cs" MasterPageFile="~/Site.Master" Inherits="AIT_research.Register" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid registerDiv">

        <button class="longButton blue">continue as anonymous 
            <span class="glyphicon glyphicon-chevron-right"></span>
        </button>
        <input type="button" id="registerMember" class="longButton yellow" value="register as a member"/>
    <div id="registerMemberDiv" class="foldOutContainer container" style="display:none">
        <div class="row registerRow">
            <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:Label class="registerLabel" text="Last Name" runat="server" ></asp:Label>
                <asp:TextBox class="registerTextBox" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6  col-sm-6 col-xs-12">
                <asp:Label class="registerLabel" text="First Name" runat="server" ></asp:Label>
                <asp:TextBox class="registerTextBox" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row registerRow">
            <div class="col-md-12">
                <asp:Label class="registerLabel" text="Date of birth" runat="server" ></asp:Label>
                <asp:TextBox class="registerTextBox" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row registerRow">
            <div class="col-md-12 col-sm-12">
                <asp:Label class="registerLabel" text="Contact number" runat="server" ></asp:Label>
                <asp:TextBox class="registerTextBox" runat="server"></asp:TextBox>
            </div>
        </div>
        <asp:Button ID="registerBtn" runat="server" text="Register" CssClass="defaultBtnAit yellow"/>
    </div>
    </div>
    
    <script type="text/javascript">
        //set page title
        document.getElementById("navTitle").innerHTML = "registeration";

        //jquery toggle function to show divs when clicked
        $(document).ready(function () {
            $("#registerMember").click(function () {
                $("#registerMemberDiv").toggle();
            })
        });
    </script>
</asp:Content>
