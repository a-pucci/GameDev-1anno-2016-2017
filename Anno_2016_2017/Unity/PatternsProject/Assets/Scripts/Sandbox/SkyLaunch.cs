using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubclassSandbox
{
	class SkyLaunch : Superpower
	{
		public override void Activate()
		{
			Move (10f);
			PlaySound ("SkyLaunch");
			SpamParticles ();
		}
	}
}
