using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ScriptableItem : ScriptableObject
{
    public string ID;
    [PreviewField]
    public Sprite sprite;
    public List<string> names = new List<string>();
    [PreviewField]
    public Sprite spriteViewer;
    public List<string> descriptions = new List<string>();
}
