using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour 
{
	#region Fields
	
	// Public
	public DailyQueue dailyQueue;
	public PersonObj person;
	public GameObject endScreen;

	// Private
	private int queueIndex = 0;

	#endregion

	#region Unity Callbacks

	private void Start () 
	{
		FindObjectOfType<Alarm>().StartTalking += ListenSpeaking;
		
		FillList();
		NextPerson();
	}

	#endregion

	#region Methods

	private void FillList()
	{
		for (int i = 0; i < dailyQueue.queue.Length; i++)
		{
			if (dailyQueue.queue[i] == null)
			{
				var sex = (Sex)Random.Range(0, 1);
				dailyQueue.queue[i] = PersonGenerator.GenerateRandomPerson(sex);
			}
		}
	}

	private void NextPerson()
	{
		if (queueIndex < dailyQueue.queue.Length)
		{
			person.NewPerson(dailyQueue.queue[queueIndex]);
			queueIndex++;
		}
		else
		{
			endScreen.SetActive(true);
		}
	}

	private void ListenSpeaking()
	{
		FindObjectOfType<Baloon>().AlarmSpeakEnded += NextPerson;
	}
	
	#endregion
}

public enum Speakers
{
	Alarm,
	PersonArriving,
	PersonLeaving
}