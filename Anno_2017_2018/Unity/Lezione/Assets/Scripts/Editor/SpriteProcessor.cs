using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpriteProcessor : AssetPostprocessor
{

    void OnPostProcessTexture(Texture2D texture)
    {
        if(assetPath.Contains("/Sprites/"))
        {
            TextureImporter textureImporter = (TextureImporter)assetImporter;
            textureImporter.textureType = TextureImporterType.Sprite;
            textureImporter.spritePixelsPerUnit = 1;
            textureImporter.textureCompression = TextureImporterCompression.Uncompressed;

            string filename = assetPath.Split('/')[assetPath.Split('/').Length - 1];
            filename = filename.Split('.')[0];
            filename = filename.ToLower();
            filename = filename.Replace(' ', '_');
            AssetDatabase.RenameAsset(assetPath, filename);
            AssetDatabase.Refresh();
        }
    }


}
