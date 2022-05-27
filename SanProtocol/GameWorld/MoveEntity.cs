using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.GameWorld
{
    public class MoveEntity : IPacket
    {
        public uint MessageId => Messages.GameWorld.MoveEntity;

        public ulong StartFrame { get; set; }
        public ulong ComponentId { get; set; }
        public List<float> StartPosition { get; set; } = new List<float>();
        public List<float> TargetPosition { get; set; } = new List<float>();
        public uint Time { get; set; }
        public Quaternion StartOrientation { get; set; } = new Quaternion();
        public Quaternion TargetOrientation { get; set; } = new Quaternion();
        public byte PositionInterpMode { get; set; }
        public byte RotationInterpMode { get; set; }

        public MoveEntity(ulong startFrame, ulong componentId, List<float> startPosition, List<float> targetPosition, uint time, Quaternion startOrientation, Quaternion targetOrientation, byte positionInterpMode, byte rotationInterpMode)
        {
            this.StartFrame = startFrame;
            this.ComponentId = componentId;
            this.StartPosition = startPosition;
            this.TargetPosition = targetPosition;
            this.Time = time;
            this.StartOrientation = startOrientation;
            this.TargetOrientation = targetOrientation;
            this.PositionInterpMode = positionInterpMode;
            this.RotationInterpMode = rotationInterpMode;
        }

        public MoveEntity(BinaryReader br)
        {
            StartFrame = br.ReadUInt64();
            ComponentId = br.ReadUInt64();
            for (var i = 0; i < 3; ++i)
            {
                var item = br.ReadSingle();
                StartPosition.Add(item);
            }
            for (var i = 0; i < 3; ++i)
            {
                var item = br.ReadSingle();
                TargetPosition.Add(item);
            }
            Time = br.ReadUInt32();

            var bitReader = new BitReader(br, (3 * 14 + 4) + (3 * 14 + 4) + 4 + 4);
            StartOrientation = bitReader.ReadQuaternion(3, 14);
            TargetOrientation = bitReader.ReadQuaternion(3, 14);
            PositionInterpMode = (byte)bitReader.ReadUnsigned(4);
            RotationInterpMode = (byte)bitReader.ReadUnsigned(4);
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(StartFrame);
                    bw.Write(ComponentId);
                    foreach (var item in StartPosition)
                    {
                        bw.Write(item);
                    }
                    foreach (var item in TargetPosition)
                    {
                        bw.Write(item);
                    }
                    bw.Write(Time);

                    var bitWriter = new BitWriter();
                    bitWriter.WriteQuaternion(StartOrientation, 14);
                    bitWriter.WriteQuaternion(TargetOrientation, 14);
                    bitWriter.WriteUnsigned(PositionInterpMode, 4);
                    bitWriter.WriteUnsigned(RotationInterpMode, 4);
                    var bits = bitWriter.GetBytes();

                    bw.Write(bits);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"GameWorld::MoveEntity:\n" +
                   $"  {nameof(StartFrame)} = {StartFrame}\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n" +
                   $"  {nameof(StartPosition)} = <{String.Join(',', StartPosition)}>\n" +
                   $"  {nameof(TargetPosition)} = <{String.Join(',', TargetPosition)}>\n" +
                   $"  {nameof(Time)} = {Time}\n" +
                   $"  {nameof(StartOrientation)} = {StartOrientation}\n" +
                   $"  {nameof(TargetOrientation)} = {TargetOrientation}\n" +
                   $"  {nameof(PositionInterpMode)} = {PositionInterpMode}\n" +
                   $"  {nameof(RotationInterpMode)} = {RotationInterpMode}\n";
        }
    }
}
