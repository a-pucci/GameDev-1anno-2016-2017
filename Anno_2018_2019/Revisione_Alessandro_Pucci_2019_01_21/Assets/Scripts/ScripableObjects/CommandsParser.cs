using System;
using UnityEngine;
using Sirenix.OdinInspector;


[CreateAssetMenu(fileName = "CommandsParser", menuName = "ScriptableObjects/CommandsParser")]
public class CommandsParser : ScriptableObject {
	[ShowInInspector] public System.Collections.Generic.List<Commands> commandsToParse;

	[Button(ButtonSizes.Medium)]
	public void ParseCommands() {
		foreach (Commands command in commandsToParse) {
			string newCommand = command.commandRegex;
			newCommand = newCommand.Replace("\\w+", "string");
			newCommand = newCommand.Replace("\\d+(,\\d+)?", "n,");
			newCommand = newCommand.Replace("\\d+", "n");
			newCommand = newCommand.Replace('$', ' ');
			RemoveParenthesis(ref newCommand);
			command.commandSyntax = newCommand;
		}
	}

	private void RemoveParenthesis(ref string stringToParse) {
		int closedParenthesis = 0;
		int openedParenthesis = 0;

		int indexToRemove = 0;
		bool removed = false;
		bool found = false;
		
		for (int i = stringToParse.Length-1; i >= 0; i--) {
			if (stringToParse[i] == ')') {
				if( stringToParse[i+1] != '+' && stringToParse[i+1] != '?' && stringToParse[i+1] != '*') {
					indexToRemove = i;
					found = true;
				}
				if(found) closedParenthesis++;
			}

			if (closedParenthesis == 1 && stringToParse[i] == '|') {
				found = false;
			}
				
			if (stringToParse[i] == '(') {
				if(found) openedParenthesis++;
				if (openedParenthesis == closedParenthesis && (closedParenthesis > 0 && closedParenthesis > 0)) {
					stringToParse = stringToParse.Remove(indexToRemove, 1);
					stringToParse = stringToParse.Remove(i, 1);
					removed = true;
					break;
				}
			}
		}
		if (removed) {
			RemoveParenthesis(ref stringToParse);
		}
	}
}

[Serializable]
public class Commands {
	public string commandName;
	public string commandRegex;
	[ReadOnly]public string commandSyntax;
}
