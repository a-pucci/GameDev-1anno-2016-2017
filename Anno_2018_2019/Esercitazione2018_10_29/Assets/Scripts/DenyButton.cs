using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DenyButton : MonoBehaviour, IPointerDownHandler
{
	#region Fields

	// Events
	public event Action PersonDenied;

	#endregion

	#region Unity Callbacks

	public void OnPointerDown(PointerEventData eventData)
	{
		if (PersonDenied != null)
		{
			PersonDenied();
		}
	}

	#endregion

	#region Methods

	#endregion
}
