using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Environments;
using UnityEngine;
using Random = UnityEngine.Random;

public class PersonGenerator : MonoBehaviour
{
	#region Fields

	// Static

	// Public
	public Names maleNames;
	public Names femaleNames;
	public Names surnames;
	public Names cities;
	public Names countries;
	public Images maleFaces;
	public Images femaleFaces;

	// Hidden Public
	
	// Private
	
	// Properties

	// Components

	// Events

	#endregion

	#region Unity Callbacks

	#endregion

	#region Methods

	public Person GenerateRandomPerson(Sex sex)
	{
		var newPerson = ScriptableObject.CreateInstance<Person>();

		newPerson.face = GetRandomFace(sex);
		newPerson.name = GetRandomName(sex);
		newPerson.surname = surnames.GetRandomElement();
		newPerson.birthdate = GetRandomDate();
		newPerson.birthplace = cities.GetRandomElement();
		newPerson.country = countries.GetRandomElement();
		newPerson.documentId = GenerateRandomId();
		
		return newPerson;
	}
	
	private string GetRandomName(Sex sex)
	{
		switch (sex)
		{
			case Sex.Male:
				return maleNames.GetRandomElement();
				break;	
			
			case Sex.Female:
				return femaleNames.GetRandomElement();
				break;
			
			default:
				return "";
		}
	}

	private Sprite GetRandomFace(Sex sex)
	{
		switch (sex)
		{
			case Sex.Male:
				return maleFaces.GetRandomImage();
				break;	
			
			case Sex.Female:
				return femaleFaces.GetRandomImage();
				break;
			
			default:
				return null;
		}
	}

	private string GetRandomDate()
	{
		DateTime startDate = new DateTime(1950, 1, 1);
		DateTime endDate = new DateTime(2000, 1, 1);
		int range = (endDate - startDate).Days;
		return startDate.AddDays(Random.Range(0, range)).ToShortDateString();
	}
	
	private int GenerateRandomId()
	{
		return Random.Range(11111, 99999);
	}
	
	#endregion
}

public enum Sex
{
	Male,
	Female
}