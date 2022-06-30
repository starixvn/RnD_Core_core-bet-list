namespace Fanex.BetList.Core.UnitTest.Utils.NPOIExt
{
    using System;
    using Fanex.BetList.Core.Utils.NPOIExt;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using NSubstitute;
    using NUnit.Framework;

    /// <summary>
    /// Unit testing for ICellExtension class.
    /// </summary>
    public class ICellExtensionTest
    {
        private ICell _cell;

        [SetUp]
        public void SetUp()
        {
            _cell = Substitute.For<ICell>();
        }

        [Test]
        public void SetCellValue_TheValueIsNull_SetStringZeroToCell()
        {
            // Act
            ICellExtension.SetCellValue(_cell, null);

            // Assert
            const string CELL_VALUE_ZERO = "0";
            _cell.Received().SetCellValue(CELL_VALUE_ZERO);
        }

        [Test]
        public void SetCellValue_TypeOfValueIsString_SetValueToCell()
        {
            // Arrange
            const string STRING_VALUE = "string text";

            // Act
            ICellExtension.SetCellValue(_cell, STRING_VALUE);

            // Assert
            _cell.Received().SetCellValue(STRING_VALUE);
        }

        [Test]
        public void SetCellValue_TypeOfValueIsInt_SetValueToCell()
        {
            // Arrange
            const int INT_VALUE = 0;

            // Act
            ICellExtension.SetCellValue(_cell, INT_VALUE);

            // Assert
            _cell.Received().SetCellValue(INT_VALUE);
        }

        [Test]
        public void SetCellValue_TypeOfValueIsLong_SetValueToCell()
        {
            // Arrange
            const long LONG_VALUE = 0;

            // Act
            ICellExtension.SetCellValue(_cell, LONG_VALUE);

            // Assert
            _cell.Received().SetCellValue(LONG_VALUE);
        }

        [Test]
        public void SetCellValue_TypeOfValueIsDouble_SetValueToCell()
        {
            // Arrange
            const double DOUBLE_VALUE = 0;

            // Act
            ICellExtension.SetCellValue(_cell, DOUBLE_VALUE);

            // Assert
            _cell.Received().SetCellValue(DOUBLE_VALUE);
        }

        [Test]
        public void SetCellValue_TypeOfValueIsBool_SetValueToCell()
        {
            // Arrange
            const bool BOOL_VALUE = false;

            // Act
            ICellExtension.SetCellValue(_cell, BOOL_VALUE);

            // Assert
            _cell.Received().SetCellValue(BOOL_VALUE);
        }

        [Test]
        public void SetCellValue_TypeOfValueIsDateTime_SetValueToCell()
        {
            // Act
            ICellExtension.SetCellValue(_cell, DateTime.MinValue);

            // Assert
            _cell.Received().SetCellValue(DateTime.MinValue);
        }

        [Test]
        public void SetCellValue_TypeOfValueIsHSSFRichTextString_SetValueToCell()
        {
            // Arrange
            IRichTextString value = new HSSFRichTextString();

            // Act
            ICellExtension.SetCellValue(_cell, value);

            // Assert
            _cell.Received().SetCellValue(Arg.Any<IRichTextString>());
        }

        [Test]
        public void SetCellValue_InvalidTypeOfValue_SetStringUndefinedToCell()
        {
            // Act
            ICellExtension.SetCellValue(_cell, (short)0);

            // Assert
            const string STRING_UNDEFINED = "Undefined";
            _cell.Received().SetCellValue(STRING_UNDEFINED);
        }
    }
}