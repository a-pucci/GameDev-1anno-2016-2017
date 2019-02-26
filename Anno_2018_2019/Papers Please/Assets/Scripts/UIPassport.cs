using System;
using UnityEngine;
using UnityEngine.UI;

public class UIPassport : MonoBehaviour {

	#region Fields

	// Public

	public Text fullname;
	public Text birthdate;
	public Text sex;
	public Text city;
	public Text country;
	
	#endregion

	private void Awake() {
		FindObjectOfType<Queue>().Popped += Show;
	}

	private void Show(Person person) {
		fullname.text = string.Format("{0}, {1}", person.surname, person.firstName);
		birthdate.text = string.Format("{0:00}/{1:00}/{2:0000}", person.birthDate.day, person.birthDate.month, person.birthDate.year);
		sex.text = Enum.GetName(typeof(Person.Sex), person.sex)[0].ToString().ToUpper();
		city.text = person.city;
		country.text = person.country.ToUpper();
	}
}