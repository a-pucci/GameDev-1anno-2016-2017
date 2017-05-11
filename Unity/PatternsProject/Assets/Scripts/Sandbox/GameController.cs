using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubclassSandbox
{
	public class GameController : MonoBehaviour
	{
		List<Superpower> superPowers = new List<Superpower>();

		// Use this for initialization
		void Start ()
		{
			superPowers.Add (new SkyLaunch ());
			superPowers.Add (new GroundDive ());
		}
		
		// Update is called once per frame
		void Update ()
		{
			for(int i = 0; i < superPowers.Count; i++)
			{
				superPowers [i].Activate ();
			}
		}
	}
}
