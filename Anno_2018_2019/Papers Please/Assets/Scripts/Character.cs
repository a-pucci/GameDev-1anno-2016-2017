using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class Character : MonoBehaviour {
	
	#region Fields

	// Public

	// Private

	// Components
	private SpriteRenderer spriteRenderer;
	
	// Events

	#endregion

	#region Unity Callbacks

	private void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		FindObjectOfType<Queue>().Popped += Show;
	}

	#endregion

	#region Methods
	
	private void Show(Person person) {
		spriteRenderer.sprite = person.sprite;
	}
	
	private void Answer() { }

	#endregion
}