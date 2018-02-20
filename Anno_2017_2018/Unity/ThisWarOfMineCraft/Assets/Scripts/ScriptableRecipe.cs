using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScriptableRecipe : ScriptableObject
{
    public string name;
    [TextArea] public string description;
    public List<RecipeIngredients> ingredients = new List<RecipeIngredients>();
    public float value = 0f;
}

public struct RecipeIngredients
{
    public Ingredients ingredient;
    public float quantity;
}
