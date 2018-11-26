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
	public event Action<Pokemon> IsExhaust;
	public event Action Attack;

	#endregion

	#region Unity Callbacks

	private void Start()
	{
		attackButton.onClick.AddListener(OnClick);
	}

	private void OnClick()
	{
		if (Attack != null)
		{
			Attack();
		}
	}

	#endregion

	#region Methods

	public void Change(Pokemon newPokemon)
	{
		pokemon = newPokemon;
		image.sprite = isOpponent ? pokemon.front : pokemon.back;
		Hp = pokemon.hp;
		Speed = pokemon.spd;
		Types = pokemon.types;
	}

	public void TakeDamage(float value)
	{
		Hp -= value;
		
		if (Hp <= 0 && IsExhaust != null)
		{
			IsExhaust(pokemon);
		}
	}

	public int AttackEnemy()
	{
		return Random.Range(10, 20);
	}

	public PokemonTypes GetPokemonType()
	{
		return Types[Random.Range(0, Types.Count)];
	}

	#endregion
}
