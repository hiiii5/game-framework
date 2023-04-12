using System.Collections.Generic;
using System.Linq;
using FishNet;
using FishNet.Managing;
using FishNet.Object;
using UnityEngine;

namespace Game.Core
{
    public abstract class BaseState : NetworkBehaviour
    {
        // get the first instance from the instance finder for fishnet.
        public NetworkManager GetNetworkManager()
        {
            return InstanceFinder.NetworkManager;
        }

        // Find a player controller by their ID
        public BaseController FindPlayerControllerByID(string id)
        {
            var players = GameObject.FindGameObjectsWithTag("Player");
            return players.Select(player =>
                player.GetComponent<BaseController>()).FirstOrDefault(playerController =>
                playerController.GetControllerID() == id);
        }

        // Create a list of all player controllers
        public List<BaseController> GetAllPlayerControllers()
        {
            var players = GameObject.FindGameObjectsWithTag("Player");
            return players.Select(player =>
                player.GetComponent<BaseController>()).ToList();
        }
    }
}
