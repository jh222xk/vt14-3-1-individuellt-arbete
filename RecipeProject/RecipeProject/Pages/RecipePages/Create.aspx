<%@ Page Title="Nytt recept - Emmas receptbok" Language="C#" MasterPageFile="~/Pages/Shared/Recipe.Master" AutoEventWireup="true" CodeBehind="Create.aspx.cs" Inherits="RecipeProject.Pages.RecipePages.Create" %>

<asp:Content ContentPlaceHolderID="NavigationPlaceHolder" runat="server">
    <li><a href='<%$ RouteUrl:routename=Default %>' runat="server">Hem</a></li>
    <li><a href='<%$ RouteUrl:routename=Recipes %>' runat="server">Recept</a></li>
    <li><a class="current" href='<%$ RouteUrl:routename=RecipeCreate %>' runat="server">Lägg till recept</a></li>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <h1>Nytt recept
    </h1>
    <asp:ValidationSummary runat="server" CssClass="alert-box alert"
        DisplayMode="BulletList"
        EnableClientScript="true"
        HeaderText="Fel inträffade. Åtgärda felen och försök igen." />
    <asp:FormView ID="RecipeFormView" runat="server"
        ItemType="RecipeProject.Model.Recipe"
        DataKeyNames="RecipeID"
        RenderOuterTable="false"
        DefaultMode="Insert"
        InsertMethod="RecipeFormView_InsertItem">
        <InsertItemTemplate>
            <div class="recipe-name">
                <label for="Recipename">Receptnamn</label>
            </div>
            <div class="recipe-name">
                <asp:TextBox ID="Recipename" runat="server" MaxLength="60" Text='<%# BindItem.Recipename %>' />
            </div>
            <div class="recipe-description">
                <label for="Description">Beskrivning</label>
            </div>
            <div class="recipe-description">
                <asp:TextBox ID="Description" TextMode="MultiLine" runat="server" Text='<%# BindItem.Description %>' />
            </div>
            <div class="recipe-instruction">
                <label for="Instruction">Instruktioner</label>
            </div>
            <div class="recipe-instruction">
                <asp:TextBox ID="Instruction" TextMode="MultiLine" runat="server" Text='<%# BindItem.Instruction %>' />
            </div>
            <div class="row">
                <div class="large-12 columns">
                    <label for="Ingredient">Ingrediens</label>
                    <asp:TextBox ID="IngredientTextBox" runat="server" MaxLength="40" Text='<%# Bind("Ingredientname") %>' />
                    <label for="Amount">Mängd</label>
                    <asp:TextBox ID="AmountTextBox" runat="server" MaxLength="25" Text='<%# Bind("RecipeAmount") %>' />
                </div>
            </div>
            <div>
                <asp:LinkButton CssClass="button" runat="server" Text="Spara" CommandName="Insert" />
                <asp:HyperLink CssClass="button" runat="server" Text="Avbryt" NavigateUrl='<%# GetRouteUrl("Recipes", null) %>' />
            </div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorInsertRecipename" runat="server" ErrorMessage="Receptets namn måste anges."
                Display="None" ControlToValidate="Recipename"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorInsertDescription" runat="server" ErrorMessage="En beskrivning måste anges."
                Display="None" ControlToValidate="Description"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorInsertInstruction" runat="server" ErrorMessage="En instruktion måste anges."
                Display="None" ControlToValidate="Instruction"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorInsertIngredient" runat="server" ErrorMessage="Ingrediens måste anges."
                Display="None" ControlToValidate="IngredientTextBox"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorInsertAmount" runat="server" ErrorMessage="Mängd måste anges."
                Display="None" ControlToValidate="AmountTextBox"></asp:RequiredFieldValidator>
        </InsertItemTemplate>
    </asp:FormView>
</asp:Content>
