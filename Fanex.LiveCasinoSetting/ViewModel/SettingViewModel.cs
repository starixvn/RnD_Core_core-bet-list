using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fanex.LiveCasinoSetting.Model;

namespace Fanex.LiveCasinoSetting.ViewModel
{
    public class SettingViewModel
    {
        private const decimal PT_GAP = 0.01m;
        private const decimal COMM_GAP = 0.0005m;

        public SettingViewModel(CustomerInfo customerInfo)
        {
            if (customerInfo == null)
                throw new ArgumentNullException("customerInfo");

            _customerInfo = customerInfo;
        }

        private CustomerInfo _customerInfo;
        public CustomerInfo CustomerInfo
        {
            get
            {
                return _customerInfo;
            }
        }
        public virtual IPTSetting PTSetting { get; set; }
        public List<GameType> GameSetting { get; set; }

        public List<decimal> MaxPT_Items
        {
            get 
            {
                var from = ValidatorSetting.MinPT_Default;
                var to = PTSetting.Upline.MaxPT 
                    - ((PTSetting is SuperPTSetting) ? 0 : (PTSetting as PTSetting).PresetPT);
                var items = CreateListItem(from, to, PT_GAP);

                return items;
            }
        }

        public List<decimal> MinPT_Items
        {
            get 
            {
                var items = CreateListItem(ValidatorSetting.MinPT_Default
                    , PTSetting.Upline.MaxPT
                    , PT_GAP);

                return items;
            }
        }

        public List<decimal> PresetPT_Items 
        {
            get
            {
                var items = CreateListItem(PTSetting.Upline.MinPT, PTSetting.Upline.MaxPT, PT_GAP);
                return items;        
            }
        }

        public List<decimal> AutoPT_Items
        {
            get
            {
                var items = CreateListItem(ValidatorSetting.MinPT_Default, PTSetting.Upline.MaxPT, PT_GAP);
                return items;
            }
        }
               
        private List<decimal> CreateListItem(decimal from, decimal to, decimal gap)
        {
            List<decimal> items = new List<decimal>();            
            for (decimal f = from; f <= to; )
            {                
                items.Add(f);
                f = f + gap;
            }

            return items;
        }
    }
}
