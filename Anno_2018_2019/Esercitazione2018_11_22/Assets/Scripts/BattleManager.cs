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

	// Hidden Public

	// Private

	// Properties

	// Components

	// Events

	#endregion

	#region Unity Callbacks

	private void Start()
	{
		SetOpponents();
		
		if (!twoPlayers)
		{
			playerTwo.gameObject.SetActive(false);
			opponentTwo.gameObject.SetActive(false);
		}
	}

	#endregion

	#region Methods

	private void SetOpponents()
	{
		playerOne.isOpponent = false;
		playerOne.RosterEmpty += FightEnded;
		
		playerTwo.isOpponent = false;
		playerTwo.RosterEmpty += FightEnded;
		
		opponentOne.isOpponent = true;
		opponentOne.RosterEmpty += FightEnded;
		
		opponentTwo.isOpponent = true;
		opponentTwo.RosterEmpty += FightEnded;
	}

	private void FightEnded(bool opponentsWin)
	{
		if (opponentsWin)
		{
			// hai perso
		}
		else
		{
			// hai vinto
		}
    }

	#endregion
}
