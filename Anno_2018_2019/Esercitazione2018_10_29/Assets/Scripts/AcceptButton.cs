using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class AcceptButton : MonoBehaviour, IPointerDownHandler
{
	#region Fields

	// Events
	public event Action PersonAccepted;
	
	#endregion

	#region Unity Callbacks

	public void OnPointerDown(PointerEventData eventData)
	{
		if (PersonAccepted != null)
		{
			PersonAccepted();
		}
	}

	#endregion

	#region Methods

	#endregion
}
