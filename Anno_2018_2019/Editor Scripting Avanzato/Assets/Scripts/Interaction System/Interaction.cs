using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
public class Interaction {
	[HideInInspector] public Object owner;
	public string name;
	public Interactable destination;
	public string condition;
	[HideLabel]	public Effect effect;
}