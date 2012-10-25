namespace Fanex.Data.Configuration
{
    using System.Configuration.Provider;

    /// <summary>
    /// 
    /// </summary>
    public class DbSettingProviderCollection : ProviderCollection 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        new public DbSettingProviderBase this[string name]
        {
            get { return (DbSettingProviderBase)base[name]; }
        }
    }
}
