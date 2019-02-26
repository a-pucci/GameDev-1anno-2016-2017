using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ui_input", menuName = "Data/UI Input")]
public class UIInput : ScriptableObject {

	[Serializable]
	private struct UIAxe {
		public string name;
		public Sprite icon;
	}
	
	#region Fields

	[SerializeField] private List<UIAxe> axes;

	#endregion

	public Sprite GetAxe(string name) {
		if (axes.Exists(a => a.name == name)) {
			return axes.Find(a => a.name == name).icon;
		}
		else {
			Debug.LogWarning($"{name} icon not found");
			return null;
		}
	}
	
}