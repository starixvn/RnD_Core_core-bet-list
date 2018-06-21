namespace Fanex.BetList.Core
{
    using System;
    using TicketHelper;

    public static class BetListManager
    {
        private static ICacheRef _registeredCacheRef;

        public static ICacheRef RegisteredCacheRef
        {
            get
            {
                VerifyRemotingRefRegistration();

                return _registeredCacheRef;
            }
        }

        public static void RegisterRemotingRef(ICacheRef cacheRef)
            => _registeredCacheRef = cacheRef ?? throw new ArgumentNullException(nameof(cacheRef));

        private static void VerifyRemotingRefRegistration()
        {
            if (_registeredCacheRef == null)
            {
                throw new InvalidOperationException("RemotingRef provider has not been provided.");
            }
        }

        public static void Verify()
        {
            VerifyRemotingRefRegistration();
        }
    }
}