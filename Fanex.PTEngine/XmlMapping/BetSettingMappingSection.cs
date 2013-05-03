namespace Fanex.PTEngine.XmlMapping
{
    using System.Configuration;

    public class BetSettingMappingSection : ConfigurationSection
    {
        [ConfigurationProperty("BetSettingMapping", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(BetSettingMappingCollection),
            AddItemName = "add")]
        public BetSettingMappingCollection BetSettingCollection
        {
            get
            {
                var collection =
                    (BetSettingMappingCollection)base["BetSettingMapping"];
                return collection;
            }
        }

        public static BetSettingMappingSection Instance
        {
            get
            {
                var section =
                 ConfigurationManager.GetSection("BetSettingMappingSection") as BetSettingMappingSection;
                return section;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class BetSettingMappingCollection : ConfigurationElementCollection
    {
        public BetSettingMappingCollection()
        {
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        public void Add(BetSettingElement betSettingElement)
        {
            BaseAdd(betSettingElement);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        public BetSettingElement this[int index]
        {
            get
            {
                return (BetSettingElement)BaseGet(index);
            }

            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }

                BaseAdd(index, value);
            }
        }

        new public BetSettingElement this[string name]
        {
            get
            {
                return (BetSettingElement)BaseGet(name);
            }
        }

        public int IndexOf(BetSettingElement betSettingElement)
        {
            return BaseIndexOf(betSettingElement);
        }

        protected override sealed ConfigurationElement CreateNewElement()
        {
            return new BetSettingElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((BetSettingElement)element).MappingTarget + ((BetSettingElement)element).TargetRoleId + ((BetSettingElement)element).Pattern + ((BetSettingElement)element).Type;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class BetSettingElement : ConfigurationElement
    {
        public BetSettingElement()
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
        public string TargetRoleId
        {
            get
            {
                return (string)this["TargetRoleId"];
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

        // Create a SportTypeId" element.
        [ConfigurationProperty("SportTypeBetSetting")]
        public SportTypeBetSettingElement SportTypeBetSetting
        {
            get
            {
                return (SportTypeBetSettingElement)this["SportTypeBetSetting"];
            }

            set
            {
                this["SportTypeBetSetting"] = value;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SportTypeBetSettingElement : ConfigurationElement
    {
        public SportTypeBetSettingElement()
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