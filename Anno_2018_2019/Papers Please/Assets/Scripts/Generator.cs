using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "generator", menuName = "Data/Generator")]
public class Generator : ScriptableObject {

	[Serializable]
	public struct GenderData {
		public List<Sprite> sprites;
		public List<string> firstNames;
	}
	
	[Serializable]
	public struct Nation {
		public string name;
		public string[] cities;
	}

	[Required] public Person randomPerson;
	[MinMaxSlider(1900, 2018, true)] public Vector2 bornYearRange;
	[Space]
	[Title("Male Data")]
	[HideLabel] public GenderData maleData;
	[Space]
	[Title("Female Data")]
	[HideLabel] public GenderData femaleData;
	[Space]
	[Title("Surnames")]
	public List<string> surnames;
	[Space] 
	[Title("Nations")] 
	public List<Nation> nations;
	
	public void Create() {
		randomPerson.sex = (Person.Sex)Random.Range(0, Enum.GetValues(typeof(Person.Sex)).Length);
		
		GenderData genderData = (randomPerson.sex == Person.Sex.Male) ? maleData : femaleData;
		randomPerson.sprite = genderData.sprites[Random.Range(0, genderData.sprites.Count)];
		randomPerson.firstName = genderData.firstNames[Random.Range(0, genderData.firstNames.Count)];
		randomPerson.surname = surnames[Random.Range(0, surnames.Count)];
		
		randomPerson.birthDate = CreateBirthDate();

		Nation nation = nations[Random.Range(0, nations.Count)];
		randomPerson.country = nation.name;
		randomPerson.city = nation.cities[Random.Range(0, nation.cities.Length)];
		
		randomPerson.id = CreateID();
	}

	private Person.Date CreateBirthDate() {
		return new Person.Date
		{
			day = Random.Range(1, 30),
			month = Random.Range(1, 12),
			year = Random.Range((int)bornYearRange.x, (int)bornYearRange.y)
		};
	}

	private static string CreateID() {
		return string.Format("{000000}", Random.Range(1, 999999));
	}
}