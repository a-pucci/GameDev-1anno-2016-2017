using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftOrRight : MonoBehaviour 
{
	public Transform youTrans;
	public Transform waypointTrans;

	// Update is called once per frame
	void Update () 
	{
		Vector3 youDir = youTrans.forward;
		Vector3 waypointDir = waypointTrans.position - youTrans.position;
		Vector3 crossProduct = Vector3.Cross (youDir, waypointDir);
		float dotProduct = Vector3.Dot (crossProduct, youTrans.up);

		if(dotProduct > 0)
		{
			Debug.Log ("Turn Right");
		}
		else
		{
			Debug.Log ("Turn Left");
		}
	}
}
