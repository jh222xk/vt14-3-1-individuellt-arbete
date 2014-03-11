using Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace RecipeProject.Model.DAL
{
    /// <summary>
    /// Base class for handling new SqlConnection.
    /// </summary>
    public abstract class DALBase
    {
        private static string _connectionString;

        protected static readonly string GenericErrorMessage = Strings.Generic_DAL_Error_Message;

        /// <summary>
        /// Creates and initializes a new connection object.
        /// </summary>
        /// <returns>Reference to a new SqlConnection-object.</returns>
        protected SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        /// <summary>
        /// Initializes static data.
        /// </summary>
        static DALBase()
        {
            // Gets the connection string from web.config.
            _connectionString = WebConfigurationManager.ConnectionStrings["RecipeConnectionString"].ConnectionString;
        }
    }
}