using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BattleManager : MonoBehaviour 
{
	#region Fields

	// Static

	// Public
	[ToggleLeft]
	public bool twoPlayers;
	
	public BattleTrainer playerOne;
	[ShowIf("twoPlayers")] public BattleTrainer playerTwo;

	public BattleTrainer opponentOne;
	[ShowIf("twoPlayers")] public BattleTrainer opponentTwo;

	public TypeChart typeChart;
	public Speaker speaker;

	public List<GameObject> trainersBgs;
	public GameObject whiteScreen;

	// Private
	private List<BattleTrainer> trainers = new List<BattleTrainer>();
	private int trainersIndex;
	private List<Actions> turnActions = new List<Actions>();
	private List<BattlePokemon> fightingPokemons = new List<BattlePokemon>();
	private int resolveIndex;
	private bool battleEnd;

	#endregion

	#region Unity Callbacks

	private void Start()
	{
		speaker.SpeakEnded += ResolveTurn;
		
		SetTrainers();
		ChangeTurn();
	}

	#endregion
	
	#region Methods
	
	private void ChangeTurn()
	{
		if (trainersIndex > 0)
		{
			trainers[trainersIndex-1].isMyTurn = false;
			trainersBgs[trainersIndex-1].SetActive(false);
		}
		if (trainersIndex == trainers.Count)
		{
			PrepareToResolveTurn();
			ResolveTurn();
			trainersIndex = -1;
		}
		else
		{
			trainers[trainersIndex].isMyTurn = true;
			trainersBgs[trainersIndex].SetActive(true);

			speaker.Speak(trainers[trainersIndex].isOpponent ? "Turno dell'avversario" : "Turno dell'allenatore");
		}

		trainersIndex++;
	}

	private void AddTrainerAction(Actions trainerAction)
	{
		turnActions[trainersIndex-1] = trainerAction;
	
		fightingPokemons.Add(trainers[trainersIndex - 1].battlePokemon);
			
		ChangeTurn();
	}

	private void PrepareToResolveTurn()
	{
		if (turnActions[0] == Actions.AttackEnemy && turnActions[1] == Actions.AttackEnemy)
		{
			if (trainers[0].battlePokemon.Speed < trainers[1].battlePokemon.Speed)
			{
				fightingPokemons = SwapListElements(fightingPokemons, 0, 1);
			}
		}
		if (twoPlayers)
		{
			if (turnActions[2] == Actions.AttackEnemy && turnActions[3] == Actions.AttackEnemy)
			{
				if (trainers[2].battlePokemon.Speed < trainers[3].battlePokemon.Speed)
				{
					fightingPokemons = SwapListElements(fightingPokemons, 2, 3);
				}
			}
		}
		
	}

	private List<T> SwapListElements<T>(List<T> listToSwap, int firstIndex, int secondIndex)
	{
		T temp = listToSwap[firstIndex];
		listToSwap[firstIndex] = listToSwap[secondIndex];
		listToSwap[secondIndex] = temp;
		return listToSwap;
	}
	
	private void ResolveTurn()
	{
		if (!battleEnd)
		{
			if(resolveIndex < turnActions.Count)
			{
				Actions action = turnActions[resolveIndex];

				switch (action) 
				{
					case Actions.AttackEnemy:
						Fight(fightingPokemons[resolveIndex], resolveIndex % 2 == 0 ? fightingPokemons[resolveIndex + 1] : fightingPokemons[resolveIndex - 1]);
						break;
				
					case Actions.PokemonChanged:
						speaker.Speak("L'Allenatore " + (resolveIndex+1) + " ha cambiato Pokemon!", true);
						break;
				
					default:
						speaker.Speak("L'Allenatore " + (resolveIndex+1) + " ha cambiato Pokemon!", true);
						break;
				}
				resolveIndex++;
			}
			else
			{
				resolveIndex = 0;
				ChangeTurn();
				fightingPokemons.Clear();
			}
		}
	}

	private void SetTrainers()
	{
		playerOne.isOpponent = false;
		trainers.Add(playerOne);

		opponentOne.isOpponent = true;
		trainers.Add(opponentOne);
		
		if (twoPlayers)
		{
			playerTwo.isOpponent = false;
			trainers.Add(playerTwo);
			
			opponentTwo.isOpponent = true;
			trainers.Add(opponentTwo);
		}
		else
		{
			playerTwo.gameObject.SetActive(false);
			opponentTwo.gameObject.SetActive(false);
		}

		foreach (BattleTrainer trainer in trainers)
		{
			trainer.RosterEmpty += BattleEnded;
			trainer.ActionTaken += AddTrainerAction;
			trainer.PokemonChanged += ChangeTurn;
		}

		turnActions = new List<Actions>(new Actions[trainers.Count]);
	}

	private void BattleEnded(string loser)
	{
		battleEnd = true;
		whiteScreen.SetActive(true);
		speaker.Speak(loser.ToUpper() + " non ha più pokemon e ha perso!");
	}

	private void Fight(BattlePokemon attacker, BattlePokemon defender)
	{
		float typeMultiplier = typeChart.AttackEffect(attacker.GetPokemonType(), defender.GetPokemonType());
		float damage = attacker.AttackEnemy() * typeMultiplier;
		
		defender.TakeDamage(damage);

		if (defender.Hp > 0)
		{
			string text = attacker.pokemon.name + " infligge " + damage + " danni a "+ defender.pokemon.name + ".\nGli rimangono " + defender.Hp + " HP.\n";

			switch (typeMultiplier.ToString())
			{
				case "0":
					text = text + "L'attacco non ha avuto effetto!";
					break;
			
				case "0.5":
					text = text +  "L'attacco non ha molto effetto!";
					break;
			
				case "1":
					text = text +  "Attacco normale!";
					break;
				
				case "2":
					text = text + "L'attacco è super efficace!";
					break;
			}
		
			speaker.Speak(text, true);
		}
	}

	#endregion
}


public enum Actions
{
	PokemonChanged,
	AttackEnemy
}
