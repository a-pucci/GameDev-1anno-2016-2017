using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BattlePokemon : MonoBehaviour 
{
	#region Fields

	// Static

	// Public
	public Pokemon pokemon;
	public bool isOpponent;
	public Image image;
	public Button attackButton;
	 
	// Hidden Public
	
	// Private


	// Properties
	public float Hp { get; set; }
	public int Speed { get; set; }
	public List<PokemonTypes> Types { get; private set; } 

	// Components

	// Events
	public event Action IsExaust;

	#endregion

	#region Unity Callbacks

	private void Start()
	{
		attackButton.onClick.AddListener(OnClick);
	}

	private void OnClick()
	{
		Attack();
	}

	#endregion

	#region Methods

	public void Change(Pokemon newPokemon)
	{
		pokemon = newPokemon;
		image.sprite = isOpponent ? pokemon.front : pokemon.back;
		Hp = pokemon.hp;
		Speed = pokemon.spd;
		Types = StringToEnumTypes(pokemon.type);
	}

	public void TakeDamage(float value)
	{
		Hp -= value;
		
		if (Hp <= 0 && IsExaust != null)
		{
			IsExaust();
		}
	}

	public void Attack()
	{	int dmg = Random.Range(10, 15);
		Debug.Log(dmg);
		//return dmg;
	}

	private List<PokemonTypes> StringToEnumTypes(List<string> stringTypes)
	{
		List<PokemonTypes> newTypes = new List<PokemonTypes>();
			
		foreach (string type in stringTypes)
		{
			foreach (PokemonTypes enumType in Enum.GetValues(typeof(PokemonTypes)))
			{
				if (type == enumType.ToString())
				{
					newTypes.Add(enumType);
				}
			}
		}
		return newTypes;
	}
	

	#endregion
}
