﻿namespace SanProtocol.ClientRegion
{
    public class VisualDebuggerCaptureToClient : IPacket
    {
        public uint MessageId => Messages.ClientRegionMessages.VisualDebuggerCaptureToClient;

        public string StartTimeFormatted { get; set; }
        public byte[] CompressedHkmBytes { get; set; }
        public ulong UncompressedSize { get; set; }
        public byte BeginCapture { get; set; }
        public byte Succeeded { get; set; }
        public string ErrorMessage { get; set; }

        public VisualDebuggerCaptureToClient(string startTimeFormatted, byte[] compressedHkmBytes, ulong uncompressedSize, byte beginCapture, byte succeeded, string errorMessage)
        {
            StartTimeFormatted = startTimeFormatted;
            CompressedHkmBytes = compressedHkmBytes;
            UncompressedSize = uncompressedSize;
            BeginCapture = beginCapture;
            Succeeded = succeeded;
            ErrorMessage = errorMessage;
        }

        public VisualDebuggerCaptureToClient(BinaryReader br)
        {
            StartTimeFormatted = br.ReadSanString();
            var compressedHkmBytesLength = br.ReadInt32();
            CompressedHkmBytes = br.ReadBytes(compressedHkmBytesLength);
            UncompressedSize = br.ReadUInt64();
            BeginCapture = br.ReadByte();
            Succeeded = br.ReadByte();
            ErrorMessage = br.ReadSanString();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(StartTimeFormatted);
                    bw.Write(CompressedHkmBytes.Length);
                    bw.Write(CompressedHkmBytes);
                    bw.Write(UncompressedSize);
                    bw.Write(BeginCapture);
                    bw.Write(Succeeded);
                    bw.WriteSanString(ErrorMessage);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::VisualDebuggerCaptureToClient:\n" +
                   $"  {nameof(StartTimeFormatted)} = {StartTimeFormatted}\n" +
                   $"  {nameof(CompressedHkmBytes)} = {CompressedHkmBytes}\n" +
                   $"  {nameof(UncompressedSize)} = {UncompressedSize}\n" +
                   $"  {nameof(BeginCapture)} = {BeginCapture}\n" +
                   $"  {nameof(Succeeded)} = {Succeeded}\n" +
                   $"  {nameof(ErrorMessage)} = {ErrorMessage}\n";
        }
    }
}
