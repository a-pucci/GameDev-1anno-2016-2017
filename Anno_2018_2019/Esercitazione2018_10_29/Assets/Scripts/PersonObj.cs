using System;
using UnityEngine;
using UnityEngine.UI;

public class PersonObj : MonoBehaviour 
{
	#region Fields

	// Public
	public GameObject baloonPrefab;
	public DocumentObj documentObj;

	// Private
	private Person personInfo;
	private Image face;
	
	// Events
	public event Action StartTalking;
	
	#endregion

	#region Unity Callbacks

	private void  Start()
	{
		FindObjectOfType<AcceptButton>().PersonAccepted += Accepted;
		FindObjectOfType<DenyButton>().PersonDenied += Denied;
		
		face = GetComponent<Image>();
	}

	#endregion

	#region Methods

	public void NewPerson(Person newPerson)
	{
		personInfo = newPerson;
		face.sprite = newPerson.face;
		documentObj.SetOwner(newPerson);
	
		Arrival();
	}
	
	private void Accepted()
	{		
		string dialogue;
		if (personInfo.acceptDialogue == null)
		{
			dialogue = "Grazie!";
		}
		else
		{
			dialogue = personInfo.acceptDialogue;
		}
		GameObject baloon = Instantiate(baloonPrefab, transform.parent);
		baloon.GetComponent<Baloon>().Speak(transform, dialogue, Speakers.PersonLeaving);

		if (StartTalking != null)
		{
			StartTalking();
		}
	}

	private void Denied()
	{		
		string dialogue;
		if (personInfo.denyDialogue == null)
		{
			dialogue = "Uffa!";
		}
		else
		{
			dialogue = personInfo.denyDialogue;
		}
		
		GameObject baloon = Instantiate(baloonPrefab, transform.parent);
		baloon.GetComponent<Baloon>().Speak(transform, dialogue, Speakers.PersonLeaving);
		
		if (StartTalking != null)
		{
			StartTalking();
		}
	}

	private void Arrival()
	{
		string dialogue;
		if (personInfo.arrivalDialogue == null)
		{
			dialogue = "Hello!";
		}
		else
		{
			dialogue = personInfo.arrivalDialogue;
		}
		
		GameObject baloon = Instantiate(baloonPrefab, transform.parent);
		baloon.GetComponent<Baloon>().Speak(transform, dialogue, Speakers.PersonArriving);
		
		if (StartTalking != null)
		{
			StartTalking();
		}
	}
	
	#endregion
}
