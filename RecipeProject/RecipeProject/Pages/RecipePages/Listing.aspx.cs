using RecipeProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RecipeProject.Pages.RecipePages
{
    public partial class Listing : System.Web.UI.Page
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
        /// Gets all the recipes stored in the database.
        /// </summary>
        public IEnumerable<Recipe> RecipeListView_GetData()
        {
            return Service.GetRecipes();
        }
    }
}