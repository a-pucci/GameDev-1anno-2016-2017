using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "Data/Crafting/Item")]
public class Item : ScriptableObject {
	public Sprite sprite;
	
	public string description;
	public int stack;
	
	#region Methods

	public string Inspect() {
		return description;
	}
	
	#endregion
}