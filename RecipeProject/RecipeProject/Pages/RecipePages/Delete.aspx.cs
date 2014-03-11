using RecipeProject.Model;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RecipeProject.Pages.RecipePages
{
    public partial class Delete : System.Web.UI.Page
    {
        /// <summary>
        /// Errormessages for recipes.
        /// </summary>
        private static readonly string Recipe_Not_Found = Strings.Recipe_Not_Found;
        private static readonly string Recipe_Deleting_Error = Strings.Recipe_Deleting_Error;
        private static readonly string Recipe_Selecting_Error = Strings.Recipe_Selecting_Error;
        private static readonly string Action_Recipe_Deleted = Strings.Action_Recipe_Deleted;

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
        /// Hook up the cancel link url, set text and so on.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            CancelHyperLink.NavigateUrl = GetRouteUrl("RecipeDetails", new { id = RecipeID });

            if (!IsPostBack)
            {
                try
                {
                    var recipe = Service.GetRecipe(RecipeID);
                    if (recipe != null)
                    {
                        ReciepName.Text = recipe.Recipename;
                        return;
                    }

                    ModelState.AddModelError(String.Empty,
                        String.Format(Recipe_Not_Found, RecipeID));
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, Recipe_Selecting_Error);
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
                Service.DeleteRecipe(RecipeID);

                Message = Action_Recipe_Deleted;
                Response.RedirectToRoute("Recipes", null);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, Recipe_Deleting_Error);
            }
        }
    }
}