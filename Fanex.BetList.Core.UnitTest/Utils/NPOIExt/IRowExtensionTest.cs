namespace Fanex.BetList.Core.UnitTest.Utils.NPOIExt
{
    using System;
    using Fanex.BetList.Core.Utils.NPOIExt;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit testing for IRowExtension class.
    /// </summary>
    public class IRowExtensionTest
    {
        private IRow _row;

        [SetUp]
        public void SetUp()
        {
            _row = Substitute.For<IRow>();
        }
    }
}
