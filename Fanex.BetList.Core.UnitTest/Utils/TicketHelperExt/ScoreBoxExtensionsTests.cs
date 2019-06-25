namespace Fanex.BetList.Core.UnitTest.Utils.TicketHelperExt
{
    using System.Collections.Generic;
    using Core.Utils.TicketHelperExt;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;

    public class ScoreBoxExtensionsTests
    {
        private readonly static object[] InputsAndExpectedWinScoreOddsResourceIds =
        {
            new object[]
            {
                "H",
                "2:3.77,1:1.18,0:-0.50,-1:-2.19,-2:-7.02",
                new List<string>
                {
                    "HWS+",
                    "HW",
                    "D",
                    "HL",
                    "HLS+"
                }
            },
            new object[]
            {
                "H",
                "3:4.12,2:3.28,1:1.45,0:-0.25,-1:-2.1,-2:-6.02,-3:-7",
                new List<string>
                {
                    "HWS+",
                    "HWS",
                    "HW",
                    "D",
                    "HL",
                    "HLS",
                    "HLS+"
                }
            },
            new object[]
            {
                "h",
                "1:1.77,0:0.68,-1:-2.11,-2:-3.29,-3:-6.52",
                new List<string>
                {
                    "HW+",
                    "D",
                    "HL",
                    "HLS",
                    "HLS+"
                }
            },
            new object[]
            {
                "h",
                "3:5.57,2:4.8,1:1.5,0:-0.19,-1:-3.02",
                new List<string>
                {
                    "HWS+",
                    "HWS",
                    "HW",
                    "D",
                    "HL+"
                }
            },
            new object[]
            {
                "h",
                "4:5.5,3:4.75,2:2.5,1:-2.19,0:-3",
                new List<string>
                {
                    "HWS+",
                    "HWS",
                    "HWS",
                    "HW",
                    "D"
                }
            },
            new object[]
            {
                "a",
                "2:-4.66,1:-1.45,0:0.48,-1:1.78,-2:5.68",
                new List<string>
                {
                    "HWS+",
                    "HW",
                    "D",
                    "HL",
                    "HLS+"
                }
            },
            new object[]
            {
                "A",
                "3:2.24,2:2.0,1:1.5,0:-0.5,-1:-2.45,-2:-4.12,-3:-5.12",
                new List<string>
                {
                    "HWS+",
                    "HWS",
                    "HW",
                    "D",
                    "HL",
                    "HLS",
                    "HLS+"
                }
            },
            new object[]
            {
                "a",
                "1:2.75,0:0.8,-1:-2.15,-2:-3.9,-3:-5.2",
                new List<string>
                {
                    "HWS+",
                    "HWS",
                    "HW",
                    "D",
                    "HL+"
                }
            },
            new object[]
            {
                "a",
                "3:5.7,2:4.18,1:1.35,0:-0.19,-1:-3.2",
                new List<string>
                {
                    "HW+",
                    "D",
                    "HL",
                    "HLS",
                    "HLS+"
                }
            },
            new object[]
            {
                "a",
                "0:1.15,-1:-1.75,-2:-2.5,-3:-2.9,-4:-3.5",
                new List<string>
                {
                    "HWS+",
                    "HWS",
                    "HWS",
                    "HW",
                    "D"
                }
            }
        };

        private readonly static object[] InputsAndExpectedTotalGoalsOddsResourceIds =
        {
            new object[]
            {
                "O",
                "6:4.20,5:1.52,4:0.00,3:-2.29,2:-6.32",
                new List<string>
                {
                    "OS+",
                    "OS",
                    "OS",
                    "OS",
                    "OS-"
                }
            },
            new object[]
            {
                "o",
                "5:4.52,4:2.12,3:0.00,2:-3.9,1:-5.22",
                new List<string>
                {
                    "OS+",
                    "OS",
                    "OS",
                    "OS",
                    "O-"
                }
            },
            new object[]
            {
                "o",
                "3:5.25,2:4.51,1:2.00,0:-4.5",
                new List<string>
                {
                    "OS+",
                    "OS",
                    "O",
                    "O"
                }
            },
            new object[]
            {
                "U",
                "6:-5.19,5:-1.88,4:0.00,3:1.85,2:5.12",
                new List<string>
                {
                    "OS+",
                    "OS",
                    "OS",
                    "OS",
                    "OS-"
                }
            },
            new object[]
            {
                "u",
                "5:-4.33,4:-2.2,3:0.00,2:3.5,1:4.32",
                new List<string>
                {
                    "OS+",
                    "OS",
                    "OS",
                    "OS",
                    "O-"
                }
            }
        };

        private ITicketHelper _ticketHelper;

        [SetUp]
        public void Setup()
        {
            _ticketHelper = Substitute.For<ITicketHelper>();
        }

        [TestCaseSource(nameof(InputsAndExpectedWinScoreOddsResourceIds))]
        public void BuilWinScoreOdds_Always_PassCorrectResourceIdToGetResourceDataOfTicketHelper(
            string betTeam,
            string betCheck,
            List<string> expectedWinScoreOddsResourceIds)
        {
            // Act.
            _ticketHelper.GetScoreBoxOdds(betTeam, betCheck);

            // Assert.
            _ticketHelper.Received(expectedWinScoreOddsResourceIds.Count).GetResourceData(ScoreBoxExtensions.ScoreBoxRefId, Arg.Any<string>());

            foreach (var expectedWinScoreOddsResourceId in expectedWinScoreOddsResourceIds)
            {
                _ticketHelper.Received().GetResourceData(ScoreBoxExtensions.ScoreBoxRefId, expectedWinScoreOddsResourceId);
            }
        }

        [TestCaseSource(nameof(InputsAndExpectedTotalGoalsOddsResourceIds))]
        public void BuilTotalGoalsOdds_Always_PassCorrectResourceIdToGetResourceDataOfTicketHelper(
            string betTeam,
            string betCheck,
            List<string> expectedTotalGoalsOddsResourceIds)
        {
            // Act.
            _ticketHelper.GetScoreBoxOdds(betTeam, betCheck);

            // Assert.
            _ticketHelper.Received(expectedTotalGoalsOddsResourceIds.Count).GetResourceData(ScoreBoxExtensions.ScoreBoxRefId, Arg.Any<string>());

            foreach (var expectedTotalGoalsOddsResourceId in expectedTotalGoalsOddsResourceIds)
            {
                _ticketHelper.Received().GetResourceData(ScoreBoxExtensions.ScoreBoxRefId, expectedTotalGoalsOddsResourceId);
            }
        }

        [TestCase("1:5")]
        [TestCase("1:5.0")]
        [TestCase("1:5.00")]
        [TestCase("1:5.000")]
        public void GetScoreBoxOdds_Always_FormatOdds(string betCheck)
        {
            // Arrange.
            const string betTeam = "H";
            const string expectedResourceId = "HW+";
            const string expectedScoreBoxDescriptionPattern = "Win By {0} Score Or More @{1}";

            _ticketHelper.GetResourceData(ScoreBoxExtensions.ScoreBoxRefId, expectedResourceId).Returns(expectedScoreBoxDescriptionPattern);

            // Act.
            var actualScoreBoxOdds = _ticketHelper.GetScoreBoxOdds(betTeam, betCheck);

            // Assert.
            const string expectedScoreBox = "Win By 1 Score Or More @5.00";

            Assert.AreEqual(expectedScoreBox, actualScoreBoxOdds[0]);
        }
    }
}