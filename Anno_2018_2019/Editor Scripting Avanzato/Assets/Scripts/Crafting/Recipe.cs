using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "recipe", menuName = "Data/Crafting/Recipe")]
public class Recipe : ScriptableObject {
	
	#region Fields
	
	public Category category;
	[Title("Ingredients")]
	public Set[] ingredients;
	[Space]
	[Title("Appliance")]
	public Appliance appliance;
	[Title("Result")]
	[Required] public Set result;

	#endregion

}