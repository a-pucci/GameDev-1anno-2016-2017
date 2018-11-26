using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speaker : MonoBehaviour 
{
	#region Fields

	// Public
	public GameObject baloon;
	public Text speechText;

	// Events
	public event Action SpeakEnded;

	#endregion

	#region Unity Callbacks

	#endregion

	#region Methods

	public void Speak(string newText, bool turnResolve = false)
	{
		baloon.SetActive(true);
		speechText.text = newText;
		StopCoroutine("SpeakingTime");
		StartCoroutine(SpeakingTime(turnResolve));
	}

	private IEnumerator SpeakingTime(bool turnResolve)
	{
		yield return  new WaitForSeconds(3f);

		if (turnResolve)
		{
			baloon.SetActive(false);
		
			if (SpeakEnded != null)
			{
				SpeakEnded();
			}
		}
	}

	#endregion
}
