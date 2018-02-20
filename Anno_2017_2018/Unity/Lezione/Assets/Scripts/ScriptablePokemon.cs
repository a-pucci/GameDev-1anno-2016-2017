using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New_Pokemon", menuName = "Pokedex/ScriptablePokemon")]
public class ScriptablePokemon : ScriptableObject
{
    [VerticalGroup]
    [HideLabel]
    public string name;
    [HideLabel]
    [VerticalGroup]
    [PreviewField] public Sprite sprite;
    public int id;

    [Space(20)]
    public List<string> type = new List<string>();

    [Space(20)]
    [MultiLineProperty]
    public string description;

    [Space(20)]
    [Title("Stats")]
    public int HP;
    public int ATK;
    public int DEF;
    public int SPD;
    public int SAT;
    public int SDF;

    [Space(20)]
    [Title("Physical Attributes")]
    public string height;
    public string weight;

    [Space(20)]
    [Header("Evolutions")]
    public List<Evolution> evolutions = new List<Evolution>();

}

[System.Serializable]
public class Evolution
{
    public int id;
    public string name;
    public int lvl;
}

