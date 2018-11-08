using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNames", menuName = "ScriptableObjects/Names")]
public class Names : ScriptableObject 
{
	#region Fields

	// Static

	// Public
	public string[] names;

	// Hidden Public

	// Private

	// Properties

	// Components

	// Events

	#endregion

	#region Unity Callbacks

	#endregion

	#region Methods

	public string GetRandomElement()
	{
		int randomNumber = Random.Range(0, names.Length);
		return names[randomNumber];
	}

	#endregion

}