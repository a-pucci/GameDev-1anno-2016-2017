using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.IO;
using Sirenix.OdinInspector;

public class InventoryItems : MonoBehaviour
{
    [DisableInPlayMode]
    public List<string> itemList;

    ScriptableItem scriptable;

    private void Start()
    {

        for(int i = 0; i < itemList.Count-1; i++)
        {
            scriptable = (ScriptableItem)AssetDatabase.LoadAssetAtPath("Assets/Resources/ScriptableObjects/" + itemList[i] + ".asset", typeof(ScriptableItem));
            GameObject item = new GameObject();
            item.name = itemList[i];
            item.AddComponent<SpriteRenderer>().sprite = scriptable.sprite;
            item.transform.SetParent(this.transform);
        }
    }
}
