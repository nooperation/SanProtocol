namespace SanProtocol
{
    public interface IPacket
    {
        public uint MessageId { get; }
        public byte[] GetBytes();
    }
}
