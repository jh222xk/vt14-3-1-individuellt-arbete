<%@ Page Title="Receptlista - Emmas receptbok" Language="C#" MasterPageFile="~/Pages/Shared/Recipe.Master" AutoEventWireup="true" CodeBehind="Listing.aspx.cs" Inherits="RecipeProject.Pages.RecipePages.Listing" %>

<asp:Content ContentPlaceHolderID="NavigationPlaceHolder" runat="server">
    <li><a href='<%$ RouteUrl:routename=Default %>' runat="server">Hem</a></li>
    <li><a class="current" href='<%$ RouteUrl:routename=Recipes %>' runat="server">Recept</a></li>
    <li><a href='<%$ RouteUrl:routename=RecipeCreate %>' runat="server">Lägg till recept</a></li>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <asp:Panel ID="PanelSuccess" runat="server" Visible="false">
        <asp:Label ID="LabelSuccess" CssClass="alert-box success" runat="server" Text="">
                <a class="close" href="#">&times;</a>
        </asp:Label>
    </asp:Panel>
    <h1>Recept</h1>
    <p>Här listas alla de recept som Emma har lagt till i sin receptsamling i webbapplikationen.</p>
    <asp:ValidationSummary runat="server" />
    <asp:ListView ID="RecipeListView" runat="server"
        ItemType="RecipeProject.Model.Recipe"
        SelectMethod="RecipeListView_GetData"
        DataKeyNames="RecipeId">
        <LayoutTemplate>
            <%-- Placeholder for recipes --%>
            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
            <div class="recipes">
                <h3 class="recipe-name">
                    <asp:HyperLink ID="HyperLink1" runat="server"
                        NavigateUrl='<%# GetRouteUrl("RecipeDetails", new { id = Item.RecipeID })  %>'
                        Text='<%# Item.Recipename %>' />
                </h3>
                <p class="recipe-description">
                    <%#: Item.Description %>
                </p>
            </div>
        </ItemTemplate>
        <EmptyDataTemplate>
            <%-- This is displayed when the recipes are missing in the database. --%>
            <p>
                Det finns inga recept...
            </p>
        </EmptyDataTemplate>
    </asp:ListView>
</asp:Content>
