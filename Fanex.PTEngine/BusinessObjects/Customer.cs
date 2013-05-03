namespace Fanex.PTEngine.BusinessObjects
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web;
    using Fanex.Logging;
    using Fanex.PTEngine.Services;

    public class Customer : ICustomer
    {
        public int CustId { get; set; }

        public string UserName { get; set; }

        public string NewUserName { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Creator { get; set; }

        public string Phone { get; set; }

        public string MobilePhone { get; set; }

        public string Email { get; set; }

        public string Fax { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string Postal { get; set; }

        public string TransferOption { get; set; }

        public decimal Credit { get; set; }

        public decimal MinCredit { get; set; }

        public decimal MaxCredit { get; set; }

        public decimal MemberMaxCredit { get; set; }

        public decimal MemberMinCredit { get; set; }

        public string SiteName { get; set; }

        public int CurrencyId { get; set; }

        public string Group { get; set; }

        public bool IsInternal { get; set; }

        public int SRecommend { get; set; }

        public int MRecommend { get; set; }

        public int ARecommend { get; set; }

        public string Language { get; set; }

        public IBettingLimit<BettingLimitItem> BettingLimit { get; set; }

        public IStatusSetting StatusSetting { get; set; }

        public ICommissionSetting CommissionSetting { get; set; }

        /// <summary>
        /// For viewing cross level
        /// </summary>
        public ICommissionSetting CommissionSetting2 { get; set; }

        public IPTGrid<SportType> PtSetting { get; set; }

        public Customer()
        {
            Password = string.Empty;
            BettingLimit = new BettingLimit();
            StatusSetting = new StatusSetting();
            CommissionSetting = new CommissionSetting();
            CommissionSetting2 = new CommissionSetting();
            PtSetting = new PTGrid();
        }

        public Hashtable MapToSpParameters(IRequestContext context)
        {
            var parameters = new Hashtable();
            try
            {
                // Map Customer Info to Sp Parameters
                MapToSpParametersCustomerInfo(parameters, context);

                // Map BettingLimit to Sp Parameters
                BettingLimit.MapToSpParameters(context, parameters);

                // Map StatusSetting to Sp Parameters
                StatusSetting.MapToSpParameters(parameters);

                // Map Commission to Sp Parameters
                CommissionSetting.MapToSpParameters(parameters);

                PtSetting.MapToSpParameters(parameters, context);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                throw new System.ArgumentException("Mapping configuration format is invalid.", ex);
            }

            return parameters;
        }

        public void MapToSpUpdateMultipleParameters(ref Hashtable parameters, Hashtable updateParameters, bool isUpdateBettingLimit)
        {
            try
            {
                // Map BettingLimit to Sp Parameters
                if (isUpdateBettingLimit)
                {
                    BettingLimit.MapToSpUpdateMultipleParameters(ref parameters, updateParameters);
                }

                // Map PT to Sp Parameters
                PtSetting.MapToSpUpdateMultipleParameters(ref parameters, updateParameters, isUpdateBettingLimit);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                throw new System.ArgumentException("Mapping configuration format is invalid.", ex);
            }

        }
        private void MapToSpParametersCustomerInfo(Hashtable parameters, IRequestContext context)
        {
            parameters.Add("userid", context.CustId);
            parameters.Add("custid", CustId);
            parameters.Add("username", UserName == null ? string.Empty : UserName);
            parameters.Add("userpwd", Password);
            parameters.Add("address", Address == null ? string.Empty : HttpUtility.HtmlDecode(Address));
            parameters.Add("postal", Postal == null ? string.Empty : HttpUtility.HtmlDecode(Postal));
            parameters.Add("city", City == null ? string.Empty : HttpUtility.HtmlDecode(City));
            parameters.Add("state", State == null ? string.Empty : HttpUtility.HtmlDecode(State));
            parameters.Add("country", Country == null ? string.Empty : HttpUtility.HtmlDecode(Country));
            parameters.Add("lastname", LastName == null ? string.Empty : HttpUtility.HtmlDecode(LastName));
            parameters.Add("firstname", FirstName == null ? string.Empty : HttpUtility.HtmlDecode(FirstName));
            parameters.Add("email", Email == null ? string.Empty : HttpUtility.HtmlDecode(Email));
            parameters.Add("phone", Phone == null ? string.Empty : HttpUtility.HtmlDecode(Phone));
            parameters.Add("mobilephone", MobilePhone == null ? string.Empty : HttpUtility.HtmlDecode(MobilePhone));
            parameters.Add("fax", Fax == null ? string.Empty : HttpUtility.HtmlDecode(Fax));
            parameters.Add("credit", Credit);
            parameters.Add("subaccid", context.SubAccId);
            // We must check this condition. Because in 2 SPs Update and AddNew use similar maxcredit field for set value.
            // I think, we need to use different name for 2 this SPs.
            // In Form Add New: Member Max Credit = MaxCredit
            // In Form Update: Member Max Credit =  MemberCredit
            if (context.RoleId == (int)CustomerRole.Master && context.TargetRoleId == (int)CustomerRole.Agent)
            {
                parameters.Add("maxcredit", MemberMaxCredit);
            }
            else
            {
                parameters.Add("maxcredit", MaxCredit);
            }

            parameters.Add("srecommend", SRecommend);
            parameters.Add("mrecommend", context.CustId);
            parameters.Add("recommend", context.CustId);
            parameters.Add("language", Language == null ? string.Empty : Language);
            parameters.Add("ip", context.IP);
            parameters.Add("site", SiteName == null ? string.Empty : SiteName);
            parameters.Add("sitename", SiteName == null ? string.Empty : SiteName); // For parameter of Add New Agent
            parameters.Add("currency", this.CurrencyId > 0 ? this.CurrencyId.ToString() : string.Empty);
            parameters.Add("isinternal", IsInternal);
            parameters.Add("transferoption", TransferOption);
            parameters.Add("ugroup", Group);
            parameters.Add("creator", context.AdminId);
        }

        #region ReadFromDataRecord

        /// <summary>
        /// Set data reader to object Customer
        /// </summary>
        /// <param name="context"></param>
        /// <param name="reader"></param>
        public void ReadFromDataRecord(IRequestContext context, System.Data.IDataReader reader)
        {
            // Customer Information
            SRecommend = context.CustId;
            Creator = context.CustId;
            CustId = context.TargetCustId;
            if (reader.HasColumn("username"))
            {
                UserName = reader["username"].ToString();
            }
            else
            {
                UserName = context.UserName;
            }

            NewUserName = reader.ToString("newusername");
            LastName = reader.ToString("lastname");
            FirstName = reader.ToString("firstname");
            Phone = reader.ToString("phone");
            MobilePhone = reader.ToString("mobilephone");
            Fax = reader.ToString("fax");
            Credit = reader.HasColumn("credit") ? Convert.ToDecimal(reader["credit"]) : 0;
            if (reader.HasColumn("credit_min"))
            {
                MinCredit = Convert.ToDecimal(reader["credit_min"]);
            }

            if (reader.HasColumn("credit_max"))
            {
                MaxCredit = Convert.ToDecimal(reader["credit_max"]);
            }

            MemberMaxCredit = reader.HasColumn("mem_maxcredit") ? Convert.ToDecimal(reader["mem_maxcredit"]) : 0;

            if (reader.HasColumn("maxcredit_min"))
            {
                MemberMinCredit = (reader["maxcredit_min"] == DBNull.Value) ? 0 : Convert.ToDecimal(reader["maxcredit_min"]);
            }

            if (reader.HasColumn("ugroup"))
            {
                Group = reader.ToString("ugroup");
            }

            SiteName = context.SiteName;
            Language = context.LangKey;

            // CommissionSetting
            IList<ICommissionSetting> commissionSettings = this.GetCommissionSetting(context, reader);
            CommissionSetting = commissionSettings[0];
            if (commissionSettings.Count > 1)
            {
                CommissionSetting2 = commissionSettings[1];
            }

            // Status
            StatusSetting.ReadFromDataRecord(reader);

            // PTGrid
            PtSetting.MapFromDataReader(context, reader);

            // BettingLimit
            BettingLimit.ReadFromDataRecord(context, reader);

            // Transfer Condition
            ICustomerService customerDataAccess = new CustomerService();
            TransferOption = customerDataAccess.GetTransferCondition(context);
        }

        /// <summary>
        /// Get Commission List
        /// </summary>
        /// <param name="context"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        private IList<ICommissionSetting> GetCommissionSetting(IRequestContext context, System.Data.IDataReader reader)
        {
            // CommissionSetting
            ICommissionSetting commission = new CommissionSetting();
            commission.ReadFromDataRecord(context, reader);

            IList<ICommissionSetting> commissionSettings = new List<ICommissionSetting>();
            commissionSettings.Add(commission);

            // If Level Super edit PT Member, we need get commission of Master
            if (context.RoleId == (int)CustomerRole.Super && context.TargetRoleId == (int)CustomerRole.Member)
            {
                ICommissionSetting commissionMaster = new CommissionSetting();
                commissionMaster.PlayerDiscount = reader.ToDecimal("adiscount");
                commissionMaster.PlayerDiscount1x2 = reader.ToDecimal("adiscount1x2");
                commissionMaster.PlayerDiscountCs = reader.ToDecimal("adiscountcs");
                commissionMaster.PlayerDiscountNumber = reader.ToDecimal("adiscount161");
                commissionSettings.Add(commissionMaster);
            }

            return commissionSettings;
        }

        #endregion ReadFromDataRecord

        public void InitDefaultValue()
        {
            // Customer Info
            this.UserName = string.Empty;
            this.Password = string.Empty;
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Phone = string.Empty;
            this.MobilePhone = string.Empty;
            this.Email = string.Empty;
            this.Fax = string.Empty;
            this.Address = string.Empty;
            this.City = string.Empty;
            this.State = string.Empty;
            this.Country = string.Empty;
            this.Postal = string.Empty;
            this.Language = string.Empty;
            this.IsInternal = false;
            this.PtSetting.Populate(true);

            this.BettingLimit.Populate();

            this.CommissionSetting = new CommissionSetting
            {
                RoleId = 4,
                GroupA = (decimal)0.0025,
                GroupB = (decimal)0.005,
                GroupC = (decimal)0.0075,
                GroupD = (decimal)0.01,
                Discount1x2 = (decimal)0.0025,
                DiscountCS = (decimal)0.01,
                DiscountNumber = (decimal)0.0025
            };
        }
    }
}