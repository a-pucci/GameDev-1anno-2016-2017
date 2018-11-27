using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;

public class SpritesSlicer : OdinEditorWindow
{
	[TabGroup("Slice By Cell Count")][TabGroup("Slice By Cell Size")][PropertyOrder(-4)][ReadOnly]
	public Size firstTextureSize;
	[Space(10)][TabGroup("Slice By Cell Size")][PropertyOrder(-4)][OnValueChanged("UpdateValues")]
	public int cellWidth;
	[TabGroup("Slice By Cell Size")][PropertyOrder(-4)][OnValueChanged("UpdateValues")]
	public int cellHeight;
	[Space(10)][TabGroup("Slice By Cell Size")][PropertyOrder(-4)][ReadOnly][LabelText("Textures Count After Slice")]
	public int texturesAfterCellSlice;
	
	[Space(10)][TabGroup("Slice By Cell Count")][PropertyOrder(-3)][OnValueChanged("UpdateValues")]
	public int rowsCount;
	[TabGroup("Slice By Cell Count")][PropertyOrder(-3)][OnValueChanged("UpdateValues")]
	public int columnsCount;
	[Space(10)][TabGroup("Slice By Cell Count")][PropertyOrder(-3)][ReadOnly][LabelText("Textures Size After Slice")]
	public Size texturesSizeAfterCountSlice;
	[TabGroup("Slice By Cell Count")][PropertyOrder(-3)][ReadOnly][LabelText("Textures Count After Slice")]
	public int texturesAfterCountSlice;
	
	[FoldoutGroup("Slice Settings")][PropertyOrder(-5)]
	public PivotPositions pivot;
	[FoldoutGroup("Slice Settings")][PropertyOrder(-5)]
	public int widthSpace;
	[FoldoutGroup("Slice Settings")][PropertyOrder(-5)]
	public int heightSpace;
	
	private SliceTypes sliceType;
	
	[Space(20)][PropertyOrder(10)][OnValueChanged("UpdateValues")]
	public List<Texture2D> textures;
	
	[MenuItem("Tools/Slice Sprites")]
	private static void OpenWindow()
	{
		var window = GetWindow<SpritesSlicer>();
		window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
	}
	
	[PropertySpace(10)]
	[PropertyOrder(-4)]
	
	[TabGroup("Slice By Cell Size")]
	[Button(ButtonSizes.Medium)]
	private void SliceSpritesByCellSize()
	{
		Slice(cellWidth, cellHeight);
	}
	
	[PropertySpace(10)]
	[PropertyOrder(-3)]
	[TabGroup("Slice By Cell Count")]
	[Button(ButtonSizes.Medium)]
	private void SliceSpritesByCellsCount()
	{
		int newCellHeight = textures[0].height / columnsCount;
		int newCellWidth = textures[0].width / rowsCount;
		Slice(newCellWidth, newCellHeight);
	}

	private void Slice(int width, int height)
	{
		for (int z = 0; z < textures.Count; z++)
		{
			string path = AssetDatabase.GetAssetPath(textures[z]);
			var ti = AssetImporter.GetAtPath(path) as TextureImporter;
			if (ti != null)
			{
				ti.isReadable = true;
				ti.spriteImportMode = SpriteImportMode.Multiple;

				var newData = new List<SpriteMetaData>();
				int count = 0;
				for (int i = 0; i < textures[z].width; i += width + widthSpace)
				{
					for (int j = textures[z].height; j > 0; j -= height + heightSpace)
					{
						var smd = new SpriteMetaData();
						smd.pivot = GetPivotPosition(pivot);
						smd.alignment = 9;
						smd.name = textures[z].name + "_" + count;
						smd.rect = new Rect(i, j - height, width, height);

						newData.Add(smd);
					}
					count++;
				}
				ti.spritesheet = newData.ToArray();
			}
			AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
		}
		Debug.Log("Done Slicing!");
	}

	private static Vector2 GetPivotPosition(PivotPositions pivotPosition)
	{
		var pivotPos = new Vector2();
		
		switch (pivotPosition)
		{
			case PivotPositions.TopLeft:
				pivotPos = new Vector2(0f, 1f);
				break;
			
			case PivotPositions.TopCenter:
				pivotPos = new Vector2(0.5f, 1f);
				break;
			
			case PivotPositions.TopRight:
				pivotPos = new Vector2(1f, 1f);
				break;
			
			case PivotPositions.CenterRight:
				pivotPos = new Vector2(1f, 0.5f);
				break;
			
			case PivotPositions.Center:
				pivotPos = new Vector2(0.5f, 0.5f);
				break;
				
			case PivotPositions.CenterLeft:
				pivotPos = new Vector2(0f, 0.5f);
				break;
			
			case PivotPositions.BottomLeft:
				pivotPos = new Vector2(0f, 0f);
				break;
			
			case PivotPositions.BottomCenter:
				pivotPos = new Vector2(0.5f, 0f);
				break;
			
			case PivotPositions.BottomRight:
				pivotPos = new Vector2(1f, 0f);
				break;
		}
		return pivotPos;
	}

	private void UpdateValues()
	{
		if(!textures.IsNullOrEmpty())
		{
			firstTextureSize.width = textures[0].width;
			firstTextureSize.height = textures[0].height;

			try 
			{ 
				texturesSizeAfterCountSlice.height = textures[0].height / columnsCount;
				texturesSizeAfterCountSlice.width = textures[0].width / rowsCount;
				texturesAfterCountSlice = columnsCount * rowsCount;
			}
			catch
			{
				texturesAfterCountSlice = 0;
			}
			
			try
			{
				texturesAfterCellSlice = (firstTextureSize.width / cellWidth) * (firstTextureSize.height / cellHeight);
			}
			catch
			{
				texturesAfterCellSlice = 0;
			}
		}
		else
		{
			firstTextureSize.width = 0;
			firstTextureSize.height = 0;
			texturesAfterCellSlice = 0;
			
			texturesSizeAfterCountSlice.height = 0;
			texturesSizeAfterCountSlice.width = 0;
			texturesAfterCountSlice = 0;
		}
	}

	private enum SliceTypes
	{
		SliceByCellsCount,
		SliceByCellSize,
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

	[Serializable]
	[InlineProperty(LabelWidth = 50)]
	public struct Size
	{
		[HorizontalGroup] public int width;
		[HorizontalGroup] public int height;
	}
}