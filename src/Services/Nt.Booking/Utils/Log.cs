
namespace Nt.Booking.Utils
{
    internal class Log
    {
        internal static void LogMessage(string message, string messageType)
        {
            var sb = new System.Text.StringBuilder();
            sb.Append(NtBooking.BookingSystem.BookingSystem).Append("|");
            sb.Append(messageType).Append("|");
            //if (message.Length > 500)
            //{
            //    sb.Append(message.Substring(0, 500)).Append("...");
            //}
            //else
            //{
            sb.Append(message);
            //}
            Nt.Logging.Log.Communication.Info(sb.ToString());

            if (message.Length > 500)
            {
                Nt.Logging.Log.Communication.Debug(message);
            }
        }
    }
}
