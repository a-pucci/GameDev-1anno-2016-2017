using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Xml;
using System.IO;

public static class PokedexCreator
{
    [MenuItem("Pokedex/Create")]
    static void CreatePokedex()
    {
        XmlDocument xData = new XmlDocument();
        xData.Load("Assets/pokedata_clear.xml");

        XmlNodeList xPokemonList = xData.SelectNodes("pokedex/pokemon");
        Sprite[] pokemonsSprite = AssetDatabase.LoadAllAssetsAtPath("Assets/Sprites/pokedex.png").OfType<Sprite>().ToArray();

        foreach (XmlNode xPokemon in xPokemonList)
        {
            ScriptablePokemon pokemonObj = new ScriptablePokemon();
            AssetDatabase.CreateAsset(pokemonObj, "Assets/Pokedex/" + xPokemon.Attributes["id"].Value + "_" + xPokemon.SelectSingleNode("name").InnerText + ".asset");

            pokemonObj.id = int.Parse(xPokemon.Attributes["id"].Value);

            int spriteID = (pokemonObj.id - 1);
            pokemonObj.sprite = pokemonsSprite[spriteID];

            foreach (XmlNode xChild in xPokemon.ChildNodes)
            {
                switch (xChild.Name)
                {
                    case "name":
                        pokemonObj.name = xChild.InnerText;

                        // add enum value
                        string codeText = File.ReadAllText("Assets/Scripts/PokemonEnum.cs");
                        int markerIndex = codeText.LastIndexOf("//#MARKER#");
                        File.WriteAllText("Assets/Scripts/PokemonEnum.cs", codeText.Insert(markerIndex, pokemonObj.name + ",\n\t"));

                        break;

                    case "type":
                        pokemonObj.type.Add(xChild.InnerText);
                        break;

                    case "stats":
                        foreach (XmlNode xGrandChild in xChild.ChildNodes)
                        {
                            switch (xGrandChild.Name)
                            {
                                case "HP":
                                    pokemonObj.HP = int.Parse(xGrandChild.InnerText);
                                    break;
                                case "ATK":
                                    pokemonObj.ATK = int.Parse(xGrandChild.InnerText);
                                    break;
                                case "DEF":
                                    pokemonObj.DEF = int.Parse(xGrandChild.InnerText);
                                    break;
                                case "SPD":
                                    pokemonObj.SPD = int.Parse(xGrandChild.InnerText);
                                    break;
                                case "SAT":
                                    pokemonObj.SAT = int.Parse(xGrandChild.InnerText);
                                    break;
                                case "SDF":
                                    pokemonObj.SDF = int.Parse(xGrandChild.InnerText);
                                    break;
                            }
                        }
                        break;

                    case "evolutions":
                        foreach (XmlNode xEvolution in xChild.ChildNodes)
                        {
                            Evolution evolution = new Evolution();
                            evolution.id = int.Parse(xEvolution.Attributes["id"].Value);
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
                        break;

                    case "height":
                        pokemonObj.height = xChild.InnerText;
                        break;

                    case "weight":
                        pokemonObj.weight = xChild.InnerText;
                        break;

                    case "description":
                        pokemonObj.description = xChild.InnerText;
                        break;
                }
            }
        }
        AssetDatabase.Refresh();
        Debug.Log("Pokedex Created");
    }
}
