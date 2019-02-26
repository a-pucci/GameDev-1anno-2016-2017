using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "category", menuName = "Data/Crafting/Category")]
public class Category : ScriptableObject {
	[PreviewField] public Sprite sprite;
}