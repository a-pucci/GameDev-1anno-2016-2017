
public class ChaseState : IState 
{
	public void UpdateState(StateController stateController)
	{
		stateController.setTarget (StateController.eTargetType.PLAYER);
	}

	public void OnTriggerEnter(StateController stateController)
	{
		// do nothing
	}

	public void OnTriggerExit(StateController stateController)
	{
		stateController.changeState (new PatrolState ());
	}
}
