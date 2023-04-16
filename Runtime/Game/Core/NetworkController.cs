namespace Game.Core
{
    public class NetworkController : Controller
    {
        // Assign the State on construction
        private void Awake() {
            // Add a BaseState component to this controller
            State = gameObject.AddComponent<NetworkState>();
        }

        private void OnDestroy() {
            // destroy the state if still valid
            if (State is not null)
            {
                Destroy((NetworkState)State);
            }
        }
    }
}