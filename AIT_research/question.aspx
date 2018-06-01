<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="question.aspx.cs" MasterPageFile="~/Site.Master" Inherits="AIT_research.question" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row questionRow">
            <asp:PlaceHolder ID="questionPlaceholder" runat="server"></asp:PlaceHolder>
            <div class="buttons">
                <asp:Button ID="nextBtn" runat="server" OnClick="nextBtn_Click" CssClass="defaultBtnAit questionBtn blue" text="Continue"/>
                <asp:Button ID="skipBtn" runat="server" OnClick="nextBtn_Click" CssClass="defaultBtnAit questionBtn yellow" text="Skip Question"/>
            </div>
        </div>
    </div>
</asp:Content>
