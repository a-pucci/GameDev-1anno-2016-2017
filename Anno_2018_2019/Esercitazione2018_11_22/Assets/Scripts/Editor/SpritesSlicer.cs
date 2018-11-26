using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

public class SpritesSlicer : OdinEditorWindow
{
	[FoldoutGroup("Slice By Cell Size")][PropertyOrder(-4)]
	public int cellWidth ;
	[FoldoutGroup("Slice By Cell Size")][PropertyOrder(-4)]
	public int cellHeight;

	[FoldoutGroup("Slice By Cell Count")] [PropertyOrder(-3)]
	public int rowsCount;
	[FoldoutGroup("Slice By Cell Count")] [PropertyOrder(-3)]
	public int columnsCount;

	//[FoldoutGroup("Slice Settings")][PropertyOrder(-5)] 
	//public Vector2 pivot = new Vector2(0.5f, 0.5f);
	[FoldoutGroup("Slice Settings")] [PropertyOrder(-5)]
	public PivotPositions pivot;
	[FoldoutGroup("Slice Settings")] [PropertyOrder(-5)]
	public int widthSpace;
	[FoldoutGroup("Slice Settings")] [PropertyOrder(-5)]
	public int heightSpace;
	
	[Space(20)]
	[PropertyOrder(10)]
	public List<Texture2D> textures;
	
	[MenuItem("Tools/Slice Sprites")]
	private static void OpenWindow()
	{
		GetWindow<SpritesSlicer>().Show();
	}
	
	[PropertyOrder(-1)]
	[FoldoutGroup("Custom Sprite Slicers")]
	[Button(ButtonSizes.Medium)]
	private void SlicePokemonSprites()
	{
		for (int z = 0; z < textures.Count; z++)
		{
			string path = AssetDatabase.GetAssetPath(textures[z]);
			var ti = AssetImporter.GetAtPath(path) as TextureImporter;
			ti.isReadable = true;
			ti.spriteImportMode = SpriteImportMode.Multiple;
 
			var newData = new List<SpriteMetaData>();

			for (int i = 0; i < 3; i++)
			{
				SpriteMetaData smd = new SpriteMetaData();
				smd.pivot = new Vector2(0.5f, 0.5f);
				smd.alignment = 9;
				smd.name = textures[z].name + "_" + i;
					
				switch (i)
				{
					case 0:
						smd.rect = new Rect(0, 0, 64, 64);
						break;
						
					case 1:
						smd.rect = new Rect(64, 0, 64, 64);
						break;
						
					case 2:
						smd.rect = new Rect(128, 0, 32, 32);
						break;

				}
				newData.Add(smd);
			}
			
			ti.spritesheet = newData.ToArray();
			AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
		}
		Debug.Log("Done Slicing!");
	}
	
	[PropertySpace(10)]
	[PropertyOrder(-4)]
	[FoldoutGroup("Slice By Cell Size")]
	[Button(ButtonSizes.Medium)]
	private void SliceSpritesByCellSize()
	{
		for (int z = 0; z < textures.Count; z++)
		{
			string path = AssetDatabase.GetAssetPath(textures[z]);
			var ti = AssetImporter.GetAtPath(path) as TextureImporter;
			ti.isReadable = true;
			ti.spriteImportMode = SpriteImportMode.Multiple;
 
			var newData = new List<SpriteMetaData>();
			int count = 0;
			for (int i = 0; i < textures[z].width; i += cellWidth + widthSpace)
			{
				for (int j = textures[z].height; j > 0; j -= cellHeight + heightSpace)
				{
					var smd = new SpriteMetaData();
					smd.pivot = GetPivotPosition(pivot);
					smd.alignment = 9;
					smd.name = textures[z].name + "_" + count;
					smd.rect = new Rect(i, j - cellHeight, cellWidth, cellHeight);
			
					newData.Add(smd);
				}
				count++;
			}
			
			ti.spritesheet = newData.ToArray();
			AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
		}
		Debug.Log("Done Slicing!");
	}
	
	[PropertySpace(10)]
	[PropertyOrder(-3)]
	[FoldoutGroup("Slice By Cell Count")]
	[Button(ButtonSizes.Medium)]
	private void SliceSpritesByCellsCount()
	{
		for (int z = 0; z < textures.Count; z++)
		{
			string path = AssetDatabase.GetAssetPath(textures[z]);
			var ti = AssetImporter.GetAtPath(path) as TextureImporter;
			ti.isReadable = true;
			ti.spriteImportMode = SpriteImportMode.Multiple;

			int newCellHeight = textures[z].height / columnsCount;
			int newCellWidth = textures[z].width / rowsCount;
 
			var newData = new List<SpriteMetaData>();
			int count = 0;
			for (int i = 0; i < rowsCount; i += newCellHeight + widthSpace)
			{
				for (int j = columnsCount; j > 0; j -= newCellWidth + heightSpace)
				{
					var smd = new SpriteMetaData();
					smd.pivot = GetPivotPosition(pivot);
					smd.alignment = 9;
					smd.name = textures[z].name + "_" + count;
					smd.rect = new Rect(i, j - newCellHeight, newCellWidth, newCellHeight);
			
					newData.Add(smd);
				}
				count++;
			}
			
			ti.spritesheet = newData.ToArray();
			AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
		}
		Debug.Log("Done Slicing!");
	}

	private Vector2 GetPivotPosition(PivotPositions pivotPosition)
	{
		var pivotPos = new Vector2();
		
		switch (pivotPosition)
		{
			case PivotPositions.Center:
				pivotPos = new Vector2(0.5f, 0.5f);
				break;
				
			case PivotPositions.TopLeft:
				pivotPos = new Vector2(0.5f, 0.5f);
				break;
			
			case PivotPositions.TopRight:
				pivotPos = new Vector2(0.5f, 0.5f);
				break;
			
			case PivotPositions.BottomLeft:
				pivotPos = new Vector2(0.5f, 0.5f);
				break;
			
			case PivotPositions.BottomRight:
				pivotPos = new Vector2(0.5f, 0.5f);
				break;
				
		}
		
		return pivotPos;
	}
	
	public enum PivotPositions
	{
		TopLeft,
		TopCenter,
		TopRight,
		CenterLeft,
		Center,
		CenterRight,
		BottomLeft,
		BottomCenter,
		BottomRight
	}
}