﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject 
{
	public int playerDamage;
	public AudioClip attackSound1;						
	public AudioClip attackSound2;						

	private Animator _animator;
	private Transform _target;
	private bool _skipMove;


	// Use this for initialization
	protected override void Start () 
	{
		GameManager.Instance.AddEnemyToList (this);

		_animator = GetComponent<Animator> ();
		_target = GameObject.FindGameObjectWithTag ("Player").transform;

		base.Start ();
	}

	protected override void AttemptMove<T> (int xDir, int yDir)
	{
		if(_skipMove)
		{
			_skipMove = false;
			return;
		}

		base.AttemptMove<T> (xDir, yDir);
		_skipMove = true;
	}

	public void MoveEnemy ()
	{
		int xDir = 0;
		int yDir = 0;

		if(Mathf.Abs (_target.transform.position.x - transform.position.x ) < float.Epsilon)
		{
			yDir = (_target.transform.position.y < transform.position.y) ? -1 : 1;
		}
		else
		{
			xDir = (_target.transform.position.x < transform.position.x) ? -1 : 1;
		}

		AttemptMove<Player> (xDir, yDir);
	}

	protected override void OnCantMove<T> (T component)
	{
		Player hitPlayer = component as Player;
		if(hitPlayer != null)
		{
			hitPlayer.LooseFood (this.playerDamage);
			_animator.SetTrigger ("EnemyAttack");
			SoundManager.Instance.RandomizeFx (attackSound1, attackSound2);
		}
	}

	protected override void OnMove ()
	{
		// Do Nothing
	}


}
