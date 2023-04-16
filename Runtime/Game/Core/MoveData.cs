using FishNet.Object.Prediction;

namespace Game.Core {
	public struct MoveData : IReplicateData
	{
		public float Horizontal;
		public float Vertical;
            
		private uint _tick;
            
		public MoveData(float horizontal, float vertical)
		{
			this.Horizontal = horizontal;
			this.Vertical = vertical;
			_tick = 0;
		}

		public void Dispose() { }
            
		public uint GetTick()
		{
			return _tick;
		}
            
		public void SetTick(uint tick)
		{
			_tick = tick;
		}
	}
}