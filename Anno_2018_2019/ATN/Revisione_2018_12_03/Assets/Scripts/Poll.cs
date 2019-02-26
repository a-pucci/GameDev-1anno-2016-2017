using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "poll", menuName = "New Poll")]
public class Poll : ScriptableObject 
{
	[OnValueChanged("UpdateList")]
	public List<Item> items = new List<Item>();

	private const float MaxPercentage = 100;
	
	[Button(ButtonSizes.Medium)]
	private void UpdateList()
	{
		foreach (Item item in items)
		{
			item.percentage = MaxPercentage / items.Count;
		}
	}
}

[Serializable]
[InlineProperty(LabelWidth = 80)]
public class Item
{
[HorizontalGroup] public string name;
[HorizontalGroup] public float percentage;
}
