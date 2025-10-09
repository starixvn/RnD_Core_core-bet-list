using System;
using Fanex.BetList.Core.App_GlobalResources;
using Fanex.BetList.Core.Constants;
using Fanex.BetList.Core.Entities;
using Fanex.BetList.Core.Templates;

namespace Fanex.BetList.Core.Utils.TicketHelperExt
{
  public static class SportbookExtensions
  {
    private const int TwoOptionsBetTeamLength = 2;
    private const int ThreeOptionsBetTeamLength = 3;
    private const string DoubleChangeFormat = "{0}/{1}";

    private const string SeparationFormat = "{0} <span class=\"underdog\">{1}</span> {2}";

    public static string BuildTwoOptionsSeperationChoice(string betTeam, Func<char, string> firstChoice, Func<char, string> secondChoice, string separaterString)
    {
      betTeam = betTeam?.ToLowerInvariant() ?? string.Empty;
      if (betTeam.Length == TwoOptionsBetTeamLength)
      {
        string firstChoiceValue = firstChoice(betTeam[0]);
        string secondChoiceValue = secondChoice(betTeam[1]);

        if (!string.IsNullOrEmpty(firstChoiceValue)
            && !string.IsNullOrEmpty(secondChoiceValue))
        {
          betTeam = string.Format(SeparationFormat, firstChoiceValue, separaterString, secondChoiceValue);
        }
      }

      return betTeam;
    }

    public static string BuildYesNoAndOverUnderChoice(string betTeam)
    {
      return BuildTwoOptionsSeperationChoice(betTeam, MapYesNoChoice, MapOverUnderChoice, CoreBetList.lblEt);
    }
    public static string BuildHomeAwayAndOverUnderChoice(string betTeam)
    {
      return BuildTwoOptionsSeperationChoice(betTeam, MapHomeAwayChoice, MapOverUnderChoice, CoreBetList.lblEt);
    }

    public static string BuildOddEvenAndOverUnderChoice(string betTeam)
    {
      return BuildTwoOptionsSeperationChoice(betTeam, MapOddEvenChoice, MapOverUnderChoice, CoreBetList.lblEt);
    }

    public static string BuildDoubleChangeChoice(string betTeam, Func<char, string> firstChoice, Func<char, string> secondChoice)
    {
      betTeam = betTeam?.ToLowerInvariant() ?? string.Empty;
      if (betTeam.Length == TwoOptionsBetTeamLength)
      {
        string firstChoiceValue = firstChoice(betTeam[0]);
        string secondChoiceValue = secondChoice(betTeam[1]);

        if (!string.IsNullOrEmpty(firstChoiceValue)
            && !string.IsNullOrEmpty(secondChoiceValue))
        {
          betTeam = string.Format(DoubleChangeFormat, firstChoiceValue, secondChoiceValue);
        }
      }

      return betTeam;
    }

    public static string BuildYesNoAndDoubleChangeChoice(string betTeam)
    {
      betTeam = betTeam?.ToLowerInvariant() ?? string.Empty;

      if (betTeam.Length == ThreeOptionsBetTeamLength)
      {
        string yesNoChoice = MapYesNoChoice(betTeam[0]);
        string firstChanceChoice = MapDoubleChanceChoice(betTeam[1]);
        string secondChanceChoice = MapDoubleChanceChoice(betTeam[2]);

        if (!string.IsNullOrEmpty(yesNoChoice)
            && !string.IsNullOrEmpty(firstChanceChoice)
            && !string.IsNullOrEmpty(secondChanceChoice))
        {
          betTeam = string.Format(SeparationFormat, yesNoChoice, CoreBetList.lblEt, string.Format(DoubleChangeFormat, firstChanceChoice, secondChanceChoice));
        }
      }

      return betTeam;
    }

    public static string BuildDoubleChangeAndOverUnderChoice(string betTeam)
    {
      betTeam = betTeam?.ToLowerInvariant() ?? string.Empty;

      if (betTeam.Length == ThreeOptionsBetTeamLength)
      {
        string firstChanceChoice = MapDoubleChanceChoice(betTeam[0]);
        string secondChanceChoice = MapDoubleChanceChoice(betTeam[1]);
        string overUnderChoice = MapOverUnderChoice(betTeam[2]);

        if (!string.IsNullOrEmpty(firstChanceChoice)
            && !string.IsNullOrEmpty(secondChanceChoice)
            && !string.IsNullOrEmpty(overUnderChoice))
        {
          betTeam = string.Format(SeparationFormat, string.Format(DoubleChangeFormat, firstChanceChoice, secondChanceChoice), CoreBetList.lblEt, overUnderChoice);
        }
      }
      return betTeam;
    }
    
    public static string BuildTeamToScoreChoice(string betTeam, string homeTeamName, string awayTeamName)
    {
      if (string.IsNullOrEmpty(betTeam))
      {
        return string.Empty;
      }

      switch (betTeam.ToLowerInvariant())
      {
        case BetTeamValue.H:
          return homeTeamName;

        case BetTeamValue.A:
          return awayTeamName;

        case BetTeamValue.B:
          return CoreBetList.lblBoth;

        case BetTeamValue.N:
          return CoreBetList.none;

        default:
          return string.Empty;
      }
    }

    public static string BuildYesNoChoice(string betTeam)
    {
      if (string.IsNullOrEmpty(betTeam))
      {
        return string.Empty;
      }

      return MapYesNoChoice(betTeam[0]);
    }

    public static string MapDoubleChanceChoice(char indicator)
    {
      switch (indicator.ToString())
      {
        case BetTeamValue.NumberOne:
          return CoreBetList.Home;

        case BetTeamValue.NumberTwo:
          return CoreBetList.Away;

        case BetTeamValue.X:
          return CoreBetList.draw;

        default:
          return string.Empty;
      }
    }

    public static string MapOverUnderChoice(char indicator)
    {
      switch (indicator.ToString())
      {
        case BetTeamValue.O:
          return CoreBetList.over;

        case BetTeamValue.U:
          return CoreBetList.under;

        default:
          return string.Empty;
      }
    }

    public static string MapYesNoChoice(char indicator)
    {
      switch (indicator.ToString())
      {
        case BetTeamValue.Y:
          return CoreBetList.lblYes;

        case BetTeamValue.N:
          return CoreBetList.lblNo;

        default:
          return string.Empty;
      }
    }

    public static string MapOddEvenChoice(char indicator)
    {
      switch (indicator.ToString())
      {
        case BetTeamValue.O:
          return CoreBetList.odd;

        case BetTeamValue.E:
          return CoreBetList.even;

        default:
          return string.Empty;
      }
    }

    public static string MapHomeAwayChoice(char indicator)
    {
      switch (indicator.ToString())
      {
        case BetTeamValue.H:
          return CoreBetList.Home;

        case BetTeamValue.A:
          return CoreBetList.Away;

        case BetTeamValue.D:
          return CoreBetList.draw;

        default:
          return string.Empty;
      }
    }

  }
}