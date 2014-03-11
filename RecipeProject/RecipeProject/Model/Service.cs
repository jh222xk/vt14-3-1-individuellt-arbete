using RecipeProject.Model.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RecipeProject.App_Infastructure;
using Resources;

namespace RecipeProject.Model
{
    /// <summary>
    /// The class provides methods presentation logic layer 
    /// Calls to manage data. Mainly contains the class 
    /// Methods that make use of classes in the data access layer.
    /// </summary>
    public class Service
    {
        #region Fields
        /// <summary>
        /// Errormessages for recipes and amount.
        /// </summary>
        private static readonly string Validation_Object_Error = Strings.Validation_Object_Error;

        private RecipeDAL _recipeDAL;
        private AmountDAL _amountDAL;
        private IngredientDAL _ingredientDAL;
        #endregion

        #region Properties

        public RecipeDAL RecipeDAL
        {
            // A RecipeDAL-object is created only when it's needed for the first time.
            get { return _recipeDAL ?? (_recipeDAL = new RecipeDAL()); }
        }

        public AmountDAL AmountDAL
        {
            // A AmountDAL-object is created only when it's needed for the first time.
            get { return _amountDAL ?? (_amountDAL = new AmountDAL()); }
        }

        public IngredientDAL IngredientDAL
        {
            // A IngredientDAL-object is created only when it's needed for the first time.
            get { return _ingredientDAL ?? (_ingredientDAL = new IngredientDAL()); }
        }

        #endregion

        #region Recipes
        /// <summary>
        /// Gets all the recipes stored in the database.
        /// </summary>
        /// <returns>List of references to Recipe-objects containing information about the recipes.</returns>
        public IEnumerable<Recipe> GetRecipes()
        {
            return RecipeDAL.GetRecipes();
        }

        /// <summary>
        /// Retrieving a recipe entry with a specific recipe number from the database.
        /// </summary>
        /// <param name="recipeId">The recipes Recipe Number.</param>
        /// <returns>A Recipe-object containing information about the recipes.</returns>
        public Recipe GetRecipe(int recipeId)
        {
            return RecipeDAL.GetRecipeById(recipeId);
        }

        /// <summary>
        /// Save the recipe information to the database.
        /// </summary>
        /// <param name="recipe">Recipe information that will be saved.</param>
        public void SaveRecipe(Recipe recipe, Amount amount)
        {
            ICollection<ValidationResult> validationResults;

            if (!recipe.Validate(out validationResults))
            {
                var ex = new ValidationException(Validation_Object_Error);
                ex.Data.Add("ValidationResult", validationResults);
                throw ex;
            }

            // Create a new entry if RecipeID is equal to zero.
            if (recipe.RecipeID == 0)
            {
                RecipeDAL.InsertRecipe(recipe, amount);
            }
            else
            {
                RecipeDAL.UpdateRecipe(recipe);
            }
        }

        /// <summary>
        /// Removes the specified recipe from the database.
        /// </summary>
        /// <param name="recipeId">The recipe to be deleted.</param>
        public void DeleteRecipe(int recipeId)
        {
            RecipeDAL.DeleteRecipe(recipeId);
        }
        #endregion

        #region Amount
        /// <summary>
        /// Retrieving a amount entry with a specific amount number from the database.
        /// </summary>
        /// <param name="amountId">The Amount's Number.</param>
        /// <returns>A Amount-object containing the amount.</returns>
        public Amount GetAmount(int amountId)
        {
            return AmountDAL.GetAmountById(amountId);
        }

        /// <summary>
        /// Retrieving a amount entry depending on the recipeId provided.
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns>A list containing the amount</returns>
        public List<Amount> GetAmountByRecipeId(int recipeId)
        {
            return AmountDAL.GetAmountByRecipeId(recipeId);
        }

        public Amount GetAmountByIngredientId(int ingredientId)
        {
            return AmountDAL.GetAmountByIngredientId(ingredientId);
        }

        /// <summary>
        /// Save the amount to the database.
        /// </summary>
        /// <param name="amount">The amount that will be saved.</param>
        public void SaveAmount(Amount amount)
        {
            ICollection<ValidationResult> validationResults;

            if (!amount.Validate(out validationResults))
            {
                var ex = new ValidationException(Validation_Object_Error);
                ex.Data.Add("ValidationResult", validationResults);
                throw ex;
            }

            if (amount.AmountID == 0)
            {
                AmountDAL.InsertAmount(amount);
            }
            else
            {
                AmountDAL.UpdateAmount(amount);
            }

        }

        /// <summary>
        /// Removes the specified amount from the database.
        /// </summary>
        /// <param name="amountId">The amount to be deleted.</param>
        public void DeleteAmount(int amountId)
        {
            AmountDAL.DeleteAmount(amountId);
        }

        #endregion

        #region Ingredients
        /// <summary>
        /// Gets all the ingredients stored in the database.
        /// </summary>
        /// <returns>List of references to Ingredient-objects containing the ingredients.</returns>
        public IEnumerable<Ingredients> GetIngredients()
        {
            return IngredientDAL.GetIngredients();
        }

        public Ingredients GetIngredients(int ingredientId)
        {
            return IngredientDAL.GetIngredientById(ingredientId);
        }
        #endregion
    }
}