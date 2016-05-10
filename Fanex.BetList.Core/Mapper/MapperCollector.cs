namespace Fanex.BetList.Core.Mapper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Caching;

    public class MapperCollector
    {
        #region Constants
        private const string CACHECHOICEKEY = "Fanex.BetList.Core.Mapper.Choice";
        private const string CACHESTATUSKEY = "Fanex.BetList.Core.Mapper.Status";
        private const string CACHESTAKEKEY = "Fanex.BetList.Core.Mapper.Stake";
        private const string CACHEODDSKEY = "Fanex.BetList.Core.Mapper.Odds";
        private const string CACHETRANSKEY = "Fanex.BetList.Core.Mapper.Trans";

        private const string CHOICECOMPONENT = "Choice";
        private const string STATUSCOMPONENT = "Status";
        private const string STAKECOMPONENT = "Stake";
        private const string ODDSCOMPONENT = "Odds";
        private const string TRANSCOMPONENT = "Trans";
        #endregion

        #region Global variables

        private static ObjectCache _cache = MemoryCache.Default;

        #endregion

        #region Temporary global varibles

        private static IDictionary<int, int> _choiceMapperData = null;
        private static IDictionary<int, int> _statusMapperData = null;
        private static IDictionary<int, int> _stakeMapperData = null;
        private static IDictionary<int, int> _oddsMapperData = null;
        private static IDictionary<int, int> _transMapperData = null;

        #endregion

        #region Public method

        public static int GetChoiceBaseBetTypeOf(int betType)
        {
            var choiceMapper = (IDictionary<int, int>)_cache[CACHECHOICEKEY];

            return (choiceMapper != null && choiceMapper.ContainsKey(betType)) ? choiceMapper[betType] : betType;
        }

        public static int GetStatusBaseBetTypeOf(int betType)
        {
            var statusMapper = (IDictionary<int, int>)_cache[CACHESTATUSKEY];

            return (statusMapper != null && statusMapper.ContainsKey(betType)) ? statusMapper[betType] : betType;
        }

        public static int GetStakeBaseBetTypeOf(int betType)
        {
            var stakeMapper = (IDictionary<int, int>)_cache[CACHESTAKEKEY];

            return (stakeMapper != null && stakeMapper.ContainsKey(betType)) ? stakeMapper[betType] : betType;
        }

        public static int GetOddsBaseBetTypeOf(int betType)
        {
            var oddsMapper = (IDictionary<int, int>)_cache[CACHEODDSKEY];

            return (oddsMapper != null && oddsMapper.ContainsKey(betType)) ? oddsMapper[betType] : betType;
        }

        public static int GetTransBaseBetTypeOf(int betType)
        {
            var transMapper = (IDictionary<int, int>)_cache[CACHETRANSKEY];

            return (transMapper != null && transMapper.ContainsKey(betType)) ? transMapper[betType] : betType;
        }

        public static void CollectAllMapperData(IList<Assembly> assemblies)
        {
            if (!CacheHasMapperData())
            {
                InitializeMapperData();

                CollectMapperDataOf(CHOICECOMPONENT, assemblies);
                CollectMapperDataOf(STATUSCOMPONENT, assemblies);
                CollectMapperDataOf(STAKECOMPONENT, assemblies);
                CollectMapperDataOf(ODDSCOMPONENT, assemblies);
                CollectMapperDataOf(TRANSCOMPONENT, assemblies);

                AddMapperDataToCache();
            }
        }

        #endregion

        #region Private Method

        private static bool CacheHasMapperData()
        {
            return (IDictionary<int, int>)_cache[CACHECHOICEKEY] != null
                || (IDictionary<int, int>)_cache[CACHESTATUSKEY] != null
                || (IDictionary<int, int>)_cache[CACHESTAKEKEY] != null
                || (IDictionary<int, int>)_cache[CACHEODDSKEY] != null
                || (IDictionary<int, int>)_cache[CACHETRANSKEY] != null;
        }

        private static void InitializeMapperData()
        {
            _choiceMapperData = new Dictionary<int, int>();
            _statusMapperData = new Dictionary<int, int>();
            _stakeMapperData = new Dictionary<int, int>();
            _oddsMapperData = new Dictionary<int, int>();
            _transMapperData = new Dictionary<int, int>();
        }

        private static void AddMapperDataToCache()
        {
            ObjectCache cache = MemoryCache.Default;

            CacheItemPolicy policy = new CacheItemPolicy();
            policy.Priority = CacheItemPriority.NotRemovable;

            cache.Set(CACHECHOICEKEY, _choiceMapperData, policy);
            cache.Set(CACHESTATUSKEY, _statusMapperData, policy);
            cache.Set(CACHESTAKEKEY, _stakeMapperData, policy);
            cache.Set(CACHEODDSKEY, _oddsMapperData, policy);
            cache.Set(CACHETRANSKEY, _transMapperData, policy);
        }

        private static void CollectMapperDataOf(string componentName, IList<Assembly> assemblies)
        {
            string typeName = string.Join(null, new string[] { "Fanex.BetList.Core.Mapper.", componentName, "Mapper" });

            foreach (var assembly in assemblies)
            {
                Type type = Type.GetType(typeName + ", " + assembly);

                if (type != null)
                {
                    IMapper mapper = Activator.CreateInstance(type) as IMapper;

                    AddDataFromMapperObj(mapper, componentName);
                }
            }
        }

        private static void AddDataFromMapperObj(IMapper mapper, string componentName)
        {
            switch (componentName)
            {
                case CHOICECOMPONENT:
                    _choiceMapperData = AppendDataToSource(_choiceMapperData, mapper.MapperData);
                    break;

                case STATUSCOMPONENT:
                    _statusMapperData = AppendDataToSource(_statusMapperData, mapper.MapperData);
                    break;

                case STAKECOMPONENT:
                    _stakeMapperData = AppendDataToSource(_stakeMapperData, mapper.MapperData);
                    break;

                case ODDSCOMPONENT:
                    _oddsMapperData = AppendDataToSource(_oddsMapperData, mapper.MapperData);
                    break;

                case TRANSCOMPONENT:
                    _transMapperData = AppendDataToSource(_transMapperData, mapper.MapperData);
                    break;
            }
        }

        private static IDictionary<int, int> AppendDataToSource(IDictionary<int, int> source, IDictionary<int, int> data)
        {
            return source.Concat(data.Where(item => !source.Keys.Contains(item.Key))).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        #endregion
    }
}