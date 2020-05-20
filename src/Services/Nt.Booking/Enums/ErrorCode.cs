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


        //1000 - 1999 Voucher
        VoucherUnknown = 1000,
        VoucherInactive = 1001,
        VoucherInvalid = 1002,
        VoucherInsufficient = 1003,
        VoucherAlreadyIssued = 1004,
        VoucherNotIssued = 1005,
        VoucherAlreadyUsed = 1006,
        VoucherPinInvalid = 1007,
        VoucherNoFullRedemption = 1008,



        //2000 - 2999 Hotel
        HotelRoomUnknown = 2000,
        HotelRoomNotBooked =2001,



        //3000 - 3999 Access
        AccessMediumUnknown =3000,



    }
}
