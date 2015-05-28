namespace Fanex.BetList.Core.Builder
{
    using System;
    using System.Linq;
    using ChoiceBuilder;
    using OddsBuilder;
    using StakeBuilder;
    using StatusBuilder;
    using TransBuilder;

    /// <summary>
    /// The bet list base builder.
    /// </summary>
    public class BetListBaseBuilder
    {
        /// <summary>
        /// The choice builder namespace.
        /// </summary>
        private const string CHOICEBUILDERNAMESPACE = "Fanex.BetList.Core.Builder.ChoiceBuilder";

        /// <summary>
        /// The odds builder namespace.
        /// </summary>
        private const string ODDSBUILDERNAMESPACE = "Fanex.BetList.Core.Builder.OddsBuilder";

        /// <summary>
        /// The stake builder namespace.
        /// </summary>
        private const string STAKEBUILDERNAMESPACE = "Fanex.BetList.Core.Builder.StakeBuilder";

        /// <summary>
        /// The trans builder namespace.
        /// </summary>
        private const string TRANSBUILDERNAMESPACE = "Fanex.BetList.Core.Builder.TransBuilder";

        /// <summary>
        /// The status builder namespace.
        /// </summary>
        private const string STATUSBUILDERNAMESPACE = "Fanex.BetList.Core.Builder.StatusBuilder";

        public BetListBaseBuilder()
        {
        }

        /// <summary>
        /// Creates the choice builder.
        /// </summary>
        /// <param name="bettype">The bet type.</param>
        /// <returns>IChoice: the Choice builder object.</returns>
        protected IChoice CreateChoiceBuilder(int bettype)
        {
            string choiceName = ".Choice";

            if (bettype < 0)
            {
                // Internal conventional bettype id of 3rd products
                choiceName = "._3rd.Choice";
            }
            else if (bettype <= 1599 && bettype >= 1501)
            {
                // Keno games.
                bettype = 1501;
            }

            string typeName = string.Join(null, new string[] { CHOICEBUILDERNAMESPACE, choiceName, bettype.ToString().Replace('-', '_') });

            Type type = GetBetListAssemblies(typeName);

            if (type == null)
            {
                return new Choice1();
            }
            else
            {
                return Activator.CreateInstance(type) as IChoice;
            }
        }

        /// <summary>
        /// Creates the odds builder.
        /// </summary>
        /// <param name="bettype">The bet type.</param>
        /// <returns>IOdds: the Odds builder object.</returns>
        protected IOdds CreateOddsBuilder(int bettype)
        {
            string oddsName = ".Odds";

            if (bettype < 0)
            {
                // Internal conventional bettype id of 3rd products
                oddsName = "._3rd.Odds";
            }
            else if (bettype <= 1199 && bettype >= 1101)
            {
                // Odds of Live Casino bet types
                bettype = 1101;
            }
            else if (bettype <= 1599 && bettype >= 1501)
            {
                // Odds of Keno games
                bettype = 1501;
            }

            string typeName = string.Join(null, new string[] { ODDSBUILDERNAMESPACE, oddsName, bettype.ToString().Replace('-', '_') });

            Type type = GetBetListAssemblies(typeName);

            if (type == null)
            {
                return new Odds1();
            }
            else
            {
                return Activator.CreateInstance(type) as IOdds;
            }
        }

        /// <summary>
        /// Creates the stake builder.
        /// </summary>
        /// <param name="bettype">The bet type.</param>
        /// <returns>IStake: the Stake builder object.</returns>
        protected IStake CreateStakeBuilder(int bettype)
        {
            string stakeName = ".Stake";

            if (bettype < 0)
            {
                // Internal conventional bettype id of 3rd products
                stakeName = "._3rd.Stake";
            }

            string typeName = string.Join(null, new string[] { STAKEBUILDERNAMESPACE, stakeName, bettype.ToString().Replace('-', '_') });

            Type type = GetBetListAssemblies(typeName);

            if (type == null)
            {
                return new Stake1();
            }
            else
            {
                return Activator.CreateInstance(type) as IStake;
            }
        }

        /// <summary>
        /// Creates the status builder.
        /// </summary>
        /// <param name="bettype">The bet type.</param>
        /// <returns>IStatus: the Status builder object.</returns>
        protected IStatus CreateStatusBuilder(int bettype)
        {
            string statusName = ".Status";

            if (bettype < 0)
            {
                // Internal conventional bettype id of 3rd products
                statusName = "._3rd.Status";
            }
            else if (bettype <= 1599 && bettype >= 1501)
            {
                // Keno games
                bettype = 1501;
            }

            string typeName = string.Join(null, new string[] { STATUSBUILDERNAMESPACE, statusName, bettype.ToString().Replace('-', '_') });

            Type type = GetBetListAssemblies(typeName);

            if (type == null)
            {
                return new Status1();
            }
            else
            {
                return Activator.CreateInstance(type) as IStatus;
            }
        }

        /// <summary>
        /// Creates the trans builder.
        /// </summary>
        /// <param name="bettype">The bet type.</param>
        /// <returns>ITrans: the Trans builder object.</returns>
        protected ITrans CreateTransBuilder(int bettype)
        {
            string transName = ".Trans";

            if (bettype < 0)
            {
                // Internal conventional bettype id of 3rd products
                transName = "._3rd.Trans";
            }
            else if (bettype <= 1599 && bettype >= 1501)
            {
                // Keno games
                bettype = 1501;
            }

            string typeName = string.Join(null, new string[] { TRANSBUILDERNAMESPACE, transName, bettype.ToString().Replace('-', '_') });

            Type type = GetBetListAssemblies(typeName);

            if (type == null)
            {
                return new Trans1();
            }
            else
            {
                return Activator.CreateInstance(type) as ITrans;
            }
        }

        private Type GetBetListAssemblies(string typeName)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            assemblies = assemblies.Where(x => x.FullName.StartsWith("Fanex.BetList.", StringComparison.InvariantCulture)).ToArray();

            foreach (var assembly in assemblies)
            {
                Type type = Type.GetType(typeName + ", " + assembly);

                if (type != null)
                {
                    return type;
                }
            }

            return null;
        }
    }
}