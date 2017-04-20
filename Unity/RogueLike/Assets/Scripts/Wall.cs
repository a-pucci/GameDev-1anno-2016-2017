using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour 
{
	public AudioClip chopSound1;				
	public AudioClip chopSound2;				
	public Sprite dmgSprite;
	public int hp = 4;

	private SpriteRenderer _spriteRenderer;

	void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	public void DamageWall(int loss)
	{
		SoundManager.Instance.RandomizeFx (chopSound1, chopSound2);
		_spriteRenderer.sprite = this.dmgSprite;
		this.hp -= loss;

		if(this.hp <= 0)
		{
			gameObject.SetActive (false);
		}
	}
}
