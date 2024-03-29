﻿namespace SanProtocol.WorldState
{
    public class DestroySourceIdSpace : IPacket
    {
        public uint MessageId => Messages.WorldStateMessages.DestroySourceIdSpace;

        public uint SourceIdSpace { get; set; }

        public DestroySourceIdSpace(uint sourceIdSpace)
        {
            SourceIdSpace = sourceIdSpace;
        }

        public DestroySourceIdSpace(BinaryReader br)
        {
            SourceIdSpace = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(SourceIdSpace);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"WorldState::DestroySourceIdSpace:\n" +
                   $"  {nameof(SourceIdSpace)} = {SourceIdSpace}\n";
        }
    }
}
