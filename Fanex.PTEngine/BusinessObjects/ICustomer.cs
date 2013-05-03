namespace Fanex.PTEngine.BusinessObjects
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Update line object
    /// </summary>
    public interface ICustomer
    {
        /// <summary>
        /// Target customer id
        /// </summary>
        int CustId { get; set; }

        string UserName { get; set; }

        string NewUserName { get; set; }

        string Password { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        int Creator { get; set; }

        string Phone { get; set; }

        string MobilePhone { get; set; }

        string Email { get; set; }

        string Fax { get; set; }

        string Address { get; set; }

        string City { get; set; }

        string State { get; set; }

        string Country { get; set; }

        string Postal { get; set; }

        int CurrencyId { get; set; }

        string Group { get; set; }

        decimal Credit { get; set; }

        decimal MinCredit { get; set; }

        decimal MaxCredit { get; set; }

        decimal MemberMaxCredit { get; set; }

        decimal MemberMinCredit { get; set; }

        string SiteName { get; set; }

        bool IsInternal { get; set; }

        int SRecommend { get; set; }

        int MRecommend { get; set; }

        int ARecommend { get; set; }

        string Language { get; set; }

        string TransferOption { get; set; }

        IBettingLimit<BettingLimitItem> BettingLimit { get; set; }

        IStatusSetting StatusSetting { get; set; }

        ICommissionSetting CommissionSetting { get; set; }

        /// <summary>
        /// For viewing cross level
        /// </summary>
        ICommissionSetting CommissionSetting2 { get; set; }

        IPTGrid<SportType> PtSetting { get; set; }

        Hashtable MapToSpParameters(IRequestContext context);

        void MapToSpUpdateMultipleParameters(ref Hashtable parameters, Hashtable updateParameters, bool isUpadateBettingLimit);

        void ReadFromDataRecord(IRequestContext context, System.Data.IDataReader reader);

        void InitDefaultValue();
    }
}