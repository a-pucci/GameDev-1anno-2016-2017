using System;
using UnityEngine;

public class Alarm : MonoBehaviour 
{
	#region Fields

	// Public
	public GameObject baloonPrefab;
	
	// Events
	public event Action StartTalking; 

	#endregion

	#region Unity Callbacks

	private void Start ()
	{
		FindObjectOfType<PersonObj>().StartTalking += ListenSpeaking;
	}
	
	#endregion

	#region Methods

	private void CallNextPerson()
	{
		GameObject baloon = Instantiate(baloonPrefab, transform.parent);
		baloon.GetComponent<Baloon>().Speak(transform, "AVANTI IL PROSSIMO!", Speakers.Alarm);
		
		if (StartTalking != null)
		{
			StartTalking();
		}
	}

	private void ListenSpeaking()
	{
		FindObjectOfType<Baloon>().LeavingSpeakEnded += CallNextPerson;
	}

	#endregion

}