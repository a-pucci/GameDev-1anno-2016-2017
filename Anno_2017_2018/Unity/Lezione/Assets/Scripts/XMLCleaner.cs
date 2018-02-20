using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Xml;

public static class XMLCleaner
{
    [MenuItem("Pokedex/ClearXML")]
    static void ClearXML()
    {
        string[] toRemove = { "ability", "exp", "ratio", "egg-group", "species", "moves" };

        XmlDocument xData = new XmlDocument();
        xData.Load("Assets/pokedata.xml");

        XmlNodeList xPokemons = xData.SelectNodes("pokedex/pokemon");

        foreach (XmlNode xPokemon in xPokemons)
        {
            for (int i = 0; i < toRemove.Length; i++)
            {
                XmlNodeList xAttributes = xPokemon.SelectNodes(toRemove[i]);

                for (int j = xAttributes.Count - 1; j >= 0; j--)
                {
                    XmlNode xNodeToRemove = xAttributes[j];
                    if (xNodeToRemove != null)
                    {
                        xPokemon.RemoveChild(xNodeToRemove);
                    }
                }
            }
        }

        xData.Save("Assets/pokedata_clear.xml");
        Debug.Log("XML Clear");
    }
}
