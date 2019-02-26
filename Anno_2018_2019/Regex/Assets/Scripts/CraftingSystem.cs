using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class CraftingSystem : MonoBehaviour {

	#region Fields

	// Public
	[Space]
	public List<Item> items;
	[Space]
	public List<Recipe> recipes;
	
	#endregion

	#region Methods
	
#if UNITY_EDITOR
	private void GetAllItems() {
		items = GetAllInstances<Item>().ToList();
	}
	
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
	
	#endregion

}

public class Recipe : ScriptableObject { }