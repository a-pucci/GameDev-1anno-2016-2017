using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine;

public class Baloon : MonoBehaviour 
{
	#region Fields

	// Public
	public Text text; 

	// Events
	public event Action AlarmSpeakEnded;
	public event Action LeavingSpeakEnded;

	#endregion

	#region Unity Callbacks

	#endregion

	#region Methods

	public void Speak(Transform speaker, string dialogue, Speakers speakerType)
	{
		Move(speaker);
		text.text = dialogue;
		
		StartCoroutine(DialogueTime(speakerType));
	}

	private void Move(Transform speaker)
	{
		Vector3 temp;
		temp.x = speaker.position.x - 350;
		temp.y = speaker.position.y;
		temp.z = speaker.position.z;
		gameObject.transform.position = temp;
	}

	private IEnumerator DialogueTime(Speakers speakerType)
	{
		Debug.Log("wait");
		yield return new WaitForSeconds(2f);
		Debug.Log("ienum");
		switch (speakerType)
		{
			case Speakers.Alarm:
				if (AlarmSpeakEnded != null)
				{
					Debug.Log("alarm");
					AlarmSpeakEnded();
				}
				break;
			
			case Speakers.PersonLeaving:
				if (LeavingSpeakEnded != null)
				{
					Debug.Log("leave");
					LeavingSpeakEnded();
				}
				break;
		}
		Destroy(gameObject);
	}

	#endregion

}