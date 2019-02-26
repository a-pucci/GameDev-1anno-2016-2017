using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue : MonoBehaviour {

	#region Fields

	// Public
	public Generator generator;
	public Day day;

	// Hidden Public

	// Private
	private Character character;
	private int indexPersonToPop;

	// Components

	// Events
	public Action<Person> Popped;

	#endregion

	#region Unity Callbacks

	private void Awake() {
		// FindObjectOfType<UIManager>().Rang += Pop;
	}

	private void Start() {
		Pop();
	}

	#endregion

	#region Methods

	public void Pop() {
		if (day.persons[indexPersonToPop] == generator.randomPerson) {
			generator.Create();
		}
		if (Popped != null) {
			Popped(day.persons[indexPersonToPop]);
		}
	}

	#endregion

}