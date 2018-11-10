using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class DocumentObj : MonoBehaviour 
{
	#region Fields

	// Public
	[FoldoutGroup("Document Fields")] public Image documentPicture;
	[FoldoutGroup("Document Fields")] public Text documentId;
	[FoldoutGroup("Document Fields")] public Text personName;
	[FoldoutGroup("Document Fields")] public Text surname;
	[FoldoutGroup("Document Fields")] public Text birthDate;
	[FoldoutGroup("Document Fields")] public Text birthPlace;
	[FoldoutGroup("Document Fields")] public Text country;
	
	// Private
	private Person person;

	#endregion

	#region Unity Callbacks

	#endregion

	#region Methods

	public void SetOwner(Person newOwner)
	{
		person = newOwner;
		FillDocument();
	}

	public void FillDocument()
	{
		documentPicture.sprite = person.document.documentPicture;
		documentId.text = person.document.id.ToString();
		personName.text = person.name;
		surname.text = person.surname;
		birthDate.text = person.birthdate;
		birthPlace.text = person.birthplace;
		country.text = person.country;
	}

	#endregion
}
