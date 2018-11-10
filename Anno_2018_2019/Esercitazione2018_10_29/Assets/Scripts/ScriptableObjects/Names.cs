using UnityEngine;

[CreateAssetMenu(fileName = "NewNames", menuName = "ScriptableObjects/Names")]
public class Names : ScriptableObject 
{
	#region Fields

	// Public
	public string[] names;

	#endregion

	#region Unity Callbacks

	#endregion

	#region Methods

	public string GetRandomElement()
	{
		int randomNumber = Random.Range(0, names.Length);
		return names[randomNumber];
	}

	#endregion

}