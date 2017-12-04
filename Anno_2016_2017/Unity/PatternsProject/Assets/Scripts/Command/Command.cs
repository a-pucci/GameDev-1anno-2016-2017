using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CommandPattern
{
	public abstract class Command
	{
		protected float moveDistance = 1f;

		public abstract void Execute (Transform boxTrans, Command command);
		public virtual void Undo (Transform boxTrans) {}
		public virtual void Move (Transform boxTrans) {}
	}

	public class MoveForward : Command
	{
		public override void Execute(Transform boxTrans, Command command)
		{
			Move (boxTrans);
			InputHandler.oldCommands.Add (command);
		}

		public override void Undo (Transform boxTrans)
		{
			boxTrans.Translate (-boxTrans.forward * moveDistance);
		}

		public override void Move (Transform boxTrans)
		{
			boxTrans.Translate (boxTrans.forward * moveDistance);
		}
	}

	public class MoveReverse : Command
	{
		public override void Execute(Transform boxTrans, Command command)
		{
			Move (boxTrans);
			InputHandler.oldCommands.Add (command);
		}

		public override void Undo (Transform boxTrans)
		{
			boxTrans.Translate (boxTrans.forward * moveDistance);
		}

		public override void Move (Transform boxTrans)
		{
			boxTrans.Translate (-boxTrans.forward * moveDistance);
		}
	}

	public class MoveLeft: Command
	{
		public override void Execute(Transform boxTrans, Command command)
		{
			Move (boxTrans);
			InputHandler.oldCommands.Add (command);
		}

		public override void Undo (Transform boxTrans)
		{
			boxTrans.Translate (boxTrans.right * moveDistance);
		}

		public override void Move (Transform boxTrans)
		{
			boxTrans.Translate (-boxTrans.right * moveDistance);
		}
	}

	public class MoveRight : Command
	{
		public override void Execute(Transform boxTrans, Command command)
		{
			Move (boxTrans);
			InputHandler.oldCommands.Add (command);
		}

		public override void Undo (Transform boxTrans)
		{
			boxTrans.Translate (-boxTrans.right * moveDistance);
		}

		public override void Move (Transform boxTrans)
		{
			boxTrans.Translate (boxTrans.right * moveDistance);
		}
	}

	public class  DoNothing : Command
	{
		public override void Execute(Transform boxTrans, Command command)
		{
			// DO NOTHING
		}
	}

	public class UndoCommand : Command
	{
		public override void Execute (Transform boxTrans, Command command)
		{
			List <Command> oldCommands = InputHandler.oldCommands;
			if(oldCommands.Count > 0)
			{
				Command latestCommand = oldCommands [oldCommands.Count - 1];

				latestCommand.Undo (boxTrans);
				oldCommands.RemoveAt (oldCommands.Count - 1);
			}
		}
	}

	public class ReplayCommand : Command
	{
		public override void Execute (Transform boxTrans, Command command)
		{
			InputHandler.shouldStartReplay = true;
		}
	}
}

