using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubclassSandbox
{
	public abstract class Superpower
	{
		public abstract void Activate ();

		protected void Move(float speed)
		{
			Debug.Log ("Moving with speed: " + speed);
		}

		protected void PlaySound (string coolSound)
		{
			Debug.Log ("Playing sound: " + coolSound);
		}

		protected void SpamParticles()
		{
		}
	}



}
