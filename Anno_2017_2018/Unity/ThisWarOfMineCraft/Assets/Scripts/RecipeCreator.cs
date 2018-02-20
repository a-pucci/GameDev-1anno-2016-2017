using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Xml;
using System;
using System.IO;

public static class RecipeCreator
{
    [MenuItem("Create/Recipes")]
    static void CreateRecipes()
    {
        XmlDocument xData = new XmlDocument();
        xData.Load("Assets/XML/RecipesXML.xml");

        XmlNodeList xRecipeList = xData.SelectNodes("root/recipe");

        foreach (XmlNode xRecipe in xRecipeList)
        {
            ScriptableRecipe recipeObj = new ScriptableRecipe();
            recipeObj.name = xRecipe.Attributes["name"].Value.Replace(" ", "_");
            recipeObj.description = xRecipe.Attributes["description"].Value;

            XmlNodeList xIngredients = xRecipe.SelectNodes("ingredient");

            foreach (XmlNode xIngredient in xIngredients)
            {
                RecipeIngredients recipeIngr = new RecipeIngredients();
                recipeIngr.ingredient = (Ingredients)Enum.Parse(typeof(Ingredients), xIngredient.Attributes["name"].Value.Replace(" ", "_"));
                recipeIngr.quantity = Convert.ToSingle(xIngredient.Attributes["quantity"].Value);
                recipeObj.ingredients.Add(recipeIngr);

                ScriptableIngredient scriptableIngr = AssetDatabase.LoadAssetAtPath("Assets/ScriptableObjects/Ingredients/"+ xIngredient.Attributes["name"].Value.Replace(" ", "_") + ".asset", typeof(ScriptableIngredient)) as ScriptableIngredient;

                recipeObj.value += scriptableIngr.cost * recipeIngr.quantity;
            }

            string codeText = File.ReadAllText("Assets/Scripts/Recipes.cs");
            int markerIndex = codeText.LastIndexOf("//#RECIPE#");
            File.WriteAllText("Assets/Scripts/Recipes.cs", codeText.Insert(markerIndex, recipeObj.name + ",\n\t"));

            AssetDatabase.CreateAsset(recipeObj, "Assets/ScriptableObjects/Recipes/" + recipeObj.name + ".asset");
            AssetDatabase.Refresh();
        }
    }
}
