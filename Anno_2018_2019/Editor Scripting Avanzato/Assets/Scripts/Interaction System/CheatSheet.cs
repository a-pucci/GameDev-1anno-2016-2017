using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "cheat_sheet", menuName = "Data/Scripting/Cheat Sheet")]
public class CheatSheet : ScriptableObject {
	
	[System.Serializable]
	public class Rule {
		public string commandName;
		public string regexPattern;
	}

	public List<Rule> rules;
}