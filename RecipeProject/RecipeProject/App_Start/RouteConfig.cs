using System.Web.Routing;

namespace RecipeProject.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            /*
             Route config for recipes.
             */

            // List routes.
            routes.MapPageRoute("Recipes", "recept", "~/Pages/RecipePages/Listing.aspx");
            routes.MapPageRoute("RecipeDetails", "recept/{id}", "~/Pages/RecipePages/Details.aspx");

            // Create, Edit and Remove routes.
            routes.MapPageRoute("RecipeCreate", "nytt/recept", "~/Pages/RecipePages/Create.aspx");
            routes.MapPageRoute("RecipeEdit", "recept/{id}/redigera", "~/Pages/RecipePages/Edit.aspx");
            routes.MapPageRoute("RecipeDelete", "recept/{id}/ta-bort", "~/Pages/RecipePages/Delete.aspx");

            // Default route.
            routes.MapPageRoute("Default", "", "~/Pages/RecipePages/Home.aspx");

            // Error route.
            routes.MapPageRoute("Error", "serverfel", "~/Pages/Shared/Error.aspx");

            /*
             Route config for ingredients. 
             */

            routes.MapPageRoute("IngredientDelete", "ingrediens/{id}/ta-bort", "~/Pages/IngredientPages/Delete.aspx");
        }
    }
}