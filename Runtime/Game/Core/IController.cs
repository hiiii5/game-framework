namespace Game.Core {
	public interface IController {
		public string GetControllerID();
		public IGameState GetState();
		public void SetState(IGameState state);
	}
}