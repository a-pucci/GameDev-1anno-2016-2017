using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Poll", menuName = "ScriptableObjects/Poll")]
public class Poll : ScriptableObject 
{
	[OnValueChanged("UpdateList", true)]
	public List<Item> items = new List<Item>();

	private const float MaxPercentage = 100;
	
	[Button(ButtonSizes.Medium)]
	private void UpdateList() {
		
		float max = MaxPercentage;
		int lockedItems = 0;
		
		foreach (Item item in items) {
			if (item.locked || item.changed) {
				max -= item.percentage;
				lockedItems++;
			}
		}

		foreach (Item item in items)
		{
			if (!item.locked && !item.changed) {
				item.percentage = max / (items.Count - lockedItems);
			}
			item.changed = false;
		}
	}
}

[Serializable]
[InlineProperty(LabelWidth = 80)]
public class Item
{
	[HorizontalGroup] public string name;
	[HorizontalGroup, Range(0, 100), OnValueChanged("Changed"), DisableIfAttribute("locked")]public float percentage;
	[HorizontalGroup] public bool locked;
	[HideInInspector] public bool changed;

	private void Changed() {
		changed = true;
	}
}
