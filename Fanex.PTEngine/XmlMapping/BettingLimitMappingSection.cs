namespace Fanex.PTEngine.XmlMapping
{
    using System.Configuration;

    public class BettingLimitMappingSection : ConfigurationSection
    {
        [ConfigurationProperty("BettingLimitMapping", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(BettingLimitMappingCollection),
            AddItemName = "add")]
        public BettingLimitMappingCollection BetSettingCollection
        {
            get
            {
                var collection =
                    (BettingLimitMappingCollection)base["BettingLimitMapping"];
                return collection;
            }
        }

        public static BettingLimitMappingSection Instance
        {
            get
            {
                var section =
                 ConfigurationManager.GetSection("BettingLimitMappingSection") as BettingLimitMappingSection;
                return section;
            }
        }
    }

    /// <summary>
    /// Betting Limit Mapping
    /// </summary>
    public class BettingLimitMappingCollection : ConfigurationElementCollection
    {
        public BettingLimitMappingCollection()
        {
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        public void Add(BettingLimitElement betSettingElement)
        {
            this.BaseAdd(betSettingElement);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            this.BaseAdd(element, false);
        }

        public BettingLimitElement this[int index]
        {
            get
            {
                return (BettingLimitElement)this.BaseGet(index);
            }

            set
            {
                if (this.BaseGet(index) != null)
                {
                    this.BaseRemoveAt(index);
                }

                this.BaseAdd(index, value);
            }
        }

        new public BettingLimitElement this[string name]
        {
            get
            {
                return (BettingLimitElement)this.BaseGet(name);
            }
        }

        public int IndexOf(BettingLimitElement betSettingElement)
        {
            return this.BaseIndexOf(betSettingElement);
        }

        protected override sealed ConfigurationElement CreateNewElement()
        {
            return new BettingLimitElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((BettingLimitElement)element).MappingTarget + ((BettingLimitElement)element).TargetRoleId + ((BettingLimitElement)element).Pattern + ((BettingLimitElement)element).Type;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class BettingLimitElement : ConfigurationElement
    {
        public BettingLimitElement()
        {
        }

        [ConfigurationProperty("Type", IsRequired = false)]
        public string Type
        {
            get
            {
                return (string)this["Type"];
            }

            set
            {
                this["Type"] = value;
            }
        }

        [ConfigurationProperty("TargetRoleId", IsRequired = false)]
        public int TargetRoleId
        {
            get
            {
                return (int)this["TargetRoleId"];
            }

            set
            {
                this["TargetRoleId"] = value;
            }
        }

        [ConfigurationProperty("MappingTarget", IsRequired = false)]
        public string MappingTarget
        {
            get
            {
                return (string)this["MappingTarget"];
            }

            set
            {
                this["MappingTarget"] = value;
            }
        }

        [ConfigurationProperty("Pattern", IsRequired = false)]
        public string Pattern
        {
            get
            {
                return (string)this["Pattern"];
            }

            set
            {
                this["Pattern"] = value;
            }
        }

        // Create a SportId element.
        [ConfigurationProperty("SportId")]
        public SportIdElement SportId
        {
            get
            {
                return (SportIdElement)this["SportId"];
            }

            set
            {
                this["SportId"] = value;
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class SportIdElement : ConfigurationElement
    {
        public SportIdElement()
        {
        }

        [ConfigurationProperty("Format", IsRequired = false)]
        public string Format
        {
            get
            {
                return (string)this["Format"];
            }

            set
            {
                this["Format"] = value;
            }
        }

        [ConfigurationProperty("OmitWhenDefault", IsRequired = false)]
        public string OmitWhenDefault
        {
            get
            {
                return (string)this["OmitWhenDefault"];
            }

            set
            {
                this["OmitWhenDefault"] = value;
            }
        }
    }
}