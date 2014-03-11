<%@ Page Title="Ta bort ingrediens & mängd - Emmas receptbok" Language="C#" MasterPageFile="~/Pages/Shared/Recipe.Master" AutoEventWireup="true" CodeBehind="Delete.aspx.cs" Inherits="RecipeProject.Pages.IngredientPages.Delete" %>

<asp:Content ContentPlaceHolderID="NavigationPlaceHolder" runat="server">
    <li><a href='<%$ RouteUrl:routename=Default %>' runat="server">Hem</a></li>
    <li><a class="current" href='<%$ RouteUrl:routename=Recipes %>' runat="server">Recept</a></li>
    <li><a href='<%$ RouteUrl:routename=RecipeCreate %>' runat="server">Lägg till recept</a></li>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <h1>Ta bort ingrediens
    </h1>
    <asp:ValidationSummary runat="server" CssClass="alert-box alert" />
    <asp:PlaceHolder runat="server" ID="ConfirmationPlaceHolder">
        <p>
            Är du säker på att du vill ta bort ingrediensen <strong>
                <asp:Literal runat="server" ID="IngredientValue" ViewStateMode="Enabled" /></strong> och dess mängd för receptet <strong>
                    <asp:Literal runat="server" ID="Recipename" ViewStateMode="Enabled" /></strong>?
        </p>
    </asp:PlaceHolder>
    <div>
        <asp:LinkButton runat="server" CssClass="button" ID="DeleteLinkButton" Text="Ja, ta bort ingrediensen"
            OnCommand="DeleteLinkButton_Command" CommandArgument='<%$ RouteValue:id %>' />
        <asp:HyperLink runat="server" CssClass="button" ID="CancelHyperLink" Text="Avbryt" />
    </div>
</asp:Content>
