using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.ClientRegion
{
    public class ClientCommand : IPacket
    {
        public uint MessageId => Messages.ClientRegionMessages.ClientCommand;

        public string Command { get; set; }
        public byte Action { get; set; }
        public List<float> Origin { get; set; } = new List<float>();
        public List<float> TargetPosition { get; set; } = new List<float>();
        public List<float> Normal { get; set; } = new List<float>();
        public ulong ComponentId { get; set; }
        public ulong Frame { get; set; }
        public byte Device { get; set; }
        public byte MouseLook { get; set; }
        public byte ControlMode { get; set; }
        public byte IsAimTarget { get; set; }

        public ClientCommand(string command, byte action, List<float> origin, List<float> targetPosition, List<float> normal, ulong componentId, ulong frame, byte device, byte mouseLook, byte controlMode, byte isAimTarget)
        {
            this.Command = command;
            this.Action = action;
            this.Origin = origin;
            this.TargetPosition = targetPosition;
            this.Normal = normal;
            this.ComponentId = componentId;
            this.Frame = frame;
            this.Device = device;
            this.MouseLook = mouseLook;
            this.ControlMode = controlMode;
            this.IsAimTarget = isAimTarget;
        }

        public ClientCommand(BinaryReader br)
        {
            Command = br.ReadSanString();
            Action = br.ReadByte();
            for (var i = 0; i < 3; ++i)
            {
                var item = br.ReadSingle();
                Origin.Add(item);
            }
            for (var i = 0; i < 3; ++i)
            {
                var item = br.ReadSingle();
                TargetPosition.Add(item);
            }
            for (var i = 0; i < 3; ++i)
            {
                var item = br.ReadSingle();
                Normal.Add(item);
            }
            ComponentId = br.ReadUInt64();
            Frame = br.ReadUInt64();
            Device = br.ReadByte();
            MouseLook = br.ReadByte();
            ControlMode = br.ReadByte();
            IsAimTarget = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.WriteSanString(Command);
                    bw.Write(Action);
                    foreach (var item in Origin)
                    {
                        bw.Write(item);
                    }
                    foreach (var item in TargetPosition)
                    {
                        bw.Write(item);
                    }
                    foreach (var item in Normal)
                    {
                        bw.Write(item);
                    }
                    bw.Write(ComponentId);
                    bw.Write(Frame);
                    bw.Write(Device);
                    bw.Write(MouseLook);
                    bw.Write(ControlMode);
                    bw.Write(IsAimTarget);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"ClientRegion::ClientCommand:\n" +
                   $"  {nameof(Command)} = {Command}\n" +
                   $"  {nameof(Action)} = {Action}\n" +
                   $"  {nameof(Origin)} = <{String.Join(',', Origin)}>\n" +
                   $"  {nameof(TargetPosition)} = <{String.Join(',', TargetPosition)}>\n" +
                   $"  {nameof(Normal)} = <{String.Join(',', Normal)}>\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(Device)} = {Device}\n" +
                   $"  {nameof(MouseLook)} = {MouseLook}\n" +
                   $"  {nameof(ControlMode)} = {ControlMode}\n" +
                   $"  {nameof(IsAimTarget)} = {IsAimTarget}\n";
        }
    }
}
