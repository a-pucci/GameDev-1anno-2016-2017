using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController: MonoBehaviour 
{
	[Header("Movement")]
	public float minSpeed;
	public float maxSpeed;

	private Rigidbody _asteroidBody;
	private Vector3 _direction;
	private float _moveSpeed;

	[Header("Rotation")]
	public float maxRotationSpeed;

	private float _rotationSpeed;
	private Vector3 _rotation;

	[Header("Death")]
	public int LifeTime;




	// Use this for initialization
	void Start () 
	{
		_asteroidBody = GetComponent<Rigidbody> ();
		_direction = new Vector3 (Random.Range (-Random.value, Random.value), Random.Range (-Random.value, Random.value), Random.Range (-Random.value, Random.value));
		_rotation = new Vector3 (Random.value, Random.value, Random.value);
		_moveSpeed = Random.Range (minSpeed, maxSpeed);

		_rotationSpeed = Random.Range (0, maxRotationSpeed);

		StartCoroutine (Death ());
	}
	
	// Update is called once per frame
	void Update () 
	{
		_asteroidBody.position += _direction * Time.deltaTime * _moveSpeed;
		_asteroidBody.transform.Rotate (_rotation * _rotationSpeed * Time.deltaTime);
		
	}

	private IEnumerator Death()
	{
		yield return new WaitForSeconds (LifeTime);
		GameObject.Destroy (this.gameObject);
	}
}
