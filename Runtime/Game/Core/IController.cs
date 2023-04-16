namespace Game.Core {
	public interface IController {
		string GetControllerID();
		NetworkState GetState();
	}
}