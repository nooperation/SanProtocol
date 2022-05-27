using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace SanBot
{
    public static class BinaryReaderExtensions
    {
        public static string ReadSanString(this BinaryReader reader)
        {
            var length = reader.ReadInt32();
            var chars = reader.ReadBytes(length);
            var str = new string(Encoding.UTF8.GetString(chars));

            return str;
        }

        public static SanUUID ReadSanUUID(this BinaryReader reader)
        {
            return new SanUUID(reader);
        }
    }

    public static class BinaryWriterExtensions
    {
        public static void Write(this BinaryWriter writer, SanUUID uuid)
        {
            uuid.WriteBytes(writer);
        }
        
        public static void WriteSanString(this BinaryWriter writer, string str)
        {
            var bytes = ASCIIEncoding.UTF8.GetBytes(str);

            writer.Write(bytes.Length);
            if (str.Length > 0)
            {
                writer.Write(bytes);
            }
        }
    }
}
