using RecipeProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;

namespace RecipeProject.Pages.RecipePages
{
    public partial class Edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Message != null)
            {
                PanelSuccess.Visible = true;
                var messageString = Message;
                LabelSuccess.Text += messageString;
                Message = messageString;
                Session.Clear();
            }
        }

        /// <summary>
        /// Errormessages for recipes and amount.
        /// </summary>
        private static readonly string Recipe_Selecting_Error = Strings.Recipe_Selecting_Error;
        private static readonly string Recipe_Updating_Error = Strings.Recipe_Updating_Error;
        private static readonly string Recipe_Not_Found = Strings.Recipe_Not_Found;
        private static readonly string Recipe_Insert_Error = Strings.Recipe_Insert_Error;
        private static readonly string Action_Recipe_Updated = Strings.Action_Recipe_Updated;
        private static readonly string Amount_Not_Found = Strings.Amount_Not_Found;
        private static readonly string Amount_Updating_Error = Strings.Amount_Updating_Error;
        private static readonly string Amount_Insert_Error = Strings.Amount_Insert_Error;
        private static readonly string Amount_Selecting_Error = Strings.Amount_Selecting_Error;
        private static readonly string Action_Amount_Added = Strings.Action_Amount_Added;
        private static readonly string Action_Amount_Updated = Strings.Action_Amount_Updated;
        
        /// <summary>
        /// Property for returning the routedata's value (id).
        /// </summary>
        public int RecipeID {
            // Return the value only if there's any to get...
            get {
                if (RouteData.Values["id"] != null)
                {
                    return int.Parse(RouteData.Values["id"].ToString());
                }
                // ... else just redirect them and return a dummy value.
                Response.RedirectToRoute("Recipes");
                Context.ApplicationInstance.CompleteRequest();
                return 1;
            } 
        }

        /// <summary>
        /// The message to be shown, either error or success.
        /// </summary>
        private string Message
        {
            get { return Session["Message"] as string; }
            set
            {
                Session["Message"] = value;
            }
        }

        private Service _service;

        private Service Service
        {
            // A Service-object is created only when it's needed for the first time.
            get { return _service ?? (_service = new Service()); }
        }

        /// <summary>
        /// Gets the recipe depending on the [RouteData]int id.
        /// </summary>
        public Recipe RecipeFormView_GetItem()
        {
            try
            {
                return Service.GetRecipe(RecipeID);
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, Recipe_Selecting_Error);
                return null;
            }
        }

        /// <summary>
        /// Updating the recipe information in the database.
        /// <param name="recipeId">The recipes number.</param>
        /// </summary>
        public void RecipeFormView_UpdateItem(int recipeId)
        {
            try
            {
                var recipe = Service.GetRecipe(recipeId);
                if (recipe == null)
                {
                    ModelState.AddModelError(String.Empty,
                        String.Format(Recipe_Not_Found, recipeId));
                    return;
                }

                if (TryUpdateModel(recipe))
                {
                    Service.SaveRecipe(recipe, null);
                    Message = Action_Recipe_Updated;
                    Response.RedirectToRoute("RecipeDetails", new { id = recipe.RecipeID });
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, Recipe_Updating_Error);
            }
        }

        /// <summary>
        /// Updating the amount information in the database.
        /// <param name="amountId">The recipes amounts number.</param>
        /// </summary>
        public void AmountFormView_UpdateItem(int amountId)
        {
            try
            {
                var amount = Service.GetAmount(amountId);
                if (amount == null)
                {
                    Page.ModelState.AddModelError(String.Empty,
                        String.Format(Amount_Not_Found, amountId));
                    return;
                }

                if (TryUpdateModel(amount))
                {
                    Service.SaveAmount(amount);

                    Message = Action_Amount_Updated;
                    Response.RedirectToRoute("RecipeEdit", new { id = RecipeID });
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, Amount_Updating_Error);
            }
        }

        /// <summary>
        /// Gets the amount depending on the [RouteData]int id for that particular recipe.
        /// </summary>
        public IEnumerable<Amount> AmountFormView_GetItem()
        {
            try
            {
                return Service.GetAmountByRecipeId(RecipeID);
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, Amount_Selecting_Error);
                return null;
            }
        }

        /// <summary>
        /// Change the label text so it displays nicely.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AmountListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var label = e.Item.FindControl("IngredientsLabel") as Label;
            if (label != null)
            {
                var recipe = (Amount)e.Item.DataItem;

                var ingredient = Service.GetIngredients()
                    .Single(ct => ct.IngredientID == recipe.IngredientID);

                label.Text = ingredient.Ingredientname;
            }

        }

        /// <summary>
        /// Adds an amount to the database.
        /// <param name="amount">The recipes amounts amount-object.</param>
        /// </summary>
        public void AmountListView_InsertItem(Amount amount)
        {
            if (Page.ModelState.IsValid)
            {
                try
                {
                    amount.RecipeID = RecipeID;
                    Service.SaveAmount(amount);

                    Message = Action_Amount_Added;
                    Response.RedirectToRoute("RecipeEdit", new { id = RecipeID });
                    Context.ApplicationInstance.CompleteRequest();
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, Amount_Insert_Error);
                }
            }
        }
    }
}