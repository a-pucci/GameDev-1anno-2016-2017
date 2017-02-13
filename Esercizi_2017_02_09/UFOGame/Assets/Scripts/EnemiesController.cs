using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour 
{
	
	public float accelleration = 10.0f;
	public Vector2 startPoint = new Vector2 (0, 0);
	public Vector2 endPoint = new Vector2 (0, 0);
	private Vector2 support = new Vector2 (0, 0); 

	// Use this for initialization
	void Start () 
	{
		
	}

	// Update is called once per frame
	void Update () 
	{		
		if (this.transform.position.x == endPoint.x && this.transform.position.y == endPoint.y) 
		{
			support = endPoint;
			endPoint = startPoint;
			startPoint = support;
		}

		transform.position = Vector2.MoveTowards (transform.position, endPoint, accelleration * Time.deltaTime);
	}
}
