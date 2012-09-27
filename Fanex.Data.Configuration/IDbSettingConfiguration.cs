using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fanex.Data.Configuration
{
    public interface IDbSettingConfiguration
    {
        IDbSetting GetSetting(string key);
    }
}
