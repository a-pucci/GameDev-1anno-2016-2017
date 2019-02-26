using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "person", menuName = "Data/Person")]
public class Person : ScriptableObject {
	
	public enum Sex {
		Male,
		Female
	}

	[Serializable]
	public struct Date {
		[MinValue(1), MaxValue(30)] public int day;
		[MinValue(1), MaxValue(12)] public int month;
		[MinValue(0), MaxValue(9999)] public int year;
	}
	
	[PreviewField] public Sprite sprite;
	public string firstName;
	public string surname;
	public Sex sex;
	[Title("Birth Date")]
	[HideLabel] public Date birthDate;
	[Title("Residence")]
	public string city;
	public string country;
	[Title("ID")]
	public string id;
}