using FishNet.Object;

namespace Game.Core
{
    public class Controller : NetworkBehaviour
    {
        // generate a unique ID for the player
        private readonly string _controllerID = System.Guid.NewGuid().ToString();

        private BaseState _state;
        
        public override void OnStartClient()
        {
            base.OnStartClient();

            // Add a BaseState component to this controller
            _state = gameObject.AddComponent<BaseState>();
        }

        public override void OnStopClient()
        {
            base.OnStopClient();
            
            // destroy the state if still valid
            if (_state is not null)
            {
                Destroy(_state);
            }
        }

        // Get the controller's ID
        public string GetControllerID()
        {
            return _controllerID;
        }
        
        // Get the controller's state
        public BaseState GetState()
        {
            return _state;
        }
    }
}