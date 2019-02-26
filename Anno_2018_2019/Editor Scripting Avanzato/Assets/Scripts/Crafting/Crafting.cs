using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

public class Crafting : MonoBehaviour {

	#region Fields

	// Public
	[Space]
	[PropertyOrder(3)] public List<Item> items;
	[Space]
	[PropertyOrder(4)] public List<Category> categories;
	[Space]
	[PropertyOrder(5)] public List<Recipe> recipes;
	
	// Private
	private Inventory inventory;
	
	#endregion

	#region Unity Messages

	private void Awake() {
		inventory = FindObjectOfType<Character>().GetComponent<Inventory>();
	}

	#endregion
	
	#region Methods
	
#if UNITY_EDITOR
	[Button, PropertyOrder(0)]
	private void GetAllItems() {
		items = GetAllInstances<Item>().ToList();
	}

	[Button, PropertyOrder(1)]
	private void GetAllCategories() {
		categories = GetAllInstances<Category>().ToList();
	}

	[Button, PropertyOrder(2)]
	private void GetAllRecipes() {
		recipes = GetAllInstances<Recipe>().ToList();
	}

	private static T[] GetAllInstances<T>() where T : ScriptableObject {
		string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
		var a = new T[guids.Length];
		for (int i = 0; i < guids.Length; i++) {
			string path = AssetDatabase.GUIDToAssetPath(guids[i]);
			a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
		}
		return a;
	}

#endif

	public void Make(Recipe recipe) {
		foreach (Set currentIngredient in recipe.ingredients) {
			inventory.Remove(currentIngredient);
		}
		inventory.Add(recipe.result);
	}
	
	#endregion

}