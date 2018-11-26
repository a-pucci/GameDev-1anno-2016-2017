using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Xml;
using System;

public static class PokedexCreator
{
    [MenuItem("Pokemon Tools/Create Pokemons")]
    public static void CreatePokedex()
    {
        var xData = new XmlDocument();
        xData.Load("Assets/pokedata_clear.xml");

        XmlNodeList xPokemonList = xData.SelectNodes("pokedex/pokemon");
        

        if (xPokemonList != null)
        {
            int progressCounter = 1;
            foreach (XmlNode xPokemon in xPokemonList)
            {
                
                if (xPokemon.Attributes != null && int.Parse(xPokemon.Attributes["id"].Value) <= 151)
                {
                    var pokemonObj = ScriptableObject.CreateInstance<Pokemon>();
                    AssetDatabase.CreateAsset(pokemonObj, "Assets/Resources/Pokedex/" + xPokemon.Attributes["id"].Value + "_" + xPokemon.SelectSingleNode("name").InnerText + ".asset");

                    pokemonObj.id = int.Parse(xPokemon.Attributes["id"].Value);
                    Sprite[] pokemonSprites = AssetDatabase.LoadAllAssetsAtPath("Assets/Sprites/Pokemons/pokemon_" + pokemonObj.id + ".png").OfType<Sprite>().ToArray();
                    
                    pokemonObj.front = pokemonSprites[0];
                    pokemonObj.back = pokemonSprites[1];
                    pokemonObj.icon = pokemonSprites[2];

                    foreach (XmlNode xChild in xPokemon.ChildNodes)
                    {
                        switch (xChild.Name)
                        {
                            case "name":
                                pokemonObj.name = xChild.InnerText;
                                break;
                    
                            case "type":
                                string typeString = xChild.InnerText;
                                PokemonTypes typeEnum = StringToEnumType(typeString);
                                pokemonObj.types.Add(typeEnum);
                                break;
                    
                            case "stats":
                                foreach (XmlNode xGrandChild in xChild.ChildNodes)
                                {
                                    switch (xGrandChild.Name)
                                    {
                                        case "HP":
                                            pokemonObj.hp = int.Parse(xGrandChild.InnerText);
                                            break;
                                        case "SPD":
                                            pokemonObj.spd = int.Parse(xGrandChild.InnerText);
                                            break;
                                    }
                                }
                                break;
                    
                            case "evolutions":
                                foreach (XmlNode xEvolution in xChild.ChildNodes)
                                {
                                    if (xEvolution.Attributes != null)
                                    {
                                        var evolution = new Evolution {id = int.Parse(xEvolution.Attributes["id"].Value)};
                                        foreach (XmlNode xEvolutionChild in xEvolution.ChildNodes)
                                        {
                                            switch (xEvolutionChild.Name)
                                            {
                                                case "name":
                                                    evolution.name = xEvolutionChild.InnerText;
                                                    break;
                                                case "lvl":
                                                    evolution.lvl = int.Parse(xEvolutionChild.InnerText);
                                                    break;
                                            }
                                        }
                                        pokemonObj.evolutions.Add(evolution);
                                    }
                                }
                                break;
                    
                            case "description":
                                pokemonObj.description = xChild.InnerText;
                                break;
                        }
                    }
                    EditorUtility.SetDirty(pokemonObj);
                    
                    float prog = progressCounter / 151 - 1;
                    EditorUtility.DisplayProgressBar("Pokedex", "Creating Pokedex " + progressCounter + " / " + 151, prog);
                    progressCounter++;
                }
            }
            EditorUtility.ClearProgressBar();
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Pokedex Created");
    }
    
    private static PokemonTypes StringToEnumType(string stringType)
    {
        PokemonTypes type = PokemonTypes.Normal;
        foreach (PokemonTypes enumType in Enum.GetValues(typeof(PokemonTypes)))
        {
            if (stringType == enumType.ToString())
            {
                type = enumType;
            }
        }
        return type;
    }
}