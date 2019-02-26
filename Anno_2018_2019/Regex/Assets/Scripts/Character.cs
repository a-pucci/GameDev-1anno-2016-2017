using System;
using System.Reflection;
using UnityEngine;

[RequireComponent(typeof(Inventory))]
public class Character : MonoBehaviour {

	[Serializable]
	public class VitalSign {
		public float value;

		public Action<float> Changed;
		public Action Dead;
	
	}
	
	#region Fields

	// Static

	// Public
	public VitalSign hunger;
	public VitalSign thirst;
	public VitalSign temperature;
	public VitalSign oxygen;
	public VitalSign rest;
	
	// Hidden Public

	// Private

	// Properties

	// Components
	public Inventory inventory;

	// Events

	#endregion

	#region Unity Messages

	private void Awake() {
		inventory = GetComponent<Inventory>();
	}

	#endregion

	#region Methods
	
	#endregion

}