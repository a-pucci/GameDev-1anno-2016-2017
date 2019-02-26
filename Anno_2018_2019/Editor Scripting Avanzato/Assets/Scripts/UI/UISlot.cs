using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour {

	#region Fields

	public static readonly Vector3 SelectedSize = new Vector3(1.2f, 1.2f, 1.2f);
	
	public Image icon;
	public TextMeshProUGUI quantity;

	public Set Set { get; set; }

	#endregion

}