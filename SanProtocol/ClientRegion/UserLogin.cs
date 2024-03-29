﻿namespace SanProtocol.ClientRegion
{
    public class UserLogin : IPacket
    {
        public uint MessageId => Messages.ClientRegionMessages.UserLogin;

        public uint Secret { get; set; }

        public UserLogin(uint secret)
        {
            Secret = secret;
        }

        public UserLogin(BinaryReader br)
        {
            Secret = br.ReadUInt32();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Secret);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::UserLogin:\n" +
                   $"  {nameof(Secret)} = {Secret}\n";
        }
    }

}
