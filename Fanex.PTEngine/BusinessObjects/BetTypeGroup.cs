namespace Fanex.PTEngine.BusinessObjects
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Fanex.PTEngine.XmlMapping;

    public class BetTypeGroup : IBetTypeGroup<BetType>, IXMLObject
    {
        public BetTypeGroup()
        {
            BetTypes = new List<BetType>();
        }

        public BetTypeGroup(XElement element)
            : this()
        {
            Map(element);
        }

        public int? Id { get; set; }

        public IList<BetType> BetTypes { get; set; }

        public void Map(XElement element)
        {
            foreach (XAttribute attribute in element.Attributes())
            {
                switch (attribute.Name.LocalName)
                {
                    case Constant.BET_TYPE_GROUP_ID:
                        Id = int.Parse(attribute.Value);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="element"></param>
        /// <param name="excluldeInvisibleBetTypes"></param>
        public void Populate(XElement element, bool excluldeInvisibleBetTypes)
        {
            foreach (var betTypesElement in element.Elements())
            {
                foreach (var betTypeElement in betTypesElement.Elements())
                {
                    // Init new bet type or populate to existing bet type
                    BetType betType = new BetType(betTypeElement);
                    BetType currentBetType = (from bt in this.BetTypes
                                              where bt.Id == betType.Id
                                              select bt).FirstOrDefault();
                    if (currentBetType == null)
                    {
                        // Excluding invisible bet type
                        if (betType.Visible == false && excluldeInvisibleBetTypes)
                        {
                            continue;
                        }

                        this.BetTypes.Add(betType);
                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="reader"></param>
        /// <param name="mappingSection"></param>
        /// <param name="sportTypeId"></param>
        /// <param name="isLive"></param>
        public void ReadFromDataReader(IRequestContext context, System.Data.IDataReader reader, PTMappingSection mappingSection, int sportTypeId, bool isLive)
        {
            foreach (BetType betType in this.BetTypes)
            {
                // Read data for bettype
                betType.ReadFromDataReader(context, reader, mappingSection, sportTypeId, isLive);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="context"></param>
        /// <param name="mappingSection"></param>
        /// <param name="sportTypeId"></param>
        /// <param name="isLive"></param>
        /// <param name="sportTypes"></param>
        public void MapToSpParameters(Hashtable parameters, IRequestContext context, PTMappingSection mappingSection, int sportTypeId, bool isLive, IList<SportType> sportTypes)
        {
            /// Map for this bettype
            foreach (BetType betType in this.BetTypes)
            {
                // Handle copy bettype setting for invisible bet types
                if ((betType.CopyFromSportType != null) &&
                    (betType.CopyFromBetType != null) &&
                    (betType.Visible == false))
                {
                    // Find correct bet type need to copy pt
                    betType.Pt = (from st in sportTypes
                                  where betType.CopyFromSportType == st.Id // Sport types

                                  from btgs in st.BetTypeGroupSets
                                  where btgs.IsLive == isLive // Deadball copied from deadball, Live copied from Live

                                  from btg in btgs.BetTypeGroups

                                  from bt in btg.BetTypes // Bet types
                                  where bt.Id == betType.CopyFromBetType

                                  select bt).FirstOrDefault().Pt;
                }

                // Mapping for bettype
                betType.MapToSpParameters(parameters, context, mappingSection, sportTypeId, isLive);
            }
        }
    }
}