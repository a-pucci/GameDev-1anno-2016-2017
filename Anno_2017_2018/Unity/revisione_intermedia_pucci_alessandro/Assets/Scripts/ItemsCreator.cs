using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Xml;
using System.IO;
using System.Linq;

public static class ItemsCreator
{
    

    [MenuItem("Create/Items")]
    public static void CreateItems()
    {
        string[] languages = { "deu", "eng", "fra", "ita", "rus", "spa" };

        string[] xItems = Directory.GetFiles("Assets/Xml/deu/items", "*.xml");

        Sprite[] sprites = AssetDatabase.LoadAllAssetsAtPath("Assets/Sprites/objects.png").OfType<Sprite>().ToArray();

        for (int i = 0; i < xItems.Length; i++)
        {
            ScriptableItem newItem = new ScriptableItem();
            // ID
            newItem.ID = Path.GetFileNameWithoutExtension(xItems[i]);

            // Sprite and SpriteViewer
            for(int j = 0; j <sprites.Length; j++)
            {
                //Sprite
                if (sprites[j].name == newItem.ID)
                {
                    newItem.sprite = sprites[j];
                }
                //SpriteViewer
                if (sprites[j].name == (newItem.ID + "_v"))
                {
                    newItem.spriteViewer = sprites[j];
                }
            }

            // Names and Descriptions
            for (int j = 0; j < languages.Length; j++)
            {
                string[] xLanguageItems = Directory.GetFiles("Assets/Xml/" + languages[j] + "/items", "*.xml");
                
                XmlDocument xData = new XmlDocument();
                xData.Load(xLanguageItems[i]);

                // Name
                XmlNode xObject = xData.SelectSingleNode("object");
                newItem.names.Add(xObject.Attributes["name"].Value);

                // Description
                string description = "";
                XmlNodeList xDescriptions = xData.SelectNodes("object/description/command");

                foreach(XmlNode xCommand in xDescriptions)
                {
                    string[] commandTokens = xCommand.InnerText.Split('"');
                    if(commandTokens[0] == "text ")
                    {
                        description += (commandTokens[1] + " ");
                    }
                }
                newItem.descriptions.Add(description);
            }

            AssetDatabase.CreateAsset(newItem, "Assets/Resources/ScriptableObjects/" + newItem.ID + ".asset");
            AssetDatabase.Refresh();            
        }
    }

}
