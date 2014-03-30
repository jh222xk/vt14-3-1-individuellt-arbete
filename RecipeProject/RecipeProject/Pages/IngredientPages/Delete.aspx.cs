using RecipeProject.Model;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RecipeProject.Pages.IngredientPages
{
    public partial class Delete : System.Web.UI.Page
    {
        /// <summary>
        /// Errormessages for amount.
        /// </summary>
        private static readonly string Amount_Not_Found = Strings.Amount_Not_Found;
        private static readonly string Amount_Selecting_Error = Strings.Amount_Selecting_Error;
        private static readonly string Action_Amount_Deleted = Strings.Action_Amount_Deleted;
        private static readonly string Amount_Deleting_Error = Strings.Amount_Deleting_Error;

        /// <summary>
        /// Property for returning the routedata's value (id).
        /// </summary>
        public int Id
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

        private Amount _amount;
        private Amount Amount
        {
            get { return _amount ?? (_amount = Service.GetAmount(Id)); }
        }

        private Ingredients _ingredients;
        private Ingredients Ingredients
        {
            get { return _ingredients ?? (_ingredients = Service.GetIngredients(Id)); }
        }

        private Recipe _recipe;
        private Recipe Recipe
        {
            get { return _recipe ?? (_recipe = Service.GetRecipe(Amount.RecipeID)); }
        }

        /// <summary>
        /// Hook up the cancel link url, set text and so on.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            CancelHyperLink.NavigateUrl = GetRouteUrl("RecipeDetails", new { id = Amount.RecipeID });

            if (!IsPostBack)
            {
                try
                {
                    if (Recipe != null)
                    {
                        IngredientValue.Text = Amount.Ingredientname;
                        Recipename.Text = Recipe.Recipename;
                        return;
                    }

                    ModelState.AddModelError(String.Empty,
                        String.Format(Strings.Amount_Not_Found, Id));
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, Amount_Selecting_Error);
                }

                ConfirmationPlaceHolder.Visible = false;
                DeleteLinkButton.Visible = false;
            }
        }

        /// <summary>
        /// Hook up the delete button!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DeleteLinkButton_Command(object sender, CommandEventArgs e)
        {
            try
            {
                var id = int.Parse(e.CommandArgument.ToString());
                Service.DeleteAmount(id);

                Message = Action_Amount_Deleted;
                Response.RedirectToRoute("RecipeEdit", new { id = Amount.RecipeID });
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, Amount_Deleting_Error);
            }
        }
    }
}