using UnityEngine.InputSystem;

namespace Game.Core {
	public interface IPlayer {
		event NetworkPlayer.OnLookChanged OnLookChangedEvent;
		event NetworkPlayer.OnMoveChanged OnMoveChangedEvent;
		event NetworkPlayer.OnMove OnMoveEvent;
		InputActionAsset InputActionMap { get; }

		/// <summary>
		/// A simple method to get input. This doesn't have any relation to the prediction.
		/// </summary>
		void CheckInput(out MoveData md);

		void BindInput();
		void LookInput(InputAction.CallbackContext obj);
		void MoveInput(InputAction.CallbackContext obj);
	}
}