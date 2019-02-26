using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;
using Object = UnityEngine.Object;

public class Parser : MonoBehaviour {
	
	private struct Command {
		public readonly Object owner;
		public readonly Match match;

		public Command(Object o, Match m) {
			owner = o;
			match = m;
		}
	}
	
	#region Fields
	
	// Static

	// Public
	public CheatSheet cheatSheet;
	public Effect effect;

	// Hidden Public

	// Private
	private Type type;
	private Character character;
	private Crafting crafting;
	private Inventory inventory;

	// Properties

	// Components

	// Events

	#endregion

	#region Unity Messages

	private void Awake() {
		type = GetType();
		character = FindObjectOfType<Character>();
		crafting = FindObjectOfType<Crafting>();
		inventory = character.GetComponent<Inventory>();
	}

	private void Start() {
		StartCoroutine(Execute(effect.script, this));
	}

	#endregion

	#region Methods

	public void Execute(Interaction interaction) {
		StartCoroutine(Execute(interaction.effect.script, interaction.owner));
	}

	private IEnumerator Execute(IEnumerable<string> commands, Object owner) {

		foreach (string command in commands) {
			if (command == "") {
				Debug.LogWarning("empty command");
			}
			else {
				string commandName = command.Split(' ')[0];
				string methodName = commandName.UppercaseFirst();
				MethodInfo methodToExecute = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(Command) }, null);
				CheatSheet.Rule rule = cheatSheet.rules.Find(r => r.commandName == commandName);
				
				// command not found
				if (rule == null) {
					Debug.LogWarning($"command with name '{commandName}' not found: {command}");
					continue;
				}
				// command not implemented
				if (methodToExecute == null) {
					Debug.LogWarning($"command with name '{commandName}' not implemented: {command}");
					continue;
				}

				Match match = Regex.Match(command, rule.regexPattern);
				if (match.Success) {
					yield return StartCoroutine(methodToExecute.Name, new Command(owner, match));
				}
				else {
					Debug.LogWarning($"command '{command}' doesn't match the syntax rules");
				}
			}
		}
		yield return null;
	}

	private IEnumerator Wait(Command command) {
		float time = float.Parse(command.match.Groups[1].Value);
		yield return new WaitForSeconds(time);
	}

	private IEnumerator Pick(Command command) {
		for (int i = 0; i < command.match.Groups[1].Captures.Count; i++) {
			string itemName = command.match.Groups[2].Captures[i].Value;
			
			Item itemToAdd = crafting.items.Find(x => x.name == itemName);
			int quantityToAdd = int.Parse(command.match.Groups[3].Captures[i].Value);
			
			if (itemToAdd == null) {
				Debug.LogWarning($"item {itemName} not found");
				continue;
			}
			if (quantityToAdd > 0) {
				var setToAdd = new Set
				{
					item = itemToAdd,
					quantity = quantityToAdd
				};
				inventory.Add(setToAdd);
			}
		}
		yield return null;
	}

	private IEnumerator Remove(Command command) {
		if (command.match.Groups[1].Value == "") {
			var setToRemove = new Set
			{
				item = (Item)command.owner,
				quantity = 1
			};
			inventory.Remove(setToRemove);
		}
		else {
			for (int i = 0; i < command.match.Groups[1].Captures.Count; i++) {
				string itemName = command.match.Groups[2].Captures[i].Value;

				Item itemToRemove = crafting.items.Find(x => x.name == itemName);
				int quantityToRemove = int.Parse(command.match.Groups[3].Captures[i].Value);

				if (itemToRemove == null) {
					Debug.LogWarning($"item {itemName} not found");
					continue;
				}
				if (quantityToRemove > 0) {
					var setToRemove = new Set
					{
						item = itemToRemove,
						quantity = quantityToRemove
					};
					inventory.Remove(setToRemove);
				}
			}
		}
		yield return null;
	}

	private IEnumerator Drop(Command command) {
		yield return null;
	}

	private IEnumerator Inventory(Command command) {
		yield return null;
	}

	private IEnumerator Survive(Command command) {
		string vitalParameter = command.match.Groups[1].Value;		
		int value = int.Parse(command.match.Groups[2].Value);
		bool hasSign = command.match.Groups[3].Value != "";
		character.SetVitalSign(vitalParameter, value, hasSign);
		yield return null;
	}

	#endregion
}