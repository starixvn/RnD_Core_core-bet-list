namespace Fanex.BetList.Core.RemotingRef.Tests
{
    using System;
    using Fanex.RemotingRef;
    using NSubstitute;
    using NUnit.Framework;

    public class CacheRefTests
    {
        private IRemotingRefService _remotingRefService;
        private CacheRef _cacheRef;
        private string _currentTestingLanguage;
        private string _defaultValue;

        [SetUp]
        public void Setup()
        {
            _remotingRefService = Substitute.For<IRemotingRefService>();
            _cacheRef = new CacheRef(_remotingRefService);
            _currentTestingLanguage = CacheRef.CurrentLanguage;
            _defaultValue = CacheRef.DefaultValue;
        }

        [Test]
        public void GetBetType_Always_CallGetBetTypeNameOfRemotingRefService()
        {
            // Arrange.
            var betTypeId = "betTypeId";
            var resourceId = "resourceId";
            var refNo = 0L;

            // Act.
            _cacheRef.GetBetType(betTypeId, resourceId, refNo);

            // Assert.
            _remotingRefService
                .Received()
                .GetBetTypeName(
                    Arg.Is(betTypeId),
                    Arg.Is(_currentTestingLanguage),
                    Arg.Is(_defaultValue),
                    Arg.Is(resourceId),
                    Arg.Is(refNo));
        }

        [Test]
        public void GetBetType_Always_ReturnExactResultOfRemotingRefService()
        {
            // Arrange.
            var betTypeId = "betTypeId";
            var resourceId = "resourceId";
            var refNo = 0L;
            var betTypeName = "betTypeName";

            _remotingRefService
                .GetBetTypeName(
                    Arg.Is(betTypeId),
                    Arg.Is(_currentTestingLanguage),
                    Arg.Is(_defaultValue),
                    Arg.Is(resourceId),
                    Arg.Is(refNo))
                .Returns(betTypeName);

            // Act.
            var actualResult = _cacheRef.GetBetType(betTypeId, resourceId, refNo);

            // Assert.
            Assert.AreSame(betTypeName, actualResult);
        }

        [Test]
        public void GetDisplayedBetType_Always_CallGetDisplayedBetTypeNameOfRemotingRefService()
        {
            // Arrange.
            var betTypeId = "betTypeId";
            var resourceId = "resourceId";
            var refNo = 0L;

            // Act.
            _cacheRef.GetDisplayedBetType(betTypeId, resourceId, refNo);

            // Assert.
            _remotingRefService
                .Received()
                .GetDisplayedBetTypeName(
                    Arg.Is(betTypeId),
                    Arg.Is(resourceId),
                    Arg.Is(refNo),
                    Arg.Is(_currentTestingLanguage),
                    Arg.Is(_defaultValue));
        }

        [Test]
        public void GetDisplayedBetType_Always_ReturnExactResultOfRemotingRefService()
        {
            // Arrange.
            var betTypeId = "betTypeId";
            var resourceId = "resourceId";
            var refNo = 0L;
            var displayedBetTypeName = "displayedBetTypeName";

            _remotingRefService
                .GetDisplayedBetTypeName(
                    Arg.Is(betTypeId),
                    Arg.Is(resourceId),
                    Arg.Is(refNo),
                    Arg.Is(_currentTestingLanguage),
                    Arg.Is(_defaultValue))
                .Returns(displayedBetTypeName);

            // Act.
            var actualResult = _cacheRef.GetDisplayedBetType(betTypeId, resourceId, refNo);

            // Assert.
            Assert.AreSame(displayedBetTypeName, actualResult);
        }

        [Test]
        public void GetParentBetTypeIdByBetTypeId_Always_CallGetParentIdByBetTypeIdOfRemotingRefService()
        {
            // Arrange.
            var betTypeId = "betTypeId";
            var defaultParentBetTypeId = "defaultParentBetTypeId";

            // Act.
            _cacheRef.GetParentBetTypeIdByBetTypeId(betTypeId, defaultParentBetTypeId);

            // Assert.
            _remotingRefService
                .Received()
                .GetParentIdByBetTypeId(Arg.Is(betTypeId), Arg.Is(defaultParentBetTypeId));
        }

        [Test]
        public void GetParentBetTypeIdByBetTypeId_Always_ReturnExactResultOfRemotingRefService()
        {
            // Arrange.
            var betTypeId = "betTypeId";
            var defaultParentBetTypeId = "defaultParentBetTypeId";
            var parentBetTypeId = "parentBetTypeId";

            _remotingRefService
                .GetParentIdByBetTypeId(Arg.Is(betTypeId), Arg.Is(defaultParentBetTypeId))
                .Returns(parentBetTypeId);

            // Act.
            var actualResult = _cacheRef.GetParentBetTypeIdByBetTypeId(betTypeId, defaultParentBetTypeId);

            // Assert.
            Assert.AreSame(parentBetTypeId, actualResult);
        }

        [Test]
        public void GetSportName_Always_CallGetSportNameOfRemotingRefService()
        {
            // Arrange.
            var sportTypeId = "sportTypeId";

            // Act.
            _cacheRef.GetSportName(sportTypeId);

            // Assert.
            _remotingRefService
                .Received()
                .GetSportName(Arg.Is(sportTypeId), Arg.Is(_currentTestingLanguage));
        }

        [Test]
        public void GetSportName_Always_ReturnExactResultOfRemotingRefService()
        {
            // Arrange.
            var sportTypeId = "sportTypeId";
            var sportTypeName = "sportTypeName";

            _remotingRefService
                .GetSportName(Arg.Is(sportTypeId), Arg.Is(_currentTestingLanguage))
                .Returns(sportTypeName);

            // Act.
            var actualResult = _cacheRef.GetSportName(sportTypeId);

            // Assert.
            Assert.AreSame(sportTypeName, actualResult);
        }

        [Test]
        public void GetTeamName_Always_CallGetTeamNameOfRemotingRefService()
        {
            // Arrange.
            var teamId = "teamId";

            // Act.
            _cacheRef.GetTeamName(teamId);

            // Assert.
            _remotingRefService
                .Received()
                .GetTeamName(Arg.Is(teamId), Arg.Is(_currentTestingLanguage));
        }

        [Test]
        public void GetTeamName_Always_ReturnExactResultOfRemotingRefService()
        {
            // Arrange.
            var teamId = "teamId";
            var teamName = "teamName";

            _remotingRefService
                .GetTeamName(Arg.Is(teamId), Arg.Is(_currentTestingLanguage))
                .Returns(teamName);

            // Act.
            var actualResult = _cacheRef.GetTeamName(teamId);

            // Assert.
            Assert.AreSame(teamName, actualResult);
        }

        [Test]
        public void GetTeamName3rd_Always_CallGetTeamName3rdOfRemotingRefService()
        {
            // Arrange.
            var team3rdId = "team3rdId";

            // Act.
            _cacheRef.GetTeamName3rd(team3rdId);

            // Assert.
            _remotingRefService
                .Received()
                .GetTeamName3rd(Arg.Is(team3rdId), Arg.Is(_currentTestingLanguage));
        }

        [Test]
        public void GetTeamName3rd_Always_ReturnExactResultOfRemotingRefService()
        {
            // Arrange.
            var team3rdId = "team3rdId";
            var team3rdName = "team3rdName";

            _remotingRefService
                .GetTeamName3rd(Arg.Is(team3rdId), Arg.Is(_currentTestingLanguage))
                .Returns(team3rdName);

            // Act.
            var actualResult = _cacheRef.GetTeamName3rd(team3rdId);

            // Assert.
            Assert.AreSame(team3rdName, actualResult);
        }

        [Test]
        public void GetRouletteBetItem_Always_CallGetRouletteBetItemOfRemotingRefService()
        {
            // Arrange.
            var itemId = "itemId";

            // Act.
            _cacheRef.GetRouletteBetItem(itemId);

            // Assert.
            _remotingRefService
                .Received()
                .GetRouletteBetItem(Arg.Is(itemId), Arg.Is(_currentTestingLanguage));
        }

        [Test]
        public void GetRouletteBetItem_Always_ReturnExactResultOfRemotingRefService()
        {
            // Arrange.
            var itemId = "itemId";
            var rouletteBetItem = "rouletteBetItem";

            _remotingRefService
                .GetRouletteBetItem(Arg.Is(itemId), Arg.Is(_currentTestingLanguage))
                .Returns(rouletteBetItem);

            // Act.
            var actualResult = _cacheRef.GetRouletteBetItem(itemId);

            // Assert.
            Assert.AreSame(rouletteBetItem, actualResult);
        }

        [Test]
        public void GetCasinoBetItem_Always_CallGetCasinoBetItemOfRemotingRefService()
        {
            // Arrange.
            var betStatusId = "betStatusId";

            // Act.
            _cacheRef.GetCasinoBetItem(betStatusId);

            // Assert.
            _remotingRefService
                .Received()
                .GetCasinoBetItem(
                    Arg.Is(betStatusId),
                    Arg.Is(_currentTestingLanguage),
                    Arg.Is(_defaultValue));
        }

        [Test]
        public void GetCasinoBetItem_Always_ReturnExactResultOfRemotingRefService()
        {
            // Arrange.
            var betStatusId = "betStatusId";
            var betItem = "betItem";

            _remotingRefService
                .GetCasinoBetItem(Arg.Is(betStatusId), Arg.Is(_currentTestingLanguage), Arg.Is(_defaultValue))
                .Returns(betItem);

            // Act.
            var actualResult = _cacheRef.GetCasinoBetItem(betStatusId);

            // Assert.
            Assert.AreSame(betItem, actualResult);
        }

        [Test]
        public void GetLeagueName_Always_CallGetLeagueNameOfRemotingRefService()
        {
            // Arrange.
            var leagueId = "leagueId";

            // Act.
            _cacheRef.GetLeagueName(leagueId);

            // Assert.
            _remotingRefService
                .Received()
                .GetLeagueName(Arg.Is(leagueId), Arg.Is(_currentTestingLanguage));
        }

        [Test]
        public void GetLeagueName_Always_ReturnExactResultOfRemotingRefService()
        {
            // Arrange.
            var leagueId = "leagueId";
            var leagueName = "leagueName";

            _remotingRefService
                .GetLeagueName(Arg.Is(leagueId), Arg.Is(_currentTestingLanguage))
                .Returns(leagueName);

            // Act.
            var actualResult = _cacheRef.GetLeagueName(leagueId);

            // Assert.
            Assert.AreSame(leagueName, actualResult);
        }

        [Test]
        public void GetLeagueName3rd_Always_CallGetLeagueName3rdOfRemotingRefService()
        {
            // Arrange.
            var league3rdId = "league3rdId";

            // Act.
            _cacheRef.GetLeagueName3rd(league3rdId);

            // Assert.
            _remotingRefService
                .Received()
                .GetLeagueName3rd(Arg.Is(league3rdId), Arg.Is(_currentTestingLanguage));
        }

        [Test]
        public void GetLeagueName3rd_Always_ReturnExactResultOfRemotingRefService()
        {
            // Arrange.
            var league3rdId = "league3rdId";
            var league3rdName = "league3rdName";

            _remotingRefService
                .GetLeagueName3rd(Arg.Is(league3rdId), Arg.Is(_currentTestingLanguage))
                .Returns(league3rdName);

            // Act.
            var actualResult = _cacheRef.GetLeagueName3rd(league3rdId);

            // Assert.
            Assert.AreSame(league3rdName, actualResult);
        }

        [Test]
        public void GetHorseTeamName_Always_CallGetHorseTeamNameOfRemotingRefService()
        {
            // Arrange.
            var codeId = "codeId";

            // Act.
            _cacheRef.GetHorseTeamName(codeId);

            // Assert.
            _remotingRefService
                .Received()
                .GetHorseTeamName(
                    Arg.Is(codeId),
                    Arg.Is(_currentTestingLanguage),
                    Arg.Is(_defaultValue));
        }

        [Test]
        public void GetHorseTeamName_Always_ReturnExactResultOfRemotingRefService()
        {
            // Arrange.
            var codeId = "codeId";
            var horseTeamName = "horseTeamName";

            _remotingRefService
                .GetHorseTeamName(Arg.Is(codeId), Arg.Is(_currentTestingLanguage), Arg.Is(_defaultValue))
                .Returns(horseTeamName);

            // Act.
            var actualResult = _cacheRef.GetHorseTeamName(codeId);

            // Assert.
            Assert.AreSame(horseTeamName, actualResult);
        }

        [Test]
        public void GetOddsTypeName_Always_CallGetOddsTypeNameOfRemotingRefService()
        {
            // Arrange.
            var oddsTypeId = "oddsTypeId";

            // Act.
            _cacheRef.GetOddsTypeName(oddsTypeId);

            // Assert.
            _remotingRefService
                .Received()
                .GetOddsTypeName(Arg.Is(oddsTypeId), Arg.Is(_currentTestingLanguage));
        }

        [Test]
        public void GetOddsTypeName_Always_ReturnExactResultOfRemotingRefService()
        {
            // Arrange.
            var oddsTypeId = "oddsTypeId";
            var oddsTypeName = "oddsTypeName";

            _remotingRefService
                .GetOddsTypeName(Arg.Is(oddsTypeId), Arg.Is(_currentTestingLanguage))
                .Returns(oddsTypeName);

            // Act.
            var actualResult = _cacheRef.GetOddsTypeName(oddsTypeId);

            // Assert.
            Assert.AreSame(oddsTypeName, actualResult);
        }

        [Test]
        public void GetKenoBetChoiceName_Always_CallGetKenoBetChoiceOfRemotingRefService()
        {
            // Arrange.
            var kenoBetChoiceId = "kenoBetChoiceId";

            // Act.
            _cacheRef.GetKenoBetChoiceName(kenoBetChoiceId, out _);

            // Assert.
            _remotingRefService
                .Received()
                .GetKenoBetChoice(
                    Arg.Is(kenoBetChoiceId),
                    Arg.Is(_currentTestingLanguage),
                    out _);
        }

        [Test]
        public void GetKenoBetChoiceName_Always_ReturnExactResultOfRemotingRefService()
        {
            // Arrange.
            var kenoBetChoiceId = "kenoBetChoiceId";
            var kenoBetChoiceName = "kenoBetChoiceName";
            var kenoBetTypeId = "kenoBetTypeId";

            _remotingRefService
                .GetKenoBetChoice(Arg.Is(kenoBetChoiceId), Arg.Is(_currentTestingLanguage), out _)
                .Returns(parameters =>
                {
                    parameters[2] = kenoBetTypeId;

                    return kenoBetChoiceName;
                });

            // Act.
            var actualKenoBetChoiceName = _cacheRef.GetKenoBetChoiceName(kenoBetChoiceId, out string actualKenoBetTypeId);

            // Assert.
            Assert.AreSame(kenoBetChoiceName, actualKenoBetChoiceName);
            Assert.AreSame(kenoBetTypeId, actualKenoBetTypeId);
        }

        [Test]
        public void GetKenoBetTypeName_Always_CallGetKenoBetTypeOfRemotingRefService()
        {
            // Arrange.
            var kenoBetTypeId = "kenoBetTypeId";

            // Act.
            _cacheRef.GetKenoBetTypeName(kenoBetTypeId);

            // Assert.
            _remotingRefService
                .Received()
                .GetKenoBetType(Arg.Is(kenoBetTypeId), Arg.Is(_currentTestingLanguage));
        }

        [Test]
        public void GetKenoBetTypeName_Always_ReturnExactResultOfRemotingRefService()
        {
            // Arrange.
            var kenoBetTypeId = "kenoBetTypeId";
            var kenoBetTypeName = "kenoBetTypeName";

            _remotingRefService
                .GetKenoBetType(Arg.Is(kenoBetTypeId), Arg.Is(_currentTestingLanguage))
                .Returns(kenoBetTypeName);

            // Act.
            var actualResult = _cacheRef.GetKenoBetTypeName(kenoBetTypeId);

            // Assert
            Assert.AreSame(kenoBetTypeName, actualResult);
        }

        [Test]
        public void GetResourceData_Always_CallGetResourceDataByRefIdAndResourceIdOfRemotingRefService()
        {
            // Arrange.
            var refId = "refId";
            var resourceId = "resourceId";

            // Act.
            _cacheRef.GetResourceData(refId, resourceId);

            // Assert.
            _remotingRefService
                .Received()
                .GetResourceDataByRefIdAndResourceId(
                    Arg.Is(refId),
                    Arg.Is(resourceId),
                    Arg.Is(_currentTestingLanguage),
                    Arg.Is(_defaultValue));
        }

        [Test]
        public void GetResourceData_Always_ReturnExactResultOfRemotingRefService()
        {
            // Arrange.
            var refId = "refId";
            var resourceId = "resourceId";
            var resourceData = "resourceData";

            _remotingRefService
                .GetResourceDataByRefIdAndResourceId(
                    Arg.Is(refId),
                    Arg.Is(resourceId),
                    Arg.Is(_currentTestingLanguage),
                    Arg.Is(_defaultValue))
                .Returns(resourceData);

            // Act.
            var actualResult = _cacheRef.GetResourceData(refId, resourceId);

            // Assert.
            Assert.AreSame(resourceData, actualResult);
        }

        [Test]
        public void GetCombinationTypeCombo_Always_CallGetCombinationTypeComboOfRemotingRefService()
        {
            // Arrange.
            var betTeam = "betTeam";
            var betCheck = "betCheck";
            var handicap1 = 0;
            var handicap2 = 0;

            // Act.
            _cacheRef.GetCombinationTypeCombo(betTeam, betCheck, handicap1, handicap2);

            // Assert.
            _remotingRefService
                .Received()
                .GetCombinationTypeCombo(
                    Arg.Is(betTeam),
                    Arg.Is(betCheck),
                    Arg.Is(handicap1),
                    Arg.Is(handicap2),
                    Arg.Is(_currentTestingLanguage));
        }

        [Test]
        public void GetCombinationTypeCombo_Always_ReturnExactResultOfRemotingRefService()
        {
            // Arrange.
            var betTeam = "betTeam";
            var betCheck = "betCheck";
            var handicap1 = 0;
            var handicap2 = 0;
            var combo = Array.Empty<string>();

            _remotingRefService
                .GetCombinationTypeCombo(
                    Arg.Is(betTeam),
                    Arg.Is(betCheck),
                    Arg.Is(handicap1),
                    Arg.Is(handicap2),
                    Arg.Is(_currentTestingLanguage))
                .Returns(combo);

            // Act.
            var actualResult = _cacheRef.GetCombinationTypeCombo(betTeam, betCheck, handicap1, handicap2);

            // Assert.
            Assert.AreSame(combo, actualResult);
        }

        [Test]
        public void GetFastMarketsChoice_Always_CallGetFastMarketsChoiceOfRemotingRefService()
        {
            // Arrange.
            var betTypeId = "betTypeId";
            var betTeam = "betTeam";
            var handicap1 = 0;
            var handicap2 = 0;

            // Act.
            _cacheRef.GetFastMarketsChoice(betTypeId, betTeam, handicap1, handicap2);

            // Assert.
            _remotingRefService
                .Received()
                .GetFastMarketsChoice(
                    Arg.Is(betTypeId),
                    Arg.Is(betTeam),
                    Arg.Is(handicap1),
                    Arg.Is(handicap2),
                    Arg.Is(_currentTestingLanguage));
        }

        [Test]
        public void GetFastMarketsChoice_Always_ReturnExactResultOfRemotingRefService()
        {
            // Arrange.
            var betTypeId = "betTypeId";
            var betTeam = "betTeam";
            var handicap1 = 0;
            var handicap2 = 0;
            var fastMarketsChoice = "fastMarketsChoice";

            _remotingRefService
                .GetFastMarketsChoice(
                    Arg.Is(betTypeId),
                    Arg.Is(betTeam),
                    Arg.Is(handicap1),
                    Arg.Is(handicap2),
                    Arg.Is(_currentTestingLanguage))
                .Returns(fastMarketsChoice);

            // Act.
            var actualResult = _cacheRef.GetFastMarketsChoice(betTypeId, betTeam, handicap1, handicap2);

            // Assert.
            Assert.AreSame(fastMarketsChoice, actualResult);
        }

        [Test]
        public void GetLotteryBetChoiceName_Always_CallGetLotteryBetChoiceOfRemotingRefService()
        {
            // Arrange.
            var lotteryBetChoiceId = "lotteryBetChoiceId";

            // Act.
            _cacheRef.GetLotteryBetChoiceName(lotteryBetChoiceId, out _);

            // Assert.
            _remotingRefService
                .Received()
                .GetLotteryBetChoice(
                    Arg.Is(lotteryBetChoiceId),
                    Arg.Is(_currentTestingLanguage),
                    out _);
        }

        [Test]
        public void GetLotteryBetChoiceName_Always_ReturnExactResultOfRemotingRefService()
        {
            // Arrange.
            var lotteryBetChoiceId = "lotteryBetChoiceId";
            var lotteryBetChoiceName = "lotteryBetChoiceName";
            var lotteryBetTypeId = "lotteryBetTypeId";

            _remotingRefService
                .GetLotteryBetChoice(Arg.Is(lotteryBetChoiceId), Arg.Is(_currentTestingLanguage), out _)
                .Returns(parameters =>
                {
                    parameters[2] = lotteryBetTypeId;

                    return lotteryBetChoiceName;
                });

            // Act.
            var actualLotteryBetChoiceName = _cacheRef.GetLotteryBetChoiceName(lotteryBetChoiceId, out string actualLotteryBetTypeId);

            // Assert.
            Assert.AreSame(lotteryBetChoiceName, actualLotteryBetChoiceName);
            Assert.AreSame(lotteryBetTypeId, actualLotteryBetTypeId);
        }

        [Test]
        public void GetLotteryBetTypeName_Always_CallGetLotteryBetTypeOfRemotingRefService()
        {
            // Arrange.
            var lotteryBetTypeId = "lotteryBetTypeId";

            // Act.
            _cacheRef.GetLotteryBetTypeName(lotteryBetTypeId);

            // Assert.
            _remotingRefService
                .Received()
                .GetLotteryBetType(Arg.Is(lotteryBetTypeId), Arg.Is(_currentTestingLanguage));
        }

        [Test]
        public void GetLotteryBetTypeName_Always_ReturnExactResultOfRemotingRefService()
        {
            // Arrange.
            var lotteryBetTypeId = "lotteryBetTypeId";
            var lotteryBetTypeName = "lotteryBetTypeName";

            _remotingRefService
                .GetLotteryBetType(Arg.Is(lotteryBetTypeId), Arg.Is(_currentTestingLanguage))
                .Returns(lotteryBetTypeName);

            // Act.
            var actualResult = _cacheRef.GetLotteryBetTypeName(lotteryBetTypeId);

            // Assert.
            Assert.AreSame(lotteryBetTypeName, actualResult);
        }

        [Test]
        public void GetBetRadarMatchCodeFormat_Always_CallGetBetRadarMatchCodeFormatOfRemotingRefService()
        {
            // Arrange.
            var matchCode = "matchCode";

            // Act.
            _cacheRef.GetBetRadarMatchCodeFormat(matchCode);

            // Assert.
            _remotingRefService
                .Received()
                .GetBetRadarMatchCodeFormat(Arg.Is(matchCode), Arg.Is(_currentTestingLanguage));
        }

        [Test]
        public void GetBetRadarMatchCodeFormat_Always_ReturnExactResultOfRemotingRefService()
        {
            // Arrange.
            var matchCode = "matchCode";
            var matchPattern = "matchPattern";

            _remotingRefService
                .GetBetRadarMatchCodeFormat(Arg.Is(matchCode), Arg.Is(_currentTestingLanguage))
                .Returns(matchPattern);

            // Act.
            var actualResult = _cacheRef.GetBetRadarMatchCodeFormat(matchCode);

            // Assert.
            Assert.AreSame(matchPattern, actualResult);
        }
    }
}