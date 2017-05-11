using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubclassSandbox
{
	class GroundDive : Superpower
	{
		public override void Activate()
		{
			Move (15f);
			PlaySound ("GroundDive");
			SpamParticles ();
		}
	}
}
