using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "NewDocument", menuName = "ScriptableObjects/Document")]
public class Document : ScriptableObject
{
	#region Fields
	
	// Public
	[PreviewField] public Sprite documentPicture;
	public int id;

	#endregion

	#region Unity Callbacks

	#endregion

	#region Methods

	#endregion

}