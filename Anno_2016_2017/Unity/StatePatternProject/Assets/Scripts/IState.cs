
public interface IState 
{
	void UpdateState (StateController stateController);
	void OnTriggerEnter(StateController stateController);
	void OnTriggerExit(StateController stateController);
}
