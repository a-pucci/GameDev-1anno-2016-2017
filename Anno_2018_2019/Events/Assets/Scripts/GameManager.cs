using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	#region Fields

	// Static

	// Public

	// Hidden Public
		
	// Private

	// Properties

	// Components

	// Events

	#endregion

	#region Unity Callbacks

	private void Start () 
	{
		FindObjectOfType<Player>().Died += GameOver;
	}
	
	#endregion

	#region Methods

	public void GameOver() 
	{
		
	}
	
	#endregion

}