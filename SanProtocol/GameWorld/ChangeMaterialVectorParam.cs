﻿namespace SanProtocol.GameWorld
{
    public class ChangeMaterialVectorParam : IPacket
    {
        public uint MessageId => Messages.GameWorldMessages.ChangeMaterialVectorParam;

        public byte Parameter { get; set; }
        public List<float> Start { get; set; } = new List<float>();
        public List<float> End { get; set; } = new List<float>();

        public ChangeMaterialVectorParam(byte parameter, List<float> start, List<float> end)
        {
            Parameter = parameter;
            Start = start;
            End = end;
        }

        public ChangeMaterialVectorParam(BinaryReader br)
        {
            Parameter = br.ReadByte();
            for (var i = 0; i < 3; ++i)
            {
                var item = br.ReadSingle();
                Start.Add(item);
            }
            for (var i = 0; i < 3; ++i)
            {
                var item = br.ReadSingle();
                End.Add(item);
            }
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Parameter);
                    foreach (var item in Start)
                    {
                        bw.Write(item);
                    }
                    foreach (var item in End)
                    {
                        bw.Write(item);
                    }
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"GameWorld::ChangeMaterialVectorParam:\n" +
                   $"  {nameof(Parameter)} = {Parameter}\n" +
                   $"  {nameof(Start)} = <{string.Join(',', Start)}>\n" +
                   $"  {nameof(End)} = <{string.Join(',', End)}>\n";
        }
    }
}
