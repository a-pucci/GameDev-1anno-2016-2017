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
	public Button changeButton;
	
	// Hidden Public
	[HideInInspector] public bool isOpponent;
	[HideInInspector] public bool isMyTurn;
	
	// Private
	private Image image;
	private List<Pokemon> roster;
	private bool pokemonExhaust;
	
	// Properties

	// Components

	// Events
	public event Action<string> RosterEmpty;
	public event Action<Actions> ActionTaken;
	public event Action PokemonChanged;

	#endregion

	#region Unity Callbacks

	private void Start()
	{
		roster = new List<Pokemon>(trainer.battleRoster);
		image = GetComponent<Image>();
		image.sprite = isOpponent ? trainer.front : trainer.back;
		
		battlePokemon.isOpponent = isOpponent;
		
		UsePokemon(roster[0]);
		
		battlePokemon.IsExhaust += ChangeExhaustedPokemon;
		battlePokemon.Attack += Attack;
		changeButton.onClick.AddListener(OnClick);
	}

	private void OnClick()
	{
		if (isMyTurn)
		{
			ChangePokemon();
		}
	}

	#endregion

	#region Methods

	private void ChangePokemon()
	{
		BattleRoster rosterInstance = Instantiate(battleRosterPrefab, transform.parent);
		rosterInstance.CreateRoster(roster);
		rosterInstance.PokemonChosen += UsePokemon;
	}

	private void ChangeExhaustedPokemon(Pokemon exhaustedPokemon)
	{
		pokemonExhaust = true;
		RemoveExhaustedPokemon(exhaustedPokemon);
		
		if (roster.Count == 0)
		{
			
			if (RosterEmpty != null)
			{
				RosterEmpty(trainer.name);
			}
		}
		else
		{
			ChangePokemon();
		}
	}

	private void Attack()
	{
		if (ActionTaken != null && isMyTurn)
		{
			ActionTaken(Actions.AttackEnemy);
		}
	}

	private void RemoveExhaustedPokemon(Pokemon exhaustedPokemon)
	{
		roster.Remove(exhaustedPokemon);
	}

	private void UsePokemon(Pokemon pokemon)
	{
		battlePokemon.Change(pokemon);
		if (ActionTaken != null && !pokemonExhaust)
		{
			ActionTaken(Actions.PokemonChanged);
		}
		else if(PokemonChanged != null)
		{
			PokemonChanged();
		}
		pokemonExhaust = false;
	}

	#endregion
}
