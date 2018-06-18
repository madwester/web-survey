<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Thankyou.aspx.cs" MasterPageFile="~/Site.Master" Inherits="AIT_research.thankyou" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container content">
        <div class="contentThankYou">
            <h2>Thanks for participating in our survey.</h2>
            <asp:Button ID="returnBtn" runat="server" CssClass="defaultBtnAit yellow" text="Return to Homepage" OnClick="returnBtn_Click"/>
        </div>
    </div>
    <script type="text/javascript">
    //set page title
    document.getElementById("navTitle").innerHTML = "End of survey";
</script>
</asp:Content>
