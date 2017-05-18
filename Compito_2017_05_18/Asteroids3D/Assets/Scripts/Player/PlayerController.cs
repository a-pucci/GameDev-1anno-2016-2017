using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	[Header("Speeds")]
	public float RotationSpeed;
	public float MovementSpeed;
	public float MovementIncrement;
	public float MovementDecrement;

	[Header("Bullet")]
	public Bullet bullet;

	private Rigidbody _shipBody;
	private Vector3 _rotation;

	// Use this for initialization
	void Start () 
	{
		_shipBody = GetComponent<Rigidbody>();

	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.W))
		{
			MovementSpeed += MovementIncrement;
		}
		else if(Input.GetKeyDown(KeyCode.S))
		{
			MovementSpeed -= MovementIncrement;
		}
		else if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			_rotation = -Vector3.right;
		}
		else if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			_rotation = Vector3.right;
		}
		else if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			_rotation = -Vector3.up;
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			_rotation = Vector3.up;
		}
		else if(Input.GetKeyDown(KeyCode.Space))
		{
			GameObject.Instantiate (bullet);
		}

		_shipBody.position += Vector3.forward * Time.deltaTime * MovementSpeed;
		transform.Rotate (_rotation * RotationSpeed * Time.deltaTime);

	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Asteroid")
		{
			Destroy (this.gameObject);
		}
	}
}
