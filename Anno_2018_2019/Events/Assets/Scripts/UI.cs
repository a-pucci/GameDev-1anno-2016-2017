using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour 
{
	#region Fields

	// Static

	// Public

	// Hidden Public
	
	// Private
	private Image healthBar;
	
	// Properties

	// Components

	// Events
	// private Action Empty;
	
	#endregion

	#region Unity Callbacks

	private void Start ()
	{
		FindObjectOfType<Player>().Died += Hide;
		// Metodo anonimo: crea a runtime un metodo void senza parametri che ha come istruzione SetHealth(0f);
		// - 1 - FindObjectOfType<Player>().Died += () => SetHealth(0f);
		
		// - 2 - Empty = () => SetHealth(0f);
		// - 2 - FindObjectOfType<Player>().Died += Empty;
		
	}

	private void OnDestroy()
	{
		FindObjectOfType<Player>().Died -= Hide;
		// - 2 - FindObjectOfType<Player>().Died -= Empty;
	}

	#endregion

	#region Methods

	public void SetHealth(float amount)
	{
		healthBar.fillAmount = amount;
	}

	public void FullHealth()
	{
		healthBar.fillAmount = 1f;
	}

	public void EmptyHealth()
	{
		healthBar.fillAmount = 0f;
	}
	
	public void Hide() 
	{
		
	}
	
	#endregion

}