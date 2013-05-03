namespace Fanex.PTEngine.BusinessObjects
{
    using System.Collections;

    public interface ICommissionSetting
    {
        int RoleId { get; set; }

        decimal GroupA { get; set; }

        decimal GroupB { get; set; }

        decimal GroupC { get; set; }

        decimal GroupD { get; set; }

        decimal DiscountCS { get; set; }

        decimal Discount1x2 { get; set; }

        decimal DiscountNumber { get; set; }

        decimal GroupAMax { get; set; }

        decimal GroupBMax { get; set; }

        decimal GroupCMax { get; set; }

        decimal GroupDMax { get; set; }

        decimal DiscountCSMax { get; set; }

        decimal Discount1x2Max { get; set; }

        decimal DiscountNumberMax { get; set; }

        decimal Discount { get; set; }

        decimal PlayerDiscount { get; set; }

        decimal PlayerDiscount1x2 { get; set; }

        decimal PlayerDiscountCs { get; set; }

        decimal PlayerDiscountNumber { get; set; }

        void ReadFromDataRecord(IRequestContext context, System.Data.IDataReader reader);

        void MapToSpParameters(Hashtable parameters);
    }
}