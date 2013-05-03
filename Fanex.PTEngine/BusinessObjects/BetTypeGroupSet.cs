namespace Fanex.PTEngine.BusinessObjects
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Fanex.PTEngine.XmlMapping;

    public class BetTypeGroupSet : IBetTypeGroupSet<BetTypeGroup>, IXMLObject
    {
        public BetTypeGroupSet()
        {
            BetTypeGroups = new List<BetTypeGroup>();
        }

        public BetTypeGroupSet(XElement element)
            : this()
        {
            Map(element);
        }

        public bool IsLive { get; set; }

        public bool? ShowGroupSetLabel { get; set; }

        public IList<BetTypeGroup> BetTypeGroups { get; set; }

        public void Map(XElement element)
        {
            foreach (XAttribute attribute in element.Attributes())
            {
                switch (attribute.Name.LocalName)
                {
                    case Constant.BETTYPE_GROUPSET_IS_LIVE:
                        IsLive = bool.Parse(attribute.Value);
                        break;
                    case Constant.BETTYPE_GROUPSET_SHOW_LABLE:
                        ShowGroupSetLabel = bool.Parse(attribute.Value);
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
        public void Populate(XElement element, bool excluldeInvisibleBetTypes)
        {
            foreach (var betTypeGroupsElement in element.Elements())
            {
                foreach (var betTypeGroupElement in betTypeGroupsElement.Elements())
                {
                    // Init new bet type group or populate to existing bet type group
                    BetTypeGroup betTypeGroup = new BetTypeGroup(betTypeGroupElement);
                    BetTypeGroup currentBetTypeGroup = (from btg in this.BetTypeGroups
                                                        where btg.Id == betTypeGroup.Id
                                                        select btg).FirstOrDefault();
                    if (currentBetTypeGroup == null)
                    {
                        currentBetTypeGroup = betTypeGroup;
                        this.BetTypeGroups.Add(betTypeGroup);
                    }

                    currentBetTypeGroup.Populate(betTypeGroupElement, excluldeInvisibleBetTypes);
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
        public void ReadFromDataReader(IRequestContext context, System.Data.IDataReader reader, PTMappingSection mappingSection, int sportTypeId)
        {
            foreach (BetTypeGroup betTypeGroup in BetTypeGroups)
            {
                betTypeGroup.ReadFromDataReader(context, reader, mappingSection, sportTypeId, this.IsLive);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="context"></param>
        /// <param name="mappingSection"></param>
        /// <param name="sportTypeId"></param>
        public void MapToSpParameters(Hashtable parameters, IRequestContext context, PTMappingSection mappingSection, int sportTypeId, IList<SportType> sportTypes)
        {
            foreach (BetTypeGroup betTypeGroup in this.BetTypeGroups)
            {
                betTypeGroup.MapToSpParameters(parameters, context, mappingSection, sportTypeId, this.IsLive, sportTypes);
            }
        }
    }
}