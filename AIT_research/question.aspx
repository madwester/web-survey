﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="question.aspx.cs" MasterPageFile="~/Site.Master" Inherits="AIT_research.question" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row questionRow">
            <asp:PlaceHolder ID="questionTemplate" runat="server"></asp:PlaceHolder>
            <button class="btn defaultBtnAit blue">Continue</button>
            <button class="btn defaultBtnAit yellow">Skip question</button>
        </div>
    </div>
</asp:Content>