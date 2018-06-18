<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="Register.aspx.cs" MasterPageFile="~/Site.Master" Inherits="AIT_research.Register" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid registerDiv">
        <asp:LinkButton ID="continueAnonymousBtn" 
                        runat="server" 
                        CssClass="longButton blue" OnClick="continueAnonymousBtn_Click" CausesValidation="False">
            continue as anonymous <i class="fa fa-chevron-right"></i>
        </asp:LinkButton>
        <input type="button" id="registerMember" class="longButton yellow" value="register as a member"/>
        
        <!-- REGISTER FORM -->
        <div id="registerMemberDiv" class="foldOutContainer container" style="display:none">
            <div class="row registerRow">
            <!-- LAST NAME -->
            <div class="col-md-6 col-sm-6 col-xs-12">
                <asp:Label class="registerLabel" text="Last Name" runat="server" ></asp:Label>
                <asp:TextBox ID="lastnameTextbox" class="registerTextBox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="lastnameValidator"
                    runat="server"
                    Display = "Dynamic"
                    ErrorMessage="Please enter your last name"
                    ControlToValidate="lastnameTextbox"
                    ForeColor="#EB0144">
                </asp:RequiredFieldValidator>
            </div>
            <!-- FIRST NAME -->
            <div class="col-md-6  col-sm-6 col-xs-12">
                <asp:Label class="registerLabel" text="First Name" runat="server" ></asp:Label>
                <asp:TextBox ID="firstnameTextbox" class="registerTextBox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="firstnameValidator"
                    runat="server"
                    Display = "Dynamic" 
                    ErrorMessage="Please enter your first name"
                    ControlToValidate="firstnameTextbox"
                    ForeColor="#EB0144">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row registerRow">
            <!-- DATE OF BIRTH -->
            <div class="col-md-12">
                <asp:Label class="registerLabel" text="Date of birth (DD/MM/YY)" runat="server" ></asp:Label>
                <asp:TextBox ID="dobTextbox" class="registerTextBox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="dobValidator"
                    runat="server"
                    Display = "Dynamic"
                    Type="Date"
                    ErrorMessage="Date of birth must be beetween 2017 and 1900"
                    ControlToValidate="dobTextbox"
                    MaximumValue="01/01/2018" MinimumValue="01/01/1900"
                    ForeColor="#EB0144">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="row registerRow">
            <!-- CONTACT NUMBER -->
            <div class="col-md-12 col-sm-12">
                <asp:Label class="registerLabel" text="Contact number" runat="server" ></asp:Label>
                <asp:TextBox ID="phoneTextbox" class="registerTextBox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="phoneValidator"
                    runat="server"
                    Display = "Dynamic"
                    Type="Phone"
                    ErrorMessage="Please enter your phone number"
                    ControlToValidate="phoneTextbox"
                    ForeColor="#EB0144">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <asp:Button ID="registerBtn" runat="server" OnClick="RegisterBtn_Click" text="Register" CssClass="defaultBtnAit yellow full"/>
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
