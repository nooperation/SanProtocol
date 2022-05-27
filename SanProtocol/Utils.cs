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
            Console.WriteLine($"{name}:");
            var x = br.ReadBytes((int)(br.BaseStream.Length - br.BaseStream.Position));
            Console.WriteLine(Utils.DumpPacket(x, false));
            Console.WriteLine("---");
            br.BaseStream.Seek(originalPosition, SeekOrigin.Begin);
        }
    }
}
