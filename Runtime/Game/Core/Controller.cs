using FishNet.Object;

namespace Game.Core {
	public class Controller : NetworkBehaviour, IController {
		private readonly string _controllerID = System.Guid.NewGuid().ToString();
		protected IGameState State;

		public string GetControllerID()
		{
			return _controllerID;
		}

		public IGameState GetState()
		{
			return State;
		}
	}
}