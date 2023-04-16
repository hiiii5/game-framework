using System.Collections.Generic;

namespace Game.Core {
	public interface IGameState {
		IController FindControllerByID(string id);
		List<IController> GetAllControllers();
	}
}