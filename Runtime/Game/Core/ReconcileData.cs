using FishNet.Object.Prediction;
using UnityEngine;

namespace Game.Core {
	/// <summary>
	/// Data on how to reconcile.
	/// Server sends this back to the client. Once the client receives this they
	/// will reset their object using this information. Like with MoveData anything that may
	/// affect your movement should be reset. Since this is just a transform only position and
	/// rotation would be reset. But a rigidbody would include velocities as well. If you are using
	/// an asset it's important to know what systems in that asset affect movement and need
	/// to be reset as well.
	/// </summary>
	public struct ReconcileData : IReconcileData
	{
		public Vector3 Position;
		public Quaternion Rotation;
		private uint _tick;

		public ReconcileData(Vector3 position, Quaternion rotation)
		{
			Position = position;
			Rotation = rotation;
			_tick = 0;
		}

		public void Dispose() { }
		public uint GetTick() => _tick;
		public void SetTick(uint value) => _tick = value;
	}
}