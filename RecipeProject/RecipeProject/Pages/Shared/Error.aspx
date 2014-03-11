<%@ Page Title="Serverfel - Emmas receptbok" Language="C#" MasterPageFile="~/Pages/Shared/Recipe.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="RecipeProject.Pages.RecipePages.Listing" %>


<asp:Content ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <h1>Serverfel</h1>
    <p>
        Vi är beklagar att ett fel inträffade och vi inte kunde hantera din förfrågan.
    </p>
    <p>
        <a id="A1" href='<%$ RouteUrl:routename=Default %>' runat="server">Tillbaka till startsidan</a>
    </p>
</asp:Content>
