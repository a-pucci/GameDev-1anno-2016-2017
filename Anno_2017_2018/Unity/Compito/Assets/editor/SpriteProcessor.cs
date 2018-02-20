using System.IO;
using UnityEngine;
using UnityEditor;

public class SpriteProcessor : AssetPostprocessor
{

    private void OnPostprocessTexture(Texture2D texture)
    {
        string lowerCaseAssetPath = assetPath.ToLower();
        if (lowerCaseAssetPath.Contains("/sprites/"))
        {
            TextureImporter textureImporter = (TextureImporter)assetImporter;
            textureImporter.textureType = TextureImporterType.Sprite;
            textureImporter.spritePixelsPerUnit = 1;
            textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
            textureImporter.spritePivot = new Vector2(0f, 1f);

            //check e fix nomenclatura file
            string filename = Path.GetFileNameWithoutExtension(assetPath).ToLower().Replace(' ', '_');
            AssetDatabase.RenameAsset(assetPath, filename);
            AssetDatabase.Refresh();
        }
    }
}
