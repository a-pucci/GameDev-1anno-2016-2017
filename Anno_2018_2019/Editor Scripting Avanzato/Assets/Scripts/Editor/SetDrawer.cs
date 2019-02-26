using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEngine;
using UnityEditor;


public class SetDrawer : OdinValueDrawer<Set> {

	private const float QuantityWidth = 30f;
	private const float QuantityHeight = 20f;
	private const float DefaultHeight = 16f;
	private const float TextureSize = 64;
	private const float ButtonWidth = 64;
	private const float ButtonHeight = 30;
	
	protected override void DrawPropertyLayout(GUIContent label) {
		
		Set set = ValueEntry.SmartValue;
		// default height 16p or 18p
		Rect rect = EditorGUILayout.GetControlRect(false, 64f);

		var fieldRect = new Rect(rect.x, rect.center.y - DefaultHeight / 2, set.item != null ? rect.width/3 : rect.width, DefaultHeight);
		set.item = (Item)SirenixEditorFields.UnityObjectField(fieldRect, set.item, typeof(Item), false);
		
		if (set.item != null && set.item.sprite != null) {
			var addButtonRect = new Rect(rect.max.x - ButtonWidth, rect.center.y - ButtonHeight-2, ButtonWidth, ButtonHeight);
			if (GUI.Button(addButtonRect, "+")) {
				set.quantity++;
			}
			
			var removeButtonRect = new Rect(rect.max.x - ButtonWidth, rect.center.y + 2, ButtonWidth, ButtonHeight);
			if (GUI.Button(removeButtonRect, "-")) {
				set.quantity--;
			}
	
			var style = new GUIStyle {fontSize = 20, fontStyle = FontStyle.Bold};
			
			var labelRect = new Rect(rect.max.x - ButtonWidth - QuantityWidth-15, rect.center.y-QuantityHeight/2, QuantityWidth, QuantityHeight);
			EditorGUI.LabelField(labelRect, "x" + set.quantity, style);
		
		
			Sprite sprite = set.item.sprite;
			var spriteRect = new Rect(sprite.rect.x / sprite.texture.width, sprite.rect.y / sprite.texture.height, sprite.rect.width/sprite.texture.width, sprite.rect.height/sprite.texture.height);
			
			float spriteCenterX = fieldRect.max.x + (labelRect.x - fieldRect.max.x)/2 -32;
			
			GUI.DrawTextureWithTexCoords(new Rect(spriteCenterX, rect.center.y-TextureSize/2, TextureSize, TextureSize), sprite.texture, spriteRect);
		}

		if (set.quantity < 1) {
			set.quantity = 1;
		}
		ValueEntry.SmartValue = set;
	}
}
