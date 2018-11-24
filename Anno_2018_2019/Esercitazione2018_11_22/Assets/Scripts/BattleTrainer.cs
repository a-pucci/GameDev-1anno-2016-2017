using System;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class BattleTrainer : MonoBehaviour 
{
	#region Fields

	// Static

	// Public
	public Trainer trainer;
	public BattlePokemon battlePokemon;
	public BattleRoster battleRosterPrefab;
	public Button ChangeButton;
	
	// Hidden Public
	[HideInInspector] public bool isOpponent;
	
	// Private
	private Image image;
	private List<Pokemon> roster;
	
	// Properties

	// Components

	// Events
	public Action<bool> RosterEmpty;

	#endregion

	#region Unity Callbacks

	private void Start()
	{
		roster = trainer.battleRoster;
		image = GetComponent<Image>();
		image.sprite = isOpponent ? trainer.front : trainer.back;
		
		battlePokemon.isOpponent = isOpponent;
		
		UsePokemon(roster[0]);
		
		battlePokemon.IsExaust += ChangePokemon;
		ChangeButton.onClick.AddListener(OnClick);
	}

	private void OnClick()
	{
		ChangePokemon();
	}

	#endregion

	#region Methods

	[Button]
	private void ChangePokemon()
	{
		RemoveExaustedPokemons();
		
		if (roster.IsNullOrEmpty())
		{
			if (RosterEmpty != null)
			{
				RosterEmpty(isOpponent);
			}
		}
		else
		{
			BattleRoster rosterInstance = Instantiate(battleRosterPrefab, transform.parent);
			rosterInstance.CreateRoster(roster);
			rosterInstance.PokemonChosen += UsePokemon;
		}
	}

	private void RemoveExaustedPokemons()
	{
		foreach (Pokemon pokemon in roster)
		{
			if (pokemon.hp <= 0)
			{
				roster.Remove(pokemon);
			}
		}
	}

	private void UsePokemon(Pokemon pokemon)
	{
		battlePokemon.Change(pokemon);
	}

	#endregion
}
