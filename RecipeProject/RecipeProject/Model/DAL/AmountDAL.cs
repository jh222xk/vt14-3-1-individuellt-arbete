using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RecipeProject.Model.DAL
{
    /// <summary>
    /// Class for managing amount data.
    /// </summary>
    public class AmountDAL : DALBase
    {

        public List<Amount> GetAmountByRecipeId(int recipeId)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("app.uspGetAmountByRecipeId", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@RecipeID", SqlDbType.Int, 4).Value = recipeId;

                    var amount = new List<Amount>(10);

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var recipeIdIndex = reader.GetOrdinal("RecipeID");
                        var amountIdIndex = reader.GetOrdinal("AmountID");
                        var ingredientIdIndex = reader.GetOrdinal("IngredientID");
                        var amountIndex = reader.GetOrdinal("Amount");
                        var ingredientIndex = reader.GetOrdinal("Ingredientname");

                        while (reader.Read())
                        {
                            amount.Add(new Amount
                            {
                                RecipeID = reader.GetInt32(recipeIdIndex),
                                AmountID = reader.GetInt32(amountIdIndex),
                                IngredientID = reader.GetInt32(ingredientIdIndex),
                                RecipeAmount = reader.GetString(amountIndex),
                                Ingredientname = reader.GetString(ingredientIndex)
                            });
                        }
                    }

                    amount.TrimExcess();

                    return amount;
                }
                catch (Exception)
                {
                    throw new ApplicationException(GenericErrorMessage);
                }

            }
        }

        public Amount GetAmountByIngredientId(int ingredientId)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("app.uspGetAmountByIngredientId", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IngredientID", SqlDbType.Int, 4).Value = ingredientId;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var recipeIdIndex = reader.GetOrdinal("RecipeID");
                        var amountIdIndex = reader.GetOrdinal("AmountID");
                        var ingredientIdIndex = reader.GetOrdinal("IngredientID");
                        var amountIndex = reader.GetOrdinal("Amount");

                        while (reader.Read())
                        {
                            return new Amount
                            {
                                RecipeID = reader.GetInt32(recipeIdIndex),
                                AmountID = reader.GetInt32(amountIdIndex),
                                IngredientID = reader.GetInt32(ingredientIdIndex),
                                RecipeAmount = reader.GetString(amountIndex)
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
        /// Retrieving a contact entry with a specific contact number from the database.
        /// </summary>
        /// <param name="contactId">Contact Information's Contact Number.</param>
        /// <returns>A Contact-object containing contact information.</returns>
        public Amount GetAmountById(int amountId)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("app.uspGetAmount", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@AmountID", SqlDbType.Int, 4).Value = amountId;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var amountIdIndex = reader.GetOrdinal("AmountID");
                        var amountIndex = reader.GetOrdinal("Amount");
                        var ingredientIndex = reader.GetOrdinal("Ingredientname");

                        if (reader.Read())
                        {
                            return new Amount
                            {
                                AmountID = reader.GetInt32(amountIdIndex),
                                RecipeAmount = reader.GetString(amountIndex),
                                Ingredientname = reader.GetString(ingredientIndex)
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

        public void UpdateAmount(Amount amount)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("app.uspUpdateAmount", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@AmountID", SqlDbType.Int, 4).Value = amount.AmountID;
                    cmd.Parameters.Add("@Amount", SqlDbType.VarChar, 50).Value = amount.RecipeAmount;
                    cmd.Parameters.Add("@Ingredientname", SqlDbType.VarChar, 50).Value = amount.Ingredientname;

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
        /// Creates a new entry in the table Contact.
        /// </summary>
        /// <param name="amount">Contact information that will be saved.</param>
        public void InsertAmount(Amount amount)
        {
            if (amount == null) { throw new ArgumentNullException("Får ej vara null..."); }

            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("app.uspAddIngredient", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Ingredientname", SqlDbType.VarChar, 50).Value = amount.Ingredientname;
                    cmd.Parameters.Add("@Amount", SqlDbType.VarChar, 50).Value = amount.RecipeAmount;
                    cmd.Parameters.Add("@RecipeID", SqlDbType.VarChar, 50).Value = amount.RecipeID;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    //amount.AmountID = (int)cmd.Parameters["@AmountID"].Value;
                }
                catch (Exception)
                {
                    throw new ApplicationException(GenericErrorMessage);
                }
            }

        }

        public void DeleteAmount(int amountId)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("app.uspDeleteIngredient", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IngredientID", SqlDbType.Int, 4).Value = amountId;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException(GenericErrorMessage);
                }
            }
        }
    }
}