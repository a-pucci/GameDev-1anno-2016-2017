using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Linq;

public class RenameAssetsWindow : OdinEditorWindow
{
	[Space(20)]
	[PropertyOrder(-10)]
	public string newName;
	[PropertyOrder(-9)]
	public int startingIndex;
	[Space(10)]
	public List<Object> assets;

	[MenuItem("Tools/Rename Assets")]
	static void OpenWindow()
	{
		GetWindow<RenameAssetsWindow>().Show();
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

			string assetName = newName + "_" + (i+startingIndex);
			AssetDatabase.RenameAsset(path, assetName);
			float prog = i / listSize - 1;
			EditorUtility.DisplayProgressBar("Assets Rename", "Renaming Asset " + i + " / " + listSize, prog);
		}
		EditorUtility.ClearProgressBar();
	}

	[ButtonGroup]
	[Button(ButtonSizes.Medium)]
	public void Clear()
	{
		assets.Clear();
		newName = "";
	}
}