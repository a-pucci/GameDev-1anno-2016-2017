using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Xml;
using System;
using System.IO;


public static class IngredientCreator
{

    [MenuItem("Create/Ingredients")]
    static void CreateIngredients()
    {
        XmlDocument xData = new XmlDocument();
        xData.Load("Assets/XML/IngredientsXML.xml");

        XmlNodeList xIngredientList = xData.SelectNodes("root/ingredient");

        foreach(XmlNode xIngredient in xIngredientList)
        {
            ScriptableIngredient ingredientObj = new ScriptableIngredient();
            ingredientObj.name = xIngredient.Attributes["name"].Value.Replace(" ", "_");
            ingredientObj.cost = Convert.ToSingle(xIngredient.Attributes["cost"].Value);
            ingredientObj.description = xIngredient.Attributes["description"].Value;

            string codeText = File.ReadAllText("Assets/Scripts/Ingredients.cs");
            int markerIndex = codeText.LastIndexOf("//#INGREDIENT#");
            File.WriteAllText("Assets/Scripts/Ingredients.cs", codeText.Insert(markerIndex, ingredientObj.name + ",\n\t"));

            AssetDatabase.CreateAsset(ingredientObj, "Assets/ScriptableObjects/Ingredients/" + ingredientObj.name + ".asset");
            AssetDatabase.Refresh();
        }

    }


}
