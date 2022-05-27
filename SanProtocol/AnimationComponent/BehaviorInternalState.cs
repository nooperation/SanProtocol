using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.AnimationComponent
{
    public class BehaviorInternalState : IPacket
    {
        public class AnimationOverride
        {
            public AnimationOverride(byte flag, PlayAnimation animation)
            {
                Flag = flag;
                Animation = animation;
            }

            public byte Flag { get; set; }
            public PlayAnimation Animation { get; set; }
        }

        public virtual uint MessageId => Messages.AnimationComponent.BehaviorInternalState;

        public ulong ComponentId { get; set; }
        public ulong Frame { get; set; }
        public List<AnimationOverride> Overrides { get; set; }
        public byte[] SlotStates { get; set; }
        public byte[] StateData { get; set; }
        public byte IsPlaying { get; set; }

        public BehaviorInternalState(ulong componentId, ulong frame, List<AnimationOverride> overrides, byte[] slotStates, byte[] stateData, byte isPlaying)
        {
            this.ComponentId = componentId;
            this.Frame = frame;
            this.Overrides = overrides;
            this.SlotStates = slotStates;
            this.StateData = stateData;
            this.IsPlaying = isPlaying;
        }

        public BehaviorInternalState(BinaryReader br)
        {
            ComponentId = br.ReadUInt64();
            Frame = br.ReadUInt64();

            var numOverrides = br.ReadInt32();
            Overrides = new List<AnimationOverride>(numOverrides);
            for (int i = 0; i < numOverrides; i++)
            {
                var flag = br.ReadByte();
                var animation = new PlayAnimation(br);

                Overrides.Add(new AnimationOverride(flag, animation));
            }

            var slotStatesLength = br.ReadUInt32();
            SlotStates = new byte[slotStatesLength];
            for (int i = 0; i < slotStatesLength; i++)
            {
                SlotStates[i] = br.ReadByte();
            }

            var stateDatalength = br.ReadInt32();
            StateData = br.ReadBytes(stateDatalength);
            IsPlaying = br.ReadByte();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(ComponentId);
                    bw.Write(Frame);
                    bw.Write(Overrides.Count);
                    foreach (var item in Overrides)
                    {
                        bw.Write(item.Flag);
                        bw.Write(item.Animation.GetBytes());
                    }
                    bw.Write(SlotStates.Length);
                    bw.Write(SlotStates);
                    bw.Write(StateData.Length);
                    bw.Write(StateData);
                    bw.Write(IsPlaying);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AnimationComponent::BehaviorInternalState:\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(SlotStates)} = {SlotStates}\n" +
                   $"  {nameof(StateData)} = {StateData}\n" +
                   $"  {nameof(IsPlaying)} = {IsPlaying}\n";
        }
    }
}
