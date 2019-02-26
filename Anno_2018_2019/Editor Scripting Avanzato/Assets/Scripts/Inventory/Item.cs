using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "Data/Crafting/Item")]
public class Item : ScriptableObject {
	[PreviewField]
	public Sprite sprite;
	public string description;
	public int stack;
	[Space]
	[ListDrawerSettings(CustomAddFunction = "AddInteraction")] public Interaction[] interactions;
	
	#region Methods

	public string Inspect() {
		Debug.Log(description);
		return description;
	}

	private Interaction AddInteraction() {
		var interaction = new Interaction();
		interaction.owner = this;
		return interaction;
	}
		
	#endregion
}