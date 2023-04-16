using System.Collections.Generic;
using System.Linq;
using FishNet;
using FishNet.Managing;
using FishNet.Object;
using UnityEngine;

namespace Game.Core
{
    public class NetworkState : NetworkBehaviour, IGameState {
        // get the first instance from the instance finder for fishnet.
        public NetworkManager GetNetworkManager()
        {
            return InstanceFinder.NetworkManager;
        }

        // Find a player controller by their ID
        public NetworkController FindPlayerControllerByID(string id)
        {
            var players = GameObject.FindGameObjectsWithTag("Player");
            return players.Select(player =>
                player.GetComponent<NetworkController>()).FirstOrDefault(playerController =>
                playerController.GetControllerID() == id);
        }

        // Create a list of all player controllers
        public List<NetworkController> GetAllPlayerControllers()
        {
            var players = GameObject.FindGameObjectsWithTag("Player");
            return players.Select(player =>
                player.GetComponent<NetworkController>()).ToList();
        }
    }
}
