using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

public class CraftingWindow : OdinEditorWindow {

	private Category[] categories;
	private Recipe[] recipes;

	public List<CraftStruct> craftStructs = new List<CraftStruct>();

	[MenuItem("Tools/Crafting Table")]
	private static void OpenWindow(){
		var window = GetWindow<CraftingWindow>();
		window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
	}

	[PropertyOrder(-10)]
	[Button(ButtonSizes.Medium)]
	private void UpdateCraftingObjects() {
		craftStructs.Clear();
		categories = GetAllInstances<Category>();
		recipes = GetAllInstances<Recipe>();

		foreach (Category category in categories) {
			var craft = new CraftStruct ();
			craft.category = category;
			
			foreach (Recipe recipe in recipes) {
				if (recipe.category == craft.category) {
					craft.recipes.Add(recipe);
				}
			}
			craftStructs.Add(craft);
		}
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

	private void OnEnable() {
		
		UpdateCraftingObjects();
	}
}
[Serializable]
public class CraftStruct {
	public Category category;
	public List<Recipe> recipes = new List<Recipe>();
}
