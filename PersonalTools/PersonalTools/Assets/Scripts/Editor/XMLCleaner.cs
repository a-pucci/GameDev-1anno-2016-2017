using UnityEditor;
using UnityEngine;
using System.Xml;

public static class XmlCleaner
{
	[MenuItem("Pokemon Tools/Clear XML")]
	public static void ClearXml()
	{
		string[] nodesToRemove = { "ability", "exp", "ratio", "egg-group", "species", "height", "weight", "moves" };

		var xData = new XmlDocument();
		xData.Load("Assets/pokedata.xml");

		XmlNodeList xPokemons = xData.SelectNodes("pokedex/pokemon");

		if (xPokemons != null)
		{
			foreach(XmlNode xPokemon in xPokemons)
			{

				foreach (string node in nodesToRemove)
				{
					XmlNodeList xAttributes = xPokemon.SelectNodes(node);
	
					if (xAttributes != null)
					{
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
			}
		}

		xData.Save("Assets/pokedata_clear.xml");
		AssetDatabase.Refresh();
		Debug.Log("XML Clear");
	}
}
