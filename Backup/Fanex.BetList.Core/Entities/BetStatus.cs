namespace Fanex.BetList.Core.Entities
{
    /// <summary>
    /// Struct BetStatus.
    /// </summary>
    public struct BetStatus
    {
        /// <summary>
        /// The running status.
        /// </summary>
        public const string Running = "running";

        /// <summary>
        /// The waiting status.
        /// </summary>
        public const string Waiting = "waiting";

        /// <summary>
        /// The refund status.
        /// </summary>
        public const string Refund = "refund";

        /// <summary>
        /// The void status.
        /// </summary>
        public const string Void = "void";

        /// <summary>
        /// The won status.
        /// </summary>
        public const string Won = "won";

        /// <summary>
        /// The lose status.
        /// </summary>
        public const string Lose = "lose";

        /// <summary>
        /// The draw status.
        /// </summary>
        public const string Draw = "draw";

        /// <summary>
        /// The reject status.
        /// </summary>
        public const string Reject = "reject";
    }

    /// <summary>
    /// Struct BetStatusId.
    /// </summary>
    public struct BetStatusId
    {
        /// <summary>
        /// The won status.
        /// </summary>
        public const string Won = "112";

        /// <summary>
        /// The lost status.
        /// </summary>
        public const string Lost = "113";

        /// <summary>
        /// The void status.
        /// </summary>
        public const string Void = "102";

        /// <summary>
        /// The reject status.
        /// </summary>
        public const string Reject = "101";

        /// <summary>
        /// The refund status.
        /// </summary>
        public const string Refund = "103";

        /// <summary>
        /// The draw status.
        /// </summary>
        public const string Draw = "111";

        /// <summary>
        /// The running status.
        /// </summary>
        public const string Running = "0";

        /// <summary>
        /// The waiting status.
        /// </summary>
        public const string Waiting = "1";
    }
}