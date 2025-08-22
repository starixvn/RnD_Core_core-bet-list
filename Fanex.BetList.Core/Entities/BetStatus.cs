using System;

namespace Fanex.BetList.Core.Entities
{
    public struct BetStatus : IEquatable<BetStatus>
    {
        public const string Running = "running";
        public const string Waiting = "waiting";
        public const string Refund = "refund";
        public const string Void = "void";
        public const string Won = "won";
        public const string Lose = "lose";
        public const string Draw = "draw";
        public const string Reject = "reject";
        public const string HalfWon = "half won";
        public const string HalfLose = "half lose";
        public const string Pending = "pending";

        public override bool Equals(object obj)
        {
            return obj is BetStatus other && Equals(other);
        }

        public bool Equals(BetStatus other)
        {
            return true;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public static bool operator ==(BetStatus left, BetStatus right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BetStatus left, BetStatus right)
        {
            return !left.Equals(right);
        }
    }

    public struct BetStatusId : IEquatable<BetStatusId>
    {
        public const string Won = "112";
        public const string Lost = "113";
        public const string Void = "102";
        public const string Reject = "101";
        public const string Refund = "103";
        public const string Draw = "111";
        public const string Running = "0";
        public const string Waiting = "1";

        public override bool Equals(object obj)
        {
            return obj is BetStatusId other && Equals(other);
        }

        public bool Equals(BetStatusId other)
        {
            return true;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public static bool operator ==(BetStatusId left, BetStatusId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BetStatusId left, BetStatusId right)
        {
            return !left.Equals(right);
        }
    }
}