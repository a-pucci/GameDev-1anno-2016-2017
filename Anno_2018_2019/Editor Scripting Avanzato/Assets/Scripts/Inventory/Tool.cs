using UnityEngine;

[CreateAssetMenu(fileName = "tool", menuName = "Data/Crafting/Tool")]
public class Tool : Item {
	public int durability;

	private void OnEnable() {
		stack = 1;
	}
}