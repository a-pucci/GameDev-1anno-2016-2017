using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour 
{
	public Transform _patrolTransform;
	public GameObject _target;

	public float _speed = 10.0f;

	public bool _smoothMovement;

	private Vector3 _initialPosition;
	private Vector3 _destination;

	private float _startTime;
	private float _journeyLenght;

	// Use this for initialization
	void Start () 
	{
		_initialPosition = _patrolTransform.position;
		_destination = _target.transform.position;
		_startTime = Time.time;
		_journeyLenght = Vector3.Distance (_initialPosition, _destination);

		_target.SetActive (false);
				
	}
	
	// Update is called once per frame
	void Update () 
	{
		float distanceCovered = (Time.time - _startTime) * _speed;
		float fractionJourney = distanceCovered / _journeyLenght;

		if (_smoothMovement)
		{
			fractionJourney = Mathf.SmoothStep (0f, 1f, fractionJourney);
		}

		_patrolTransform.position = Vector3.Lerp (_initialPosition, _destination, fractionJourney);

		if (_patrolTransform.position == _destination)
		{
			Vector3 tempPosition = _initialPosition;
			_initialPosition = _destination;
			_destination = tempPosition;

			_startTime = Time.time;
		}
	}
}
