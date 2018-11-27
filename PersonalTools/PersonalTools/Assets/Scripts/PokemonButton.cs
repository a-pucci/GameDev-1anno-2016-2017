using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class PokemonButton : MonoBehaviour
{
	public Button button;
	public Image icon;
	public Text pokemonName;

	//private Pokemon pokemon;

	//public event Action<Pokemon> PokemonChosen;

	// public void SetPokemon(Pokemon newPokemon)
	// {
	// 	pokemon = newPokemon;
	// 	icon.sprite = pokemon.icon;
	// 	pokemonName.text = pokemon.name;
	// 	button.onClick.AddListener(OnClick);
	// }
	//
	// private void OnClick()
	// {
	// 	if (PokemonChosen != null)
	// 	{
	// 		PokemonChosen(pokemon);
	// 	}
	// }
}