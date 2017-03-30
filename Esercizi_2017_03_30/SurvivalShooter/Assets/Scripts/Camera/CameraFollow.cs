using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{

	public Transform playerTransform;
	public float smoothing = 5f;
	public float yRotation = 5f;
	public float xRotation = 5f;

	private Vector3 _positionOffset;

	// Use this for initialization
	void Start () 
	{
		yRotation += Input.GetAxis ("Mouse Y");
		xRotation += Input.GetAxis ("Mouse X");

		
		if(playerTransform != null)
		{ 
			_positionOffset = transform.position - playerTransform.position;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void FixedUpdate()
	{
		if(playerTransform != null)
		{
			Vector3 newCameraPosition = playerTransform.position + _positionOffset;
			transform.position = Vector3.Lerp (transform.position, newCameraPosition, smoothing * Time.deltaTime);

			transform.eulerAngles = new Vector3 (xRotation, yRotation, 0);
		}
	}
}
