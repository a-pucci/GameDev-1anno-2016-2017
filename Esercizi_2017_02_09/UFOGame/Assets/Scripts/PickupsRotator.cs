using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupsRotator : MonoBehaviour 
{

	public float rotationSpeed = 20.0f;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate (Vector3.forward, rotationSpeed * Time.deltaTime);
	}
}
