using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecipeProject.Model
{
    /// <summary>
    /// Class for managing recipes.
    /// </summary>
    public class Recipe
    {
        // Recipe in the database

        public int RecipeID { get; set; }

        [Required(ErrorMessage = "Ett namn måste anges.")]
        [StringLength(60, ErrorMessage = "Namnet kan bestå av som mest 60 tecken.")]
        public string Recipename { get; set; }

        [Required(ErrorMessage = "En beskrivning måste anges.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "En instruktion måste anges.")]
        public string Instruction { get; set; }
    }
}