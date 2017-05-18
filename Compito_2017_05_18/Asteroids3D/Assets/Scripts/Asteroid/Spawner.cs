using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour 
{
	public GameObject Ship;
	public GameObject Asteroid;

	[Header("Spawn")]
	public float spawnTime;
	public float minDistance;
	public float maxDistance;


	// Use this for initialization
	void Start () 
	{
		
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}

	private void Spawn()
	{
		float spawnX = Ship.transform.position.x;
		float spawnY = Ship.transform.position.y;
		float spawnZ = Ship.transform.position.z;

		Vector3 spawn = new Vector3 (Random.Range (spawnX - minDistance, spawnX + maxDistance), Random.Range (spawnY - minDistance, spawnY + maxDistance), Random.Range (spawnZ - minDistance, spawnZ + maxDistance));
		Instantiate (Asteroid, spawn, this.transform.rotation);
	}

}
