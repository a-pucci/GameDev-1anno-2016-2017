using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "NewImages", menuName = "ScriptableObjects/Images")]
public class Images : ScriptableObject 
{
	#region Fields

	// Public
	public Sprite[] faces;
	
	#endregion

	#region Unity Callbacks

	#endregion

	#region Methods

	public Sprite GetImageAt(int index)
	{
		return faces[index];
	}

	[Button]
	private void SortImages()
	{
		Sprite[] newFaces = faces.OrderBy(x => x.name).ToArray();
		faces = newFaces;
	}
	
	#endregion

}