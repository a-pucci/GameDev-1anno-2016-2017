using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New_Pokemon", menuName = "Pokedex/Pokemon")]
public class Pokemon : ScriptableObject
{
    public string name;
    public int id;
    [TextArea] public string description;
    public bool isShiny;
    public Sprite sprite;
}