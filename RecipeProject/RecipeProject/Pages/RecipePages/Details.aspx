<%@ Page Title="Receptdetaljer - Emmas receptbok" Language="C#" MasterPageFile="~/Pages/Shared/Recipe.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="RecipeProject.Pages.RecipePages.Details" %>

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
    <asp:FormView ID="RecipeFormView" runat="server"
        ItemType="RecipeProject.Model.Recipe"
        DataKeyNames="RecipeId"
        DefaultMode="ReadOnly"
        RenderOuterTable="false"
        SelectMethod="RecipeFormView_GetItem">
        <ItemTemplate>
            <div class="recipe-name">
                <h1><%#: Item.Recipename %></h1>
                <p class="publisher">Recept av: Klara Karlsson</p>
            </div>
            <div class="recipe-description">
                <h3>Beskrivning</h3>
            </div>
            <div class="recipe-description">
                <p><%#: Item.Description %></p>
            </div>
            <div class="recipe-instruction">
                <h3>Instruktioner</h3>
            </div>
            <div class="recipe-instruction">
                <p><%# Item.Instruction %></p>
            </div>
            <h3>Ingredienser & Mängd</h3>
            <%-- ListView featuring a recepts amount. --%>
            <asp:ListView ID="AmountListView" runat="server"
                ItemType="RecipeProject.Model.Amount"
                DataKeyNames="AmountID, RecipeID, IngredientID"
                SelectMethod="AmountListView_GetData"
                OnItemDataBound="AmountListView_ItemDataBound">
                <LayoutTemplate>
                    <ul>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                    </ul>
                </LayoutTemplate>
                <EmptyDataTemplate>
                    <p>
                        Ingredienser och dess mängd saknas.
                    </p>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <asp:Label ID="ContactTypeNameLabel" runat="server" Text='<%#: Item.RecipeAmount %>' />
                    <asp:Label ID="IngredientsLabel" runat="server" Text="" />
                    <br />
                </ItemTemplate>
            </asp:ListView>
            <div class="edit-links">
                <asp:HyperLink runat="server" class="button" Text="Redigera" NavigateUrl='<%# GetRouteUrl("RecipeEdit", new { id = Item.RecipeID }) %>' />
                <asp:HyperLink runat="server" class="button" Text="Ta bort" NavigateUrl='<%# GetRouteUrl("RecipeDelete", new { id = Item.RecipeID }) %>' />
                <asp:HyperLink runat="server" class="button" Text="Tillbaka" NavigateUrl='<%# GetRouteUrl("Recipes", null)%>' />
            </div>
        </ItemTemplate>
        <InsertItemTemplate>
            <div class="recipe-name">
                <label for="Recipename">Namn</label>
            </div>
            <div class="recipe-name">
                <asp:TextBox ID="Recipename" runat="server" Text='<%# BindItem.Recipename %>' />
            </div>
            <div class="recipe-description">
                <label for="Description">Beskrivning</label>
            </div>
            <div class="recipe-description">
                <asp:TextBox ID="Description" runat="server" Text='<%# BindItem.Description %>' />
            </div>
            <div>
                <asp:LinkButton ID="LinkButton1" runat="server" Text="Spara" CommandName="Insert" />
                <asp:HyperLink ID="HyperLink1" runat="server" Text="Avbryt" NavigateUrl='<%# GetRouteUrl("Recipes", null) %>' />
            </div>
        </InsertItemTemplate>
        <EditItemTemplate>
            <div class="recipe-name">
                <label for="Recipename">Namn</label>
            </div>
            <div class="recipe-name">
                <asp:TextBox ID="Recipename" runat="server" Text='<%# BindItem.Recipename %>' />
            </div>
            <div class="recipe-description">
                <label for="Description">Beskrivning</label>
            </div>
            <div class="recipe-description">
                <asp:TextBox ID="Description" runat="server" Text='<%# BindItem.Description %>' />
            </div>
            <div>
                <asp:LinkButton ID="LinkButton2" runat="server" Text="Spara" CommandName="Update" />
                <asp:HyperLink ID="HyperLink2" runat="server" Text="Avbryt" NavigateUrl='<%# GetRouteUrl("RecipeDetails", new { id = Item.RecipeID }) %>' />
            </div>
        </EditItemTemplate>
    </asp:FormView>
</asp:Content>
