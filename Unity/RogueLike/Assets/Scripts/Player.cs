using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MovingObject 
{
	public int wallDamage = 1;
	public int pointsPerFood = 10;
	public int pointsPerSoda = 20;
	public float restartLevelDelay = 1f;
	public Text foodText;
	public AudioClip moveSound1;				
	public AudioClip moveSound2;				
	public AudioClip eatSound1;					
	public AudioClip eatSound2;					
	public AudioClip drinkSound1;				
	public AudioClip drinkSound2;				
	public AudioClip gameOverSound;				

	private Animator _animator;
	private int _food;

	// Use this for initialization
	protected override void Start () 
	{
		_animator = GetComponent<Animator> ();
		_food = GameManager.Instance.playerFoodPoints;

		foodText.text = "Food: " + _food;

		base.Start ();
	}

	private void OnDisable ()
	{
		GameManager.Instance.playerFoodPoints = _food;
	}

	private void CheckIfGameOver ()
	{
		if (_food <= 0)
		{
			SoundManager.Instance.PlaySingle (gameOverSound);
			SoundManager.Instance.musicSource.Stop();
			GameManager.Instance.GameOver ();
		}
	}

	protected override void AttemptMove<T> (int xDir, int yDir)
	{
		_food--;
		foodText.text = "Food: " + _food;
		base.AttemptMove<T> (xDir, yDir);

		RaycastHit2D hit;

		//If Move returns true, meaning Player was able to move into an empty space.
		if (Move (xDir, yDir, out hit)) 
		{
			//Call RandomizeSfx of SoundManager to play the move sound, passing in two audio clips to choose from.
			SoundManager.Instance.RandomizeFx (moveSound1, moveSound2);
		}

		CheckIfGameOver ();
		GameManager.Instance.playerTurn = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!GameManager.Instance.playerTurn)
		{
			return;
		}

		int horizontal = 0;
		int vertical = 0;

		horizontal = (int)Input.GetAxisRaw ("Horizontal");
		vertical = (int)Input.GetAxisRaw ("Vertical");

		if(horizontal != 0)
		{
			vertical = 0;
		}

		if(horizontal != 0 || vertical != 0)
		{
			AttemptMove<Wall> (horizontal, vertical);

		}
	}

	protected override void OnCantMove<T> (T component)
	{
		Wall hitwall = component as Wall;
		if(hitwall != null)
		{
			hitwall.DamageWall (this.wallDamage);
			_animator.SetTrigger ("PlayerChop");
		}
	}

	protected override void OnMove ()
	{
		// TODO: Add audio effect
	}

	private void Restart()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Log ("Collide");
		if(other.CompareTag ("Exit"))
		{
			Debug.Log ("Exit");
			Invoke ("Restart", this.restartLevelDelay);
			this.enabled = false;
		}
		else if(other.CompareTag ("Food"))
		{
			Debug.Log ("Food");
			_food += this.pointsPerFood;
			this.foodText.text = "+" + pointsPerFood + " Food: " + _food;
			other.gameObject.SetActive (false);
			SoundManager.Instance.RandomizeFx (eatSound1, eatSound2);
		}
		else if(other.CompareTag ("Soda"))
		{
			Debug.Log ("Soda");
			_food += this.pointsPerSoda;
			this.foodText.text = "+" + pointsPerSoda + " Food: " + _food;
			other.gameObject.SetActive (false);
			SoundManager.Instance.RandomizeFx (drinkSound1, drinkSound2);
		}
	}

	public void LooseFood (int loss)
	{
		_animator.SetTrigger ("PlayerHit");
		_food -= loss;
		this.foodText.text = "-" + loss + " Food: " + _food;
		CheckIfGameOver ();
	}
}
