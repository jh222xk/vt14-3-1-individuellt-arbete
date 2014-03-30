using RecipeProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;

namespace RecipeProject.Pages.RecipePages
{
    public partial class Create : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Unused...
        }

        /// <summary>
        /// Errormessages for recipes and amount.
        /// </summary>
        private static readonly string Action_Recipe_Added = Strings.Action_Recipe_Added;
        private static readonly string Recipe_Insert_Error = Strings.Recipe_Insert_Error;
        private static readonly string Amount_Insert_Error = Strings.Amount_Insert_Error;

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
        /// Adds an amount to the database.
        /// </summary>
        /// <param name="amount">A amount-object</param>
        /*public void AmountFormView_InsertItem(Amount amount)
        {
            if (Page.ModelState.IsValid)
            {
                try
                {
                    amount.RecipeID = RecipeID;
                    Service.SaveAmount(amount);
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, Amount_Insert_Error);
                }
            }
        }*/

        /// <summary>
        /// Adds an recipe and amount to the database.
        /// </summary>
        /// <param name="recipe">A recipe-object</param>
        /// <param name="amount">A amount-object</param>
        public void RecipeFormView_InsertItem(Recipe recipe, Amount amount)
        {
            if (Page.ModelState.IsValid)
            {
                try
                {
                    Service.SaveRecipe(recipe, amount);

                    Message = Action_Recipe_Added;
                    Response.RedirectToRoute("RecipeDetails", new { id = recipe.RecipeID });
                    Context.ApplicationInstance.CompleteRequest();
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, Recipe_Insert_Error);
                }
            }
        }

        public IEnumerable<Ingredients> IngredientsDropDownList_GetData()
        {
            return Service.GetIngredients();
        }
    }
}