using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecipeProject.Model
{
    /// <summary>
    /// Class for managing ingredients.
    /// </summary>
    public class Ingredients
    {
        public int IngredientID { get; set; }

        [Required(ErrorMessage = "Ingrediensen måste anges.")]
        [StringLength(40, ErrorMessage = "Ingrediensen kan bestå av som mest 40 tecken.")]
        public string Ingredientname { get; set; }
    }
}