using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour 
{
	#region Fields

	// Static

	// Public
	public DailyList dailyPeople;
	public Document document;
	public PersonGenerator personGenerator;
	public Image person;
	
	// Hidden Public
	
	// Private

	// Properties

	// Components

	// Events

	#endregion

	#region Unity Callbacks

	private void Start () 
	{
		FillList();
	}

	#endregion

	#region Methods

	private void FillList()
	{
		for (int i = 0; i < dailyPeople.people.Length; i++)
		{
			if (dailyPeople.people[i] == null)
			{
				var sex = (Sex)UnityEngine.Random.Range(0, 1);
				dailyPeople.people[i] = personGenerator.GenerateRandomPerson(sex);
			}
		}
	}
	
	#endregion

}