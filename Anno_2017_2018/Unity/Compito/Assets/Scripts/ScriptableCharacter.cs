using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableCharacter : ScriptableObject
{
    public string ID;
    public string name;
    public Vector2 spritePosition = new Vector2();
    public string mask;
    public Vector2 maskPosition = new Vector2();
    public Sprite bitmap;
    public bool shown; 
}
