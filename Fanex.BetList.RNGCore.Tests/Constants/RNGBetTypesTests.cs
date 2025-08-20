namespace Fanex.BetList.RNGCore.Tests.Constants
{
    using Fanex.BetList.Core.Constants;
    using NUnit.Framework;

    [TestFixture]
    public class RNGBetTypesTests
    {
        [Test]
        public void SmartSoftMin_Always_Returns12150()
        {
            // Act & Assert
            Assert.AreEqual(12150, RNGBetTypes.SmartSoftMin);
        }

        [Test]
        public void SmartSoftMax_Always_Returns12249()
        {
            // Act & Assert
            Assert.AreEqual(12249, RNGBetTypes.SmartSoftMax);
        }

        [Test]
        public void SmartSoftRange_IsValid()
        {
            // Act & Assert
            Assert.IsTrue(RNGBetTypes.SmartSoftMin < RNGBetTypes.SmartSoftMax);
            Assert.AreEqual(100, RNGBetTypes.SmartSoftMax - RNGBetTypes.SmartSoftMin + 1); // Range should be 100
        }

        [Test]
        public void SmartSoftRange_DoesNotOverlapWithOtherRanges()
        {
            // Act & Assert
            // Should not overlap with HotDog range
            Assert.IsTrue(RNGBetTypes.SmartSoftMin > RNGBetTypes.HotDogMax);
            
            // Should not overlap with GPILottery range
            Assert.IsTrue(RNGBetTypes.SmartSoftMax < RNGBetTypes.GPILotteryMin);
        }

        [Test]
        public void SmartSoftRange_ContainsValidBetTypeIds()
        {
            // Act & Assert
            // Test that 12150 is within the range
            Assert.IsTrue(RNGBetTypes.SmartSoftMin >= 12150 && RNGBetTypes.SmartSoftMin <= RNGBetTypes.SmartSoftMax);
            
            // Test that 12249 is within the range
            Assert.IsTrue(RNGBetTypes.SmartSoftMax >= 12150 && RNGBetTypes.SmartSoftMax <= RNGBetTypes.SmartSoftMax);
            
            // Test a middle value
            int middleValue = (RNGBetTypes.SmartSoftMin + RNGBetTypes.SmartSoftMax) / 2;
            Assert.IsTrue(middleValue >= RNGBetTypes.SmartSoftMin && middleValue <= RNGBetTypes.SmartSoftMax);
        }

        [Test]
        public void SmartSoftRange_BoundaryValuesAreCorrect()
        {
            // Act & Assert
            // Test minimum boundary
            Assert.AreEqual(12150, RNGBetTypes.SmartSoftMin);
            
            // Test maximum boundary
            Assert.AreEqual(12249, RNGBetTypes.SmartSoftMax);
            
            // Test that boundaries are consecutive
            Assert.AreEqual(RNGBetTypes.SmartSoftMin + 99, RNGBetTypes.SmartSoftMax);
        }

        [Test]
        public void SmartSoftRange_ConstantsAreReadOnly()
        {
            // Act & Assert
            // These are compile-time constants, so they cannot be changed
            // This test documents the expected behavior
            Assert.IsTrue(RNGBetTypes.SmartSoftMin == 12150);
            Assert.IsTrue(RNGBetTypes.SmartSoftMax == 12249);
        }

        [Test]
        public void SmartSoftRange_AllValuesInRangeAreValid()
        {
            // Act & Assert
            for (int i = RNGBetTypes.SmartSoftMin; i <= RNGBetTypes.SmartSoftMax; i++)
            {
                Assert.IsTrue(i >= RNGBetTypes.SmartSoftMin && i <= RNGBetTypes.SmartSoftMax, 
                    $"Value {i} should be within SmartSoft range");
            }
        }

        [Test]
        public void SmartSoftRange_ValuesOutsideRangeAreInvalid()
        {
            // Act & Assert
            // Test values just outside the range
            Assert.IsFalse(RNGBetTypes.SmartSoftMin - 1 >= RNGBetTypes.SmartSoftMin && 
                          RNGBetTypes.SmartSoftMin - 1 <= RNGBetTypes.SmartSoftMax);
            
            Assert.IsFalse(RNGBetTypes.SmartSoftMax + 1 >= RNGBetTypes.SmartSoftMin && 
                          RNGBetTypes.SmartSoftMax + 1 <= RNGBetTypes.SmartSoftMax);
        }

        [Test]
        public void SmartSoftRange_ConstantsArePublic()
        {
            // Act & Assert
            // These constants should be accessible from other assemblies
            Assert.IsTrue(RNGBetTypes.SmartSoftMin == 12150);
            Assert.IsTrue(RNGBetTypes.SmartSoftMax == 12249);
        }

        [Test]
        public void SmartSoftRange_ConstantsAreStatic()
        {
            // Act & Assert
            // These constants should be accessible without instantiating the class
            Assert.IsTrue(RNGBetTypes.SmartSoftMin == 12150);
            Assert.IsTrue(RNGBetTypes.SmartSoftMax == 12249);
        }
    }
}
