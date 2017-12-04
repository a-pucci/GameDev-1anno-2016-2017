using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObserverPattern
{
	public class JumpLittle : BoxEvents
	{
		public override float GetJumpForce()
		{
			return 30f;
		}
	}
}
