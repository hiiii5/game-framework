using System.Collections.Generic;
using System.Linq;
using FishNet;
using FishNet.Managing;
using FishNet.Object;
using UnityEngine;

namespace Game.Core
{
    public class NetworkState : NetworkBehaviour, IGameState {
        /// <summary>
        /// Gets the NetworkManager for FishNet.
        /// </summary>
        /// <returns>The first instance of the network manager. See <see cref="InstanceFinder"/>.</returns>
        public NetworkManager GetNetworkManager()
        {
            return InstanceFinder.NetworkManager;
        }

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
