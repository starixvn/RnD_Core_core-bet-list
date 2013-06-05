using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fanex.LiveCasinoSetting.Model;

namespace Fanex.LiveCasinoSetting.ViewModel
{
    public class MasterSettingView 
    {
        public CustomerInfo CustomerInfo { get; set; }
        public IPTSetting IBCSetting { get; set; }
        public double UplinePresetPT { get; set; }
        public double UplineAutoPresetPT { get; set; }
       
    }
}
