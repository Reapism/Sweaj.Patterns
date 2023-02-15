namespace Sweaj.Patterns.Requests
{
    public sealed class HeartbeatStatus
    {
        public HeartbeatStatus(bool isAlive, int statusCode, string statusMessage)
        {
            IsAlive = isAlive;
            StatusCode = statusCode;
            StatusMessage = statusMessage;
        }

        public bool IsAlive { get; }
        /*/
         * Store this on the caller side, not needed every hit.
         */
        //public DateTimeOffset LastAlive { get; }
        public int StatusCode { get; }
        public string StatusMessage { get; }

        public bool IsSuccessfulStatusCode()
        {
            return false;
        }

        public bool IsSuccessful() { return IsAlive && IsSuccessfulStatusCode(); }
    }
}
