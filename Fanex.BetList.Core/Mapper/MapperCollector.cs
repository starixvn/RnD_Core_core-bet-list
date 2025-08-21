namespace Fanex.BetList.Core.Mapper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Caching;

    public static class MapperCollector
    {
        #region Constants

        private const string BetListMapperNamespace = "Fanex.BetList.Core.Mapper.{0}Mapper";

        private const string CacheKey = "Fanex.BetList.Core.Mapper.{0}";
        private const string Choice = "Choice";
        private const string Status = "Status";
        private const string Stake = "Stake";
        private const string Odds = "Odds";
        private const string Trans = "Trans";
        private const string Representative = "Representative";

        #endregion Constants

        #region Global variables

        private static ObjectCache _cache = MemoryCache.Default;
        private static readonly object _locker = new object();

        #endregion Global variables

        #region Public method

        public static void CollectAllMapperData(IList<Assembly> assemblies)
        {
            lock (_locker)
            {
                if (!CacheHasMapperData())
                {
                    CollectAndCacheMapperData(assemblies);
                }
            }
        }

        public static int GetChoiceBaseBetTypeOf(int betTypeId)
        {
            var choiceMapper = (IDictionary<int, int>)_cache[string.Format(CacheKey, Choice)];

            return (choiceMapper != null && choiceMapper.ContainsKey(betTypeId)) ? choiceMapper[betTypeId] : betTypeId;
        }

        public static int GetStatusBaseBetTypeOf(int betTypeId)
        {
            var statusMapper = (IDictionary<int, int>)_cache[string.Format(CacheKey, Status)];

            return (statusMapper != null && statusMapper.ContainsKey(betTypeId)) ? statusMapper[betTypeId] : betTypeId;
        }

        public static int GetStakeBaseBetTypeOf(int betTypeId)
        {
            var stakeMapper = (IDictionary<int, int>)_cache[string.Format(CacheKey, Stake)];

            return (stakeMapper != null && stakeMapper.ContainsKey(betTypeId)) ? stakeMapper[betTypeId] : betTypeId;
        }

        public static int GetOddsBaseBetTypeOf(int betTypeId)
        {
            var oddsMapper = (IDictionary<int, int>)_cache[string.Format(CacheKey, Odds)];

            return (oddsMapper != null && oddsMapper.ContainsKey(betTypeId)) ? oddsMapper[betTypeId] : betTypeId;
        }

        public static int GetTransBaseBetTypeOf(int betTypeId)
        {
            var transMapper = (IDictionary<int, int>)_cache[string.Format(CacheKey, Trans)];

            return (transMapper != null && transMapper.ContainsKey(betTypeId)) ? transMapper[betTypeId] : betTypeId;
        }

        public static int GetRepresentativeBetTypeOf(int betTypeId)
        {
            var representativeMapper = (IDictionary<int, int>)_cache[string.Format(CacheKey, Representative)];

            return (representativeMapper != null && representativeMapper.ContainsKey(betTypeId)) ? representativeMapper[betTypeId] : default(int);
        }

        #endregion Public method

        #region Private Method

        private static bool CacheHasMapperData()
        {
            return (IDictionary<int, int>)_cache[string.Format(CacheKey, Choice)] != null
                || (IDictionary<int, int>)_cache[string.Format(CacheKey, Status)] != null
                || (IDictionary<int, int>)_cache[string.Format(CacheKey, Stake)] != null
                || (IDictionary<int, int>)_cache[string.Format(CacheKey, Odds)] != null
                || (IDictionary<int, int>)_cache[string.Format(CacheKey, Trans)] != null
                || (IDictionary<int, int>)_cache[string.Format(CacheKey, Representative)] != null;
        }

        private static void CollectAndCacheMapperData(IList<Assembly> assemblies)
        {
            var choiceMapperData = CollectMapperDataOf(Choice, assemblies);
            var statusMapperData = CollectMapperDataOf(Status, assemblies);
            var stakeMapperData = CollectMapperDataOf(Stake, assemblies);
            var oddsMapperData = CollectMapperDataOf(Odds, assemblies);
            var transMapperData = CollectMapperDataOf(Trans, assemblies);
            var representativeMapperData = CollectMapperDataOf(Representative, assemblies);

            var policy = new CacheItemPolicy
            {
                Priority = CacheItemPriority.NotRemovable
            };

            _cache.Set(string.Format(CacheKey, Choice), choiceMapperData, policy);
            _cache.Set(string.Format(CacheKey, Status), statusMapperData, policy);
            _cache.Set(string.Format(CacheKey, Stake), stakeMapperData, policy);
            _cache.Set(string.Format(CacheKey, Odds), oddsMapperData, policy);
            _cache.Set(string.Format(CacheKey, Trans), transMapperData, policy);
            _cache.Set(string.Format(CacheKey, Representative), representativeMapperData, policy);
        }

        private static IDictionary<int, int> CollectMapperDataOf(string componentName, IList<Assembly> assemblies)
        {
            string typeName = string.Join(null, string.Format(BetListMapperNamespace, componentName));

            IDictionary<int, int> mapperData = new Dictionary<int, int>();

            foreach (var assembly in assemblies)
            {
                Type type = Type.GetType(typeName + ", " + assembly);

                if (type != null)
                {
                    IMapper mapper = Activator.CreateInstance(type) as IMapper;

                    mapperData = AddDataFromMapperObj(mapper, mapperData);
                }
            }

            return mapperData;
        }

        private static IDictionary<int, int> AddDataFromMapperObj(IMapper mapper, IDictionary<int, int> mapperData)
        {
            return mapperData
                .Concat(mapper.MapperData.Where(item => !mapperData.Keys.Contains(item.Key)))
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        #endregion Private Method
    }
}