using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RecipeProject.Model.DAL
{
    /// <summary>
    /// Class for managing recipe data.
    /// </summary>
    public class RecipeDAL : DALBase
    {

        private const int MAX_LENGTH_VARCHAR = -1;

        /// <summary>
        /// Gets all the recipes stored in the database.
        /// </summary>
        /// <returns>List of references to Recipe-objects containing information about the recipes.</returns>
        public IEnumerable<Recipe> GetRecipes()
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var recipes = new List<Recipe>(100);

                    var cmd = new SqlCommand("app.uspGetRecipes", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var recipeIdIndex = reader.GetOrdinal("RecipeID");
                        var recipeNameIndex = reader.GetOrdinal("Recipename");
                        var recipeDescriptionIndex = reader.GetOrdinal("Description");
                        var recipeInstructionIndex = reader.GetOrdinal("Instruction");

                        while (reader.Read())
                        {
                            recipes.Add(new Recipe
                            {
                                RecipeID = reader.GetInt32(recipeIdIndex),
                                Recipename = reader.GetString(recipeNameIndex),
                                Description = reader.GetString(recipeDescriptionIndex),
                                Instruction = reader.GetString(recipeInstructionIndex)
                            });
                        }
                    }

                    recipes.TrimExcess();

                    return recipes;
                }
                catch (Exception)
                {
                    throw new ApplicationException(GenericErrorMessage);
                }
            }
        }

        /// <summary>
        /// Retrieving a recipe entry with a specific recipe number from the database.
        /// </summary>
        /// <param name="recipeId">The recipe number</param>
        /// <returns>A Recipe-object containing information about the recipes.</returns>
        public Recipe GetRecipeById(int recipeId)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("app.uspGetRecipes", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@RecipeID", SqlDbType.Int, 4).Value = recipeId;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var recipeIdIndex = reader.GetOrdinal("RecipeID");
                        var recipeNameIndex = reader.GetOrdinal("Recipename");
                        var recipeDescriptionIndex = reader.GetOrdinal("Description");
                        var recipeInstructionIndex = reader.GetOrdinal("Instruction");

                        if (reader.Read())
                        {
                            return new Recipe
                            {
                                RecipeID = reader.GetInt32(recipeIdIndex),
                                Recipename = reader.GetString(recipeNameIndex),
                                Description = reader.GetString(recipeDescriptionIndex),
                                Instruction = reader.GetString(recipeInstructionIndex)
                            };
                        }
                    }

                    return null;
                }
                catch (Exception)
                {
                    throw new ApplicationException(GenericErrorMessage);
                }
            }
        }

        /// <summary>
        /// Creates a new entry in the table Recipe.
        /// </summary>
        /// <param name="recipe">Recipe information that will be saved.</param>
        public void InsertRecipe(Recipe recipe, Amount amount)
        {
            /*
                @Ingredientname varchar(25),
                @Recipename varchar(60),
                @Description varchar(MAX),
                @Instruction varchar(MAX),
                @Amount varchar(25)
            */

            if (recipe == null) { throw new ArgumentNullException("Får ej vara null..."); }

            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("app.uspAddRecipe", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Ingredientname", SqlDbType.VarChar, 25).Value = amount.Ingredientname;
                    cmd.Parameters.Add("@Recipename", SqlDbType.VarChar, 50).Value = recipe.Recipename;
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar, 50).Value = recipe.Description;
                    cmd.Parameters.Add("@Instruction", SqlDbType.VarChar, 50).Value = recipe.Instruction;
                    cmd.Parameters.Add("@Amount", SqlDbType.VarChar, 50).Value = amount.RecipeAmount;

                    cmd.Parameters.Add("@RecipeID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    recipe.RecipeID = (int)cmd.Parameters["@RecipeID"].Value;
                }
                catch (Exception)
                {
                    throw new ApplicationException(GenericErrorMessage);
                }
            }

        }

        /// <summary>
        /// Updating recipe information in the table Recipe.
        /// </summary>
        /// <param name="recipe">Recipe information that will be saved.</param>
        public void UpdateRecipe(Recipe recipe)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("app.uspUpdateRecipe", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@RecipeID", SqlDbType.Int, 4).Value = recipe.RecipeID;
                    cmd.Parameters.Add("@Recipename", SqlDbType.VarChar, 60).Value = recipe.Recipename;
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar, MAX_LENGTH_VARCHAR).Value = recipe.Description;
                    cmd.Parameters.Add("@Instruction", SqlDbType.VarChar, MAX_LENGTH_VARCHAR).Value = recipe.Instruction;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw new ApplicationException(GenericErrorMessage);
                }
            }
        }

        /// <summary>
        /// Removes the specified recipe from the database.
        /// </summary>
        /// <param name="recipeId">The recipe to be deleted.</param>
        public void DeleteRecipe(int recipeId)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("app.uspDeleteRecipe", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@RecipeID", SqlDbType.Int, 4).Value = recipeId;
                    cmd.Parameters["@RecipeID"].Value = recipeId;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw new ApplicationException(GenericErrorMessage);
                }

            }
        }

    }
}