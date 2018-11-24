using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New_Pokedex", menuName = "Pokedex/Create Pokedex")]
public class Pokedex : ScriptableObject
{
	public Pokemon[] pokemons;

	public Pokemon GetRandomPokemon()
	{
		return (pokemons[Random.Range(0, pokemons.Length)]);
	}

	[HideInPlayMode]
	[PropertyOrder(-1)]
	[Button(ButtonSizes.Medium)]
	private void FillPokedex()
	{
		Object[] pokemonsObjects = Resources.LoadAll("Pokedex", typeof(Pokemon));
		
		pokemons = new Pokemon[pokemonsObjects.Length];

		for (int i = 0; i < pokemonsObjects.Length; i++)
		{
			pokemons[i] = (Pokemon)pokemonsObjects[i];
		}
		
		Pokemon[] newPokemons = pokemons.OrderBy(o => o.id).ToArray();
		pokemons = newPokemons;
	}
	
	[HideInPlayMode]
	[Button(ButtonSizes.Medium)]
	private void ClearPokedex()
	{
		pokemons = new Pokemon[0];
	}

}
