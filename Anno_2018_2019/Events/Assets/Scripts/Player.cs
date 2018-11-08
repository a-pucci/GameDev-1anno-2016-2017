using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
	#region Fields

	// Static

	// Public

	// Hidden Public
		
	// Private

	// Properties

	// Components

	// Events
	
	public event Action Died;

	#endregion

	#region Unity Callbacks

	private void Start () 
	{
		
	}
	
	private void Update () 
	{
		
	}

	#endregion

	#region Methods

	public void Die() 
	{		
		// die instructions
		if (Died != null)
		{
			Died();
		}
	}

	#endregion

}