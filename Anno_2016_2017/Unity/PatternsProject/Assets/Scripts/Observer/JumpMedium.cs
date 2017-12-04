using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
	public class JumpMedium : BoxEvents
	{
		public override float GetJumpForce()
		{
			return 60f;
		}
	}

}
