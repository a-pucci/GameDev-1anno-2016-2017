using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	public float speed;

	// Use this for initialization
	void Start () 
	{
		GetComponent <Rigidbody> ().velocity = transform.forward * speed;
		Destroy (this.gameObject, 10f);
	}

	void Update()
	{
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Asteroid")
		{
			GameObject.Destroy (other.gameObject);
		}
		
	}
}
