using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
	public class JumpHigh : BoxEvents
	{
		public override float GetJumpForce()
		{
			return 90f;
		}
	}

}
