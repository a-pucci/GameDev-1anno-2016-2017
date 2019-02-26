using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using UnityEditor;
using UnityEngine;

public static class Utility {

	private const string DataPath = "Assets/Data/Generator/";
	private const string PersonSpritesPath = "Assets/Sprites/Persons/";
	private const string MaleSpritesFolder = "Males/";
	private const string FemaleSpritesFolder = "Females/";
	
	private const string GeneratorFile = "generator.asset";
	private const string MaleNamesFile = "first_names_m.txt";
	private const string FemaleNamesFile = "first_names_f.txt";
	private const string SurnamesFile = "surnames.txt";
	private const string NationsFile = "nations.xml";
	
	#region Methods

	[MenuItem("Utility/Fill Generator Data")]
	private static void FillGenerator() {
		// generator
		Generator generator;
		if ((generator = AssetDatabase.LoadAssetAtPath<Generator>(DataPath + GeneratorFile)) == null) {
			generator = ScriptableObject.CreateInstance<Generator>();
			Debug.Log("Created " + GeneratorFile);
			AssetDatabase.CreateAsset(generator, DataPath + GeneratorFile);
		}
		// data
		generator.maleData.sprites = GetSprites(MaleSpritesFolder);
		generator.maleData.firstNames = GetNames(MaleNamesFile);
		generator.femaleData.sprites = GetSprites(FemaleSpritesFolder);
		generator.femaleData.firstNames = GetNames(FemaleNamesFile);
		generator.surnames = GetNames(SurnamesFile);
		generator.nations = GetNations(NationsFile);
	}

	private static List<string> GetNames(string fileToRead) {
		var list = new List<string>();
		
		if (!File.Exists(DataPath + fileToRead)) {
			Debug.LogWarning(DataPath + fileToRead + " not found.");
			return list;
		}
		
		using (var streamReader = new StreamReader(DataPath + fileToRead)) {
			while (!streamReader.EndOfStream) {
				string line = streamReader.ReadLine();
				line = (line != null && line.Contains("/")) ? line.Split('/')[0] : line;
				list.Add(line);
			}
		}
		return list;
	}

	private static List<Generator.Nation> GetNations(string fileToRead) {
		var nations = new List<Generator.Nation>();

		if (!File.Exists(DataPath + fileToRead)) {
			Debug.LogWarning(DataPath + fileToRead + " not found.");
			return nations;
		}
		
		var xml = new XmlDocument();
		xml.Load(DataPath + fileToRead);
		XmlNode nationsNode = xml.SelectSingleNode(".//nations");
		foreach (XmlElement currentNationNode in nationsNode.ChildNodes) {
			var nation = new Generator.Nation
			{
				name = currentNationNode.GetAttribute("name"), 
				cities = currentNationNode.GetAttribute("cities").Split(';')
			};
			nations.Add(nation);
		}
		return nations;
	}

	private static List<Sprite> GetSprites(string spritesFolder) {
		var sprites = new List<Sprite>(); 
		string[] files = Directory.GetFiles(PersonSpritesPath + spritesFolder);
		foreach (string currentFile in files) {
			sprites.AddRange(AssetDatabase.LoadAllAssetsAtPath(currentFile).OfType<Sprite>().ToArray());
		}
		return sprites;
	}

	#endregion
 
}