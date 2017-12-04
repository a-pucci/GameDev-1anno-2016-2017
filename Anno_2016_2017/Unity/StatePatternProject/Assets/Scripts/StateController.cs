using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class StateController : MonoBehaviour 
{
	public enum eTargetType
	{
		WAYPOINT,
		PLAYER
	}

	IState currentState;

	public GameObject player;
	public GameObject[] waypoint;

	private AICharacterControl characterController;
	private int waypointCounter = 0;

	private void Awake()
	{
		currentState = new PatrolState ();
		characterController = gameObject.GetComponent <AICharacterControl> ();
	}

	void Update()
	{
		currentState.UpdateState (this);
		if(Vector3.Distance(this.transform.position, waypoint[waypointCounter].transform.position) < 5f)
		{
			waypointCounter = (waypointCounter + 1) % 4;
		}

	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag ("Player"))
		{
			currentState.OnTriggerEnter (this);
		}			
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.CompareTag ("Player"))
		{
			currentState.OnTriggerExit (this);
		}
	}

	public void changeState(IState newState)
	{
		currentState = newState;
	}

	public void setTarget(eTargetType target)
	{
		if(characterController != null)
		{
			switch(target)
			{

			case eTargetType.WAYPOINT:
				characterController.SetTarget (waypoint[waypointCounter].transform);
				break;

			case eTargetType.PLAYER:
				characterController.SetTarget (player.transform);
				break;				
			}
		}
	}

}
