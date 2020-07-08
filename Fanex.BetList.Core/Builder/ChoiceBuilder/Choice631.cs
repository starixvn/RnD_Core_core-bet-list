namespace Fanex.BetList.Core.Builder.ChoiceBuilder
{
    using System.Collections.Generic;
    using App_GlobalResources;
    using Constants;
    using Entities;

    /// <summary>
    /// Quarter X Double Chance
    /// </summary>
    public class Choice631 : Choice1
    {
        private readonly Dictionary<string, string> betTeamPatterns
                = new Dictionary<string, string>
                {
                    { BetTeamValue.H, CoreBetList.Home },
                    { BetTeamValue.D, CoreBetList.Away }
                };

        protected override void BuildBetTeam(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            const int expectedBetTeamLength = 2;

            if (!string.IsNullOrEmpty(ticket.BetTeam) && ticket.BetTeam.Length == expectedBetTeamLength)
            {
                var betTeam = ticket.BetTeam.ToLowerInvariant();
                var firstIndicator = betTeam[0].ToString();
                var secondIndicator = betTeam[1].ToString();
                var firstChance = betTeamPatterns.ContainsKey(firstIndicator) ? betTeamPatterns[firstIndicator] : string.Empty;
                var secondChance = GetSecondText(firstIndicator, secondIndicator);

                Template.betTeam = $"{firstChance}<span class=\"{Underdog}\"> {CoreBetList.lblOr} </span>{secondChance}";
            }
            else
            {
                Template.betTeam = string.Empty;
            }
        }

        protected override void BuildBetTeamClassNameAndHandicap(ITicket ticket)
        {
            Template.betTeamClassName = Favorite;
            Template.Handicap.handicap = null;
        }

        protected override void BuildBetType(ITicket ticket, ITicketHelper ticketHelper, List<ITicketData> ticketData)
        {
            Template.BetType.betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId, ticket.RefNo, ticket.BetId, ticket.BetCheck);
        }

        private static string GetSecondText(string firstChance, string secondChance)
        {
            if (secondChance == BetTeamValue.A)
            {
                return firstChance == BetTeamValue.D ? CoreBetList.draw : CoreBetList.Away;
            }

            return secondChance == BetTeamValue.D ? CoreBetList.draw : string.Empty;
        }
    }
}