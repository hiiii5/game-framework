namespace Game.Core
{
    public class NetworkController : Controller
    {
        // generate a unique ID for the player

        public override void OnStartClient()
        {
            base.OnStartClient();

            // Add a BaseState component to this controller
            State = gameObject.AddComponent<NetworkState>();
        }

        public override void OnStopClient()
        {
            base.OnStopClient();
            
            // destroy the state if still valid
            if (State is not null)
            {
                Destroy((NetworkState)State);
            }
        }
    }
}