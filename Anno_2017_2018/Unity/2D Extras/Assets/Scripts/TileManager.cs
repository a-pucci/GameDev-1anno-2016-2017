using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour {

    public Tilemap tilemap;
    public Tile tileToReplace;

	void Start ()
    {
		foreach(var tilePosition in tilemap.cellBounds.allPositionsWithin)
        {
            if(tilemap.GetTile(tilePosition) != null)
            {
                tilemap.SetTile(tilePosition, tileToReplace);
            }
        }
	}
	

	void Update ()
    {
		if(Input.GetMouseButton(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
            tilemap.SetTile(cellPosition, null);
        }
	}
}
