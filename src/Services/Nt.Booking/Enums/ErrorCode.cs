namespace Nt.Booking.Enums
{
    /// <summary>
    /// 
    /// </summary>
    public enum ErrorCode
    {
        //All
        Ok = 0,
        Error = 1,
        HostUnavailable = 2,
        Timeout = 3,


        //1000 - 1999 Voucher todo: recheck, currently oriented on SSV
        VoucherNoFullRedemption = 1000,
        VoucherInvalidBarcode = 1001,
        VoucherInactive = 1002,
        VoucherInvalidNumber = 1003,
        VoucherInvalidTransactionCode = 1004,
        VoucherInsufficientFunds = 1005,
        VoucherUnknown = 1008,
        VoucherInvalidCcvOrSsc = 1019,
        VoucherInvalidPin = 1020,
        VoucherAlreadyIssued = 1021,
        VoucherNotIssued = 1022,
        VoucherAlreadyUsed = 1023,
        VoucherFrozen = 1033,

        //2000 - 2999 Hotel
        HotelRoomUnknown = 2000,
        HotelRoomNotBooked = 2001,



        //3000 - 3999 Access
        AccessMediumUnknown = 3000,



    }
}
