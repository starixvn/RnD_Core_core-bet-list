namespace Fanex.BetList.Core.TransId3rdHelper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class TransId3rdRNGHelper
    {
        private static string TransId3rdSeperator = "|$|";

        public static string GetTransId3rd(string transId)
        {
            if(transId == null || !transId.Contains(TransId3rdSeperator))
            {
                return transId;
            }

            var transIdParts = transId.Split(new string[] { TransId3rdSeperator }, StringSplitOptions.RemoveEmptyEntries);

            return transIdParts[transIdParts.Length - 1];
        }
    }
}