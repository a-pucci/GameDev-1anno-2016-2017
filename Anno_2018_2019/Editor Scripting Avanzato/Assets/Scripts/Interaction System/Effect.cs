using System;
using System.Linq;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
using Sirenix.Utilities.Editor;
#endif

using UnityEngine;
using System.Text.RegularExpressions;
using UnityEditor;

[Serializable]
public class Effect {

	private CheatSheet cheatSheet;
	
 	[ListDrawerSettings(OnEndListElementGUI = nameof(EndDrawListElement))]
	public string[] script;

#if UNITY_EDITOR

	private void EndDrawListElement(int index) {
		if (cheatSheet == null) {
			cheatSheet = Resources.FindObjectsOfTypeAll<CheatSheet>().FirstOrDefault();
		}
		if (cheatSheet == null) {
			SirenixEditorGUI.MessageBox("cheat sheet not found", MessageType.Error);
		}
		else {
			if (script[index] != "") {
				string commandName = script[index].Split(' ')[0];
				if (commandName != "") {
					CheatSheet.Rule rule = cheatSheet.rules.Find(c => c.commandName == commandName);
					if (rule == null) {
						SirenixEditorGUI.MessageBox($"command name '{commandName}' not found", MessageType.Error);
					}
					else {
						if (!Regex.IsMatch(script[index], rule.regexPattern)) {
							string syntax = rule.regexPattern.Replace(@"\d+(,\d+)?", "n,");
							syntax = syntax.Replace(@"\d+", "n");
							syntax = syntax.Replace(@"\w+", "string");
							syntax = syntax.Replace("$", " ");
							syntax = syntax.Replace(@"\+", "+");
							syntax = RemoveParentheses(syntax);
							SirenixEditorGUI.MessageBox($"{syntax}", MessageType.Error);
						}
					}
				}
			}
		}
	}

	private string RemoveParentheses(string syntax) {
		string result = syntax;
		int closingParenthesesCount = 0;
		int closingParenthesesIndex = 0;
		bool foundPipe = false;
		for (int i = syntax.Length - 2; i >= 0; i--) {
			if (syntax[i] == ')') {
				if (closingParenthesesIndex == 0 && (
					    syntax[i + 1] != '+' &&
					    syntax[i + 1] != '?' &&
					    syntax[i + 1] != '*' &&
					    syntax[i + 1] != '{')) {
					closingParenthesesIndex = i;
					closingParenthesesCount = 1;
				}
				else {
					closingParenthesesCount++;
				}
			}
			if (syntax[i] == '|') {
				foundPipe = true;
			}
			if (syntax[i] == '(') {
				if (closingParenthesesCount == 1 && closingParenthesesIndex > 0 && !foundPipe) {
					result = result.Remove(closingParenthesesIndex, 1);
					result = result.Remove(i, 1);
					result = RemoveParentheses(result);
					break;
				}
				closingParenthesesCount--;
				foundPipe = false;
			}
		}
		return result;
	}

#endif
	
}



