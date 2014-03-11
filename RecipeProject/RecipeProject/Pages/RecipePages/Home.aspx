<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Recipe.Master" AutoEventWireup="true" CodeBehind="Listing.aspx.cs" Inherits="RecipeProject.Pages.RecipePages.Listing" %>

<asp:Content ContentPlaceHolderID="NavigationPlaceHolder" runat="server">
    <li><a class="current" href='<%$ RouteUrl:routename=Default %>' runat="server">Hem</a></li>
    <li><a href='<%$ RouteUrl:routename=Recipes %>' runat="server">Recept</a></li>
    <li><a href='<%$ RouteUrl:routename=RecipeCreate %>' runat="server">Lägg till recept</a></li>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <h1>Hem</h1>
    <p>Välkommen till Emmas receptbok!</p>
    <p>
        Emma har en stor receptbok fylld med härliga recept och 
        vill dela med sig utav dessa med världen så hon ska
        använda sig utav denna webbapplikationen för att 
        dela med sig utav sina recept. 
    </p>
    <p>
        Recepten är kategoriserade i kategorierna 
        förrätt, huvudrätt och efterrätt.
    </p>
    <p><a class="button" href='<%$ RouteUrl:routename=Recipes %>' runat="server">Gå till recepten</a></p>
</asp:Content>
