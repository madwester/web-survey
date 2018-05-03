<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AIT_research._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-12 text-center column">
                <h1>Welcome to AIT research</h1>
                <h4>Are you interested in participating in market researches? If yes, we need to know a little bit about you and your habits. Please take our survey, it will only take a few minutes and you can be anonymus if you'd wish.</h4>
                <button class="btn defaultBtnAit blue">Take Survey</button>
            </div>
            <div class="col-md-12 text-center column">
                <h4>Are you working at AIT Research and would like to login?</h4>
                <button class="btn defaultBtnAit yellow">Login in staff</button>
            </div>
        </div>
    </div>
</asp:Content>
