using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game.Core {
	public class DefaultState : MonoBehaviour, IGameState {
		// Find all game objects in the scene with player components and return the one with the matching ID.
		public IController FindControllerByID(string id)
		{
			return FindObjectsOfType<NetworkPlayer>().FirstOrDefault(player => player.GetControllerID() == id);
		}
        
		// Find all game objects in the scene with player components and return them all.
		public List<IController> GetAllControllers()
		{
			var list = FindObjectsOfType<NetworkPlayer>().ToList();
            
			return list.Cast<IController>().ToList();
		}
	}
}