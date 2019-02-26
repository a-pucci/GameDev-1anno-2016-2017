using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.UI;

public class UICrafting : MonoBehaviour {

	#region Fields

	// Public
	public GameObject slotPrefab;
	
	[Title("Input Timing")]
	public float quickNavigationWaitingTime;
	public float slotQuickNavigationTime;
	
	[Title("Categories")]
	public Transform categoriesTransform;
	[Title("Recipes")]
	public Transform recipesTransform;
	public Transform recipesScrollbarTransform;
	public Transform ingredientsTransform;
	public TextMeshProUGUI resultText;
	public Image craftButton;
	public TextMeshProUGUI tipText;
	
	
	// Private
	private readonly Vector3 openCraftingSize = new Vector3(1.2f, 1.2f, 1f);

	private UIManager uiManager;
	private Inventory inventory;
	private Crafting crafting;

	private List<UISlot> categorySlots;
	private List<UISlot> recipeSlots;
	private List<UISlot> ingredientSlots;

	private bool isOpen;
	private bool canGoQuickHorizontalNavigation;
	private bool canGoQuickVerticalNavigation;
	private int horizontalDirection;
	private int verticalDirection;
	
	private Recipe selectedRecipe;
	
	// Properties
	private int selectedCategoryIndex;
	private int SelectedCategoryIndex {
		get { return selectedCategoryIndex; }
		set {
			DeselectAll(categorySlots);
			if (value != -1) {
				categorySlots[value].transform.localScale = UISlot.SelectedSize;
				OpenRecipesList(value);
				// SelectedRecipeIndex = 0;
			}
			else {
				Close(recipesTransform.gameObject);
			}
			selectedCategoryIndex = value;
		}
	}
	
	private int selectedRecipeIndex;
	private int SelectedRecipeIndex {
		get { return selectedRecipeIndex; }
		set {
			DeselectAll(recipeSlots);
			if (value != -1) {
				recipeSlots[value].transform.localScale = UISlot.SelectedSize;
				OpenRecipe(value);
			}
			else {
				Close(recipesTransform.gameObject);
			}
			selectedRecipeIndex = value;
		}
	}

	// Events

	#endregion

	#region Unity Messages

	private void Awake() {
		uiManager = FindObjectOfType<UIManager>();
		crafting = FindObjectOfType<Crafting>();
		inventory = FindObjectOfType<Character>().GetComponent<Inventory>();
		inventory.Changed += UpdateRecipe;
		
		categorySlots = new List<UISlot>();
		foreach (Category currentCategory in crafting.categories) {
			var slot = Instantiate(slotPrefab, categoriesTransform).GetComponent<UISlot>();
			slot.icon.enabled = true;
			slot.icon.sprite = currentCategory.sprite;
			categorySlots.Add(slot);
		}
		
		recipeSlots = new List<UISlot>();
		ingredientSlots = new List<UISlot>();
	}

	private void Start() {
		craftButton.sprite = uiManager.uiInput.GetAxe("a");
	}

	private void Update() {
		if (Input.GetAxis("LT") > 0f) {
			if (!isOpen) {
				Open();
			}
		}
		else if (Input.GetAxis("LT") == 0f) {
			if (isOpen) {
				Close();
			}
		}
		if (isOpen) {
			// int currentVerticalDirection = (int)Mathf.Sign(Input.GetAxis("Vertical L-Stick"));
			// int currentHorizontalDirection = (int)Mathf.Sign(Input.GetAxis("Horizontal L-Stick"));
			//
			// if (currentHorizontalDirection != horizontalDirection) {
			// 	horizontalDirection = currentHorizontalDirection;
			// 	StopAllCoroutines();
			// }
			// if (currentVerticalDirection != verticalDirection) {
			// 	verticalDirection = currentVerticalDirection;
			// 	StopAllCoroutines();
			// }
			
			if (Mathf.Abs(Input.GetAxis("Vertical L-Stick")) > 0.3f) {
				verticalDirection = (int)Mathf.Sign(Input.GetAxis("Vertical L-Stick"));
				int categoryIndex = SelectedCategoryIndex + verticalDirection;
				categoryIndex = Mathf.Clamp(categoryIndex, 0, categorySlots.Count - 1);
				if (SelectedCategoryIndex != categoryIndex) {
					StartCoroutine(MoveVerticallyTo(categoryIndex));
				}
			}
			// else {
			// 	StopAllCoroutines();
			// }
			
			if (Mathf.Abs(Input.GetAxis("Horizontal L-Stick")) > 0.3f) {
				horizontalDirection = (int)Mathf.Sign(Input.GetAxis("Horizontal L-Stick"));
				int recipeIndex = SelectedRecipeIndex + horizontalDirection;
				recipeIndex = Mathf.Clamp(recipeIndex, 0, recipeSlots.Count - 1);
				if (SelectedRecipeIndex != recipeIndex) {
					SelectedRecipeIndex = recipeIndex;
				}
			}
			// else {
			// 	StopAllCoroutines();
			// }
			
			if (Input.GetButtonUp("A")) {
				if (craftButton.gameObject.activeSelf) {
					crafting.Make(selectedRecipe);
				}
			}
		}
	}

	#endregion

	#region Methods

	private void Open() {
		transform.localScale = openCraftingSize;
		SelectedCategoryIndex = 0;
		SelectedRecipeIndex = 0;
		isOpen = true;
	}

	private void Close() {
		transform.localScale = Vector3.one;
		SelectedCategoryIndex = -1;
		SelectedRecipeIndex = -1;
		isOpen = false;
	}

	private IEnumerator ActivateQuickNavigation() {
		yield return new WaitForSeconds(slotQuickNavigationTime);
	}

	private IEnumerator MoveVerticallyTo(int indexSlot) {
		yield return new WaitForSeconds(slotQuickNavigationTime);
		SelectedCategoryIndex = indexSlot;
		SelectedRecipeIndex = 0;
	}

	private void DeselectAll(List<UISlot> slots) {
		foreach (UISlot currentSlot in slots) {
			currentSlot.transform.localScale = Vector3.one;
		}
	}

	private void OpenRecipesList(int categoryIndex) {
		recipesTransform.gameObject.SetActive(true);
		
		// Vector3 position = recipesTransform.position;
		// position = new Vector3(position.x, categorySlots[categoryIndex].icon.transform.position.y, position.z);
		// recipesTransform.position = position;
		
		// destroy old slots, TODO: introduce a "pooling system"
		foreach (UISlot currentRecipeSlot in recipeSlots) {
			Destroy(currentRecipeSlot.gameObject);
		}
		recipeSlots.Clear();

		List<Recipe> recipes = crafting.recipes.FindAll(r => r.category == crafting.categories[categoryIndex]);
		foreach (Recipe currentRecipe in recipes) {
			var slot = Instantiate(slotPrefab, recipesScrollbarTransform).GetComponent<UISlot>();
			slot.icon.enabled = true;
			slot.icon.sprite = currentRecipe.result.item.sprite;
			recipeSlots.Add(slot);
		}
	}

	private void OpenRecipe(int index) {
		
		// Vector3 position = recipesScrollbarTransform.position;
		// position = new Vector3(position.x, categorySlots[categoryIndex].icon.transform.position.y, position.z);
		// recipesScrollbarTransform.position = position;
		
		// destroy old slots, TODO: introduce a "pooling system"
		foreach (UISlot currentIngredientSlot in ingredientSlots) {
			Destroy(currentIngredientSlot.gameObject);
		}
		ingredientSlots.Clear();
		
		selectedRecipe = crafting.recipes.FindAll(r => r.category == crafting.categories[SelectedCategoryIndex])[index];
		foreach (Set currentIngredient in selectedRecipe.ingredients) {
			var slot = Instantiate(slotPrefab, ingredientsTransform).GetComponent<UISlot>();
			slot.Set = new Set
			{
				item = currentIngredient.item,
				quantity = currentIngredient.quantity
			};
			slot.icon.enabled = true;
			slot.icon.sprite = currentIngredient.item.sprite;
			slot.quantity.enabled = true;
			UpdateQuantity(slot);
			ingredientSlots.Add(slot);
		}

		resultText.text = selectedRecipe.result.item.name;

		tipText.gameObject.SetActive(ingredientSlots.Exists(i => i.quantity.color == Color.red));
		craftButton.gameObject.SetActive(ingredientSlots.TrueForAll(i => i.quantity.color == Color.white));
	}

	private void UpdateRecipe(int index, Set set) {
		ingredientSlots.ForEach(UpdateQuantity);
		tipText.gameObject.SetActive(ingredientSlots.Exists(i => i.quantity.color == Color.red));
		craftButton.gameObject.SetActive(ingredientSlots.TrueForAll(i => i.quantity.color == Color.white));
	}

	private void UpdateQuantity(UISlot slot) {
		slot.quantity.text = $"{inventory.GetAmountOf(slot.Set.item)}/{slot.Set.quantity}";
		slot.quantity.color = (inventory.GetAmountOf(slot.Set.item) < slot.Set.quantity) ? Color.red : Color.white;
	}

	private void Close(GameObject gameObjectToClose) {
		gameObjectToClose.SetActive(false);
	}

	#endregion
}