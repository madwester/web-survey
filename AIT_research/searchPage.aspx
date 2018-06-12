<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="searchPage.aspx.cs" Inherits="AIT_research.searchPage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid searchDiv">
        <input type="button" id="showAll" class="longButton blue" value="show all respondants"/>
    <div id="showAllDiv" class="foldOutContainer" style="display:none">
       <asp:GridView ID="allRespGridView" runat="server"></asp:GridView>
    </div>
        <input type="button" id="searchFor" class="longButton yellow" value="search for respondants"/>
    <div id="searchForDiv" class="container foldOutContainer" style="display:none">
   
        <div class="row rowSearch">
            <div class="col-md-4 col-sm-4 columnSearch">
                <asp:Label runat="server" Text="Last Name">
                </asp:Label>
                <asp:TextBox runat="server" CssClass="searchInput"></asp:TextBox>
            </div>
            <div class="col-md-4 col-sm-4 columnSearch">
                <asp:Label runat="server" Text="First Name">
                </asp:Label>
                <asp:TextBox runat="server" CssClass="searchInput"></asp:TextBox>
            </div>
            <div class="col-md-4 col-sm-4 columnSearch">
                <asp:Label runat="server" Text="Email Name">
                </asp:Label>
                <asp:TextBox runat="server" CssClass="searchInput"></asp:TextBox>
            </div>
        </div>
        <div class="row rowSearch">
            <div class="col-md-12 col-xs-12 columnSearch">
                <asp:Label runat="server" Text="Gender"></asp:Label>
                <asp:CheckBoxList ID="genderList" runat="server"></asp:CheckBoxList>
            </div>
        </div>
        <div class="row rowSearch">
            <div class="col-md-12 col-xs-12 columnSearch">
                <asp:Label runat="server" Text="Age range"></asp:Label>
                <asp:CheckBoxList ID="ageList" runat="server"></asp:CheckBoxList>
            </div>
        </div>
        <div class="row rowSearch">
            <div class="col-md-4 col-sm-4 columnSearch">
                <asp:Label runat="server" Text="Home Suburb">
                </asp:Label>
                <asp:TextBox runat="server" CssClass="searchInput"></asp:TextBox>
            </div>
            <div class="col-md-4 col-sm-4 columnSearch">
                <asp:Label runat="server" Text="Home Postcode">
                </asp:Label>
                <asp:TextBox runat="server" CssClass="searchInput"></asp:TextBox>
            </div>
        </div>
        <div class="row rowSearch">
            <div class="col-md-12 col-xs-12 columnSearch">
                <asp:Label runat="server" Text="State/Territory"></asp:Label>
                <asp:CheckBoxList ID="stateList" runat="server"></asp:CheckBoxList>
            </div>
        </div>
        <div class="row rowSearch">
            <div class="col-md-12 col-xs-12 columnSearch">
                <asp:Label runat="server" Text="Banks Used"></asp:Label>
                <asp:CheckBoxList ID="banksList" runat="server"></asp:CheckBoxList>
            </div>
        </div>
        <div class="row rowSearch">
            <div class="col-md-12 col-xs-12 columnSearch">
                <asp:Label runat="server" Text="Bank Services"></asp:Label>
                <asp:CheckBoxList ID="bankserviceList" runat="server"></asp:CheckBoxList>
            </div>
        </div>
        <div class="row rowSearch">
            <div class="col-md-12 col-xs-12 columnSearch">
                <asp:Label runat="server" Text="Newspaper Read"></asp:Label>
                <asp:CheckBoxList ID="newspaperList" runat="server"></asp:CheckBoxList>
            </div>
        </div>
        <div class="row rowSearch">
            <div class="col-md-12 col-xs-12 columnSearch">
                <asp:Label runat="server" Text="Aditional interests"></asp:Label>
                <asp:CheckBoxList ID="interestList" runat="server"></asp:CheckBoxList>
            </div>
        </div>
        <div class="row rowSearch">
            <div class="col-md-12 col-xs-12 columnSearch">
                <asp:Label runat="server" Text="Interests in Sport"></asp:Label>
                <asp:CheckBoxList ID="sportList" runat="server"></asp:CheckBoxList>
            </div>
        </div>
        <div class="row rowSearch">
            <div class="col-md-12 col-xs-12 columnSearch">
                <asp:Label runat="server" Text="Interest in Travel"></asp:Label>
                <asp:CheckBoxList ID="travelList" runat="server"></asp:CheckBoxList>
            </div>
        </div>
        <asp:Button ID="searchBtn" runat="server" OnClick="search_Click" CssClass="defaultBtnAit yellow full" text="Search"/>
        <asp:GridView ID="searchResultGridView" runat="server"></asp:GridView>
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