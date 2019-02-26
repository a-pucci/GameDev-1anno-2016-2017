using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour {
	
	#region Fields

	// Public
	public float slotQuickNavigationTime;
	[Space]
	public GameObject slotPrefab;
	public Transform slotTransform;
	[Title("Interactions")]
	public GameObject interactionPrefab;
	public Transform interactionsTransform;
	public TextMeshProUGUI itemNameText;

	// Private
	private UIManager uiManager;
	private Inventory inventory;
	private Parser parser;

	private bool isPressedHorizontalDPad;
	private bool isInteractionsToShown;
	
	private List<UIInteraction> uiInteractions;
	private List<UISlot> slots;

	// Properties
	private int selectedSlotIndex;
	private int SelectedSlotIndex {
		get => selectedSlotIndex;
		set {
			DeselectAll();
			slots[value].transform.localScale = UISlot.SelectedSize;
			selectedSlotIndex = value;
			UpdateInteractions();
		}
	}

	#endregion

	#region Unity Messages

	private void Awake() {
		uiManager = FindObjectOfType<UIManager>();
		inventory = FindObjectOfType<Character>().GetComponent<Inventory>();
		inventory.Changed += Set;
		parser = FindObjectOfType<Parser>();

		uiInteractions = new List<UIInteraction>();
		slots = new List<UISlot>();
		for (int i = 0; i < inventory.slotCount; i++) {
			GameObject slot = Instantiate(slotPrefab, slotTransform);
			slots.Add(slot.GetComponent<UISlot>());
		}
	}

	private void Update() {
		if (Mathf.Abs(Input.GetAxis("Horizontal R-Stick")) > 0.3f) {
			isInteractionsToShown = true;
			int currentSlotIndex = (int)Mathf.Sign(Input.GetAxis("Horizontal R-Stick")) + selectedSlotIndex;
			currentSlotIndex = Mathf.Clamp(currentSlotIndex, 0, slots.Count - 1);
			if (selectedSlotIndex != currentSlotIndex) {
				StartCoroutine(SetSelectedSlotAt(currentSlotIndex));
			}
		}
		if (interactionsTransform.gameObject.activeSelf) {
			if (Input.GetAxis("Vertical DPAD") == 1f) {
				slots[SelectedSlotIndex].Set.item.Inspect();
			}
			if (Input.GetAxis("Vertical DPAD") == -1) {
				inventory.Drop(slots[SelectedSlotIndex].Set);
			}
			
			if (Input.GetAxis("Horizontal DPAD") == 1f && !isPressedHorizontalDPad) {
				isPressedHorizontalDPad = true;
				if (slots[SelectedSlotIndex].Set?.item.interactions.Length > 0) {
					parser.Execute(slots[SelectedSlotIndex].Set.item.interactions[0]);
				}
			}
			if (Input.GetAxis("Horizontal DPAD") == -1 && !isPressedHorizontalDPad) {
				isPressedHorizontalDPad = true;
				if (slots[SelectedSlotIndex].Set?.item.interactions.Length > 1) {
					parser.Execute(slots[SelectedSlotIndex].Set.item.interactions[1]);
				}
			}
			if (Input.GetAxis("Horizontal DPAD") == 0) {
				isPressedHorizontalDPad = false;
			}
		}
	}

	#endregion

	#region Methods

	private void Set(int index, Set set) {
		slots[index].Set = set;
		slots[index].icon.enabled = set?.item != null;
		slots[index].icon.sprite = set?.item.sprite;
		slots[index].quantity.enabled = set?.quantity > 1;
		slots[index].quantity.text = set?.quantity.ToString();
		UpdateInteractions();
	}

	private IEnumerator SetSelectedSlotAt(int index) {
		yield return new WaitForSeconds(slotQuickNavigationTime);
		
		SelectedSlotIndex = index;		
	}

	private void DeselectAll() {
		foreach (UISlot currentSlot in slots) {
			currentSlot.transform.localScale = Vector3.one;
		}
	}

	private void UpdateInteractions() {
		if (slots[SelectedSlotIndex].Set == null) {
			interactionsTransform.gameObject.SetActive(false);
		}
		else {
			if (isInteractionsToShown) {
				interactionsTransform.gameObject.SetActive(true);
			
				Vector3 newPosition = interactionsTransform.position;
				newPosition.x = slots[SelectedSlotIndex].transform.position.x;
				interactionsTransform.position = newPosition;
			
				itemNameText.text = slots[SelectedSlotIndex].Set.item.name;

				// destroy old slots, TODO: introduce a "pooling system"
				foreach (UIInteraction currentUIInteraction in uiInteractions) {
					Destroy(currentUIInteraction.gameObject);
				}
				uiInteractions.Clear();

				// active interactions
				Interaction[] interactions = slots[SelectedSlotIndex].Set.item.interactions;
				for (int i = 0; i < interactions.Length; i++) {
					var uiInteraction = Instantiate(interactionPrefab, interactionsTransform).GetComponent<UIInteraction>();
					uiInteraction.transform.SetSiblingIndex(uiInteraction.transform.parent.childCount - 2);
					uiInteraction.button.sprite = uiManager.uiInput.GetAxe("d_pad" + "_" + (i * 2 + 1));
					uiInteraction.label.text = interactions[i].name;
					uiInteractions.Add(uiInteraction);
				}
			}
		}
	}

	#endregion
	
}