﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	#region Fields

	// Static

	// Public
	
	// Hidden Public

	// Private
	[SerializeField] public int slotCount;
	private List<Set> slots;

	// Properties

	// Components

	// Events
	public Action<int, Set> Changed;
	public Action<Set> Fulled;

	#endregion

	#region Unity Messages

	private void Awake() {
		slots = new List<Set>();
		for (int i = 0; i < slotCount; i++) {
			slots.Add(null);
		}
	}

	#endregion

	#region Methods

	public void Add(Set set) {

		int indexOfFirstEmptySlot = -1;
			
		for (int i = 0; i < slots.Count; i++) {
			// find empty slot
			if (slots[i] == null && indexOfFirstEmptySlot == -1) {
				indexOfFirstEmptySlot = i;
			}
			// same item slot not completely fulled
			if (slots[i]?.item == set.item && slots[i].quantity < set.item.stack) {
				int quantitySum = slots[i].quantity + set.quantity;
				if (quantitySum > set.item.stack) {
					slots[i].quantity = set.item.stack;
					set.quantity = quantitySum - set.item.stack;
					Changed?.Invoke(i, slots[i]);
					Add(set);
					return;
				}
				else {
					slots[i].quantity += set.quantity;
					Changed?.Invoke(i, slots[i]);
					return;
				}
			}
		}
		// add in the empty slot
		if (indexOfFirstEmptySlot != -1) {
			slots[indexOfFirstEmptySlot] = set;
			Changed?.Invoke(indexOfFirstEmptySlot, slots[indexOfFirstEmptySlot]);
		}
		else {
			Fulled?.Invoke(set);
		}
		
	}

	public void Remove(Set set) {
		for (int i = 0; i < slots.Count; i++) {
			if (slots[i]?.item == set.item) {
				if (slots[i].quantity > set.quantity) {
					slots[i].quantity -= set.quantity;
				}
				else {
					slots[i] = null;
				}
				Changed?.Invoke(i, slots[i]);
				return;
			}
		}
		Debug.LogWarning(set + " not removed");
	}

	public bool Check(Set set) {
		foreach (Set currentSet in slots) {
			if (currentSet?.item == set.item) {
				return currentSet.quantity >= set.quantity;
			}
		}
		return false;
	}

	#endregion
}