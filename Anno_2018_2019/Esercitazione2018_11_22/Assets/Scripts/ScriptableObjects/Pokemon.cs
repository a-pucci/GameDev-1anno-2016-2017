using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New_Pokemon", menuName = "Pokedex/Create Pokemon")]
public class Pokemon : ScriptableObject
{
	[HideLabel]public new string name;

	[HorizontalGroup][HideLabel][PreviewField] public Sprite front;
	[HorizontalGroup][HideLabel][PreviewField] public Sprite back;
	[HorizontalGroup][HideLabel][PreviewField] public Sprite icon;
	
	public int id;

	[Space(20)]
	public List<string> type = new List<string>();

	[Space(20)]
	[MultiLineProperty]
	public string description;

	[Space(20)]
	[Title("Stats")]
	public int hp;
	public int spd;

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