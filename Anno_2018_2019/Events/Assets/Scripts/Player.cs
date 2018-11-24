using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
	#region Fields

	// Events
	
	public event Action Died;

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