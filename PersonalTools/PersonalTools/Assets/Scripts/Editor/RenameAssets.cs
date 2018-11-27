using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Linq;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;

public class RenameAssetsWindow : OdinEditorWindow
{
	[Space(20)]
	[PropertyOrder(-10)]
	public string assetsName;
	[PropertyOrder(-9)]
	public int startingIndex;
	[Space(10)]
	public List<Object> assets;

	[MenuItem("Tools/Rename Assets")]
	private static void OpenWindow()
	{
		var window = GetWindow<RenameAssetsWindow>();
		window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
	}

	[PropertyOrder(-9)]
	[ButtonGroup]
	[Button(ButtonSizes.Medium)]
	public void Sort()
	{
		List<Object> newList = assets.OrderBy(o => o.name).ToList();
		assets = newList;
	}

	[ButtonGroup]
	[Button(ButtonSizes.Medium)]
	public void RenameAssets()
	{
		int listSize = assets.Count;
		for (int i = 0; i < listSize; i++)
		{
			string path = AssetDatabase.GetAssetPath(assets[i]);

			string newName = assetsName + "_" + (i+startingIndex);
			AssetDatabase.RenameAsset(path, newName);
			float progress = i / listSize - 1;
			EditorUtility.DisplayProgressBar("Assets Rename", "Renaming Asset " + i + " / " + listSize, progress);
		}
		EditorUtility.ClearProgressBar();
	}

	[ButtonGroup]
	[Button(ButtonSizes.Medium)]
	public void Clear()
	{
		assets.Clear();
		assetsName = "";
	}
}