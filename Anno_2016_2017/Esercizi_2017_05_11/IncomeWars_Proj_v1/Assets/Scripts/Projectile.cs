using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	[HideInInspector] public float speed;
	[HideInInspector] public int damage;
	[HideInInspector] public Player owner;
	[HideInInspector] public Base baseOwner;
	
	void Start()
	{
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
		Destroy (gameObject, 10.0f);
	}
}
