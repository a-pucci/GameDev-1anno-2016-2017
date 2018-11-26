using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleRoster : MonoBehaviour 
{
	#region Fields

	// Static

	// Public
	public PokemonButton buttonPrefab;
	// Hidden Public

	// Private
	private List<Image> pokemonIcons;
	
	// Properties

	// Components

	// Events
	public event Action<Pokemon> PokemonChosen;

	#endregion

	#region Unity Callbacks

	#endregion

	#region Methods

	public void CreateRoster(List<Pokemon> pokemons)
	{
		foreach (Pokemon pokemon in pokemons)
		{
			PokemonButton buttonInstance = Instantiate(buttonPrefab, transform);
			buttonInstance.SetPokemon((pokemon));
			buttonInstance.PokemonChosen += Chosen;
		}
	}

	private void Chosen(Pokemon pokemon)
	{
		if (PokemonChosen != null)
		{
			PokemonChosen(pokemon);
		}
		Destroy(gameObject);
	}
	
	#endregion
}