using System;
using UnityEngine;
using Random = UnityEngine.Random;

public static class PersonGenerator
{
	#region Fields

	// Private
	private static Names maleNames;
	private static Names femaleNames;
	private static Names surnames;
	private static Names cities;
	private static Names countries;
	
	private static Images maleFaces;
	private static Images maleDocumentFaces;
	private static Images femaleFaces;
	private static Images femaleDocumentFaces;

	#endregion

	#region Unity Callbacks

	#endregion

	#region Methods

	public static Person GenerateRandomPerson(Sex sex)
	{
		LoadAssets();
		
		var newPerson = ScriptableObject.CreateInstance<Person>();
		newPerson.document = ScriptableObject.CreateInstance<Document>();

		SetRandomFace(sex, ref newPerson);
		newPerson.name = GetRandomName(sex);
		newPerson.surname = surnames.GetRandomElement();
		newPerson.birthdate = GetRandomDate();
		newPerson.birthplace = cities.GetRandomElement();
		newPerson.country = countries.GetRandomElement();
		newPerson.document.id = GetRandomId();
		
		return newPerson;
	}
	
	private static void LoadAssets()
	{
		maleNames = Resources.Load<Names>("ScriptableObjects/Names/MaleNames");
		femaleNames = Resources.Load<Names>("ScriptableObjects/Names/FemaleNames");
		surnames = Resources.Load<Names>("ScriptableObjects/Names/Surnames");
		cities = Resources.Load<Names>("ScriptableObjects/Names/Cities");
		countries = Resources.Load<Names>("ScriptableObjects/Names/Countries");
		
		maleFaces = Resources.Load<Images>("ScriptableObjects/Faces/MaleFaces");
		maleDocumentFaces = Resources.Load<Images>("ScriptableObjects/Faces/MaleDocumentFaces");
		femaleFaces = Resources.Load<Images>("ScriptableObjects/Faces/FemaleFaces");
		femaleDocumentFaces = Resources.Load<Images>("ScriptableObjects/Faces/FemaleDocumentFaces");
	}
	
	private static string GetRandomName(Sex sex)
	{
		switch (sex)
		{
			case Sex.Male:
				return maleNames.GetRandomElement();
			
			case Sex.Female:
				return femaleNames.GetRandomElement();
			
			default:
				return "";
		}
	}

	private static void SetRandomFace(Sex sex, ref Person person )
	{
		int randomNumber;
		
		switch (sex)
		{
			case Sex.Male:
				randomNumber = Random.Range(0, maleFaces.faces.Length);

				person.face = maleFaces.GetImageAt(randomNumber);
				person.document.documentPicture = maleDocumentFaces.GetImageAt(randomNumber); 
				break;	
			
			case Sex.Female:
				randomNumber = Random.Range(0, femaleFaces.faces.Length);

				person.face = femaleFaces.GetImageAt(randomNumber);
				person.document.documentPicture = femaleDocumentFaces.GetImageAt(randomNumber); 
				break;
		}
	}

	private static string GetRandomDate()
	{
		DateTime startDate = new DateTime(1950, 1, 1);
		DateTime endDate = new DateTime(2000, 1, 1);
		int range = (endDate - startDate).Days;
		return startDate.AddDays(Random.Range(0, range)).ToShortDateString();
	}
	
	private static int GetRandomId()
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