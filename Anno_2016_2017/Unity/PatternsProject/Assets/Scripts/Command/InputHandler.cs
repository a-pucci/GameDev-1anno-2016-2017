using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CommandPattern
{
	public class InputHandler : MonoBehaviour
	{
		public Transform boxTrans;

		private Command buttonW, buttonA, buttonS, buttonD, buttonB, buttonZ, buttonR;

		public static List<Command> oldCommands = new List<Command>();
		public Vector3 boxStartPos;

		private Coroutine replayCoroutine;
		public static bool shouldStartReplay;
		private bool isReplaying;

		// Use this for initialization
		void Start () 
		{
			buttonW = new MoveForward ();
			buttonA = new MoveLeft ();
			buttonS = new MoveReverse ();
			buttonD = new MoveRight ();
			buttonB = new DoNothing ();
			buttonZ = new UndoCommand ();
			buttonR = new ReplayCommand ();

			boxStartPos = boxTrans.position;
		}

		// Update is called once per frame
		void Update () 
		{
			if(!isReplaying)
			{
				HandleInput ();
			}
			StartReplay ();
		}

		public void HandleInput()
		{
			if(Input.GetKeyDown(KeyCode.A))
			{
				buttonA.Execute (boxTrans, buttonA);
			}
			else if(Input.GetKeyDown(KeyCode.B))
			{
				buttonB.Execute (boxTrans, buttonB);
			}
			else if(Input.GetKeyDown(KeyCode.D))
			{
				buttonD.Execute (boxTrans, buttonD);
			}
			else if(Input.GetKeyDown(KeyCode.R))
			{
				buttonR.Execute (boxTrans, buttonR);
			}
			else if(Input.GetKeyDown(KeyCode.S))
			{
				buttonS.Execute (boxTrans, buttonS);
			}
			else if(Input.GetKeyDown(KeyCode.W))
			{
				buttonW.Execute (boxTrans, buttonW);
			}
			else if(Input.GetKeyDown(KeyCode.Z))
			{
				buttonZ.Execute (boxTrans, buttonZ);
			}
		}

		void StartReplay()
		{
			if(shouldStartReplay && oldCommands.Count > 0)
			{
				shouldStartReplay = false;
				if(replayCoroutine != null)
				{
					StopCoroutine (replayCoroutine);
				}

				replayCoroutine = StartCoroutine (ReplayCommands (boxTrans));
			}
		}

		IEnumerator ReplayCommands(Transform boxTrans)
		{
			isReplaying = true;
			boxTrans.position = boxStartPos;
			for(int i = 0; i < oldCommands.Count; i++)
			{
				oldCommands [i].Move (boxTrans);
				yield return new WaitForSeconds (0.3f);
			}
			isReplaying = false;
		}
	}
}

