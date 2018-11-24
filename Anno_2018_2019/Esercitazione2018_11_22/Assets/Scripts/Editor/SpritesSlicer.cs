using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

public class SpritesSlicer : OdinEditorWindow
{
	[PropertyOrder(-4)]
	public int sliceWidth ;
	[PropertyOrder(-3)]
	public int sliceHeight;
	[Space(20)]
	public List<Texture2D> textures;
	
	[MenuItem("Tools/Slice Sprites")]
	static void OpenWindow()
	{
		GetWindow<SpritesSlicer>().Show();
	}
	
	[PropertyOrder(-1)]
	[ButtonGroup]
	[Button(ButtonSizes.Medium)]
	void SlicePokemonSprites()
	{
		for (int z = 0; z < textures.Count; z++)
		{
			string path = AssetDatabase.GetAssetPath(textures[z]);
			TextureImporter ti = AssetImporter.GetAtPath(path) as TextureImporter;
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
	
	[PropertyOrder(-1)]
	[ButtonGroup]
	[Button(ButtonSizes.Medium)]
	void SliceSprites()
	{
		for (int z = 0; z < textures.Count; z++)
		{
			string path = AssetDatabase.GetAssetPath(textures[z]);
			TextureImporter ti = AssetImporter.GetAtPath(path) as TextureImporter;
			ti.isReadable = true;
			ti.spriteImportMode = SpriteImportMode.Multiple;
 
			var newData = new List<SpriteMetaData>();
			int count = 0;
			for (int i = 0; i < textures[z].width; i += sliceWidth)
			{
				for (int j = textures[z].height; j > 0; j -= sliceHeight)
				{
					SpriteMetaData smd = new SpriteMetaData();
					smd.pivot = new Vector2(0.5f, 0.5f);
					smd.alignment = 9;
					smd.name = textures[z].name + "_" + count;
					smd.rect = new Rect(i, j - sliceHeight, sliceWidth, sliceHeight);
			
					newData.Add(smd);
				}
				count++;
			}
			
			ti.spritesheet = newData.ToArray();
			AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
		}
		Debug.Log("Done Slicing!");
	}
}