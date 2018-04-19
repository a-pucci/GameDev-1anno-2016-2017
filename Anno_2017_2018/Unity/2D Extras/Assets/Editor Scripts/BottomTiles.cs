using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class BottomTiles : EditorWindow
{
    public Tilemap tilemap;
    public Tile tileToAdd;

    [MenuItem("Tool/Tile Window")]
    static void Init()
    {
        BottomTiles window = (BottomTiles)EditorWindow.GetWindow(typeof(BottomTiles));
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Base Setting", EditorStyles.boldLabel);
        tilemap = (Tilemap)EditorGUILayout.ObjectField(tilemap, typeof(Tilemap));
        tileToAdd = (Tile)EditorGUILayout.ObjectField(tileToAdd, typeof(Tile));

        if(GUILayout.Button("Add Bottom Tiles"))
        {
            AddBottomTiles();
        }
        if(GUILayout.Button("Remove Bottom Tiles"))
        {
            RemoveBottomTiles();
        }
    }

    void AddBottomTiles()
    {
        foreach (var tilePosition in tilemap.cellBounds.allPositionsWithin)
        {
            
            if (tilemap.GetTile(tilePosition) != null && tilemap.GetTile(tilePosition) != tileToAdd)                    
            {
                Vector3Int tileToCheck = new Vector3Int(tilePosition.x, tilePosition.y - 1, tilePosition.z);
                if (tilemap.GetTile(tileToCheck) == null )
                {
                    tilemap.SetTile(tileToCheck, tileToAdd);
                }                
            }
        }
    }

    void RemoveBottomTiles()
    {
        foreach (var tilePosition in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.GetTile(tilePosition) == tileToAdd)
            {
                tilemap.SetTile(tilePosition, null);
            }
        }
    }
}


