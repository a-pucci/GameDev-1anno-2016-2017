using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Xml;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.IO;

public static class xReader
{
    [MenuItem("Scenes/Create Scenes")]
    public static void CreateScenes()
    {
        string[] xDocuments = Directory.GetFiles("Assets/Xml", "*.xml");
        Debug.Log(xDocuments.Length);
        for (int k = 0; k < xDocuments.Length; k++)
        {
            XmlDocument xData = new XmlDocument();
            xData.Load(xDocuments[k]);  

            Scene newScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

           // EditorSceneManager.LoadScene(newScene.name);
            GameObject mainCamera = new GameObject("Main Camera");
            mainCamera.AddComponent<Camera>();
            mainCamera.GetComponent<Camera>().orthographicSize = 540;
            mainCamera.GetComponent<Camera>().transform.position = new Vector3(960, -540, -10);


            /*
             * MANCA LA CREAZIONE DELLE SCENE
             */

            XmlNode xLocation = xData.SelectSingleNode("location");

            // Background
            string BGName = xLocation.Attributes["background"].Value;

            GameObject bg = new GameObject();
            bg.name = BGName + "_bg";
            bg.AddComponent<SpriteRenderer>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/sprites/location/"
                + BGName + "/background/" + BGName + ".png");

            bg.transform.position = Vector2.zero;

            bool haveFG = bool.Parse(xLocation.Attributes["foreground"].Value);
            if (haveFG)
            {
                GameObject fg = new GameObject();
                fg.name = BGName + "_fg";

                fg.AddComponent<SpriteRenderer>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/sprites/location/"
                + BGName + "/foreground/" + BGName + ".png");
                fg.transform.position = Vector2.zero;
            }

            XmlNode xHotspots = xData.SelectSingleNode("location/hotspots");

            // Load Sprites
            Sprite[] bitmapSprites = AssetDatabase.LoadAllAssetsAtPath("Assets/sprites/location/"
            + BGName + "/bitmap/sheet0.png").OfType<Sprite>().ToArray();
            Sprite[] maskSprites = AssetDatabase.LoadAllAssetsAtPath("Assets/sprites/location/"
            + BGName + "/bitmask/hotspot/sheet0.png").OfType<Sprite>().ToArray();


            XmlNodeList xChilds = xHotspots.ChildNodes;

            foreach (XmlNode xChild in xChilds)
            {
                if (xChild.Name != "goto")
                {
                    GameObject newChar = new GameObject();

                    string ID = xChild.Attributes["ID"].Value;
                    newChar.name = ID;
                    string name = xChild.Attributes["name"].Value;

                    // Position
                    newChar.AddComponent<Transform>();
                    Vector2 spritePos = new Vector2();
                    if (xChild.Attributes["X"] != null && xChild.Attributes["Y"] != null)
                    {
                        spritePos.x = int.Parse(xChild.Attributes["X"].Value);
                        spritePos.y = -int.Parse(xChild.Attributes["Y"].Value);
                        newChar.transform.position = spritePos;
                    }


                    // Sprite            
                    string bitmapName = xChild.Attributes["bitmap"].Value;
                    newChar.AddComponent<SpriteRenderer>();
                    if (bitmapName != "")
                    {
                        Sprite bitmapSprite = new Sprite();
                        for (int i = 0; i < bitmapSprites.Length; i++)
                        {
                            if (bitmapSprites[i].name == bitmapName)
                            {
                                bitmapSprite = bitmapSprites[i];
                            }
                        }
                        newChar.GetComponent<SpriteRenderer>().sprite = bitmapSprite;

                        bool shown = bool.Parse(xChild.Attributes["shown"].Value);
                        if (!shown) { newChar.GetComponent<SpriteRenderer>().enabled = false; }
                    }

                    // Mask
                    string maskName = xChild.Attributes["mask"].Value;
                    if (maskName != "null")
                    {
                        GameObject charMask = new GameObject();
                        charMask.name = maskName + "_mask";
                        charMask.transform.SetParent(newChar.transform);
                        charMask.AddComponent<SpriteRenderer>();

                        Sprite bitmaskSprite = new Sprite();
                        for (int i = 0; i < maskSprites.Length; i++)
                        {
                            if (maskSprites[i].name == maskName)
                            {
                                bitmaskSprite = maskSprites[i];
                            }
                        }
                        charMask.GetComponent<SpriteRenderer>().sprite = bitmaskSprite;
                        charMask.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);

                        charMask.AddComponent<Transform>();
                        if (xChild.Attributes["MX"] != null && xChild.Attributes["MY"] != null)
                        {
                            Vector2 maskPos = new Vector2();
                            maskPos.x = int.Parse(xChild.Attributes["MX"].Value);
                            maskPos.y = -int.Parse(xChild.Attributes["MY"].Value);
                            charMask.transform.position = maskPos;
                        }
                        else
                        {
                            charMask.transform.position = spritePos;
                        }
                    }
                }
            }
            EditorSceneManager.SaveScene(newScene, "Assets/Scenes/" + xData.Name + ".unity");
        }        
    }
}
