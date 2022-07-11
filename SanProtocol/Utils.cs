using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace SanProtocol
{
    public class Utils
    {
        public static string DumpPacket(byte[] packet, bool isSending)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{(isSending ? "-->" : "<--")} [{packet.Length}]");

            foreach (var item in packet)
            {
                sb.Append($"{item:X2} ");
            }

            return sb.ToString();
        }

        internal static void DumpReader(BinaryReader br, string name)
        {
            var originalPosition = br.BaseStream.Position;
            var bytes = br.ReadBytes((int)(br.BaseStream.Length - br.BaseStream.Position));
            br.BaseStream.Seek(originalPosition, SeekOrigin.Begin);

            Console.WriteLine($"{name}: {Utils.DumpPacket(bytes, false)}");
        }
        internal static void DumpWriter(MemoryStream ms, string name)
        {
            var bytes = ms.ToArray().Skip(4).ToArray();

            Console.WriteLine($"{name}: {Utils.DumpPacket(bytes, true)}");
        }
    }
}
