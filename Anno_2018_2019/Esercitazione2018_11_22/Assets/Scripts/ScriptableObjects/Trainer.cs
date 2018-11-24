using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "New_Trainer", menuName = "Pokedex/Create Trainer")]
public class Trainer : ScriptableObject
{
	private const int RosterSize = 6;
	
	[VerticalGroup][PreviewField] public Sprite front;
	[VerticalGroup][PreviewField] public Sprite back;
	
	public Pokedex pokedex;
	[PropertyOrder(1)] public List<Pokemon> battleRoster = new Pokemon[RosterSize].ToList();

	[Button(ButtonSizes.Medium)]
	public void ClearRoster()
	{
		battleRoster = new Pokemon[RosterSize].ToList();
	}
	
	[Button(ButtonSizes.Medium)]
	public void FillRoster()
	{
		for (int i = 0; i < battleRoster.Count; i++)
		{
			if (battleRoster[i] == null)
			{
				battleRoster[i] = pokedex.GetRandomPokemon();
			}
		}
	}
}
