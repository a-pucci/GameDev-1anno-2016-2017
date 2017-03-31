using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGranade : MonoBehaviour {

	public float timeBetweenGranades = 1f;
	public float throwForce = 6f;
	public float explosionRadius = 10f;
	public GameObject granade;
	public Rigidbody playerRB;

	float timer;
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;

		if(Input.GetButton ("Fire2") && timer >= timeBetweenGranades && Time.timeScale != 0)
		{
			Throw ();
		}
	}		

	void Throw()
	{
		timer = 0f;
		GameObject granadeInstance = Instantiate (granade, this.transform.position, this.transform.rotation);
		granadeInstance.AddComponent<Rigidbody> ();
		Rigidbody granadeRB = granadeInstance.GetComponent <Rigidbody> ();

		granadeRB.AddForce (playerRB.transform.forward * throwForce, ForceMode.Impulse);
		granadeRB.AddForce (playerRB.transform.up * throwForce, ForceMode.Impulse);

	}
		
}
