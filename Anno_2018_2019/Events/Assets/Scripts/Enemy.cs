using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
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
		FindObjectOfType<Player>().Died += Exult;
	}

	#endregion

	#region Methods

	public void Exult() 
	{
		
	}
	
	#endregion

}