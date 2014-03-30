<%@ Page Title="Redigera recept - Emmas receptbok" Language="C#" MasterPageFile="~/Pages/Shared/Recipe.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="RecipeProject.Pages.RecipePages.Edit" %>

<asp:Content ContentPlaceHolderID="NavigationPlaceHolder" runat="server">
    <li><a href='<%$ RouteUrl:routename=Default %>' runat="server">Hem</a></li>
    <li><a class="current" href='<%$ RouteUrl:routename=Recipes %>' runat="server">Recept</a></li>
    <li><a href='<%$ RouteUrl:routename=RecipeCreate %>' runat="server">Lägg till recept</a></li>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <h1>Redigera recept
    </h1>
    <asp:Panel ID="PanelSuccess" runat="server" Visible="false">
        <asp:Label ID="LabelSuccess" CssClass="alert-box success" runat="server" Text="">
                <a class="close" href="#">&times;</a>
        </asp:Label>
    </asp:Panel>
    <asp:ValidationSummary runat="server" CssClass="alert-box alert"
        DisplayMode="BulletList"
        EnableClientScript="true"
        ShowModelStateErrors="False"
        HeaderText="Fel inträffade. Åtgärda felen och försök igen." />
    <asp:ValidationSummary runat="server" CssClass="alert-box alert"
        ValidationGroup="InsertValidationGroup"
        DisplayMode="BulletList"
        EnableClientScript="true"
        ShowModelStateErrors="False"
        HeaderText="Fel inträffade. Åtgärda felen och försök igen." />
    <asp:FormView ID="RecipeFormView" runat="server"
        ItemType="RecipeProject.Model.Recipe"
        DataKeyNames="RecipeId"
        RenderOuterTable="false"
        DefaultMode="Edit"
        UpdateMethod="RecipeFormView_UpdateItem"
        SelectMethod="RecipeFormView_GetItem"
        InsertMethod="RecipeFormView_InsertItem"
        InsertItemPosition="LastItem">
        <EditItemTemplate>
            <%--  Edittemplate for the recipename and so on --%>
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
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorInsertRecipename" runat="server" ErrorMessage="Receptets namn måste anges."
                Display="None" ControlToValidate="Recipename"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorInsertDescription" runat="server" ErrorMessage="En beskrivning måste anges."
                Display="None" ControlToValidate="Description"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorInsertInstruction" runat="server" ErrorMessage="En instruktion måste anges."
                Display="None" ControlToValidate="Instruction"></asp:RequiredFieldValidator>
            <%--  Editview for the amount and ingredients --%>
            <fieldset class="ingredient-and-amount">
                <legend>Ingredienser & Mängd</legend>
                <asp:ListView ID="AmountListView" runat="server"
                    ItemType="RecipeProject.Model.Amount"
                    DataKeyNames="AmountID, RecipeID, IngredientID"
                    RenderOuterTable="false"
                    UpdateMethod="AmountFormView_UpdateItem"
                    SelectMethod="AmountFormView_GetItem"
                    InsertMethod="AmountListView_InsertItem"
                    OnItemDataBound="AmountListView_ItemDataBound"
                    InsertItemPosition="LastItem">
                    <ItemTemplate>
                        <asp:MultiView ID="ContactMultiView" runat="server" ActiveViewIndex="0">
                            <asp:View ID="EditView" runat="server">
                                <div class="row">
                                    <div class="large-4 columns">
                                        <asp:DropDownList ID="IngredientDropDownList" runat="server"
                                            ItemType="RecipeProject.Model.Ingredients"
                                            SelectMethod="IngredientsDropDownList_GetData"
                                            DataTextField="Ingredientname"
                                            DataValueField="IngredientID"
                                            Enabled="false"
                                            SelectedValue='<%# Item.IngredientID %>' />
                                    </div>
                                    <div class="large-4 columns">
                                        <asp:Label ID="AmountLabel" CssClass="bold" runat="server" Text="Mängd för: " />
                                        <asp:Label CssClass="bold" ID="IngredientsLabel" runat="server" Text="" />
                                        <asp:TextBox ID="AmountTextBox" runat="server" Text='<%# Item.RecipeAmount %>' MaxLength="50" Enabled="false" />
                                    </div>
                                    <div class="large-4 columns" style="margin-top: 15px;">
                                        <asp:LinkButton runat="server" CssClass="button" CommandName="Edit" Text="Redigera" CausesValidation="false" />
                                        <asp:HyperLink runat="server" CssClass="button" Text="Ta bort" NavigateUrl='<%# GetRouteUrl("IngredientDelete", new { id = Item.AmountID }) %>' />
                                    </div>
                                </div>
                            </asp:View>
                        </asp:MultiView>
                    </ItemTemplate>
                    <InsertItemTemplate>
                        <div class="row">
                            <div class="large-4 columns">
                                <label for="Ingredient">Ingrediens</label>
                                <asp:DropDownList ID="IngredientDropDownList" runat="server"
                                    ItemType="RecipeProject.Model.Ingredients"
                                    SelectMethod="IngredientsDropDownList_GetData"
                                    DataTextField="Ingredientname"
                                    DataValueField="IngredientID"
                                    SelectedValue='<%# BindItem.IngredientID %>' />
                            </div>
                            <div class="large-4 columns">
                                <label for="Amount">Mängd</label>
                                <asp:TextBox ID="AmountTextBox" runat="server" MaxLength="25" Text='<%# Bind("RecipeAmount") %>' />
                            </div>
                            <div class="large-4 columns" style="margin-top: 15px;">
                                <asp:LinkButton ValidationGroup="InsertValidationGroup" CssClass="button" runat="server" CommandName="Insert" Text="Spara" />
                            </div>
                        </div>
                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidatorInsertIngredient" runat="server" ErrorMessage="Ingrediens måste anges."
                            Display="None" ControlToValidate="IngredientTextBox" ValidationGroup="InsertValidationGroup"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorInsertAmount" runat="server" ErrorMessage="Mängd måste anges."
                            Display="None" ControlToValidate="AmountTextBox" ValidationGroup="InsertValidationGroup"></asp:RequiredFieldValidator>--%>
                    </InsertItemTemplate>
                    <EditItemTemplate>
                        <div class="row">
                            <div class="large-4 columns">
                                <label>Ingrediens</label>
                                <asp:DropDownList ID="IngredientDropDownList" runat="server"
                                    ItemType="RecipeProject.Model.Ingredients"
                                    SelectMethod="IngredientsDropDownList_GetData"
                                    DataTextField="Ingredientname"
                                    DataValueField="IngredientID"
                                    SelectedValue='<%# BindItem.IngredientID %>' />
                            </div>
                            <div class="large-4 columns">
                                <asp:Label ID="AmountLabel" CssClass="bold" runat="server" Text="Mängd för: " />
                                <asp:Label CssClass="bold" ID="IngredientsLabel" runat="server" Text="" />
                                <asp:TextBox ID="Amount" runat="server" Text='<%# Bind("RecipeAmount") %>' />
                            </div>
                            <div class="large-4 columns" style="margin-top: 15px;">
                                <asp:LinkButton ID="LinkButton3" CssClass="button" runat="server" Text="Spara" CommandName="Update" />
                                <asp:LinkButton ID="LinkButton4" CssClass="button" runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="false" />
                            </div>
                        </div>
                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidatorInsertFirstName" runat="server" ErrorMessage="Ingrediens måste anges."
                            Display="None" ControlToValidate="Ingredient"></asp:RequiredFieldValidator> --%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorInsertLastName" runat="server" ErrorMessage="Mängd måste anges."
                            Display="None" ControlToValidate="Amount"></asp:RequiredFieldValidator>
                    </EditItemTemplate>
                </asp:ListView>
            </fieldset>


            <div>
                <asp:LinkButton class="button" runat="server" Text="Spara" CommandName="Update" />
                <asp:HyperLink class="button" runat="server" Text="Avbryt" NavigateUrl='<%# GetRouteUrl("RecipeDetails", new { id = Item.RecipeID }) %>' />
            </div>
        </EditItemTemplate>
    </asp:FormView>
</asp:Content>
