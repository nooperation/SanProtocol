namespace SanProtocol
{
    public class SanUUID
    {
        public const string Zero = "00000000000000000000000000000000";

        private Guid? _guid;
        public Guid Guid
        {
            get
            {
                if (_guid == null)
                {
                    _guid = new Guid(Format());
                }

                return _guid.Value;
            }
        }

        public string UUID { get; set; }
        public ulong Upper { get; set; }
        public ulong Lower { get; set; }

        public SanUUID()
        {
            UUID = Zero;
            Upper = 0;
            Lower = 0;
        }

        public SanUUID(string uuidString)
        {
            UUID = uuidString.Trim().ToLower().Replace("-", "");

            var upperStr = UUID.Substring(0, 16);
            var lowerStr = UUID.Substring(16);

            Upper = ulong.Parse(upperStr, System.Globalization.NumberStyles.HexNumber);
            Lower = ulong.Parse(lowerStr, System.Globalization.NumberStyles.HexNumber);
        }

        public SanUUID(BinaryReader br)
        {
            Upper = br.ReadUInt64();
            Lower = br.ReadUInt64();

            UUID = $"{Upper:x16}{Lower:x16}";
        }

        public byte[] GetBytes()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    WriteBytes(bw);
                    return ms.ToArray();
                }
            }
        }

        public void WriteBytes(BinaryWriter bw)
        {
            bw.Write(Upper);
            bw.Write(Lower);
        }

        public string Format()
        {
            var uuidFormatted = $"{UUID.Substring(0, 8)}-{UUID.Substring(8, 4)}-{UUID.Substring(12, 4)}-{UUID.Substring(16, 4)}-{UUID.Substring(20, 12)}";
            return uuidFormatted;
        }

        public override string ToString()
        {
            return UUID;
        }

        public override int GetHashCode()
        {
            return UUID.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is SanUUID targetUUID)
            {
                return Lower == targetUUID?.Lower && Upper == targetUUID?.Upper;
            }

            return false;
        }

        public static bool operator ==(SanUUID? x, SanUUID? y)
        {
            return x?.Lower == y?.Lower && x?.Upper == y?.Upper;
        }

        public static bool operator !=(SanUUID? x, SanUUID? y)
        {
            return !(x == y);
        }

        public static implicit operator SanUUID(string str)
        {
            return new SanUUID(str);
        }
    }
}
