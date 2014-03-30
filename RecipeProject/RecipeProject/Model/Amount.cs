using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecipeProject.Model
{
    /// <summary>
    /// Class for managing amounts and ingredients.
    /// </summary>
    public class Amount
    {
        public int RecipeID { get; set; }
        public int AmountID { get; set; }
        public int IngredientID { get; set; }

        [Required(ErrorMessage = "Mängden måste anges.")]
        [StringLength(25, ErrorMessage = "Mängden kan bestå av som mest 25 tecken.")]
        public string RecipeAmount { get; set; }

        //[Required(ErrorMessage = "Ingrediensen måste anges.")]
        //[StringLength(40, ErrorMessage = "Ingrediensen kan bestå av som mest 40 tecken.")]
        public string Ingredientname { get; set; }
    }
}