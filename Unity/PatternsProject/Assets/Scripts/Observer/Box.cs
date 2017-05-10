using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
	class Box : Observer
	{
		GameObject boxObj;

		BoxEvents boxEvent;

		public Box(GameObject boxObj, BoxEvents boxEvent)
		{
			this.boxObj = boxObj;
			this.boxEvent = boxEvent;
		}

		public override void OnNotify()
		{
			Jump (boxEvent.GetJumpForce ());
		}

		void Jump (float jumpForce)
		{
			if(boxObj.transform.position.y < 0.5f)
			{
				boxObj.GetComponent<Rigidbody>().AddForce (Vector3.up * jumpForce);
			}
		}
	}
}
