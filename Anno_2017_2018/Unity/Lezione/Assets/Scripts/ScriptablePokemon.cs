using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_Pokemon", menuName = "Pokedex/ScriptablePokemon")]
public class ScriptablePokemon : ScriptableObject
{
    public Sprite sprite;
    public int id;
    public string name;

    [Space(20)]
    public List<string> type = new List<string>();

    [Space(20)]
    [Header("Stats")]
    public int HP;
    public int ATK;
    public int DEF;
    public int SPD;
    public int SAT;
    public int SDF;

    [Space(20)]
    [Header("Evolutions")]
    public List<Evolution> evolutions = new List<Evolution>();

    [Space(20)]
    public string height;
    public string weight;

    [Space(20)]
    [TextArea]
    public string description;
}

[System.Serializable]
public class Evolution
{
    public int id;
    public string name;
    public int lvl;
}

