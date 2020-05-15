namespace Nt.Booking.Enums
{
    /// <summary>
    /// 
    /// </summary>
    public enum StatusCode
    {
        //All
        Ok = 0,
        Error = 1,
        PartnerUnavailable = 2,


        //1000 - Voucher
        VoucherInactive = 1000,
        VoucherInvalid = 1001,
        VoucherInsufficient = 1002,
        VoucherAlreadyIssued = 1009,
        

        //2000 - Hotel


        //3000 - Access

    }
}
