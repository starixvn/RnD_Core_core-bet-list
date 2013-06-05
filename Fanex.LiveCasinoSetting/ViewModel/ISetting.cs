using System;
namespace Fanex.LiveCasinoSetting.ViewModel
{
    /// <summary>
    /// prepresent basic customer information
    /// </summary>
    public interface ISetting
    {
        CustomerInfo CustomerInfo { get; set; }
        double MinPT { get; set; }
        double MaxPT { get; set; }
        double Comm { get; set; }
        bool IsValid { get; }

    }
}
