using System.ServiceModel.Channels;
using System.Xml;

namespace Nt.Booking.Utils.Soap
{
    /// <summary>
    /// 
    /// </summary>
    public class SecurityHeader : MessageHeader
    {
        private readonly string _password, _username;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public SecurityHeader(string id, string username, string password)
        {
            _password = password;
            _username = username;
        }

        /// <summary>
        /// 
        /// </summary>
        public override bool MustUnderstand => true;

        /// <summary>
        /// 
        /// </summary>
        public override string Name
        {
            get { return "Security"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string Namespace
        {
            get { return "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="messageVersion"></param>
        protected override void OnWriteStartHeader(XmlDictionaryWriter writer, MessageVersion messageVersion)
        {
            writer.WriteStartElement("wsse", Name, Namespace);
            writer.WriteAttributeString("s", "mustUnderstand", "http://schemas.xmlsoap.org/soap/envelope/", "1");
            writer.WriteXmlnsAttribute("wsse", Namespace);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="messageVersion"></param>
        protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
        {
            writer.WriteStartElement("wsse", "UsernameToken", Namespace);
            //writer.WriteAttributeString("wsu", "Id", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd", "UsernameToken-32");
            // Username
            writer.WriteStartElement("wsse", "Username", Namespace);
            writer.WriteValue(_username);
            writer.WriteEndElement();
            // Password
            writer.WriteStartElement("wsse", "Password", Namespace);
            //writer.WriteAttributeString("Type", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText");
            writer.WriteValue(_password);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
    }
}
