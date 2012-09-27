using Fanex.Data.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
namespace Fanex.DataTest
{


    /// <summary>
    ///This is a test class for DbSettingBaseTest and is intended
    ///to contain all DbSettingBaseTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DbSettingBaseTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        internal virtual DbSettingProvider CreateDbSettingBase()
        {
            // TODO: Instantiate an appropriate concrete class.
            Mock mock = new Mock<DbSettingProvider>();
            DbSettingProvider target = (DbSettingProvider)mock.Object;
            return target;
        }

        /// <summary>
        ///A test for GetSetting
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetSetting_Throw_Exception_If_Key_Is_Null_Or_WhiteSpace()
        {
            var mock = new Mock<DbSettingProvider> { CallBase = true };
            string key = null;

            mock.Setup(foo => foo.GetSetting(key))
                .Throws<ArgumentNullException>();

            var target = (DbSettingProvider)mock.Object; target.GetSetting(key);

            target.GetSetting(key);
        }
    }
}
