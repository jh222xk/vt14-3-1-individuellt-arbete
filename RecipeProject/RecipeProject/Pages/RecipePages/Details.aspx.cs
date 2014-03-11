using RecipeProject.Model;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RecipeProject.Pages.RecipePages
{
    public partial class Details : System.Web.UI.Page
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
        /// Errormessages for recipes.
        /// </summary>
        private static readonly string Recipe_Selecting_Error = Strings.Recipe_Selecting_Error;

        /// <summary>
        /// Property for returning the routedata's value (id).
        /// </summary>
        public int RecipeID
        {
            // Return the value only if there's any to get...
            get
            {
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
                var recipe = Service.GetRecipe(RecipeID);
                // Replace the \r\n with <br /> tags.
                recipe.Instruction = recipe.Instruction.Replace("\r\n", "<br />");
                return recipe;
            }
            catch (Exception)
            {
                Page.ModelState.AddModelError(String.Empty, Recipe_Selecting_Error);
                return null;
            }
        }

        /// <summary>
        /// Gets the amount depending on the RecipeID for that particular recipe.
        /// </summary>
        public IEnumerable<Amount> AmountListView_GetData()
        {
            return Service.GetAmountByRecipeId(RecipeID);
        }

        /// <summary>
        /// Gets all the ingredients.
        /// </summary>
        public IEnumerable<Ingredients> IngredientsListView_GetData()
        {
            return Service.GetIngredients();
        }

        protected void AmountListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var label = e.Item.FindControl("ContactTypeNameLabel") as Label;
            var span = e.Item.FindControl("IngredientsLabel") as Label;
            if (label != null)
            {
                var recipe = (Amount)e.Item.DataItem;

                var ingredient = Service.GetIngredients()
                    .Single(ct => ct.IngredientID == recipe.IngredientID);

                span.Text = ingredient.Ingredientname;
            }
        }

    }
}