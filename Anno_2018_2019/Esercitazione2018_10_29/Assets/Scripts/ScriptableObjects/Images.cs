using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewImages", menuName = "ScriptableObjects/Images")]
public class Images : ScriptableObject 
{
	#region Fields

	// Static

	// Public
	public Sprite[] faces;
	
	// Hidden Public
	
	// Private

	// Properties

	// Components

	// Events

	#endregion

	#region Unity Callbacks

	private void Start () 
	{
		
	}
	
	private void Update () 
	{
		
	}

	#endregion

	#region Methods
	
	public Sprite GetRandomImage()
	{
		int randomNumber = Random.Range(0, faces.Length);
		return faces[randomNumber];
	}
	
	#endregion

}