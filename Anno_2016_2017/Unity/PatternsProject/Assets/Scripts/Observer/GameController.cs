using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
	public class GameController : MonoBehaviour
	{
	
		public GameObject SphereObj;

		public GameObject Box1Obj;
		public GameObject Box2Obj;
		public GameObject Box3Obj;

		Subject subject = new Subject();

		// Use this for initialization
		void Start ()
		{
			Box box1 = new Box (Box1Obj, new JumpLittle ());
			Box box2 = new Box (Box2Obj, new JumpMedium ());
			Box box3 = new Box (Box3Obj, new JumpHigh ());

			subject.AddObserver(box1);
			subject.AddObserver(box2);
			subject.AddObserver(box3);
		}
		
		// Update is called once per frame
		void Update ()
		{
			if((SphereObj.transform.position).magnitude < 0.5f)
			{
				subject.Notify ();
			}
		}
	}
}
