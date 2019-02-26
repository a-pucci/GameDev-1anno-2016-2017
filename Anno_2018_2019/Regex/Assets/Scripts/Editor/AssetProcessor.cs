using UnityEditor;
using UnityEngine;

public class AssetProcessor : AssetPostprocessor {
	
	public void OnPostprocessTexture(Texture2D texture) 
	{
		if (assetPath.Contains("Sprites/"))
		{
			var textureImporter = (TextureImporter)assetImporter;
			textureImporter.textureType = TextureImporterType.Sprite;
			textureImporter.mipmapEnabled = false;
		}
	}
}