using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "day", menuName = "Data/Day")]
public class Day : ScriptableObject {
	public List<Person> persons;
}