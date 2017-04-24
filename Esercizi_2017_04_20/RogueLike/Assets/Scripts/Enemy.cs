using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject 
{
	public int playerDamage;
	public int health;
	public int wallDamage;

	public AudioClip enemyAttack1;
	public AudioClip enemyAttack2;

	private Animator _animator;
	private Transform _target;
	private bool _skipMove;

	// Use this for initialization
	protected override void Start () 
	{
		GameManager.instance.AddEnemyToList (this);

		_animator = GetComponent<Animator> ();
		_target = GameObject.FindGameObjectWithTag ("Player").transform;

		base.Start ();
	}

	protected override void AttemptMove <T1, T2, T3> (int xDir, int yDir)
	{
		if (_skipMove) 
		{
			_skipMove = false;
			return;
		}

		base.AttemptMove <T1, T2, T3> (xDir, yDir);

		_skipMove = true;
	}

	public void MoveEnemy ()
	{
		int xDir = 0;
		int yDir = 0;

		if (Mathf.Abs (_target.position.x - transform.position.x) < float.Epsilon) 
		{
			yDir = (_target.position.y < transform.position.y) ? -1 : 1;
		}
		else
		{
			xDir = (_target.position.x < transform.position.x) ? -1 : 1;
		}

		AttemptMove<Player, Wall, Enemy> (xDir, yDir);
	}

	protected override void OnCantMove <T> (T component)
	{
		if(component.tag == "Player")
		{
			Player hitPlayer = component as Player;
			if (hitPlayer != null)
			{
				hitPlayer.LoseFood (this.playerDamage);
				_animator.SetTrigger ("EnemyAttack");

				SoundManager.instance.RandomizeSfx (this.enemyAttack1, this.enemyAttack2);
			}
		}
		else if(component.tag == "Wall")
		{
			Wall hitWall = component as Wall;
			if (hitWall != null)
			{
				hitWall.DamageWall (wallDamage);
				_animator.SetTrigger ("EnemyAttack");

				SoundManager.instance.RandomizeSfx (this.enemyAttack1, this.enemyAttack2);
			}
		}
	}

	public void TakeDamage(int damage)
	{
		this.health -= damage;
		if(health <= 0)
		{
			if(GameManager.instance.GetLevel() % 5 == 0 && GameManager.instance.playerFoodPoints > 0)
			{
				GameManager.instance.Win ();
			}

			this.gameObject.SetActive (false);
		}
			
	}

	protected override void OnMove ()
	{
		// Do nothing
	}
}
