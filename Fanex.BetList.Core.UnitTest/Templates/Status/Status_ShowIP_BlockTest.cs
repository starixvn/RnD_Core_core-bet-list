namespace Fanex.BetList.Core.UnitTest.Templates.Status
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Fanex.BetList.Core.Templates;
    using NUnit.Framework;

    /// <summary>
    /// The wrapper for Status_ShowIP_Block class.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class WrapperStatus_ShowIP_Block : Status_ShowIP_Block
    {
        public void SetAssigned(bool isAssigned)
        {
            this.IsAssigned = isAssigned;
        }

        public void SetValueExtend(StringBuilder value)
        {
            this.Value = value;
        }
    }

    /// <summary>
    /// The Unit testing for class Status_ShowIP_Block.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Status_ShowIP_BlockTest
    {
        private const string StatusTemplate = "<div class=\"ip\"><br /><div class=\"{0}\" onclick=\"{1}('{2}');\">{3}</div></div>";

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlStringDefaultValue()
        {
            // Arrange
            var ipLink = "iplink";
            var openIpFunc = "OpenIPInfo";
            var betIp = "{betIp}";
            var statusHtml = new StringBuilder();
            var block = new Status_ShowIP_Block();

            // Act
            block.ToString(ref statusHtml);

            // Assert
            var expectedHtml = string.Format(StatusTemplate, ipLink, openIpFunc, betIp, betIp);
            Assert.AreEqual(expectedHtml, statusHtml.ToString());
        }

        /// <summary>
        /// Visible the property is true return HTML string.
        /// </summary>
        [Test]
        public void ToString_WhenCalled_ReturnHtmlString()
        {
            // Arrange
            var ipLink = "iplink-Customize";
            var openIpFunc = "OpenIPInfo-Customize";
            var betIp = "{betIp}-Customize";
            var statusHtml = new StringBuilder();
            var block = new Status_ShowIP_Block();
            block.divIP = ipLink;
            block.openIpFunc = openIpFunc;
            block.betIp = betIp;

            // Act
            block.ToString(ref statusHtml);

            // Assert
            var expectedHtml = string.Format(StatusTemplate, ipLink, openIpFunc, betIp, betIp);
            Assert.AreEqual(expectedHtml, statusHtml.ToString());
        }

        /// <summary>
        /// Visible the property is false return empty string.
        /// </summary>
        [Test]
        public void ToString_BlockIsVisible_ReturnEmptyString()
        {
            // Arrange
            var statusHtml = new StringBuilder();
            var block = new Status_ShowIP_Block();
            block.Visible = false;

            // Act
            block.ToString(ref statusHtml);

            // Assert
            Assert.IsNullOrEmpty(statusHtml.ToString());
        }

        /// <summary>
        /// Assigned the property is true return value append to HTML string.
        /// </summary>
        [Test]
        public void ToString_AssignedBlockIsTrue_ReturnHtmlStringAppendValue()
        {
            // Arrange
            var ipLink = "iplink";
            var openIpFunc = "OpenIPInfo";
            var betIp = "{betIp}";
            var value = "Tennis";
            var statusHtml = new StringBuilder();
            var block = new WrapperStatus_ShowIP_Block();
            block.SetValueExtend(new StringBuilder(value));
            block.SetAssigned(true);

            // Act
            block.ToString(ref statusHtml);

            // Assert
            var expectedHtml = string.Format(StatusTemplate, ipLink, openIpFunc, betIp, betIp) + value;
            Assert.AreEqual(expectedHtml, statusHtml.ToString());
        }
    }
}