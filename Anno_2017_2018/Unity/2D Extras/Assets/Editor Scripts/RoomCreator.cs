using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

enum Directions {UP, RIGHT, DOWN, LEFT};

public class RoomCreator : EditorWindow
{
    public ScriptableDungeon dungeon;
    public Tilemap groundTilemap;
    public Tilemap bridgeTilemap;
    public Tilemap pickupsTilemap;

    private int roomsNumber;
    private int roomSize;

    private RuleTile bridgeTile;
    private RuleTile groundTile;
    private Tile barrelTile;
    private Tile chestTile;
    private float barrelRate;
    private float chestRate;


    List<Vector3Int> roomsPositions = new List<Vector3Int>();
    int counter;
    int roomsCreated;


    [MenuItem("Tool/Room Window")]
    static void Init()
    {
        RoomCreator window = (RoomCreator)EditorWindow.GetWindow(typeof(RoomCreator));
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Rooms Creator", EditorStyles.boldLabel);
        GUILayout.Space(10);
        GUILayout.Label("Scriptable Dungeon", EditorStyles.label);
        dungeon = (ScriptableDungeon)EditorGUILayout.ObjectField(dungeon, typeof(ScriptableDungeon));
        //GUILayout.Label("Rooms Number", EditorStyles.label);
        //roomsNumber = (int)EditorGUILayout.IntField(roomsNumber);
        //GUILayout.Label("Room Size", EditorStyles.label);
        //roomSize = (int)EditorGUILayout.IntField(roomSize);
        GUILayout.Space(20);
        GUILayout.BeginVertical("Box"); 
        GUILayout.Label("Ground Tilemap", EditorStyles.label);
        groundTilemap = (Tilemap)EditorGUILayout.ObjectField(groundTilemap, typeof(Tilemap));
        GUILayout.Label("Bridge Tilemap", EditorStyles.label);
        bridgeTilemap = (Tilemap)EditorGUILayout.ObjectField(bridgeTilemap, typeof(Tilemap));
        GUILayout.Label("Pickups Tilemap", EditorStyles.label);
        pickupsTilemap = (Tilemap)EditorGUILayout.ObjectField(pickupsTilemap, typeof(Tilemap));
        //GUILayout.Label("Ground Tile", EditorStyles.label);
        //groundTile = (RuleTile)EditorGUILayout.ObjectField(groundTile, typeof(RuleTile));
        //GUILayout.Label("Bridge Tile", EditorStyles.label);
        //bridgeTile = (RuleTile)EditorGUILayout.ObjectField(bridgeTile, typeof(RuleTile));
        GUILayout.EndVertical();

        GUILayout.Space(10);
        if (GUILayout.Button("Create Dungeon"))
        {
            CreateRooms();
        }
        GUILayout.Space(10);
        if (GUILayout.Button("Clear Tilemaps"))
        {
            ClearTilemaps();
        }
    }

    void CreateRooms()
    {
        roomsNumber = dungeon.roomsNumber;
        roomSize = dungeon.roomSize;
        bridgeTile = dungeon.bridgeTile;
        groundTile = dungeon.groundTile;
        barrelTile = dungeon.barrelTile;
        chestTile = dungeon.chestTile;
        barrelRate = dungeon.barrelsRate;
        chestRate = dungeon.chestRate;

        // svuota la tilemap        
        ClearTilemaps();
        roomsCreated = 0;

        Vector3Int roomPosition = Vector3Int.zero;
       
        for (int i = 0; i < roomsNumber; i++)
        {
            counter = 0;
            CreateRoom(roomPosition);
            roomsPositions.Add(roomPosition);

            int direction;

            // non deve creare il passaggio nell'ultima stanza
            if(i != roomsNumber-1)
            {
                bool isEmpty;
                do
                {
                    direction = Random.Range(0, 4);
                    isEmpty = EmptySpot(roomPosition, direction);
                    counter++;
                }
                while ((isEmpty == false) && (counter <= 20));

                Debug.Log("Direction: " + direction + " , Empty: " + isEmpty);
                if(counter == 20)
                {
                    i = roomsNumber;
                    Debug.Log("Dead End");
                }
                else
                {
                    CreatePassage(roomPosition, direction);
                    roomPosition = GetNewPosition(roomPosition, direction);
                }
            }
            roomsCreated++;
        }
        Debug.Log("Rooms Created: " + roomsCreated);
    }

    void CreateRoom(Vector3Int startPosition)
    {
        Vector3Int pos = startPosition;
        for(int i = 0; i < roomSize; i++)
        {
            for(int j = 0; j < roomSize; j++)
            {
                pos.x = startPosition.x + i;
                pos.y = startPosition.y + j;
                groundTilemap.SetTile(pos, groundTile);
            }
        }
    }

    void ClearTilemaps()
    {
        foreach (var tilePosition in groundTilemap.cellBounds.allPositionsWithin)
        {
            groundTilemap.SetTile(tilePosition, null);
        }
        foreach (var tilePosition in bridgeTilemap.cellBounds.allPositionsWithin)
        {
            bridgeTilemap.SetTile(tilePosition, null);
        }
        foreach (var tilePosition in pickupsTilemap.cellBounds.allPositionsWithin)
        {
            pickupsTilemap.SetTile(tilePosition, null);
        }
    }

    bool EmptySpot(Vector3Int position, int dir)
    {
        bool empty = false;

        Vector3Int pos = new Vector3Int();
        switch (dir)
        {
            // UP
            case (int)Directions.UP:
                pos = new Vector3Int(position.x, position.y + roomSize + 1, position.z);
                break;
            // RIGHT
            case (int)Directions.RIGHT:
                pos = new Vector3Int(position.x + roomSize + 1, position.y, position.z);
                break;
            // DOWN
            case (int)Directions.DOWN:
                pos = new Vector3Int(position.x, position.y - roomSize - 1, position.z);
                break;
            // LEFT
            case (int)Directions.LEFT:
                pos = new Vector3Int(position.x - roomSize - 1, position.y, position.z);
                break;
        }
        if (groundTilemap.GetTile(pos) == null)
        {
            empty = true;
        }
        return empty;
    }

    void CreatePassage(Vector3Int roomStart, int dir)
    {
        Vector3Int passagePosition = new Vector3Int();
        Vector3Int endPassage = new Vector3Int();
        Vector3Int startPassage = new Vector3Int();

        switch (dir)
        {
            // UP
            case (int)Directions.UP:
                passagePosition = new Vector3Int(roomStart.x + (roomSize / 2), roomStart.y + roomSize, roomStart.z);
                startPassage = new Vector3Int(passagePosition.x, passagePosition.y - 1, passagePosition.z);
                endPassage = new Vector3Int(passagePosition.x, passagePosition.y + 1, passagePosition.z);
                break;

            // RIGHT
            case (int)Directions.RIGHT:
                passagePosition = new Vector3Int(roomStart.x + roomSize, roomStart.y + (roomSize/2), roomStart.z);
                startPassage = new Vector3Int(passagePosition.x - 1, passagePosition.y, passagePosition.z);
                endPassage = new Vector3Int(passagePosition.x + 1, passagePosition.y, passagePosition.z);
                break;

            // DOWN
            case (int)Directions.DOWN:
                passagePosition = new Vector3Int(roomStart.x + (roomSize / 2), roomStart.y - 1, roomStart.z);
                startPassage = new Vector3Int(passagePosition.x, passagePosition.y - 1, passagePosition.z);
                endPassage = new Vector3Int(passagePosition.x, passagePosition.y + 1, passagePosition.z);
                break;

            // LEFT
            case (int)Directions.LEFT:
                passagePosition = new Vector3Int(roomStart.x - 1, roomStart.y + (roomSize / 2), roomStart.z);
                startPassage = new Vector3Int(passagePosition.x - 1, passagePosition.y, passagePosition.z);
                endPassage = new Vector3Int(passagePosition.x + 1, passagePosition.y, passagePosition.z);
                break;
        }
        bridgeTilemap.SetTile(startPassage, bridgeTile);
        bridgeTilemap.SetTile(passagePosition, bridgeTile);
        bridgeTilemap.SetTile(endPassage, bridgeTile);
    }

    Vector3Int GetNewPosition(Vector3Int position, int dir)
    {
        Vector3Int newPos = new Vector3Int();
        switch(dir)
        {
            // UP
            case (int)Directions.UP:
                newPos = new Vector3Int(position.x , position.y + roomSize + 1, position.z);
                break;
            // RIGHT
            case (int)Directions.RIGHT:
                newPos = new Vector3Int(position.x + roomSize + 1, position.y, position.z);
                break;
            // DOWN
            case (int)Directions.DOWN:
                newPos = new Vector3Int(position.x, position.y - roomSize - 1, position.z);
                break;
            // LEFT
            case (int)Directions.LEFT:
                newPos = new Vector3Int(position.x - roomSize - 1, position.y, position.z);
                break;
        }
        return newPos;
    }
}
