using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	#region Fields

	// Public
	public Button approvedButton;
	public Button deniedButton;
	public Button bellButton;

	// Private

	// Events
	public Action<bool> Evaluated;
	public Action Rang;

	#endregion

	#region Unity Callbacks

	private void Awake() {
		approvedButton.onClick.AddListener(ShowBell);
		deniedButton.onClick.AddListener(ShowBell);
		bellButton.onClick.AddListener(() => {
			bellButton.gameObject.SetActive(false);
			FindObjectOfType<Queue>().Pop();
		});
	}

	private void ShowBell() {
		bellButton.gameObject.SetActive(true);
	}

	#endregion
	
}