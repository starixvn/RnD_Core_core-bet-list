using System;
namespace Fanex.Data.Configuration
{
    public interface IDbSetting
    {
        string ConnectionString { get; }
        string SpName { get; set; }
    }
}
