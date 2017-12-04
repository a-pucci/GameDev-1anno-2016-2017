using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassedWaypoint : MonoBehaviour 
{
	public Transform youTrans;
	public Transform waypointFromTrans;
	public Transform waypointToTrans;
	public Transform debugTrans;
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 a = youTrans.position - waypointFromTrans.position;
		Vector3 b = waypointToTrans.position - waypointFromTrans.position;

		float progress = (a.x * b.x + a.y * b.y + a.z * b.z) / (b.x * b.x + b.y * b.y + b.z * b.z);

		if(progress > 1f)
		{
			Debug.Log ("Change Waypoint");
		}
		else
		{
			Debug.Log ("Move On");
		}
		debugTrans.position = progress * b + waypointFromTrans.position;
	}
}
