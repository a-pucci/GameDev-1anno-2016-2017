using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour {
	
	#region Fields

	// Static

	// Public
	public GameObject slotPrefab;
	
	// Hidden Public

	// Private
	private Inventory inventory;
	private List<UISlot> slots;

	#endregion

	#region Unity Messages

	private void Awake() {
		inventory = FindObjectOfType<Character>().GetComponent<Inventory>();
		inventory.Changed += Set;
		
		slots = new List<UISlot>();
		for (int i = 0; i < inventory.slotCount; i++) {
			GameObject slot = Instantiate(slotPrefab, transform);
			slots.Add(slot.GetComponent<UISlot>());			
		}
	}

	#endregion

	#region Methods

	private void Set(int index, Set set) {
		slots[index].item.enabled = set?.item != null;
		slots[index].item.sprite = set?.item.sprite;
		slots[index].quantity.enabled = set?.quantity > 1;
		slots[index].quantity.text = set?.quantity.ToString();
	}

	#endregion

}