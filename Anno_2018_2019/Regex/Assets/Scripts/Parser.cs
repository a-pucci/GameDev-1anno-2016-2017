using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class Parser : MonoBehaviour {

	#region Fields
	
	// Static

	// Public
	public string[] script;

	// Hidden Public

	// Private
	private string instruction = "\\w";
	private string wait = "wait (\\d+(,\\d+)?)";
	private string pick = "pick( (\\w+)-(\\d+))+";
	private string survive = "survive (hunger|thirst|temperature|oxygen|rest) (\\+|-)?(\\d+)";
	private string remove = "remove( (\\w+)-(\\d+))*";

	private Character character;
	private List<Item> items;

	// Properties

	// Components

	// Events

	#endregion

	#region Methods

	private void Awake() {
		GetAllItems();
	}

	private void Start() {
		character = FindObjectOfType<Character>();
		StartCoroutine(ParseCommands());
	}

	private IEnumerator ParseCommands() {
		foreach (string command in script) {
			Match match = Regex.Match(command, instruction);
			
			if (match.Success) {
				switch (match.Groups[0].Value) {
					case "wait":
						float time = float.Parse(match.Groups[1].Value);
						Debug.Log("Wait start");
						yield return new WaitForSeconds(time);
						Debug.Log("Wait end");
						break;
					
					case "pick":
						Pick(command);
						break;
					
					case "survive":
						Survive(command);
						break;
					
					case "remove":
						Remove(command);
						break;
				}
			}
		}
	}

	private void Pick(string command) {
		Match pickMatch = Regex.Match(command, pick);
		
	}

	private void Survive(string command) {
		
		Match surviveMatch = Regex.Match(command, survive);
		string stat = surviveMatch.Groups[1].Value;
		string sign = surviveMatch.Groups[2].Value;
		string statValue = surviveMatch.Groups[3].Value;
		
		Debug.Log("Survive " + stat + " " + sign + " " + statValue);
		
		if (surviveMatch.Success) {
			switch (stat) {
				case "hunger":
					if (sign == "") {
						character.hunger.value = Int32.Parse(statValue);
					}
					else if (sign == "-") {
						character.hunger.value -= Int32.Parse(statValue);
					}
					else if (sign == "+") {
						character.hunger.value += Int32.Parse(statValue);
					}
					break;
				
				case "thirst":
					if (sign == "") {
						character.thirst.value = Int32.Parse(statValue);
					}
					else if (sign == "-") {
						character.thirst.value -= Int32.Parse(statValue);
					}
					else if (sign == "+") {
						character.thirst.value += Int32.Parse(statValue);
					}
					break;
				
				case "temperature":
					if (sign == "") {
						character.temperature.value = Int32.Parse(statValue);
					}
					else if (sign == "-") {
						character.temperature.value -= Int32.Parse(statValue);
					}
					else if (sign == "+") {
						character.temperature.value += Int32.Parse(statValue);
					}
					break;
				
				case "oxygen":
					if (sign == "") {
						character.oxygen.value = Int32.Parse(statValue);
					}
					else if (sign == "-") {
						character.oxygen.value -= Int32.Parse(statValue);
					}
					else if (sign == "+") {
						character.oxygen.value += Int32.Parse(statValue);
					}
					break;
				
				case "rest":
					if (sign == "") {
						character.rest.value = Int32.Parse(statValue);
					}
					else if (sign == "-") {
						character.rest.value -= Int32.Parse(statValue);
					}
					else if (sign == "+") {
						character.rest.value += Int32.Parse(statValue);
					}
					break;
			}
		}
		
	}

	private void Remove(string command) {
		Match removeMatch = Regex.Match(command, remove);
	}

	private void GetAllItems() {
		items = GetAllInstances<Item>().ToList();
	}

	private static T[] GetAllInstances<T>() where T : ScriptableObject {
		string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
		var a = new T[guids.Length];
		for (int i = 0; i < guids.Length; i++) {
			string path = AssetDatabase.GUIDToAssetPath(guids[i]);
			a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
		}
		return a;
	}
	
	#endregion

}