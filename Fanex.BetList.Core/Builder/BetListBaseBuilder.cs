namespace Fanex.BetList.Core.Builder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Caching;
    using System.Web.Compilation;
    using ChoiceBuilder;
    using Mapper;
    using OddsBuilder;
    using StakeBuilder;
    using StatusBuilder;
    using TransBuilder;

    /// <summary>
    /// The bet list base builder.
    /// </summary>
    public class BetListBaseBuilder : IBetListBaseBuilder
    {
        private const string ChoiceBuilderNamespace = "Fanex.BetList.Core.Builder.ChoiceBuilder";
        private const string OddsBuilderNamespace = "Fanex.BetList.Core.Builder.OddsBuilder";
        private const string StakeBuilderNamespace = "Fanex.BetList.Core.Builder.StakeBuilder";
        private const string TransBuilderNamespace = "Fanex.BetList.Core.Builder.TransBuilder";
        private const string StatusBuilderNamespace = "Fanex.BetList.Core.Builder.StatusBuilder";

        private const string CacheKey = "Fanex.BetList.Core.CacheKey";

        private const string ChoiceName = ".Choice";
        private const string Choice3rdName = "._3rd.Choice";
        private const string OddsName = ".Odds";
        private const string Odds3rdName = "._3rd.Odds";
        private const string StatusName = ".Status";
        private const string Status3rdName = "._3rd.Status";
        private const string StakeName = ".Stake";
        private const string Stake3rdName = "._3rd.Stake";
        private const string TransName = ".Trans";
        private const string Trans3rdName = "._3rd.Trans";

        private readonly ObjectCache _cache;
        private readonly CacheItemPolicy _cachePolicy;

        public BetListBaseBuilder()
        {
            _cache = MemoryCache.Default;
            _cachePolicy = new CacheItemPolicy();
            _cachePolicy.Priority = CacheItemPriority.NotRemovable;

            var assemblies = GetBetListAssemblies();
            MapperCollector.CollectAllMapperData(assemblies);
        }

        /// <summary>
        /// Creates the choice builder.
        /// </summary>
        /// <param name="bettype">The bet type.</param>
        /// <returns>IChoice: the Choice builder object.</returns>
        public IChoice CreateChoiceBuilder(int bettype)
        {
            var choiceName = ChoiceName;
            var betTypeId = bettype;

            if (betTypeId < 0)
            {
                // Internal conventional bettype id of 3rd products
                choiceName = Choice3rdName;
            }

            if (betTypeId <= 1599 && betTypeId >= 1501)
            {
                // Keno games.
                betTypeId = 1501;
            }

            betTypeId = MapperCollector.GetChoiceBaseBetTypeOf(betTypeId);

            string typeName = string.Join(null, new[] { ChoiceBuilderNamespace, choiceName, betTypeId.ToString().Replace('-', '_') });

            return CreateInstance<IChoice, Choice1>(typeName);
        }

        /// <summary>
        /// Creates the odds builder.
        /// </summary>
        /// <param name="bettype">The bet type.</param>
        /// <returns>IOdds: the Odds builder object.</returns>
        public IOdds CreateOddsBuilder(int bettype)
        {
            var oddsName = OddsName;
            var betTypeId = bettype;

            if (betTypeId < 0)
            {
                // Internal conventional bettype id of 3rd products
                oddsName = Odds3rdName;
            }

            if (betTypeId <= 1199 && betTypeId >= 1101)
            {
                // Odds of Live Casino bet types
                betTypeId = 1101;
            }

            if (betTypeId <= 1599 && betTypeId >= 1501)
            {
                // Odds of Keno games
                betTypeId = 1501;
            }

            betTypeId = MapperCollector.GetOddsBaseBetTypeOf(betTypeId);

            string typeName = string.Join(null, new[] { OddsBuilderNamespace, oddsName, betTypeId.ToString().Replace('-', '_') });

            return CreateInstance<IOdds, Odds1>(typeName);
        }

        /// <summary>
        /// Creates the stake builder.
        /// </summary>
        /// <param name="bettype">The bet type.</param>
        /// <returns>IStake: the Stake builder object.</returns>
        public IStake CreateStakeBuilder(int bettype)
        {
            var stakeName = StakeName;
            var betTypeId = bettype;

            if (bettype < 0)
            {
                // Internal conventional bettype id of 3rd products
                stakeName = Stake3rdName;
            }

            betTypeId = MapperCollector.GetStakeBaseBetTypeOf(betTypeId);

            string typeName = string.Join(null, new[] { StakeBuilderNamespace, stakeName, betTypeId.ToString().Replace('-', '_') });

            return CreateInstance<IStake, Stake1>(typeName);
        }

        /// <summary>
        /// Creates the status builder.
        /// </summary>
        /// <param name="bettype">The bet type.</param>
        /// <returns>IStatus: the Status builder object.</returns>
        public IStatus CreateStatusBuilder(int bettype)
        {
            var statusName = StatusName;
            var betTypeId = bettype;

            if (betTypeId < 0)
            {
                // Internal conventional bettype id of 3rd products
                statusName = Status3rdName;
            }

            if (betTypeId <= 1599 && betTypeId >= 1501)
            {
                // Keno games
                betTypeId = 1501;
            }

            betTypeId = MapperCollector.GetStatusBaseBetTypeOf(betTypeId);

            string typeName = string.Join(null, new[] { StatusBuilderNamespace, statusName, betTypeId.ToString().Replace('-', '_') });

            return CreateInstance<IStatus, Status1>(typeName);
        }

        /// <summary>
        /// Creates the trans builder.
        /// </summary>
        /// <param name="bettype">The bet type.</param>
        /// <returns>ITrans: the Trans builder object.</returns>
        public ITrans CreateTransBuilder(int bettype)
        {
            var transName = TransName;
            var betTypeId = bettype;

            if (betTypeId < 0)
            {
                // Internal conventional bettype id of 3rd products
                transName = Trans3rdName;
            }

            if (betTypeId <= 1599 && betTypeId >= 1501)
            {
                // Keno games
                betTypeId = 1501;
            }

            betTypeId = MapperCollector.GetTransBaseBetTypeOf(betTypeId);

            string typeName = string.Join(null, new[] { TransBuilderNamespace, transName, betTypeId.ToString().Replace('-', '_') });

            return CreateInstance<ITrans, Trans1>(typeName);
        }

        private T CreateInstance<T, TDefault>(string typeName) where TDefault : T, new()
        {
            T instance;
            Type type = GetBetListAssemblies(typeName);

            if (type == null)
            {
                instance = new TDefault();
            }
            else
            {
                instance = (T)Activator.CreateInstance(type);
            }

            _cache.Set(typeName, instance.GetType(), _cachePolicy);

            return instance;
        }

        private Type GetBetListAssemblies(string typeName)
        {
            var assemblies = GetBetListAssemblies();

            return GetType(typeName, assemblies);
        }

        private List<Assembly> GetBetListAssemblies()
        {
            var assemblies = (List<Assembly>)_cache[CacheKey];

            if (assemblies == null || !assemblies.Any())
            {
                try
                {
                    assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToList();
                }
                catch
                {
                    assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
                }

                assemblies = assemblies.Where(x => x.FullName.StartsWith("Fanex.BetList.", StringComparison.InvariantCulture)).ToList();

                _cache.Set(CacheKey, assemblies, _cachePolicy);
            }

            return assemblies;
        }

        private Type GetType(string typeName, List<Assembly> assemblies)
        {
            var type = (Type)_cache[typeName];

            if (type == null)
            {
                foreach (var assembly in assemblies)
                {
                    type = Type.GetType(typeName + ", " + assembly);

                    if (type != null)
                    {
                        return type;
                    }
                }
            }

            return type;
        }
    }
}