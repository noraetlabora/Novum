namespace Nt.Printer.Transmission
{
    public interface ITransmission
    {
        void Send(byte[] data);
    }
}