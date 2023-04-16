using UnityEngine;
using FishNet.Object.Prediction;
using FishNet.Transporting;

namespace Game.Core {
	public class DefaultReconciliationStrategy : IReconciliationStrategy {
		/// <summary>
		/// A Reconcile attribute indicates the client will reconcile
		/// using the data and logic within the method. When asServer
		/// is true the data is sent to the client with redundancy,
		/// and the server will not run the logic.
		/// When asServer is false the client will reset using the logic
		/// you supply then replay their inputs.
		/// </summary>
		[Reconcile]
		public void Perform(ReconcileData rd, bool asServer, Transform transform, Channel channel = Channel.Unreliable)
		{
			transform.position = rd.Position;
			transform.rotation = rd.Rotation;
		}
	}
}