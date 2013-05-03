namespace Fanex.PTEngine.BusinessObjects
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Fanex.PTEngine.XmlMapping;

    public class SportType : ISportType<BetTypeGroupSet>, IXMLObject
    {
        public SportType()
        {
            BetTypeGroupSets = new List<BetTypeGroupSet>();
        }

        public SportType(XElement element)
            : this()
        {
            Map(element);
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Key { get; set; }

        public string ResourceKey { get; set; }

        public bool? Collapsed { get; set; }

        public IList<BetTypeGroupSet> BetTypeGroupSets { get; set; }

        public void Map(XElement element)
        {
            foreach (XAttribute attribute in element.Attributes())
            {
                switch (attribute.Name.LocalName)
                {
                    case Constant.SPORT_TYPE_ID:
                        Id = int.Parse(attribute.Value);
                        break;
                    case Constant.SPORT_TYPE_NAME:
                        Name = attribute.Value;
                        break;
                    case Constant.SPORT_TYPE_KEY:
                        Key = attribute.Value;
                        break;
                    case Constant.SPORT_TYPE_RESOURCE_KEY:
                        ResourceKey = attribute.Value;
                        break;
                    case Constant.SPORT_TYPE_COLLAPSED:
                        Collapsed = bool.Parse(attribute.Value);
                        break;
                    default:
                        break;
                }

                if (string.IsNullOrEmpty(Key) && !string.IsNullOrEmpty(Name))
                {
                    Key = Name;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="element"></param>
        public void Populate(XElement element, bool excluldeInvisibleBetTypes)
        {
            foreach (var betTypeGroupSetsElement in element.Elements())
            {
                foreach (var betTypeGroupSetElement in betTypeGroupSetsElement.Elements())
                {
                    // Init new bet type group set or populate to existing bet type group set
                    BetTypeGroupSet betTypeGroupSet = new BetTypeGroupSet(betTypeGroupSetElement);
                    BetTypeGroupSet currentBetTypeGroupSet = (from btgs in this.BetTypeGroupSets
                                                              where btgs.IsLive == betTypeGroupSet.IsLive
                                                              select btgs).FirstOrDefault();

                    if (currentBetTypeGroupSet == null)
                    {
                        currentBetTypeGroupSet = betTypeGroupSet;
                        this.BetTypeGroupSets.Add(betTypeGroupSet);
                    }

                    currentBetTypeGroupSet.Populate(betTypeGroupSetElement, excluldeInvisibleBetTypes);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="reader"></param>
        /// <param name="mappingSection"></param>
        public void ReadFromDataReader(IRequestContext context, System.Data.IDataReader reader, PTMappingSection mappingSection)
        {
            foreach (BetTypeGroupSet betTypeGroupSet in BetTypeGroupSets)
            {
                betTypeGroupSet.ReadFromDataReader(context, reader, mappingSection, this.Id);
            }
        }

        public void MapToSpParameters(Hashtable parameters, IRequestContext context, PTMappingSection mappingSection, IList<SportType> sportTypes)
        {
            foreach (BetTypeGroupSet betTypeGroupSet in this.BetTypeGroupSets)
            {
                betTypeGroupSet.MapToSpParameters(parameters, context, mappingSection, this.Id, sportTypes);
            }
        }
    }
}