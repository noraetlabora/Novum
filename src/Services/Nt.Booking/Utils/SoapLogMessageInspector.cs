using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace Nt.Booking.Utils
{
    public class SoapLogMessageInspector : IClientMessageInspector
    {
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            using (var buffer = reply.CreateBufferedCopy(int.MaxValue))
            {
                var document = GetDocument(buffer.CreateMessage());
                //log soap response
                LogSoapMessage(document.OuterXml, "Soap   Response");
                reply = buffer.CreateMessage();
            }
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            using (var buffer = request.CreateBufferedCopy(int.MaxValue))
            {
                var document = GetDocument(buffer.CreateMessage());
                //log soap request
                LogSoapMessage(document.OuterXml, "Soap   Request ");
                request = buffer.CreateMessage();
                return null;
            }
        }

        private static void LogSoapMessage(string soapMessage, string messageType)
        {
            var sb = new System.Text.StringBuilder();
            sb.Append(NtBooking.BookingSystem.Type).Append("|");
            sb.Append(messageType).Append("|");
            if (soapMessage.Length > 500)
            {
                sb.Append(soapMessage.Substring(0, 500)).Append("...");
            }
            else
            {
                sb.Append(soapMessage);
            }
            Nt.Logging.Log.Communication.Info(sb.ToString());

            if (soapMessage.Length > 500)
            {
                Nt.Logging.Log.Communication.Debug(soapMessage);
            }
        }

        private XmlDocument GetDocument(Message request)
        {
            XmlDocument document = new XmlDocument();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // write request to memory stream
                XmlWriter writer = XmlWriter.Create(memoryStream);
                request.WriteMessage(writer);
                writer.Flush();
                memoryStream.Position = 0;

                // load memory stream into a document
                document.Load(memoryStream);
            }

            return document;
        }
    }
}
