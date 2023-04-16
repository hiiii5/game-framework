using System.Collections.Generic;

namespace Game.Core {
	public interface IGameState {
		NetworkController FindPlayerControllerByID(string id);
		List<NetworkController> GetAllPlayerControllers();
	}
}