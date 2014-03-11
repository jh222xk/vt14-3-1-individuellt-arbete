using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RecipeProject.Model.DAL
{
    public class IngredientDAL : DALBase
    {
        /// <summary>
        /// Gets all the recipes stored in the database.
        /// </summary>
        /// <returns>List of references to Recipe-objects containing information about the recipes.</returns>
        public IEnumerable<Ingredients> GetIngredients()
        {
            using (var conn = CreateConnection())
            {

                    var ingredients = new List<Ingredients>(10);

                    var cmd = new SqlCommand("app.uspGetIngredients", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var ingredientIdIndex = reader.GetOrdinal("IngredientID");
                        var ingredientnameIndex = reader.GetOrdinal("Ingredientname");

                        while (reader.Read())
                        {
                            ingredients.Add(new Ingredients
                            {
                                IngredientID = reader.GetInt32(ingredientIdIndex),
                                Ingredientname = reader.GetString(ingredientnameIndex)
                            });
                        }
                    }

                    ingredients.TrimExcess();

                    return ingredients;

            }
        }


        public Ingredients GetIngredientById(int ingredientId)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("app.uspGetIngredient", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IngredientID", SqlDbType.Int, 4).Value = ingredientId;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var ingredientIdIndex = reader.GetOrdinal("IngredientID");
                        var ingredientnameIndex = reader.GetOrdinal("Ingredientname");

                        if (reader.Read())
                        {
                            return new Ingredients
                            {
                                IngredientID = reader.GetInt32(ingredientIdIndex),
                                Ingredientname = reader.GetString(ingredientnameIndex)
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
    }
}