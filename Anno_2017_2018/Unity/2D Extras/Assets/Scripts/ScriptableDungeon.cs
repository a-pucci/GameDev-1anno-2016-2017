using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New_Dungeon", menuName = "Dungeon/ScriptableDungeon")]
public class ScriptableDungeon : ScriptableObject
{
    [BoxGroup("Rooms Settings")]
    public int roomsNumber;
    [BoxGroup("Rooms Settings")]
    public int roomSize;
    [Space(20)]
    [BoxGroup("Ground")]
    [PreviewField]
    public RuleTile groundTile;
    [Space(20)]
    [BoxGroup("Bridges")]
    [PreviewField]
    public RuleTile bridgeTile;
    [Space(20)]
    [BoxGroup("Pickups")]
    [PreviewField]
    public Tile barrelTile;
    [BoxGroup("Pickups")]
    [Range(0f,1f)]
    public float barrelsRate;
    [BoxGroup("Pickups")]
    [PreviewField]
    public Tile chestTile;
    [BoxGroup("Pickups")]
    [Range(0f,1f)]
    public float chestRate;
}
