using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FlyweightPattern
{
	public class Flyweight : MonoBehaviour
	{
		List<Alien> allAliens = new List<Alien> ();

		public List<Vector3> eyePositions;
		public List<Vector3> legPositions;
		public List<Vector3> armPositions;

		// Use this for initialization
		void Start ()
		{
			eyePositions = GetBodyPartPositions ();
			legPositions = GetBodyPartPositions ();
			armPositions = GetBodyPartPositions ();

			for(int i =0; i < 10000; i++)
			{
				Alien newAlien = new Alien ();

//				Without Flyweight
//				newAlien.eyePositions = GetBodyPartPositions ();
//				newAlien.legPositions = GetBodyPartPositions ();
//				newAlien.armPositions = GetBodyPartPositions ();

//				With Flyweight
				newAlien.eyePositions = eyePositions;
				newAlien.legPositions = legPositions;
				newAlien.armPositions = armPositions;

				allAliens.Add (newAlien);
			}
		}

		List<Vector3> GetBodyPartPositions()
		{
			List<Vector3> bodyPartPositions = new List<Vector3> ();

			for(int i = 0; i < 1000; i++)
			{
				bodyPartPositions.Add (new Vector3 ());
			}

			return bodyPartPositions;
		}
	}
}

