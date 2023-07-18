namespace Sweaj.Patterns.Requests
{
    public sealed class HeartbeatSynthesizer
    {
        private HeartbeatSynthesizer(HeartbeatStatus heartbeatStatus)
        {
            RequestId = Guid.NewGuid();
            Status = heartbeatStatus;
        }

        public Guid RequestId { get; }
        public HeartbeatStatus Status { get; }

        public static async Task<HeartbeatSynthesizer> CreateAsync<TParam>(Func<TParam, Task<HeartbeatStatus>> heartbeatCall, TParam param)
        {
            var heartbeatStatus = await heartbeatCall(param);
            return new HeartbeatSynthesizer(heartbeatStatus);
        }
    }
}
