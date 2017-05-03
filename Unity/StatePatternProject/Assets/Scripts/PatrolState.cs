
public class PatrolState : IState 
{
	public void UpdateState(StateController stateController)
	{
		stateController.setTarget (StateController.eTargetType.WAYPOINT);
	}

	public void OnTriggerEnter(StateController stateController)
	{
		stateController.changeState (new ChaseState());
	}

	public void OnTriggerExit(StateController stateController)
	{
		// do nothing
	}
}
