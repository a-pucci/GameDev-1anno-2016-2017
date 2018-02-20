using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableIngredient : ScriptableObject
{
    public string name;
    public float cost;
    [TextArea] public string description;
}
