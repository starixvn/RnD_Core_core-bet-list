namespace Fanex.PTEngine.BusinessObjects
{
    using System;
    using System.Collections;

    public class CommissionSetting : ICommissionSetting
    {
        public int RoleId { get; set; }

        public decimal GroupA { get; set; }

        public decimal GroupB { get; set; }

        public decimal GroupC { get; set; }

        public decimal GroupD { get; set; }

        public decimal DiscountCS { get; set; }

        public decimal Discount1x2 { get; set; }

        public decimal DiscountNumber { get; set; }

        public decimal GroupAMax { get; set; }

        public decimal GroupBMax { get; set; }

        public decimal GroupCMax { get; set; }

        public decimal GroupDMax { get; set; }

        public decimal DiscountCSMax { get; set; }

        public decimal Discount1x2Max { get; set; }

        public decimal DiscountNumberMax { get; set; }

        public decimal Discount { get; set; }

        public decimal PlayerDiscount { get; set; }

        public decimal PlayerDiscount1x2 { get; set; }

        public decimal PlayerDiscountCs { get; set; }

        public decimal PlayerDiscountNumber { get; set; }

        public void ReadFromDataRecord(IRequestContext context, System.Data.IDataReader reader)
        {
            RoleId = context.TargetRoleId;

            GroupA = reader.ToDecimal("groupa");
            GroupB = reader.ToDecimal("groupb");
            GroupC = reader.ToDecimal("groupc");
            GroupD = reader.ToDecimal("groupd");

            GroupAMax = reader.HasColumn("groupa_max") ? Convert.ToDecimal(reader["groupa_max"]) : reader.ToDecimal("groupa");
            GroupBMax = reader.HasColumn("groupb_max") ? Convert.ToDecimal(reader["groupb_max"]) : reader.ToDecimal("groupb");
            GroupCMax = reader.HasColumn("groupc_max") ? Convert.ToDecimal(reader["groupc_max"]) : reader.ToDecimal("groupc");
            GroupDMax = reader.HasColumn("groupd_max") ? Convert.ToDecimal(reader["groupd_max"]) : reader.ToDecimal("groupd");

            // Field name discount161 for super edit member
            DiscountNumberMax = reader.HasColumn("discountnumber_max") ? Convert.ToDecimal(reader["discountnumber_max"]) :
                reader.HasColumn("discountnumber") ? Convert.ToDecimal(reader["discountnumber"]) : reader.ToDecimal("discount161");
            Discount1x2Max = reader.HasColumn("discount1x2_max") ? Convert.ToDecimal(reader["discount1x2_max"]) : reader.ToDecimal("discount1x2");
            DiscountCSMax = reader.HasColumn("discountcs_max") ? Convert.ToDecimal(reader["discountcs_max"]) : reader.ToDecimal("discountcs");

            Discount1x2 = reader.ToDecimal("discount1x2");
            DiscountCS = reader.ToDecimal("discountcs");

            // Field name discount161 for super edit member
            DiscountNumber = reader.HasColumn("discountnumber") ? Convert.ToDecimal(reader["discountnumber"]) : reader.ToDecimal("discount161");
            Discount = reader.HasColumn("discount") ? Convert.ToDecimal(reader["discount"]) : 0;

            PlayerDiscount = reader.ToDecimal("playerdiscount");
            PlayerDiscount1x2 = reader.ToDecimal("playerdiscount1x2");
            PlayerDiscountCs = reader.ToDecimal("playerdiscountcs");
            PlayerDiscountNumber = reader.HasColumn("playerdiscountnumber") ? Convert.ToDecimal(reader["playerdiscountnumber"]) : reader.ToDecimal("playerdiscount161");
        }

        /// <summary>
        /// Mapping to Sp Parameters
        /// </summary>
        /// <param name="parameters"></param>
        public void MapToSpParameters(Hashtable parameters)
        {
            parameters.Add("roleid", RoleId);
            parameters.Add("groupa", GroupA);
            parameters.Add("groupb", GroupB);
            parameters.Add("groupc", GroupC);
            parameters.Add("groupd", GroupD);
            parameters.Add("groupa_max", GroupAMax);
            parameters.Add("groupb_max", GroupBMax);
            parameters.Add("groupc_max", GroupCMax);
            parameters.Add("groupd_max", GroupDMax);

            parameters.Add("discount1x2", Discount1x2);
            parameters.Add("discountcs", DiscountCS);
            parameters.Add("discountnumber", DiscountNumber);
            parameters.Add("discount1x2_max", Discount1x2Max);
            parameters.Add("discountcs_max", DiscountCSMax);
            parameters.Add("discountnumber_max", DiscountNumberMax);

            // Add this for add new member
            parameters.Add("playerdiscount", PlayerDiscount);
            parameters.Add("playerdiscount1x2", PlayerDiscount1x2);
            parameters.Add("playerdiscountcs", PlayerDiscountCs);
            parameters.Add("playerdiscountnumber", PlayerDiscountNumber);
        }
    }
}