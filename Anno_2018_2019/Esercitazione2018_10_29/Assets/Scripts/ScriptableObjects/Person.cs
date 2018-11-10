using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "NewPerson", menuName = "ScriptableObjects/Person")]
public class Person : ScriptableObject
{
	#region Fields

	// Public
	[PreviewField] public Sprite face;
	public new string name;
	public string surname;
	public string birthdate;
	public string birthplace;
	public string country;
	public Document document;

	[TextArea] public string arrivalDialogue;
	[TextArea] public string acceptDialogue;
	[TextArea] public string denyDialogue;

	#endregion

	#region Unity Callbacks

	#endregion

	#region Methods

	#endregion

}
