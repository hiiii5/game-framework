namespace Game.Core {
	public interface IController {
		string GetControllerID();
		IGameState GetState();
	}
}