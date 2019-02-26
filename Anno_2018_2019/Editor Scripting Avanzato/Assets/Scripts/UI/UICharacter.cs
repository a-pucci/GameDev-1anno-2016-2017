using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UICharacter : MonoBehaviour {

	#region Fields

	// Static

	// Public
	public Image hunger;
	public Image thirst;
	public Image temperature;
	public Image oxygen;
	public Image rest;

	// Hidden Public

	// Private
	private Character character;
	
	// Properties

	// Components

	// Events

	#endregion

	#region Unity Messages

	private void Awake() {
		character = FindObjectOfType<Character>();
		character.hunger.Changed += (valueToSet) => SetVitalParameter(hunger, valueToSet);
		character.thirst.Changed += (valueToSet) => SetVitalParameter(thirst, valueToSet);
		character.temperature.Changed += (valueToSet) => SetVitalParameter(temperature, valueToSet);
		character.oxygen.Changed += (valueToSet) => SetVitalParameter(oxygen, valueToSet);
		character.rest.Changed += (valueToSet) => SetVitalParameter(rest, valueToSet);
	}

	#endregion

	#region Methods

	private void SetVitalParameter(Image filler, float valueToSet) {
		filler.fillAmount = valueToSet / 100f;
	}
	
	#endregion

}