using FishNet.Transporting;
using UnityEngine;

namespace Game.Core {
	public interface IReconciliationStrategy {
		/// <summary>
		/// A Reconcile attribute indicates the client will reconcile
		/// using the data and logic within the method. When asServer
		/// is true the data is sent to the client with redundancy,
		/// and the server will not run the logic.
		/// When asServer is false the client will reset using the logic
		/// you supply then replay their inputs.
		/// </summary>
		void Perform(ReconcileData rd, bool asServer, Transform transform, Channel channel = Channel.Unreliable);
	}
}