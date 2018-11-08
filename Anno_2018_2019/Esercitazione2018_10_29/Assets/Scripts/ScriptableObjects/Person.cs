using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "NewPerson", menuName = "ScriptableObjects/Person")]
public class Person : ScriptableObject
{
	#region Fields

	// Static

	// Public
	public Sprite face;
	public new string name;
	public string surname;
	public string birthdate;
	public string birthplace;
	public string country;
	
	[MinValue(11111)]
	[MaxValue(99999)]
	public int documentId;

	// Hidden Public

	// Private

	// Properties

	// Components

	// Events

	#endregion

	#region Unity Callbacks

	#endregion

	#region Methods

	#endregion

}
