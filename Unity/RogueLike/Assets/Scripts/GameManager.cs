using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	public static GameManager Instance = null;

	public int playerFoodPoints = 100;
	[HideInInspector] public bool playerTurn = true;


	private BoardManager _boardManager;
	private int _level = 3;

	void Awake()
	{
		// Singleton Pattern - Start
		if(Instance == null)
		{
			Instance = this;
		}
		else if(Instance != this)
		{
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
		// Singleton Pattern - End

		_boardManager = GetComponent <BoardManager> ();
	}

	// Use this for initialization
	void Start () 
	{
		this.InitGame ();
	}
	
	// Update is called once per frame
	void InitGame () 
	{
		_boardManager.SetupScene (_level);
	}

	public void GameOver()
	{
		this.enabled = true;
	}
}
